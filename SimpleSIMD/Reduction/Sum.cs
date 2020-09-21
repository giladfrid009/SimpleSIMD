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

        
        public static T Sum<F1, F2>(in Span<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vSum += vSelector.Invoke(vsSpan[i]);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                sum = NumOps<T>.Add(sum, selector.Invoke(span[i]));
            }

            return sum;
        }
    }
}