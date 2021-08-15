using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    class OverloadGenerator : ISourceGenerator
    {
        private static string AttributeText = $@"
        using System;
        namespace {Consts.AttrNamespace}
        {{
            [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
            sealed class {Consts.AttrName} : Attribute
            {{
                public {Consts.AttrName}()
                {{
                }}
            }}
        }}
        ";

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource(Consts.AttrName, SourceText.From(AttributeText, Encoding.UTF8));

            var receiver = context.SyntaxReceiver as SyntaxReceiver;

            if (receiver is null)
            {
                return;
            }

            var options = (context.Compilation as CSharpCompilation).SyntaxTrees[0].Options as CSharpParseOptions;

            var compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(AttributeText, Encoding.UTF8), options));

            var attribute = compilation.GetTypeByMetadataName($"{Consts.AttrNamespace}.{Consts.AttrName}");

            var methods = new List<IMethodSymbol>();

            foreach (var methodDecl in receiver.CandidateMethods)
            {
                var model = compilation.GetSemanticModel(methodDecl.SyntaxTree);

                var method = model.GetDeclaredSymbol(methodDecl);

                if (method.GetAttributes().Any(A => A.AttributeClass.Equals(attribute, SymbolEqualityComparer.Default)))
                {
                    methods.Add(method);
                }
            }

            methods = methods.Where(M => M.ContainingType.ToDisplayString() == $"{Consts.AttrNamespace}.{Consts.ClassName}").ToList();

            string source = ProcessClass(methods);

            context.AddSource($"{Consts.FileName}.cs", SourceText.From(source, Encoding.UTF8));
        }

        private string ProcessClass(List<IMethodSymbol> methods)
        {
            StringBuilder source = new StringBuilder($@"
            namespace {Consts.AttrNamespace}
            {{
                public partial class {Consts.ClassName}
                {{
            ");

            foreach (var method in methods)
            {
                ProcessMethod(source, method);
            }

            source.Append("} }");

            return source.ToString();
        }

        private void ProcessMethod(StringBuilder source, IMethodSymbol method)
        {
            var parameters = method.Parameters.Take(method.Parameters.Length - 1).ToArray();

            var retType = Utils.ReturnType(method);

            if (retType == "")
            {
                return;
            }

            string spanParam = Utils.SpanParam(parameters);

            if (spanParam == "")
            {
                return;
            }

            var paramSignatures = string.Join(",", parameters.Select(P => $"{P.ToDisplayString()} {P.Name}"));

            var paramNames = string.Join(",", parameters.Select(P => $"{P.Name}"));

            var genericTypes = method.TypeParameters;

            string generics = "";

            var genericConstsraints = new StringBuilder();

            if (method.IsGenericMethod)
            {
                generics = "<" + string.Join(",", genericTypes.Select(P => P.Name)) + ">";

                foreach (var T in genericTypes)
                {
                    string typeConstrains = string.Join(",", Utils.TypeConstraints(T));

                    if (typeConstrains.Length > 0)
                    {
                        genericConstsraints.Append($"where {T.Name} : {typeConstrains} ");
                    }
                }
            }

            source.Append($@"
            public static {retType}[] {method.Name}{generics}({paramSignatures}) {genericConstsraints}
            {{
                {retType}[] result = new {retType}[{spanParam}.Length];
                {method.Name}{generics}({paramNames}, result);
                return result;
            }}"
            );
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }
    }
}