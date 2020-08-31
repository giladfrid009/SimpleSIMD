using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Min<T>(this T[] source) where T : unmanaged
        {
            var vMin = new Vector<T>(NOperations<T>.MaxVal);
            T min = NOperations<T>.MaxVal;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vMin = Vector.Min(vMin, new Vector<T>(source, i));
            }

            for (int j = 0; j < vLen; ++j)
            {
                min = NOperations<T>.Min(min, vMin[j]);
            }

            for (; i < source.Length; i++)
            {
                min = NOperations<T>.Min(min, source[i]);
            }

            return min;
        }

        public static T Min<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            var vMin = new Vector<T>(NOperations<T>.MaxVal);
            T min = NOperations<T>.MaxVal;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vMin = Vector.Min(vMin, vSelector(new Vector<T>(source, i)));
            }

            for (int j = 0; j < vLen; ++j)
            {
                min = NOperations<T>.Min(min, vMin[j]);
            }

            for (; i < source.Length; i++)
            {
                min = NOperations<T>.Min(min, selector(source[i]));
            }

            return min;
        }
    }
}
