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
        private static Span<Vector<U>> AsVectors<U>(Span<U> span) where U : unmanaged
        {
            return MemoryMarshal.Cast<U, Vector<U>>(span);
        }
    }
}
