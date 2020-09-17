using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Negate(T[] array, T[] result)
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
                vsResult[i] = Vector.Negate(vsArray[i]);
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                result[i] = MathOps<T>.Negate(array[i]);
            }
        }

        public static T[] Negate(T[] array)
        {
            var result = new T[array.Length];

            Negate(array, result);

            return result;
        }
    }
}
