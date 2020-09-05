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
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vMax = Vector.Max(vMax, new Vector<T>(array, i));
            }

            for (int j = 0; j < vLen; ++j)
            {
                if (MathOps<T>.Greater(vMax[j], max))
                {
                    max = vMax[j];
                }
            }

            for (; i < array.Length; i++)
            {
                if (MathOps<T>.Greater(array[i], max))
                {
                    max = array[i];
                }
            }

            return max;
        }

        public static T Max(T[] array, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            var vMax = new Vector<T>(MathOps<T>.MinValue);
            T max = MathOps<T>.MinValue;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vMax = Vector.Max(vMax, vSelector(new Vector<T>(array, i)));
            }

            for (int j = 0; j < vLen; ++j)
            {
                if (MathOps<T>.Greater(vMax[j], max))
                {
                    max = vMax[j];
                }
            }

            for (; i < array.Length; i++)
            {
                T res = selector(array[i]);

                if (MathOps<T>.Greater(res, max))
                {
                    max = res;
                }
            }

            return max;
        }
    }
}
