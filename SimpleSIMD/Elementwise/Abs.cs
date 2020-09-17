using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Abs(T[] array, T[] result)
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
                vsResult[i] = Vector.Abs(vsArray[i]);
            }

            i *= Vector<T>.Count;

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
