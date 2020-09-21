using System;
using System.Linq.Expressions;

namespace SimpleSimd
{
    /// <summary>
    /// Generic runtime-generated numeric operations where the output type differs from the input type. 
    /// </summary>
    public static class NumOps<T, TRes> where T : unmanaged where TRes : unmanaged
    {
        private static readonly Func<T, TRes> convFunc;

        static NumOps()
        {
            ParameterExpression X = Expression.Parameter(typeof(T));
            convFunc = Expression.Lambda<Func<T, TRes>>(Expression.Convert(X, typeof(TRes)), X).Compile();
        }

        public static TRes Convert(T value) => convFunc(value);
    }
}
