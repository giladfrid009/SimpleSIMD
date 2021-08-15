namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool GreaterOrEqual(ReadOnlySpan<T> left, T right)
        {
            return !Less(left, right);
        }

        public static bool GreaterOrEqual(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        {
            return !Less(left, right);
        }
    }
}
