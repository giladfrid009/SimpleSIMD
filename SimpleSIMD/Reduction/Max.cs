using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Max(in ReadOnlySpan<T> span)
        {
            T max = NumOps<T>.MinValue;

            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vMax = new Vector<T>(max);

                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vMax = Vector.Max(vMax, Offset(vrSpan, i));
                }

                for (int j = 0; j < Vector<T>.Count; ++j)
                {
                    max = NumOps<T>.Max(max, vMax[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                max = NumOps<T>.Max(max, Offset(rSpan, i));
            }

            return max;
        }

        public static T Max<F1, F2>(in ReadOnlySpan<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            T max = NumOps<T>.MinValue;

            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vMax = new Vector<T>(max);

                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vMax = Vector.Max(vMax, vSelector.Invoke(Offset(vrSpan, i)));
                }

                for (int j = 0; j < Vector<T>.Count; ++j)
                {
                    max = NumOps<T>.Max(max, vMax[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                max = NumOps<T>.Max(max, selector.Invoke(Offset(rSpan, i)));
            }

            return max;
        }
    }
}
