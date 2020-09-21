using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Min(in Span<T> span)
        {
            var vMin = new Vector<T>(NumOps<T>.MaxValue);
            T min = NumOps<T>.MaxValue;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vMin = Vector.Min(vMin, vsSpan[i]);
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                min = NumOps<T>.Min(min, vMin[j]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                min = NumOps<T>.Min(min, span[i]);
            }

            return min;
        }

        public static T Min<F1, F2>(in Span<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            var vMin = new Vector<T>(NumOps<T>.MaxValue);
            T min = NumOps<T>.MaxValue;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vMin = Vector.Min(vMin, vSelector.Invoke(vsSpan[i]));
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                min = NumOps<T>.Min(min, vMin[j]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                min = NumOps<T>.Min(min, selector.Invoke(span[i]));
            }

            return min;
        }
    }
}
