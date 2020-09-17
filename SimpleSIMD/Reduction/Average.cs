using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Average(in Span<T> span)
        {
            return NumOps<T>.Divide(Sum(span), NumOps<int, T>.Convert(span.Length));
        }

        public static T Average(in Span<T> span, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            return NumOps<T>.Divide(Sum(span, vSelector, selector), NumOps<int, T>.Convert(span.Length));
        }
    }
}
