using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static T Sum(T[] array)
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vSum += new Vector<T>(array, i);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            for (; i < array.Length; i++)
            {
                sum = MathOps<T>.Add(sum, array[i]);
            }

            return sum;
        }

        public static T Sum(T[] array, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vSum += vSelector(new Vector<T>(array, i));
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            for (; i < array.Length; i++)
            {
                sum = MathOps<T>.Add(sum, selector(array[i]));
            }

            return sum;
        }

        public static T Sum(T[] array, Func<Vector<T>, int, Vector<T>> vSelector, Func<T, int, T> selector)
        {
            Vector<T> vSum = Vector<T>.Zero;
            T sum;
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vSum += vSelector(new Vector<T>(array, i), i);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            for (; i < array.Length; i++)
            {
                sum = MathOps<T>.Add(sum, selector(array[i], i));
            }

            return sum;
        }
    }
}