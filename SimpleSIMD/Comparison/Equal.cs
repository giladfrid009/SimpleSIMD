using System;
using System.Numerics;

namespace SimpleSimd;

public static partial class SimdOps
{
	private struct Equal_VSelector<T> : IFunc<Vector<T>, Vector<T>, bool> where T : struct, INumber<T>
	{
		public bool Invoke(Vector<T> left, Vector<T> right)
		{
			return left == right;
		}
	}

	private struct Equal_Selector<T> : IFunc<T, T, bool> where T : struct, INumber<T>
	{
		public bool Invoke(T left, T right)
		{
			return left == right;
		}
	}

	public static bool Equal<T>(ReadOnlySpan<T> left, T right) where T : struct, INumber<T>
	{
		return All(left, right, new Equal_VSelector<T>(), new Equal_Selector<T>());
	}

	public static bool Equal<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right) where T : struct, INumber<T>
	{
		return All(left, right, new Equal_VSelector<T>(), new Equal_Selector<T>());
	}
}
