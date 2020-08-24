using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Neg<T>(this T[] source, T[] result) where T : unmanaged
        {
            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                Vector.Negate(new Vector<T>(source, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = Ops<T>.Neg(source[i]);
            }
        }

        public static T[] Neg<T>(this T[] source) where T : unmanaged
        {
            var result = new T[source.Length];

            result.Neg(result);

            return result;
        }
    }
}
