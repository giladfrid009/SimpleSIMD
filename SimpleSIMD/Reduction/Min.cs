using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Min<T>(this T[] source) where T : unmanaged
        {
            var vMin = new Vector<T>(Operations<T>.MaxValue);
            T min = Operations<T>.MaxValue;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vMin = Vector.Min(vMin, new Vector<T>(source, i));
            }

            for (int j = 0; j < vLen; ++j)
            {
                if (Operations<T>.Less(vMin[j], min))
                {
                    min = vMin[j];
                }
            }

            for (; i < source.Length; i++)
            {
                if (Operations<T>.Less(source[i], min))
                {
                    min = source[i];
                }
            }

            return min;
        }

        public static T Min<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            var vMin = new Vector<T>(Operations<T>.MaxValue);
            T min = Operations<T>.MaxValue;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vMin = Vector.Min(vMin, vSelector(new Vector<T>(source, i)));
            }

            for (int j = 0; j < vLen; ++j)
            {
                if (Operations<T>.Less(vMin[j], min))
                {
                    min = vMin[j];
                }
            }

            for (; i < source.Length; i++)
            {
                T res = selector(source[i]);

                if (Operations<T>.Less(res, min))
                {
                    min = res;
                }
            }

            return min;
        }
    }
}
