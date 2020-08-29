using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Neg<T>(this T[] source, T[] result) where T : unmanaged
        {
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                Vector.Negate(new Vector<T>(source, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = Operations<T>.Neg(source[i]);
            }
        }

        public static T[] Neg<T>(this T[] source) where T : unmanaged
        {
            var result = new T[source.Length];

            source.Neg(result);

            return result;
        }
    }
}
