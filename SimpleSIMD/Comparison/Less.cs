using System;
using System.Numerics;

namespace SimpleSimd;

public static partial class SimdOps
{
	private struct Less_VSelector<T> : IFunc<Vector<T>, Vector<T>, bool> where T : struct, INumber<T>
	{
		public bool Invoke(Vector<T> left, Vector<T> right)
		{
			return Vector.LessThanAll(left, right);
		}
	}

	private struct Less_Selector<T> : IFunc<T, T, bool> where T : struct, INumber<T>
	{
		public bool Invoke(T left, T right)
		{
			return left < right;
		}
	}

	public static bool Less<T>(ReadOnlySpan<T> left, T right) where T : struct, INumber<T>
	{
		return All(left, right, new Less_VSelector<T>(), new Less_Selector<T>());
	}

	public static bool Less<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right) where T : struct, INumber<T>
	{
		return All(left, right, new Less_VSelector<T>(), new Less_Selector<T>());
	}
}
