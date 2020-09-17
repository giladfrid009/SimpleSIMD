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

        public static T Min(in Span<T> span, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            var vMin = new Vector<T>(NumOps<T>.MaxValue);
            T min = NumOps<T>.MaxValue;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vMin = Vector.Min(vMin, vSelector(vsSpan[i]));
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                min = NumOps<T>.Min(min, vMin[j]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                min = NumOps<T>.Min(min, selector(span[i]));
            }

            return min;
        }
    }
}
