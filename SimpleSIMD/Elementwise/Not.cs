using System;
using System.Numerics;

namespace SimpleSimd;

public static partial class SimdOps
{
	private struct Not_VSelector<T> : IFunc<Vector<T>, Vector<T>> where T : struct, IBinaryNumber<T>
	{
		public Vector<T> Invoke(Vector<T> vec)
		{
			return ~vec;
		}
	}

	private struct Not_Selector<T> : IFunc<T, T> where T : struct, IBinaryNumber<T>
	{
		public T Invoke(T val)
		{
			return ~val;
		}
	}

	[ArrOverload]
	public static void Not<T>(ReadOnlySpan<T> span, Span<T> result) where T : struct, IBinaryNumber<T>
	{
		Select(span, new Not_VSelector<T>(), new Not_Selector<T>(), result);
	}
}
