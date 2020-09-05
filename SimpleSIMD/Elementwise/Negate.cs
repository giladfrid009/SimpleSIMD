using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Negate(T[] array, T[] result)
        {
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                Vector.Negate(new Vector<T>(array, i)).CopyTo(result, i);
            }

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
