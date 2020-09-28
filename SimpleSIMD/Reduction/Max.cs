using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Max(in ReadOnlySpan<T> span)
        {
            T max = NumOps<T>.MinValue;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vMax = new Vector<T>(max);
                var vsSpan = AsVectors(span);

                for (; i < vsSpan.Length; i++)
                {
                    vMax = Vector.Max(vMax, vsSpan[i]);
                }

                for (int j = 0; j < Vector<T>.Count; ++j)
                {
                    max = NumOps<T>.Max(max, vMax[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                max = NumOps<T>.Max(max, span[i]);
            }

            return max;
        }

        public static T Max<F1, F2>(in ReadOnlySpan<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            T max = NumOps<T>.MinValue;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vMax = new Vector<T>(max);
                var vsSpan = AsVectors(span);

                for (; i < vsSpan.Length; i++)
                {
                    vMax = Vector.Max(vMax, vSelector.Invoke(vsSpan[i]));
                }

                for (int j = 0; j < Vector<T>.Count; ++j)
                {
                    max = NumOps<T>.Max(max, vMax[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                max = NumOps<T>.Max(max, selector.Invoke(span[i]));
            }

            return max;
        }
    }
}
