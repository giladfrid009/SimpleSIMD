namespace SimpleSimd;

public static partial class SimdOps
{
	public static bool Greater<T>(ReadOnlySpan<T> left, T right) where T : struct, INumber<T>
	{
		return All(left, right, new Greater_VSelector<T>(), new Greater_Selector<T>());
	}

	public static bool Greater<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right) where T : struct, INumber<T>
	{
		return All(left, right, new Greater_VSelector<T>(), new Greater_Selector<T>());
	}
}

file struct Greater_VSelector<T> : IFunc<Vector<T>, Vector<T>, bool> where T : struct, INumber<T>
{
	public bool Invoke(Vector<T> left, Vector<T> right)
	{
		return Vector.GreaterThanAll(left, right);
	}
}

file struct Greater_Selector<T> : IFunc<T, T, bool> where T : struct, INumber<T>
{
	public bool Invoke(T left, T right)
	{
		return left > right;
	}
}
