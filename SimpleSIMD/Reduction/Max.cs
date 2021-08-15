using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Max(ReadOnlySpan<T> span)
        {
            return Max(span, new ID_VSelector(), new ID_Selector());
        }

        public static T Max<F1, F2>(ReadOnlySpan<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            T max = T.MinValue;

            ref T rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                Vector<T> vMax = new(max);

                ref Vector<T> vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vMax = Vector.Max(vMax, vSelector.Invoke(vrSpan.Offset(i)));
                }

                for (int j = 0; j < Vector<T>.Count; j++)
                {
                    max = T.Max(max, vMax[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                max = T.Max(max, selector.Invoke(rSpan.Offset(i)));
            }

            return max;
        }
    }
}
