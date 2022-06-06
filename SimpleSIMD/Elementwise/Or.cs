using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps
	{
		private struct Or_VSelector<T> : IFunc<Vector<T>, Vector<T>, Vector<T>> where T : struct, IBinaryNumber<T>
		{
			public Vector<T> Invoke(Vector<T> left, Vector<T> right)
			{
				return Vector.BitwiseOr(left, right);
			}
		}

		private struct Or_Selector<T> : IFunc<T, T, T> where T : struct, IBinaryNumber<T>
		{
			public T Invoke(T left, T right)
			{
				return left | right;
			}
		}

		[ArrOverload]
		public static void Or<T>(ReadOnlySpan<T> left, T right, Span<T> result) where T : struct, IBinaryNumber<T>
		{
			Concat(left, right, new Or_VSelector<T>(), new Or_Selector<T>(), result);
		}

		[ArrOverload]
		public static void Or<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result) where T : struct, IBinaryNumber<T>
		{
			Concat(left, right, new Or_VSelector<T>(), new Or_Selector<T>(), result);
		}
	}
}
