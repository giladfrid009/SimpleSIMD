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
        protected string AttrName { get; }
        protected string FileName { get; }
        protected string NamespaceName { get; } = "SimpleSimd";
        protected string ClassName { get; } = "SimdOps<T>";

        protected string AttrSource { get; }

        protected BaseGenerator(string attrName, string fileName)
        {
            AttrName = attrName;
            FileName = fileName;

            AttrSource = 
                $@"
                using System;
                namespace {NamespaceName}
                {{
                    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
                    sealed class {AttrName} : Attribute
                    {{
                        public {AttrName}()
                        {{
                        }}
                    }}
                }}
                ";
        }

        protected BaseGenerator(string attrName, string fileName, string namespaceName, string className) : this(attrName, fileName)
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
            context.AddSource(AttrName, SourceText.From(AttrSource, Encoding.UTF8));

            var syntaxReciever = context.SyntaxReceiver as SyntaxReceiver;

            if (syntaxReciever is null)
            {
                return;
            }

            var parseOptions = ((CSharpCompilation)context.Compilation).SyntaxTrees[0].Options as CSharpParseOptions;

            var compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(AttrSource, Encoding.UTF8), parseOptions));

            var methods = new List<IMethodSymbol>();

            foreach (var declaration in syntaxReciever.CandidateMethods)
            {
                var model = compilation.GetSemanticModel(declaration.SyntaxTree);

                var method = model.GetDeclaredSymbol(declaration);

                if (method == null || method.ContainingType.ToDisplayString() != $"{NamespaceName}.{ClassName}")
                {
                    continue;
                }

                if (method.GetAttributes().Any(A => A.AttributeClass?.ToDisplayString() == $"{NamespaceName}.{AttrName}"))
                {
                    methods.Add(method);
                }
            }

            string source = CreateSource(methods);

            context.AddSource($"{FileName}.cs", SourceText.From(source, Encoding.UTF8));
        }

        private string CreateSource(IEnumerable<IMethodSymbol> methods)
        {
            var source = new StringBuilder(
                $@"
                namespace {NamespaceName}
                {{
                    public partial class {ClassName}
                    {{
                ");

            foreach (var method in methods)
            {
                ProcessMethod(source, method);
            }

            source.Append("} }");

            return source.ToString();
        }

        protected abstract void ProcessMethod(StringBuilder source, IMethodSymbol methodSymbol);

        protected string ParamNames(IEnumerable<IParameterSymbol> paramSymbols)
        {
            return string.Join(",", paramSymbols.Select(P => P.Name));
        }

        protected string ParamSignatures(IEnumerable<IParameterSymbol> paramSymbols)
        {
            return string.Join(",", paramSymbols.Select(P => $"{P.ToDisplayString()} {P.Name}"));
        }

        protected string GenericTypes(IMethodSymbol methodSymbol)
        {
            if (methodSymbol.IsGenericMethod)
            {
                return string.Format($"<{string.Join(",", methodSymbol.TypeParameters.Select(P => P.Name))}>");
            }

            return "";
        }

        protected string GenericConstraints(IMethodSymbol methodSymbol)
        {
            var genericConstsraints = new StringBuilder();

            if (methodSymbol.IsGenericMethod)
            {
                foreach (var genericType in methodSymbol.TypeParameters)
                {
                    string typeConstrains = string.Join(",", TypeConstraints(genericType));

                    if (typeConstrains.Length > 0)
                    {
                        genericConstsraints.Append($"where {genericType.Name} : {typeConstrains} ");
                    }
                }
            }

            return genericConstsraints.ToString();
        }

        private IEnumerable<string> TypeConstraints(ITypeParameterSymbol typeSymbol)
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

            foreach (var T in typeSymbol.ConstraintTypes)
            {
                yield return T.ToDisplayString();
            }
        }
    }
}