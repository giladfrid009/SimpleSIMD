namespace SimpleSimd;

public static partial class SimdOps
{
	[ArrOverload]
	public static void Negate<T>(ReadOnlySpan<T> span, Span<T> result) where T : struct, INumber<T>
	{
		Select(span, new Negate_VSelector<T>(), new Negate_Selector<T>(), result);
	}
}

file struct Negate_VSelector<T> : IFunc<Vector<T>, Vector<T>> where T : struct, INumber<T>
{
	public Vector<T> Invoke(Vector<T> vec)
	{
		return -vec;
	}
}

file struct Negate_Selector<T> : IFunc<T, T> where T : struct, INumber<T>
{
	public T Invoke(T val)
	{
		return -val;
	}
}
