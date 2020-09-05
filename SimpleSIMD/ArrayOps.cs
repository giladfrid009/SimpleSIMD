using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T> where T : unmanaged
    {
        private static readonly int vLen = Vector<T>.Count;
    }
}
