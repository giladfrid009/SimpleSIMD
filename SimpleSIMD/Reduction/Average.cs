using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static T Average(T[] array)
        {
            return MathOps<T>.Divide(Sum(array), MathOps<int, T>.Convert(array.Length));
        }

        public static T Average(T[] array, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            return MathOps<T>.Divide(Sum(array, vSelector, selector), MathOps<int, T>.Convert(array.Length));
        }
    }
}
