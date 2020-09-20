using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Select<TRes>(in Span<T> span, Func<Vector<T>, Vector<TRes>> vSelector, Func<T, TRes> selector, in Span<TRes> result) where TRes : unmanaged
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
                vsResult[i] = vSelector(vsSpan[i]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = selector(span[i]);
            }
        }

        public static void Select<TRes>(in Span<T> span, Func<Vector<T>, int, Vector<TRes>> vSelector, Func<T, int, TRes> selector, in Span<TRes> result) where TRes : unmanaged
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
                vsResult[i] = vSelector(vsSpan[i], i * Vector<T>.Count);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = selector(span[i], i);
            }
        }

        public static TRes[] Select<TRes>(T[] array, Func<Vector<T>, Vector<TRes>> vSelector, Func<T, TRes> selector) where TRes : unmanaged
        {
            var result = new TRes[array.Length];

            Select(array, vSelector, selector, result);

            return result;
        }

        public static TRes[] Select<TRes>(T[] array, Func<Vector<T>, int, Vector<TRes>> vSelector, Func<T, int, TRes> selector) where TRes : unmanaged
        {
            var result = new TRes[array.Length];

            Select(array, vSelector, selector, result);

            return result;
        }
    }
}
