using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Min_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.Min(left, right);
            }
        }

        private struct Min_Selector : IFunc<T, T, T>
        {
            public T Invoke(T left, T right)
            {
                return NumOps<T>.Min(left, right);
            }
        }
        public static T Min(in ReadOnlySpan<T> span)
        {
            return Aggregate(span, NumOps<T>.MaxValue, new Min_VSelector(), new Min_Selector());
        }

        public static T Min<F1, F2>(in ReadOnlySpan<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            T min = NumOps<T>.MaxValue;

            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vMin = new Vector<T>(min);

                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vMin = Vector.Min(vMin, vSelector.Invoke(vrSpan.Offset(i)));
                }

                for (int j = 0; j < Vector<T>.Count; j++)
                {
                    min = NumOps<T>.Min(min, vMin[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                min = NumOps<T>.Min(min, selector.Invoke(rSpan.Offset(i)));
            }

            return min;
        }
    }
}
