using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Max<T>(this T[] source) where T : unmanaged
        {
            var vMax = new Vector<T>(Operations<T>.MinVal);
            T max = Operations<T>.MinVal;

            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                vMax = Vector.Max(vMax, new Vector<T>(source, i));
            }

            for (int j = 0; j < vLength; ++j)
            {
                max = Operations<T>.Max(max, vMax[j]);
            }

            for (; i < source.Length; i++)
            {
                max = Operations<T>.Max(max, source[i]);
            }

            return max;
        }

        public static T Max<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            var vMax = new Vector<T>(Operations<T>.MinVal);
            T max = Operations<T>.MinVal;

            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                vMax = Vector.Max(vMax, vSelector(new Vector<T>(source, i)));
            }

            for (int j = 0; j < vLength; ++j)
            {
                max = Operations<T>.Max(max, vMax[j]);
            }

            for (; i < source.Length; i++)
            {
                max = Operations<T>.Max(max, selector(source[i]));
            }

            return max;
        }
    }
}
