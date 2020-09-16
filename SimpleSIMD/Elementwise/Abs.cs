using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Abs(T[] array, T[] result)
        {
            if (result.Length != array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                Vector.Abs(new Vector<T>(array, i)).CopyTo(result, i);
            }

            for (; i < array.Length; i++)
            {
                result[i] = MathOps<T>.Abs(array[i]);
            }
        }

        public static T[] Abs(T[] array)
        {
            var result = new T[array.Length];

            Abs(array, result);

            return result;
        }
    }
}
