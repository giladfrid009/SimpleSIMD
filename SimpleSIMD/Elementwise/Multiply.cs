namespace SimpleSimd;

public static partial class SimdOps
{
	private struct Multiply_VSelector<T> : IFunc<Vector<T>, Vector<T>, Vector<T>> where T : struct, INumber<T>
	{
		public Vector<T> Invoke(Vector<T> left, Vector<T> right)
		{
			return left * right;
		}
	}

	private struct Multiply_Selector<T> : IFunc<T, T, T> where T : struct, INumber<T>
	{
		public T Invoke(T left, T right)
		{
			return left * right;
		}
	}

	[ArrOverload]
	public static void Multiply<T>(ReadOnlySpan<T> left, T right, Span<T> result) where T : struct, INumber<T>
	{
		Concat(left, right, new Multiply_VSelector<T>(), new Multiply_Selector<T>(), result);
	}

	[ArrOverload]
	public static void Multiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result) where T : struct, INumber<T>
	{
		Concat(left, right, new Multiply_VSelector<T>(), new Multiply_Selector<T>(), result);
	}
}
