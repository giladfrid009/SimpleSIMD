using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Not(in ReadOnlySpan<T> span, in ReadOnlySpan<T> result)
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            ref var rSpan = ref GetRef(span);
            ref var rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrSpan = ref AsVector(rSpan);
                ref var vrResult = ref AsVector(rResult);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    Offset(vrResult, i) = ~Offset(vrSpan, i);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                Offset(rResult, i) = NumOps<T>.Not(Offset(rSpan, i));
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
