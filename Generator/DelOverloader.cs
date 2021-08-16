using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class DelOverloader : BaseGenerator
    {
        public DelOverloader() : base("DelOverloadAttribute", "DelOverloads")
        {

        }

        protected override void ProcessMethod(StringBuilder source, IMethodSymbol method)
        {
            if (method.IsGenericMethod == false) return;

            var genericSymbols = method.TypeParameters;

            if (genericSymbols.All(S => GetValueDelegate(S) is null)) return;

            var methodNode = (MethodDeclarationSyntax)method.DeclaringSyntaxReferences[0].GetSyntax();

            var methodBody = methodNode?.Body?.GetText();

            string methodName = method.Name;

            string methodReturn = method.ReturnsVoid ? "void" : method.ReturnType.ToDisplayString();

            string methodSignature = string.Join(",", method.Parameters.TypesNames().Select(S => $"{S.Type} {S.Name}"));

            var notVDelegates = genericSymbols.Where(P => GetValueDelegate(P) is null).Names();

            string methodGenerics = notVDelegates.Any() ? $"<{string.Join(",", notVDelegates)}>" : string.Empty;

            var methodConstraints = new StringBuilder();

            foreach (var typeSymbol in genericSymbols)
            {
                var valueDelegate = GetValueDelegate(typeSymbol);

                if (valueDelegate is null)
                {
                    methodConstraints.Append(TypeConstraintsFormat(typeSymbol));
                    continue;
                }

                methodSignature = methodSignature.Replace(typeSymbol.Name, ToDelegate(valueDelegate.ToDisplayString()));
            }

            source.Append($@"public static {methodReturn} {methodName} {methodGenerics} ({methodSignature}) {methodConstraints} {methodBody}");
        }

        protected ITypeSymbol? GetValueDelegate(ITypeParameterSymbol typeSymbol)
        {
            if (typeSymbol.ConstraintTypes.Length == 0) return null;

            foreach (var S in typeSymbol.ConstraintTypes)
            {
                if (S.Name == "IAction" || S.Name == "IFunc")
                {
                    return S;
                }
            }

            return null;
        }

        protected string ToDelegate(string valueDelegate)
        {
            return valueDelegate.Replace("SimpleSimd.IFunc", "System.Func").Replace("SimpleSimd.IAction", "System.Action");
        }
    }
}