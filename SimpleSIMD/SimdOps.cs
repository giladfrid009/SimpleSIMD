using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SimpleSimd
{
    public static partial class SimdOps<T> where T : struct
    {
        private static ref Vector<U> AsVector<U>(in U value) where U : struct
        {
            return ref Unsafe.As<U, Vector<U>>(ref Unsafe.AsRef(value));
        }

        private static ref U GetRef<U>(ReadOnlySpan<U> span) where U : struct
        {
            return ref MemoryMarshal.GetReference(span);
        }

        private static ref U GetRef<U>(Span<U> span) where U : struct
        {
            return ref MemoryMarshal.GetReference(span);
        }
    }

    internal static class SimdOps
    {
        internal static ref T Offset<T>(this ref T source, int count) where T : struct
        {
            return ref Unsafe.Add(ref source, count);
        }
    }
}
