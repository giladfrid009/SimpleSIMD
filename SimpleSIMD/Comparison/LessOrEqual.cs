using System;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool LessOrEqual(in Span<T> left, T right)
        {
            return !Greater(left, right);
        }

        public static bool LessOrEqual(in Span<T> left, in Span<T> right)
        {
            return !Greater(left, right);
        }
    }
}
