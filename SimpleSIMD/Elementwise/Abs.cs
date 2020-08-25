using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Abs<T>(this T[] source, T[] result) where T : unmanaged
        {
            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                Vector.Abs(new Vector<T>(source, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = Operations<T>.Abs(source[i]);
            }
        }

        public static T[] Abs<T>(this T[] source) where T : unmanaged
        {
            var result = new T[source.Length];

            result.Abs(result);

            return result;
        }
    }
}
