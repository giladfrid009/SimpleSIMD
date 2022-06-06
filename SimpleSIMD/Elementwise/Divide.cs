using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps
	{
		private struct Divide_VSelector<T> : IFunc<Vector<T>, Vector<T>, Vector<T>> where T : struct, INumber<T>
		{
			public Vector<T> Invoke(Vector<T> left, Vector<T> right)
			{
				return Vector.Divide(left, right);
			}
		}

		private struct Divide_Selector<T> : IFunc<T, T, T> where T : struct, INumber<T>
		{
			public T Invoke(T left, T right)
			{
				return left / right;
			}
		}

		[ArrOverload]
		public static void Divide<T>(ReadOnlySpan<T> left, T right, Span<T> result) where T : struct, INumber<T>
		{
			Concat(left, right, new Divide_VSelector<T>(), new Divide_Selector<T>(), result);
		}

		[ArrOverload]
		public static void Divide<T>(T left, ReadOnlySpan<T> right, Span<T> result) where T : struct, INumber<T>
		{
			Concat(left, right, new Divide_VSelector<T>(), new Divide_Selector<T>(), result);
		}

		[ArrOverload]
		public static void Divide<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result) where T : struct, INumber<T>
		{
			Concat(left, right, new Divide_VSelector<T>(), new Divide_Selector<T>(), result);
		}
	}
}
