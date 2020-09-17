using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Sum(in Span<T> span)
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vSum += vsSpan[i];
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                sum = NumOps<T>.Add(sum, span[i]);
            }

            return sum;
        }

        public static T Sum(in Span<T> span, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vSum += vSelector(vsSpan[i]);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                sum = NumOps<T>.Add(sum, selector(span[i]));
            }

            return sum;
        }

        public static T Sum(in Span<T> span, Func<Vector<T>, int, Vector<T>> vSelector, Func<T, int, T> selector)
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vSum += vSelector(vsSpan[i], i * Vector<T>.Count);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                sum = NumOps<T>.Add(sum, selector(span[i], i));
            }

            return sum;
        }
    }
}