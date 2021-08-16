using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        protected override void ProcessMethod(StringBuilder source, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.IsGenericMethod == false) return;

            if (methodSymbol.TypeParameters.All(S => GetValueDelegate(S) is null)) return;

            var methodNode = (MethodDeclarationSyntax)methodSymbol.DeclaringSyntaxReferences[0].GetSyntax();

            var methodBody = methodNode?.Body?.GetText();

            string methodName = methodSymbol.Name;

            string staticModifier = MethodStaticModifier(methodSymbol);

            string returnType = methodSymbol.ReturnType.ToDisplayString();

            string parameters = MethodParameters(methodSymbol);

            var genericSymbols = methodSymbol.TypeParameters.Where(P => GetValueDelegate(P) is null).Names();

            string generics = genericSymbols.Any() ? $"<{string.Join(",", genericSymbols)}>" : string.Empty;

            var constraints = new StringBuilder();

            foreach (var typeSymbol in methodSymbol.TypeParameters)
            {
                var valueDelegate = GetValueDelegate(typeSymbol);

                if (valueDelegate is null)
                {
                    constraints.Append(TypeConstraints(typeSymbol));
                    continue;
                }

                parameters = parameters.Replace(typeSymbol.Name, ToDelegateString(valueDelegate.ToDisplayString()));
            }

            source.Append($@"public {staticModifier} {returnType} {methodName} {generics} ({parameters}) {constraints} {methodBody}");
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

        protected string ToDelegateString(string valueDelegate)
        {
            return valueDelegate.Replace($"{NamespaceName}.IFunc", "System.Func").Replace($"{NamespaceName}.IAction", "System.Action");
        }
    }
}