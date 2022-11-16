namespace SimpleSimd;

public static partial class SimdOps
{
	[ArrOverload]
	public static void Abs<T>(ReadOnlySpan<T> span, Span<T> result) where T : struct, INumber<T>
	{
		Select(span, new Abs_VSelector<T>(), new Abs_Selector<T>(), result);
	}
}

file struct Abs_VSelector<T> : IFunc<Vector<T>, Vector<T>> where T : struct, INumber<T>
{
	public Vector<T> Invoke(Vector<T> vec)
	{
		return Vector.Abs(vec);
	}
}

file struct Abs_Selector<T> : IFunc<T, T> where T : struct, INumber<T>
{
	public T Invoke(T val)
	{
		return T.Abs(val);
	}
}
