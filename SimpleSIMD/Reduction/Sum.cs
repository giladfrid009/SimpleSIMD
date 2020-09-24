using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Sum(in Span<T> span)
        {
            T sum = NumOps<T>.Zero;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vSum = Vector<T>.Zero;
                var vsSpan = AsVectors(span);

                for (; i < vsSpan.Length; i++)
                {
                    vSum += vsSpan[i];
                }

                sum = Vector.Dot(vSum, Vector<T>.One);

                i *= Vector<T>.Count;
            }

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
            T sum = NumOps<T>.Zero;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vSum = Vector<T>.Zero;
                var vsSpan = AsVectors(span);

                for (; i < vsSpan.Length; i++)
                {
                    vSum += vSelector.Invoke(vsSpan[i]);
                }

                sum = Vector.Dot(vSum, Vector<T>.One);

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                sum = NumOps<T>.Add(sum, selector.Invoke(span[i]));
            }

            return sum;
        }
    }
}