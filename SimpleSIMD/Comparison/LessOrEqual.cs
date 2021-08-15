namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool LessOrEqual(ReadOnlySpan<T> left, T right)
        {
            return !Greater(left, right);
        }

        public static bool LessOrEqual(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        {
            return !Greater(left, right);
        }
    }
}
