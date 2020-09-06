using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static T Min(T[] array)
        {
            var vMin = new Vector<T>(MathOps<T>.MaxValue);
            T min = MathOps<T>.MaxValue;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vMin = Vector.Min(vMin, new Vector<T>(array, i));
            }

            for (int j = 0; j < vLen; ++j)
            {
                min = MathOps<T>.Min(min, vMin[j]);
            }

            for (; i < array.Length; i++)
            {
                min = MathOps<T>.Min(min, array[i]);
            }

            return min;
        }

        public static T Min(T[] array, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            var vMin = new Vector<T>(MathOps<T>.MaxValue);
            T min = MathOps<T>.MaxValue;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vMin = Vector.Min(vMin, vSelector(new Vector<T>(array, i)));
            }

            for (int j = 0; j < vLen; ++j)
            {
                min = MathOps<T>.Min(min, vMin[j]);
            }

            for (; i < array.Length; i++)
            {
                min = MathOps<T>.Min(min, selector(array[i]));
            }

            return min;
        }
    }
}
