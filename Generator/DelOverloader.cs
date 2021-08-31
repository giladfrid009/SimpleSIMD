using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator
{
    [Generator]
    public class DelOverloader : BaseGenerator
    {
        public DelOverloader() : base("DelOverloadAttribute", "SimpleSimd")
        {

        }

        protected override void ProcessMethod(StringBuilder source, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.TypeParameters.All(S => IsValueDelegate(S) == false))
            {
                ReportDiagnostic(
                    "DSG001",
                    $"Invalid '{AttributeName}' attribute target - method skipped for source generation.",
                    "The method does not have a parameter constrained to be a Value-Delegate. " +
                    "At least one of the parameters must be a generic type constrained as IFunc or IAction.",
                    methodSymbol);

               return;
            }

            string methodBody = MethodBody(methodSymbol);

            if (string.IsNullOrEmpty(methodBody))
            {
                return;
            }

            string methodName = methodSymbol.Name;
            string accessibility = Accessibility(methodSymbol);
            string staticModifier = StaticModifier(methodSymbol);
            string returnType = ReturnType(methodSymbol);
            string parameters = Parameters(methodSymbol);
            string generics = Generics(methodSymbol);
            string constraints = Constraints(methodSymbol);

            source.Append($"{accessibility} {staticModifier} {returnType} {methodName} {generics} ({parameters}) {constraints} {methodBody}");
        }

        protected override string Generics(IMethodSymbol methodSymbol)
        {
            var generics = methodSymbol.TypeParameters
                .Where(P => IsValueDelegate(P) == false)
                .Names();

            return generics.Any() ? $"<{generics.CommaSeperated()}>" : string.Empty;
        }

        protected override string Parameters(IMethodSymbol methodSymbol)
        {
            string parameters = base.Parameters(methodSymbol);

            foreach (var typeSymbol in methodSymbol.TypeParameters)
            {
                foreach (var constraintSymbol in typeSymbol.ConstraintTypes)
                {
                    if (constraintSymbol.Name == "IFunc")
                    {
                        parameters = parameters.Replace(typeSymbol.Name,
                            constraintSymbol
                            .ToDisplayString()
                            .Replace("SimpleSimd.IFunc", "System.Func"));
                    }
                    else if (constraintSymbol.Name == "IAction")
                    {
                        parameters = parameters.Replace(typeSymbol.Name,
                            constraintSymbol
                            .ToDisplayString()
                            .Replace("SimpleSimd.IAction", "System.Action"));
                    }
                }
            }

            return parameters;
        }

        protected override string Constraints(IMethodSymbol methodSymbol)
        {
            var constraints = new StringBuilder();

            foreach (var typeSymbol in methodSymbol.TypeParameters)
            {
                if (IsValueDelegate(typeSymbol) == false)
                {
                    constraints.Append(Constraints(typeSymbol));
                }
            }

            return constraints.ToString();
        }

        protected bool IsValueDelegate(ITypeParameterSymbol typeSymbol)
        {
            foreach (var C in typeSymbol.ConstraintTypes)
            {
                if (C.Name is "IAction" or "IFunc")
                {
                    return true;
                }
            }

            return false;
        }

        protected string MethodBody(IMethodSymbol methodSymbol)
        {
            var methodNode = methodSymbol.DeclaringSyntaxReferences[0].GetSyntax() as MethodDeclarationSyntax;

            return methodNode?.Body?.GetText().ToString() ?? string.Empty;
        }
    }
}