using Microsoft.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class ArrOverloader : BaseGenerator
    {
        public ArrOverloader() : base("ArrOverloadAttribute", "ArrayOverloads")
        {

        }

        protected override void ProcessMethod(StringBuilder source, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.Parameters.Length <= 1) return;

            var paramSymbols = methodSymbol.Parameters.Take(methodSymbol.Parameters.Length - 1);

            string returnType = ReturnType(methodSymbol);

            if (string.IsNullOrEmpty(returnType)) return;

            string lengthArgument = LengthArgument(methodSymbol);
            
            if (string.IsNullOrEmpty(lengthArgument)) return;

            string methodName = methodSymbol.Name;

            string staticModifier = MethodStaticModifier(methodSymbol);

            string arguments = string.Join(",", paramSymbols.Names());

            string parameters = string.Join(",", paramSymbols.TypesNames().Select(S => $"{S.Type} {S.Name}"));

            string generics = MethodGenerics(methodSymbol);

            string constraints = MethodConstraints(methodSymbol);

            source.Append(
                $@"
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public {staticModifier} {returnType}[] {methodName} {generics} ({parameters}) {constraints}
                {{
                    {returnType} [] result = new {returnType} [{lengthArgument}.Length];
                    {methodName} {generics} ({arguments}, result);
                    return result;
                }}"
                );
        }

        private string ReturnType(IMethodSymbol methodSymbol)
        {
            var resultParameter = methodSymbol.Parameters[methodSymbol.Parameters.Length - 1].Type as INamedTypeSymbol;

            if (resultParameter is null)
            {
                return string.Empty;
            }

            if (resultParameter.Name != "Span")
            {
                return string.Empty;
            }

            if (resultParameter.IsGenericType)
            {
                return resultParameter.TypeArguments[0].Name;
            }

            return string.Empty;
        }

        private string LengthArgument(IMethodSymbol methodSymbol)
        {
            var parameterSymbols = methodSymbol.Parameters;

            for (int i = 0; i < parameterSymbols.Length - 1; i++)
            {
                if ((parameterSymbols[i].Type as INamedTypeSymbol)?.Name == "ReadOnlySpan")
                {
                    return parameterSymbols[i].Name;
                }
            }

            return string.Empty;
        }
    }
}