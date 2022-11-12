namespace SimpleSimd;

public static partial class SimdOps
{
	private struct Sqrt_VSelector<T> : IFunc<Vector<T>, Vector<T>> where T : struct, IFloatingPointIeee754<T>
	{
		public Vector<T> Invoke(Vector<T> vec)
		{
			return Vector.SquareRoot(vec);
		}
	}

	private struct Sqrt_Selector<T> : IFunc<T, T> where T : struct, IFloatingPointIeee754<T>
	{
		public T Invoke(T val)
		{
			return T.Sqrt(val);
		}
	}

	[ArrOverload]
	public static void Sqrt<T>(ReadOnlySpan<T> span, Span<T> result) where T : struct, IFloatingPointIeee754<T>
	{
		Select(span, new Sqrt_VSelector<T>(), new Sqrt_Selector<T>(), result);
	}
}
