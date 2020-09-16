using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static T Max(T[] array)
        {
            var vMax = new Vector<T>(MathOps<T>.MinValue);
            T max = MathOps<T>.MinValue;
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vMax = Vector.Max(vMax, new Vector<T>(array, i));
            }

            for (int j = 0; j < vLen; ++j)
            {
                max = MathOps<T>.Max(max, vMax[j]);
            }

            for (; i < array.Length; i++)
            {
                max = MathOps<T>.Max(max, array[i]);
            }

            return max;
        }

        public static T Max(T[] array, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            var vMax = new Vector<T>(MathOps<T>.MinValue);
            T max = MathOps<T>.MinValue;
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vMax = Vector.Max(vMax, vSelector(new Vector<T>(array, i)));
            }

            for (int j = 0; j < vLen; ++j)
            {
                max = MathOps<T>.Max(max, vMax[j]);
            }

            for (; i < array.Length; i++)
            {
                max = MathOps<T>.Max(max, selector(array[i]));
            }

            return max;
        }
    }
}
