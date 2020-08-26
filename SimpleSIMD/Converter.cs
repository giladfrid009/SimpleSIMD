using System;
using System.Linq.Expressions;

namespace SimpleSimd
{
    //todo: bad performance
    public static class Converter<T, U> where T : unmanaged where U : unmanaged
    {
        private static readonly Func<T, U> ConvFunc;

        static Converter()
        {
            ParameterExpression X = Expression.Parameter(typeof(T));
            ConvFunc = Expression.Lambda<Func<T, U>>(Expression.Convert(X, typeof(U)), X).Compile();
        }

        public static U Convert(T value) => ConvFunc(value);
    }
}
