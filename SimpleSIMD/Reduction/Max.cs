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

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vMax = Vector.Max(vMax, vsArray[i]);
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                max = MathOps<T>.Max(max, vMax[j]);
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                max = MathOps<T>.Max(max, array[i]);
            }

            return max;
        }

        public static T Max(T[] array, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)
        {
            var vMax = new Vector<T>(MathOps<T>.MinValue);
            T max = MathOps<T>.MinValue;
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vMax = Vector.Max(vMax, vSelector(vsArray[i]));
            }

            for (int j = 0; j < Vector<T>.Count; ++j)
            {
                max = MathOps<T>.Max(max, vMax[j]);
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                max = MathOps<T>.Max(max, selector(array[i]));
            }

            return max;
        }
    }
}
