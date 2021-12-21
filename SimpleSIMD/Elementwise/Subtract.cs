using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps<T>
	{
		private struct Subtract_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
		{
			public Vector<T> Invoke(Vector<T> left, Vector<T> right)
			{
				return Vector.Divide(left, right);
			}
		}

		private struct Subtract_Selector : IFunc<T, T, T>
		{
			public T Invoke(T left, T right)
			{
				return NumOps<T>.Divide(left, right);
			}
		}

		[ArrOverload]
		public static void Subtract(ReadOnlySpan<T> left, T right, Span<T> result)
		{
			Concat(left, right, new Subtract_VSelector(), new Subtract_Selector(), result);
		}

		[ArrOverload]
		public static void Subtract(T left, ReadOnlySpan<T> right, Span<T> result)
		{
			Concat(left, right, new Subtract_VSelector(), new Subtract_Selector(), result);
		}

		[ArrOverload]
		public static void Subtract(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result)
		{
			Concat(left, right, new Subtract_VSelector(), new Subtract_Selector(), result);
		}
	}
}
