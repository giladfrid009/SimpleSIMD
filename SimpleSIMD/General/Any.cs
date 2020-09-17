using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool Any(T[] array, Func<Vector<T>, bool> vPredicate, Func<T, bool> predicate)
        {
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                if (vPredicate(vsArray[i]))
                {
                    return true;
                }
            }

            i *= Vector<T>.Count;

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
