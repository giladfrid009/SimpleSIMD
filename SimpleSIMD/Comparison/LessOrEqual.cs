namespace SimpleSimd;

public static partial class SimdOps
{
	public static bool LessOrEqual<T>(ReadOnlySpan<T> left, T right) where T : struct, INumber<T>
	{
		return !Greater(left, right);
	}

	public static bool LessOrEqual<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right) where T : struct, INumber<T>
	{
		return !Greater(left, right);
	}
}
