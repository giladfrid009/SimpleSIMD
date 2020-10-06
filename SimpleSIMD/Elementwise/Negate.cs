using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Negate(in ReadOnlySpan<T> span, in ReadOnlySpan<T> result)
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
                    vrResult.Offset(i) = Vector.Negate(vrSpan.Offset(i));
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                rResult.Offset(i) = NumOps<T>.Negate(rSpan.Offset(i));
            }
        }

        public static T[] Negate(T[] array)
        {
            var result = new T[array.Length];

            Negate(array, result);

            return result;
        }
    }
}
