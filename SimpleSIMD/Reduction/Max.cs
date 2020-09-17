using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Max(in Span<T> span)
        {
            var vMax = new Vector<T>(NumOps<T>.MinValue);
            T max = NumOps<T>.MinValue;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vMax = Vector.Max(vMax, vsSpan[i]);
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                max = NumOps<T>.Max(max, vMax[j]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                max = NumOps<T>.Max(max, span[i]);
            }

            return max;
        }

        public static T Max(in Span<T> span, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            var vMax = new Vector<T>(NumOps<T>.MinValue);
            T max = NumOps<T>.MinValue;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vMax = Vector.Max(vMax, vSelector(vsSpan[i]));
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                max = NumOps<T>.Max(max, vMax[j]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                max = NumOps<T>.Max(max, selector(span[i]));
            }

            return max;
        }
    }
}
