using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Avg<T>(this T[] source) where T : unmanaged
        {
            return Operations<T>.Div(source.Sum(), Operations<T>.FromInt(source.Length));
        }

        public static T Avg<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            return Operations<T>.Div(source.Sum(vSelector, selector), Operations<T>.FromInt(source.Length));
        }
    }
}
