using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Select<F1, F2, TRes>(in Span<T> span, F1 vSelector, F2 selector, in Span<TRes> result)

            where F1 : struct, IFunc<Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, TRes>
            where TRes : unmanaged

        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }       

            if (Vector<TRes>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(TRes).Name);
                return;
            }

            int i;

            var vsSpan = AsVectors(span);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vsResult[i] = vSelector.Invoke(vsSpan[i]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = selector.Invoke(span[i]);
            }
        }

        public static TRes[] Select<F1, F2, TRes>(T[] array, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, TRes>
            where TRes : unmanaged

        {
            var result = new TRes[array.Length];

            Select<F1, F2, TRes>(array, vSelector, selector, result);

            return result;
        }
    }
}
