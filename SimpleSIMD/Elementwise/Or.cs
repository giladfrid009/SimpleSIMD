﻿namespace SimpleSimd;

public static partial class SimdOps
{
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

file struct Or_VSelector<T> : IFunc<Vector<T>, Vector<T>, Vector<T>> where T : struct, IBinaryNumber<T>
{
	public Vector<T> Invoke(Vector<T> left, Vector<T> right)
	{
		return left | right;
	}
}

file struct Or_Selector<T> : IFunc<T, T, T> where T : struct, IBinaryNumber<T>
{
	public T Invoke(T left, T right)
	{
		return left | right;
	}
}
