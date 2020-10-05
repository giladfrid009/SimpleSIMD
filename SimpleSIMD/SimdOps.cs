using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SimpleSimd
{
    public static partial class SimdOps<T> where T : unmanaged
    {
        private static ref Vector<U> AsVector<U>(in U value) where U : unmanaged
        {
            return ref Unsafe.As<U, Vector<U>>(ref Unsafe.AsRef(value));
        }

        private static ref U GetRef<U>(ReadOnlySpan<U> span) where U : unmanaged
        {
            return ref MemoryMarshal.GetReference(span);
        }

        private static ref U Offset<U>(in U first, int count) where U : struct
        {
            return ref Unsafe.Add(ref Unsafe.AsRef(in first), count);
        }
    }
}
