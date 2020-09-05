using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static int IndexOf(T[] array, T value)
        {
            var vVal = new Vector<T>(value);
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                if (Vector.EqualsAny(new Vector<T>(array, i), vVal))
                {
                    for (int j = i; j < i + vLen; j++)
                    {
                        if (MathOps<T>.Equals(array[j], value))
                        {
                            return j;
                        }
                    }
                }
            }

            for (; i < array.Length; i++)
            {
                if (MathOps<T>.Equals(array[i], value))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
