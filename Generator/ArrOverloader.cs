using Microsoft.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class ArrOverloader : BaseGenerator
    {
        const string ImplNamespace = "System.Runtime.CompilerServices";

        public ArrOverloader() : base("ArrOverloadAttribute", "ArrayOverloads")
        {

        }

        protected override void ProcessMethod(StringBuilder source, IMethodSymbol method)
        {
            if (method.Parameters.Length <= 1) return;

            var paramSymbols = method.Parameters.Take(method.Parameters.Length - 1);

            var genericParams = method.TypeParameters;

            string resultType = ResultType(method);

            if (string.IsNullOrEmpty(resultType)) return;

            string lengthParam = LengthParam(method);
            
            if (string.IsNullOrEmpty(lengthParam)) return;

            string methodName = method.Name;

            string paramNames = string.Join(",", paramSymbols.Names());

            string paramSignatures = string.Join(",", paramSymbols.TypesNames().Select(S => $"{S.Type} {S.Name}"));

            string genericTypes = method.IsGenericMethod ? $"<{string.Join(",", genericParams.Names())}>" : string.Empty;

            string genericConstraints = AllConstraintsFormat(genericParams);

            source.Append(
                $@"
                [{ImplNamespace}.MethodImpl({ImplNamespace}.MethodImplOptions.AggressiveInlining)]
                public static {resultType}[] {methodName}{genericTypes}({paramSignatures}) {genericConstraints}
                {{
                    {resultType}[] result = new {resultType}[{lengthParam}.Length];
                    {methodName}{genericTypes}({paramNames}, result);
                    return result;
                }}"
                );
        }

        private string ResultType(IMethodSymbol method)
        {
            var allParams = method.Parameters;

            var resultParam = allParams[allParams.Length - 1].Type as INamedTypeSymbol;

            if (resultParam is null)
            {
                return string.Empty;
            }

            if (resultParam.Name != "Span")
            {
                return string.Empty;
            }

            if (resultParam.IsGenericType)
            {
                return resultParam.TypeArguments[0].Name;
            }

            return string.Empty;
        }

        private string LengthParam(IMethodSymbol method)
        {
            var allParams = method.Parameters;

            for (int i = 0; i < allParams.Length - 1; i++)
            {
                if ((allParams[i].Type as INamedTypeSymbol)?.Name == "ReadOnlySpan")
                {
                    return allParams[i].Name;
                }
            }

            return string.Empty;
        }
    }
}