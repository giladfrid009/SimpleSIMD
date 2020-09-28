using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Min(in ReadOnlySpan<T> span)
        {
            T min = NumOps<T>.MaxValue;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vMin = new Vector<T>(min);
                var vsSpan = AsVectors(span);

                for (; i < vsSpan.Length; i++)
                {
                    vMin = Vector.Min(vMin, vsSpan[i]);
                }

                for (int j = 0; j < Vector<T>.Count; ++j)
                {
                    min = NumOps<T>.Min(min, vMin[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                min = NumOps<T>.Min(min, span[i]);
            }

            return min;
        }

        public static T Min<F1, F2>(in ReadOnlySpan<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            T min = NumOps<T>.MaxValue;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vMin = new Vector<T>(min);
                var vsSpan = AsVectors(span);

                for (; i < vsSpan.Length; i++)
                {
                    vMin = Vector.Min(vMin, vSelector.Invoke(vsSpan[i]));
                }

                for (int j = 0; j < Vector<T>.Count; ++j)
                {
                    min = NumOps<T>.Min(min, vMin[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                min = NumOps<T>.Min(min, selector.Invoke(span[i]));
            }

            return min;
        }
    }
}
