using System;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool LessOrEqual(in ReadOnlySpan<T> left, T right)
        {
            return !Greater(left, right);
        }

        public static bool LessOrEqual(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right)
        {
            return !Greater(left, right);
        }
    }
}
