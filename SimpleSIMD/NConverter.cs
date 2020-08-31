using System;
using System.Linq.Expressions;

namespace SimpleSimd
{
    public static class NConverter<TIn, TOut> where TIn : unmanaged where TOut : unmanaged
    {
        private static readonly Func<TIn, TOut> Func;

        static NConverter()
        {
            ParameterExpression X = Expression.Parameter(typeof(TIn));
            Func = Expression.Lambda<Func<TIn, TOut>>(Expression.Convert(X, typeof(TOut)), X).Compile();
        }

        public static TOut Convert(TIn value) => Func(value);
    }
}
