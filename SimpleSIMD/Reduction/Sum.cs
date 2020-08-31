using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Sum<T>(this T[] source) where T : unmanaged
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vSum += new Vector<T>(source, i);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            for (; i < source.Length; i++)
            {
                sum = NOperations<T>.Add(sum, source[i]);
            }

            return sum;
        }

        public static T Sum<T>(this T[] source, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) where T : unmanaged
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vSum += vSelector(new Vector<T>(source, i));
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            for (; i < source.Length; i++)
            {
                sum = NOperations<T>.Add(sum, selector(source[i]));
            }

            return sum;
        }

        public static T Sum<T>(this T[] source, Func<Vector<T>, int, Vector<T>> vSelector, Func<T, int, T> selector) where T : unmanaged
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vSum += vSelector(new Vector<T>(source, i), i);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            for (; i < source.Length; i++)
            {
                sum = NOperations<T>.Add(sum, selector(source[i], i));
            }

            return sum;
        }
    }
}