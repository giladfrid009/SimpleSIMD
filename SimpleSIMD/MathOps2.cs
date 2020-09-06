using System;
using System.Linq.Expressions;

namespace SimpleSimd
{
    /// <summary>
    /// Generic runtime-generated numeric operations where the output type differs from the input type. 
    /// </summary>
    /// <typeparam name="TIn">Input numeric type</typeparam>
    /// <typeparam name="TOut">Output numeric type</typeparam>
    public static class MathOps<TIn, TOut> where TIn : unmanaged where TOut : unmanaged
    {
        private static readonly Func<TIn, TOut> convFunc;

        static MathOps()
        {
            ParameterExpression X = Expression.Parameter(typeof(TIn));
            convFunc = Expression.Lambda<Func<TIn, TOut>>(Expression.Convert(X, typeof(TOut)), X).Compile();
        }

        public static TOut Convert(TIn value) => convFunc(value);
    }
}
