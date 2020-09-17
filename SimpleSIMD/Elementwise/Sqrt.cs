using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Sqrt(in Span<T> span, in Span<T> result)
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int i;

            var vsSpan = AsVectors(span);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vsResult[i] = Vector.SquareRoot(vsSpan[i]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = NumOps<double, T>.Convert(Math.Sqrt(NumOps<T, double>.Convert(span[i])));
            }
        }

        public static T[] Sqrt(T[] array)
        {
            var result = new T[array.Length];

            Sqrt(array, result);

            return result;
        }
    }
}
