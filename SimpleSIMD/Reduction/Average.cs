using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Average<T>(this T[] source) where T : unmanaged
        {
            return Operations<T>.Divide(source.Sum(), Operations<int, T>.Convert(source.Length));
        }

        public static T Average<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            return Operations<T>.Divide(source.Sum(vSelector, selector), Operations<int, T>.Convert(source.Length));
        }
    }
}
