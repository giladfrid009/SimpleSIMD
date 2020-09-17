using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Sqrt(T[] array, T[] result)
        {
            int vLen = Vector<T>.Count;
            int i;

            if (result.Length != array.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                Vector.SquareRoot(new Vector<T>(array, i)).CopyTo(result, i);
            }

            for (; i < array.Length; i++)
            {
                result[i] = MathOps<double, T>.Convert(Math.Sqrt(MathOps<T, double>.Convert(array[i])));
            }
        }

        public static T[] Sqrt(T[] array)
        {
            var result = new T[array.Length];

            Sqrt(array, result);

            return result;
        }
    }
}
