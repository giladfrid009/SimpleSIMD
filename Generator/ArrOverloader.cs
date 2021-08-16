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

        protected override void ProcessMethod(StringBuilder source, IMethodSymbol method)
        {
            if (method.Parameters.Length <= 1) return;

            var paramSymbols = method.Parameters.Take(method.Parameters.Length - 1);

            var genericSymbols = method.TypeParameters;

            string methodReturn = ResultType(method);

            if (string.IsNullOrEmpty(methodReturn)) return;

            string lengthParam = LengthParam(method);
            
            if (string.IsNullOrEmpty(lengthParam)) return;

            string methodName = method.Name;

            string methodParams = string.Join(",", paramSymbols.Names());

            string methodSignatures = string.Join(",", paramSymbols.TypesNames().Select(S => $"{S.Type} {S.Name}"));

            string methodGenerics = method.IsGenericMethod ? $"<{string.Join(",", genericSymbols.Names())}>" : string.Empty;

            string methodConstraints = AllConstraintsFormat(genericSymbols);

            source.Append(
                $@"
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static {methodReturn}[] {methodName}{methodGenerics}({methodSignatures}) {methodConstraints}
                {{
                    {methodReturn}[] result = new {methodReturn}[{lengthParam}.Length];
                    {methodName}{methodGenerics}({methodParams}, result);
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