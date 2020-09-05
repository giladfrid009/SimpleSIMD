using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Max<T>(this T[] source) where T : unmanaged
        {
            var vMax = new Vector<T>(Operations<T>.MinValue);
            T max = Operations<T>.MinValue;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vMax = Vector.Max(vMax, new Vector<T>(source, i));
            }

            for (int j = 0; j < vLen; ++j)
            {
                if (Operations<T>.Greater(vMax[j], max))
                {
                    max = vMax[j];
                }
            }

            for (; i < source.Length; i++)
            {
                if (Operations<T>.Greater(source[i], max))
                {
                    max = source[i];
                }
            }

            return max;
        }

        public static T Max<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            var vMax = new Vector<T>(Operations<T>.MinValue);
            T max = Operations<T>.MinValue;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vMax = Vector.Max(vMax, vSelector(new Vector<T>(source, i)));
            }

            for (int j = 0; j < vLen; ++j)
            {
                if (Operations<T>.Greater(vMax[j], max))
                {
                    max = vMax[j];
                }
            }

            for (; i < source.Length; i++)
            {
                T res = selector(source[i]);

                if (Operations<T>.Greater(res, max))
                {
                    max = res;
                }
            }

            return max;
        }
    }
}
