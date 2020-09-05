using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Fill(T[] array, T value)
        {
            var vVal = new Vector<T>(value);
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vVal.CopyTo(array, i);
            }

            for (; i < array.Length; i++)
            {
                array[i] = value;
            }
        }

        public static void Fill(T[] array, Func<Vector<T>> vFunc, Func<T> func)
        {
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vFunc().CopyTo(array, i);
            }

            for (; i < array.Length; i++)
            {
                array[i] = func();
            }
        }
    }
}
