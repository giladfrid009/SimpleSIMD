using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Sqrt<T>(this T[] source, T[] result) where T : unmanaged
        {
            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                Vector.SquareRoot(new Vector<T>(source, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                double sqrt = Math.Sqrt(Converter<T, double>.Convert(source[i]));
                result[i] = Converter<double, T>.Convert(sqrt);
            }
        }

        public static T[] Sqrt<T>(this T[] source) where T : unmanaged
        {
            var result = new T[source.Length];

            result.Abs(result);

            return result;
        }
    }
}
