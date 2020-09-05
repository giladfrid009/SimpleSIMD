using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool Any(T[] array, Func<Vector<T>, bool> vPredicate, Func<T, bool> predicate)
        {
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                if (vPredicate(new Vector<T>(array, i)))
                {
                    return true;
                }
            }

            for (; i < array.Length; i++)
            {
                if (predicate(array[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
