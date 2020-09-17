using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Fill(T[] array, T value)
        {
            var vValue = new Vector<T>(value);
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vsArray[i] = vValue;
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                array[i] = value;
            }
        }

        public static void Fill(T[] array, Func<Vector<T>> vFunc, Func<T> func)
        {
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vsArray[i] = vFunc();
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                array[i] = func();
            }
        }
    }
}
