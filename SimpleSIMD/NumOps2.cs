using System;
using System.Linq.Expressions;

namespace SimpleSimd
{
	public static class NumOps<T, TRes> where T : struct where TRes : struct
	{
		private static readonly Func<T, TRes> convFunc;

		static NumOps()
		{
			ParameterExpression X = Expression.Parameter(typeof(T));
			convFunc = Expression.Lambda<Func<T, TRes>>(Expression.Convert(X, typeof(TRes)), X).Compile();
		}

		public static TRes Convert(T value)
		{
			return convFunc(value);
		}
	}
}
