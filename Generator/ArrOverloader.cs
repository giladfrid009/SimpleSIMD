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
            var paramSymbols = method.Parameters.Take(method.Parameters.Length - 1);

            string resultType = ResultType(method);
            if (resultType == "") return;

            string lengthParam = LengthParam(method);
            if (lengthParam == "") return;

            string methodName = method.Name;
            string paramNames = ParamNames(paramSymbols);
            string paramSignatures = ParamSignatures(paramSymbols);
            string genericTypes = GenericTypes(method);
            string genericConstraints = GenericConstraints(method);

            source.Append(
                $@"
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

            string resultType = allParams[allParams.Length - 1].ToDisplayString();

            return resultType switch
            {
                "System.Span<T>" => "T",
                "System.Span<TRes>" => "TRes",
                _ => ""
            };
        }

        private string LengthParam(IMethodSymbol method)
        {
            var allParams = method.Parameters;

            if (allParams.Length == 0)
            {
                return "";
            }

            if (allParams[0].ToDisplayString() == "System.ReadOnlySpan<T>")
            {
                return allParams[0].Name;
            }

            if (allParams.Length > 1 && allParams[1].ToDisplayString() == "System.ReadOnlySpan<T>")
            {
                return allParams[1].Name;
            }

            return "";
        }        
    }
}