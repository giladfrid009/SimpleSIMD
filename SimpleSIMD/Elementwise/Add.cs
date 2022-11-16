namespace SimpleSimd;

public static partial class SimdOps
{
	[ArrOverload]
	public static void Add<T>(ReadOnlySpan<T> left, T right, Span<T> result) where T : struct, INumber<T>
	{
		Concat(left, right, new Add_VSelector<T>(), new Add_Selector<T>(), result);
	}

	[ArrOverload]
	public static void Add<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result) where T : struct, INumber<T>
	{
		Concat(left, right, new Add_VSelector<T>(), new Add_Selector<T>(), result);
	}
}

file struct Add_VSelector<T> : IFunc<Vector<T>, Vector<T>, Vector<T>> where T : struct, INumber<T>
{
	public Vector<T> Invoke(Vector<T> left, Vector<T> right)
	{
		return left + right;
	}
}

file struct Add_Selector<T> : IFunc<T, T, T> where T : struct, INumber<T>
{
	public T Invoke(T left, T right)
	{
		return left + right;
	}
}
