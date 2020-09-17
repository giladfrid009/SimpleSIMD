using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Select<U>(in Span<T> span, Func<Vector<T>, Vector<U>> vSelector, Func<T, U> selector, in Span<U> result) where U : unmanaged
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }       

            if (Vector<U>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(U).Name);
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

        public static void Select<U>(in Span<T> span, Func<Vector<T>, int, Vector<U>> vSelector, Func<T, int, U> selector, in Span<U> result) where U : unmanaged
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            if (Vector<U>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(U).Name);
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

        public static U[] Select<U>(T[] array, Func<Vector<T>, Vector<U>> vSelector, Func<T, U> selector) where U : unmanaged
        {
            var result = new U[array.Length];

            Select(array, vSelector, selector, result);

            return result;
        }

        public static U[] Select<U>(T[] array, Func<Vector<T>, int, Vector<U>> vSelector, Func<T, int, U> selector) where U : unmanaged
        {
            var result = new U[array.Length];

            Select(array, vSelector, selector, result);

            return result;
        }
    }
}
