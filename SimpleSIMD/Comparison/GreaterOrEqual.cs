using System;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool GreaterOrEqual(in ReadOnlySpan<T> left, T right)
        {
            return !Less(left, right);
        }

        public static bool GreaterOrEqual(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right)
        {
            return !Less(left, right);
        }
    }
}
