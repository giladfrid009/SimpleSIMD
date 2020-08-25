using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Mul<T>(this T[] source, T value, T[] result) where T : unmanaged
        {
            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            var vVal = new Vector<T>(value);
            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                Vector.Multiply(new Vector<T>(source, i), vVal).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = Operations<T>.Mul(source[i], value);
            }
        }

        public static void Mul<T>(this T[] source, T[] other, T[] result) where T : unmanaged
        {
            if (other.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(other));
            }

            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                Vector.Multiply(new Vector<T>(source, i), new Vector<T>(other, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = Operations<T>.Mul(source[i], other[i]);
            }
        }

        public static T[] Mul<T>(this T[] source, T value) where T : unmanaged
        {
            var result = new T[source.Length];

            result.Mul(value, result);

            return result;
        }

        public static T[] Mul<T>(this T[] source, T[] other) where T : unmanaged
        {
            var result = new T[source.Length];

            result.Mul(other, result);

            return result;
        }
    }
}
