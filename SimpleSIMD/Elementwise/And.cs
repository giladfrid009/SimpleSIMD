using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps
	{
		private struct And_VSelector<T> : IFunc<Vector<T>, Vector<T>, Vector<T>> where T : struct, IBinaryNumber<T>
		{
			public Vector<T> Invoke(Vector<T> left, Vector<T> right)
			{
				return Vector.BitwiseAnd(left, right);
			}
		}

		private struct And_Selector<T> : IFunc<T, T, T> where T : struct, IBinaryNumber<T>
		{
			public T Invoke(T left, T right)
			{
				return  left & right;
			}
		}

		[ArrOverload]
		public static void And<T>(ReadOnlySpan<T> left, T right, Span<T> result) where T : struct, IBinaryNumber<T>
		{
			Concat(left, right, new And_VSelector<T>(), new And_Selector<T>(), result);
		}

		[ArrOverload]
		public static void And<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result) where T : struct, IBinaryNumber<T>
		{
			Concat(left, right, new And_VSelector<T>(), new And_Selector<T>(), result);
		}
	}
}
