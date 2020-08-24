using System;
using System.Numerics;

namespace SimpleSimd.Reduction
{
    public static partial class ReductionExt
    {
        public static T Avg<T>(this T[] source) where T : unmanaged
        {
            return Ops<T>.Div(source.Sum(), Ops<T>.FromInt(source.Length));
        }

        public static T Avg<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            return Ops<T>.Div(source.Sum(vSelector, selector), Ops<T>.FromInt(source.Length));
        }
    }
}
