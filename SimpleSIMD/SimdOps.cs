using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SimpleSimd
{
    /// <summary>
    /// Generic SIMD-accelerated operations.
    /// </summary>
    public static partial class SimdOps<T> where T : unmanaged
    {
        private static ReadOnlySpan<Vector<T>> AsVectors(ReadOnlySpan<T> span)
        {
            return MemoryMarshal.Cast<T, Vector<T>>(span);
        }

        private static Span<Vector<TRes>> AsVectors<TRes>(Span<TRes> span) where TRes : unmanaged
        {
            return MemoryMarshal.Cast<TRes, Vector<TRes>>(span);
        }
    }
}
