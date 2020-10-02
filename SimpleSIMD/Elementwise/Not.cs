using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Not(in ReadOnlySpan<T> span, in Span<T> result)
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vsSpan = AsVectors(span);
                var vsResult = AsVectors(result);

                for (; i < vsSpan.Length; i++)
                {
                    vsResult[i] = ~vsSpan[i];
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                result[i] = NumOps<T>.Not(span[i]);
            }
        }

        public static T[] Not(T[] array)
        {
            var result = new T[array.Length];

            Not(array, result);

            return result;
        }
    }
}
