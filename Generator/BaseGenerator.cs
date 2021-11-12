global using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Text;

namespace Generator
{
    public abstract class BaseGenerator : ISourceGenerator
    {
        public string AttributeName { get; }
        public string AttributeNamespace { get; }

        private INamedTypeSymbol? attributeSymbol;
        private GeneratorExecutionContext? executionContext;

        protected BaseGenerator(string attributeName, string attributeNamespace)
        {
            AttributeName = attributeName;
            AttributeNamespace = attributeNamespace;
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            executionContext = context;

            if (context.SyntaxReceiver is not SyntaxReceiver syntaxReciever)
            {
                return;
            }

            Compilation compilation = InjectAttribute(context);

            List<IMethodSymbol> methodSymbols = new();

            foreach (MethodDeclarationSyntax methodDeclarations in syntaxReciever.MethodCandidates)
            {
                SemanticModel semanticModel = compilation.GetSemanticModel(methodDeclarations.SyntaxTree);
                IMethodSymbol? methodSymbol = semanticModel.GetDeclaredSymbol(methodDeclarations);

                if (HasAttribute(methodSymbol, attributeSymbol))
                {
                    methodSymbols.Add(methodSymbol!);
                }
            }

            IEnumerable<IGrouping<ISymbol, IMethodSymbol>> methodGroups = methodSymbols.GroupBy(M => M.ContainingSymbol, SymbolEqualityComparer.Default);

            foreach (IGrouping<ISymbol, IMethodSymbol> methodGroup in methodGroups)
            {
                if (methodGroup.Key is not INamedTypeSymbol classSymbol)
                {
                    continue;
                }

                string source = ProcessClass(classSymbol, methodGroup);

                context.AddSource(ToFileName(classSymbol.Name), SourceText.From(source, Encoding.UTF8));
            }
        }

        private Compilation InjectAttribute(GeneratorExecutionContext context)
        {
            SourceText source = SourceText.From(
                $@"
                using System;
                namespace {AttributeNamespace}
                {{                    
                    /// <summary>
                    /// An attribute marking a method as a candidate for source generator.
                    /// </summary>
                    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
                    public sealed class {AttributeName} : Attribute
                    {{
                        public {AttributeName}()
                        {{
                        }}
                    }}
                }}"
                , Encoding.UTF8);

            context.AddSource(ToFileName(AttributeName), source);

            CSharpParseOptions? options = context.Compilation.SyntaxTrees.First().Options as CSharpParseOptions;

            SyntaxTree Syntaxtree = CSharpSyntaxTree.ParseText(source, options);

            Compilation compilation = context.Compilation.AddSyntaxTrees(Syntaxtree);

            attributeSymbol = compilation.GetTypeByMetadataName($"{AttributeNamespace}.{AttributeName}");

            return compilation;
        }

        private bool HasAttribute(IMethodSymbol? methodSymbol, INamedTypeSymbol? attributeSymbol)
        {
            if (methodSymbol == null)
            {
                return false;
            }

            foreach (INamedTypeSymbol? candidateSymbol in methodSymbol.GetAttributes().Select(A => A.AttributeClass))
            {
                if (candidateSymbol?.Equals(attributeSymbol, SymbolEqualityComparer.Default) ?? false)
                {
                    return true;
                }
            }

            return false;
        }

        protected void ReportDiagnostic(string id, string title, string message, ISymbol? sourceSymbol)
        {
            DiagnosticDescriptor descriptor = new(id, title, message, $"SourceGenerator.{GetType().Name}", DiagnosticSeverity.Warning, true);

            Location? location = sourceSymbol?.DeclaringSyntaxReferences[0].GetSyntax().GetLocation();

            executionContext?.ReportDiagnostic(Diagnostic.Create(descriptor, location));
        }

        private string ProcessClass(INamedTypeSymbol classSymbol, IEnumerable<IMethodSymbol> classMethods)
        {
            INamespaceSymbol namespaceSymbol = classSymbol.ContainingNamespace;

            if (namespaceSymbol is null)
            {
                ReportDiagnostic(
                    "SG001",
                    $"Invalid '{AttributeName}' attribute target - class or struct skipped for source generation.",
                    "Invalid class or struct namespace for source generation. Class or struct can't be top level.",
                    classSymbol);

                return string.Empty;
            }

            string accessibility = GetAccessibility(classSymbol);

            if (accessibility is not "public" or "internal")
            {
                ReportDiagnostic(
                    "SG002",
                    $"Invalid '{AttributeName}' attribute target - class or struct skipped for source generation.",
                    "Invalid class or struct accessibility for source generation. Accessibility must be internal or public.",
                    classSymbol);

                return string.Empty;
            }

            string generics = GetGenerics(classSymbol);

            StringBuilder source = new(
                $@"
                #nullable enable
                namespace {namespaceSymbol.ToDisplayString()}
                {{
                    using System;
                    using System.Numerics;
                    using System.Runtime.CompilerServices;

                    {accessibility} partial class {classSymbol.Name} {generics}
                    {{
                ");

            foreach (IMethodSymbol method in classMethods)
            {
                ProcessMethod(source, method);
            }

            source.Append("} }");

            return source.ToString();
        }

        protected abstract void ProcessMethod(StringBuilder source, IMethodSymbol methodSymbol);

        private string ToFileName(string name)
        {
            return $"{name}_Generated.cs";
        }

        private string GetGenerics(INamedTypeSymbol classSymbol)
        {
            if (classSymbol.IsGenericType == false)
            {
                return string.Empty;
            }

            return $"<{classSymbol.TypeParameters.Names().CommaSeperated()}>";
        }

        protected string GetAccessibility(ISymbol symbol)
        {
            return SyntaxFacts.GetText(symbol.DeclaredAccessibility);
        }

        protected string GetStaticModifier(ISymbol symbol)
        {
            return symbol.IsStatic ? "static" : string.Empty;
        }

        protected virtual string GetReturnType(IMethodSymbol methodSymbol)
        {
            return methodSymbol.ReturnType.ToDisplayString();
        }

        protected virtual string GetArguments(IMethodSymbol methodSymbol)
        {
            return methodSymbol.Parameters.Names().CommaSeperated();
        }

        protected virtual string GetParameters(IMethodSymbol methodSymbol)
        {
            return methodSymbol.Parameters
                .TypesNames()
                .Select(S => $"{S.Type} {S.Name}")
                .CommaSeperated();
        }

        protected virtual string GetGenerics(IMethodSymbol methodSymbol)
        {
            if (methodSymbol.IsGenericMethod == false)
            {
                return string.Empty;
            }

            return $"<{methodSymbol.TypeParameters.Names().CommaSeperated()}>";
        }

        protected virtual string GetConstraints(IMethodSymbol methodSymbol)
        {
            StringBuilder builder = new();

            foreach (ITypeParameterSymbol typeSymbol in methodSymbol.TypeParameters)
            {
                builder.Append(GetConstraints(typeSymbol));
            }

            return builder.ToString();
        }

        protected string GetConstraints(ITypeParameterSymbol typeSymbol)
        {
            IEnumerable<string> constraints = EnumerateConstraints(typeSymbol);

            if (constraints.Any())
            {
                return $"where {typeSymbol.Name} : {constraints.CommaSeperated()} ";
            }

            return string.Empty;
        }

        private IEnumerable<string> EnumerateConstraints(ITypeParameterSymbol typeSymbol)
        {
            if (typeSymbol.HasNotNullConstraint)
            {
                yield return "notnull";
            }
            else if (typeSymbol.HasUnmanagedTypeConstraint)
            {
                yield return "unmanaged";
            }
            else if (typeSymbol.HasValueTypeConstraint)
            {
                yield return "struct";
            }
            else if (typeSymbol.HasReferenceTypeConstraint)
            {
                yield return "class";
            }

            if (typeSymbol.HasConstructorConstraint)
            {
                yield return "new()";
            }

            IEnumerable<string> names = typeSymbol.ConstraintTypes.Types();

            foreach (string name in names)
            {
                yield return name;
            }
        }
    }
}