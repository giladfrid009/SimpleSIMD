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
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vSum += vsArray[i];
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            i *= Vector<T>.Count;

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
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vSum += vSelector(vsArray[i]);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            i *= Vector<T>.Count;

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
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vSum += vSelector(vsArray[i], i * Vector<T>.Count);
            }

            sum = Vector.Dot(vSum, Vector<T>.One);

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                sum = MathOps<T>.Add(sum, selector(array[i], i));
            }

            return sum;
        }
    }
}