namespace SimpleSimd;

public static partial class SimdOps
{
	public static bool GreaterOrEqual<T>(ReadOnlySpan<T> left, T right) where T : struct, INumber<T>
	{
		return !Less(left, right);
	}

	public static bool GreaterOrEqual<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right) where T : struct, INumber<T>
	{
		return !Less(left, right);
	}
}
