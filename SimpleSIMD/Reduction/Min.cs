using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Min(in ReadOnlySpan<T> span)
        {
            return Min(span, new ID_VSelector(), new ID_Selector());
        }

        public static T Min<F1, F2>(in ReadOnlySpan<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            T min = T.MaxValue;

            ref T rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                Vector<T> vMin = new(min);

                ref Vector<T> vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vMin = Vector.Min(vMin, vSelector.Invoke(vrSpan.Offset(i)));
                }

                for (int j = 0; j < Vector<T>.Count; j++)
                {
                    min = T.Min(min, vMin[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                min = T.Min(min, selector.Invoke(rSpan.Offset(i)));
            }

            return min;
        }
    }
}
