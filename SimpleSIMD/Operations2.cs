using System;
using System.Linq.Expressions;

namespace SimpleSimd
{
    public static class Operations<TIn, TOut> where TIn : unmanaged where TOut : unmanaged
    {
        private static readonly Func<TIn, TOut> ConvFunc;

        static Operations()
        {
            ParameterExpression X = Expression.Parameter(typeof(TIn));
            ConvFunc = Expression.Lambda<Func<TIn, TOut>>(Expression.Convert(X, typeof(TOut)), X).Compile();
        }

        public static TOut Convert(TIn value) => ConvFunc(value);
    }
}
