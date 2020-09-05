using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool Contains(T[] array, T value)
        {
            var vVal = new Vector<T>(value);
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                if (Vector.EqualsAny(new Vector<T>(array, i), vVal))
                {
                    return true;
                }
            }

            for (; i < array.Length; i++)
            {
                if (MathOps<T>.Equals(array[i], value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
