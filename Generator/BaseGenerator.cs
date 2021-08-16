using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator
{
    public abstract class BaseGenerator : ISourceGenerator
    {
        protected string AttributeName { get; }
        protected string FileName { get; }
        protected string NamespaceName { get; } = "SimpleSimd";
        protected string ClassName { get; } = "SimdOps<T>";

        protected string AttributeSource { get; }

        protected BaseGenerator(string attrName, string fileName)
        {
            AttributeName = attrName;
            FileName = fileName;

            AttributeSource = 
                $@"
                using System;
                namespace {NamespaceName}
                {{
                    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
                    sealed class {AttributeName} : Attribute
                    {{
                        public {AttributeName}()
                        {{
                        }}
                    }}
                }}
                ";
        }

        protected BaseGenerator(string attributeName, string fileName, string namespaceName, string className) : this(attributeName, fileName)
        {
            NamespaceName = namespaceName;
            ClassName = className;
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource(AttributeName, SourceText.From(AttributeSource, Encoding.UTF8));

            var syntaxReciever = context.SyntaxReceiver as SyntaxReceiver;

            if (syntaxReciever is null)
            {
                return;
            }

            var parseOptions = ((CSharpCompilation)context.Compilation).SyntaxTrees[0].Options as CSharpParseOptions;

            var compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(AttributeSource, Encoding.UTF8), parseOptions));

            var methodSymbols = new List<IMethodSymbol>();

            foreach (var methodDeclarations in syntaxReciever.CandidateMethods)
            {
                var semanticModel = compilation.GetSemanticModel(methodDeclarations.SyntaxTree);

                var methodSymbol = semanticModel.GetDeclaredSymbol(methodDeclarations);

                if (methodSymbol == null || methodSymbol.ContainingType.ToDisplayString() != $"{NamespaceName}.{ClassName}")
                {
                    continue;
                }

                if (methodSymbol.GetAttributes().Any(A => A.AttributeClass?.ToDisplayString() == $"{NamespaceName}.{AttributeName}"))
                {
                    methodSymbols.Add(methodSymbol);
                }
            }

            string source = CreateSource(methodSymbols);

            context.AddSource($"{FileName}.cs", SourceText.From(source, Encoding.UTF8));
        }

        private string CreateSource(IEnumerable<IMethodSymbol> methodSymbols)
        {
            var source = new StringBuilder(
                $@"
                namespace {NamespaceName}
                {{
                    using System;
                    using System.Numerics;
                    using System.Runtime.CompilerServices;

                    public partial class {ClassName}
                    {{
                ");

            foreach (var method in methodSymbols)
            {
                ProcessMethod(source, method);
            }

            source.Append("} }");

            return source.ToString();
        }

        protected abstract void ProcessMethod(StringBuilder source, IMethodSymbol methodSymbol);  

        protected string AllConstraintsFormat(IEnumerable<ITypeParameterSymbol> typeSymbols)
        {          
            var str = new StringBuilder();

            foreach (var typeSymbol in typeSymbols)
            {
                str.Append(TypeConstraintsFormat(typeSymbol));
            }

            return str.ToString();            
        }

        protected string TypeConstraintsFormat(ITypeParameterSymbol typeSymbol)
        {
            var constraints = TypeConstraints(typeSymbol);

            if (constraints.Any())
            {
                return $"where {typeSymbol.Name} : {string.Join(",", constraints)}";
            }

            return string.Empty;
        }

        protected IEnumerable<string> TypeConstraints(ITypeParameterSymbol typeSymbol)
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

            var types = typeSymbol.ConstraintTypes.Types();

            foreach (var type in types)
            {
                yield return type;
            }
        }
    }
}