using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Not(T[] array, T[] result)
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
                vsResult[i] = ~vsArray[i];
            }

            i *= Vector<T>.Count;

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
