using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Not(T[] array, T[] result)
        {
            if (result.Length != array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                (~new Vector<T>(array, i)).CopyTo(result, i);
            }

            for (; i < array.Length; i++)
            {
                result[i] = MathOps<T>.Not(array[i]);
            }
        }

        public static T[] Not(T[] array)
        {
            var result = new T[array.Length];

            Not(array, result);

            return result;
        }
    }
}
