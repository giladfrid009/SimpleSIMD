using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Avg<T>(this T[] source) where T : unmanaged
        {
            return NOperations<T>.Div(source.Sum(), NConverter<int, T>.Convert(source.Length));
        }

        public static T Avg<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            return NOperations<T>.Div(source.Sum(vSelector, selector), NConverter<int, T>.Convert(source.Length));
        }
    }
}
