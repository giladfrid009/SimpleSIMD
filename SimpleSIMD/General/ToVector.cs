using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static Vector<T> ToVector<T>(this T[] source, int index) where T : unmanaged
        {
            return new Vector<T>(source, index);
        }
    }
}
