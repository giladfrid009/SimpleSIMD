using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Generator
{
    static class Utils
    {
        public static string ReturnType(IMethodSymbol method)
        {
            var resParam = method.Parameters[method.Parameters.Length - 1].ToDisplayString();

            return resParam switch
            {
                "System.Span<T>" => "T",
                "System.Span<TRes>" => "TRes",
                _ => ""
            };
        }

        public static string SpanParam(IParameterSymbol[] paramSyms)
        {
            if (paramSyms.Length == 0)
            {
                return "";
            }

            if (paramSyms[0].ToDisplayString() == "System.ReadOnlySpan<T>")
            {
                return paramSyms[0].Name;
            }

            if (paramSyms.Length > 1 && paramSyms[1].ToDisplayString() == "System.ReadOnlySpan<T>")
            {
                return paramSyms[1].Name;
            }

            return "";
        }

        public static IEnumerable<string> TypeConstraints(ITypeParameterSymbol typeSymbol)
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
