using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Sqrt(T[] array, T[] result)
        {
            if (result.Length != array.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int i;

            var vsArray = AsVectors(array);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsArray.Length; i++)
            {
                vsResult[i] = Vector.SquareRoot(vsArray[i]);
            }

            i *= Vector<T>.Count;

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
