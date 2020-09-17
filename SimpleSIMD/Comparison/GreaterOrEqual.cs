using System;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool GreaterOrEqual(in Span<T> left, T right)
        {
            return !Less(left, right);
        }

        public static bool GreaterOrEqual(in Span<T> left, in Span<T> right)
        {
            return !Less(left, right);
        }
    }
}
