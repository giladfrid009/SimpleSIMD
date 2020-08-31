using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Max<T>(this T[] source) where T : unmanaged
        {
            var vMax = new Vector<T>(NOperations<T>.MinVal);
            T max = NOperations<T>.MinVal;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vMax = Vector.Max(vMax, new Vector<T>(source, i));
            }

            for (int j = 0; j < vLen; ++j)
            {
                max = NOperations<T>.Max(max, vMax[j]);
            }

            for (; i < source.Length; i++)
            {
                max = NOperations<T>.Max(max, source[i]);
            }

            return max;
        }

        public static T Max<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            var vMax = new Vector<T>(NOperations<T>.MinVal);
            T max = NOperations<T>.MinVal;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vMax = Vector.Max(vMax, vSelector(new Vector<T>(source, i)));
            }

            for (int j = 0; j < vLen; ++j)
            {
                max = NOperations<T>.Max(max, vMax[j]);
            }

            for (; i < source.Length; i++)
            {
                max = NOperations<T>.Max(max, selector(source[i]));
            }

            return max;
        }
    }
}
