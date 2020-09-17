using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Copy(T[] array, T[] result)
        {
            if (result.Length != array.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                new Vector<T>(array, i).CopyTo(result, i);
            }

            for (; i < array.Length; i++)
            {
                result[i] = array[i];
            }
        }

        public static T[] Copy(T[] array)
        {
            var result = new T[array.Length];

            Copy(array, result);

            return result;
        }
    }
}
