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

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vMin = Vector.Min(vMin, vsArray[i]);
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                min = MathOps<T>.Min(min, vMin[j]);
            }

            i *= Vector<T>.Count;

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

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vMin = Vector.Min(vMin, vSelector(vsArray[i]));
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                min = MathOps<T>.Min(min, vMin[j]);
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                min = MathOps<T>.Min(min, selector(array[i]));
            }

            return min;
        }
    }
}
