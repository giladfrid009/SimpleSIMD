using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SimpleSimd
{
    /// <summary>
    /// Generic SIMD-accelerated array operations.
    /// </summary>
    /// <typeparam name="T">Array element type</typeparam>
    public static partial class ArrayOps<T> where T : unmanaged
    {
        private static Span<Vector<U>> AsVectors<U>(U[] array) where U : unmanaged
        {
            return MemoryMarshal.Cast<U, Vector<U>>(array);
        }
    }
}
