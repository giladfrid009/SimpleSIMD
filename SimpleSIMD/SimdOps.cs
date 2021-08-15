using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SimpleSimd
{
    public static partial class SimdOps<T> where T : unmanaged, IBinaryNumber<T>, IMinMaxValue<T>
    {
        private static ref Vector<U> AsVector<U>(in U value) where U : unmanaged, IBinaryNumber<U>
        {
            return ref Unsafe.As<U, Vector<U>>(ref Unsafe.AsRef(value));
        }

        private static ref U GetRef<U>(ReadOnlySpan<U> span) where U : unmanaged, IBinaryNumber<U>
        {
            return ref MemoryMarshal.GetReference(span);
        }

        private static ref U GetRef<U>(Span<U> span) where U : unmanaged, IBinaryNumber<U>
        {
            return ref MemoryMarshal.GetReference(span);
        }

        private static U Convert<U>(T value) where U : unmanaged, IBinaryNumber<U>
        {
            return U.Create(value);
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
