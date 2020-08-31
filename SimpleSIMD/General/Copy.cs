using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Copy<T>(this T[] source, T[] result) where T : unmanaged
        {
            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                new Vector<T>(source, i).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = source[i];
            }
        }

        public static T[] Copy<T>(this T[] source) where T : unmanaged
        {
            var result = new T[source.Length];

            source.Copy(result);

            return result;
        }
    }
}
