using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool All(T[] array, Func<Vector<T>, bool> vPredicate, Func<T, bool> predicate)
        {
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                if (vPredicate(new Vector<T>(array, i)) == false)
                {
                    return false;
                }
            }

            for (; i < array.Length; i++)
            {
                if (predicate(array[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
