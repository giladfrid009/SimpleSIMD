using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Select<TRes, F1, F2>(in Span<T> span, F1 vSelector, F2 selector, in Span<TRes> result)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, TRes>

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

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vsSpan = AsVectors(span);
                var vsResult = AsVectors(result);

                for (; i < vsSpan.Length; i++)
                {
                    vsResult[i] = vSelector.Invoke(vsSpan[i]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                result[i] = selector.Invoke(span[i]);
            }
        }

        public static TRes[] Select<TRes, F1, F2>(T[] array, F1 vSelector, F2 selector)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, TRes>

        {
            var result = new TRes[array.Length];

            Select<TRes, F1, F2>(array, vSelector, selector, result);

            return result;
        }
    }
}
