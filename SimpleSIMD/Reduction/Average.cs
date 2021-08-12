using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Average(ReadOnlySpan<T> span)
        {
            return Average(span, new ID_VSelector(), new ID_Selector());
        }

        public static T Average<F1, F2>(ReadOnlySpan<T> span, F1 vSelector, F2 selector)
            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>
        {
            return NumOps<T>.Divide(Sum(span, vSelector, selector), NumOps<int, T>.Convert(span.Length));
        }
    }
}
