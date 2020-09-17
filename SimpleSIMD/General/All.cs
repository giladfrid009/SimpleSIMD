using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool All(T[] array, Func<Vector<T>, bool> vPredicate, Func<T, bool> predicate)
        {
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                if (vPredicate(vsArray[i]) == false)
                {
                    return false;
                }
            }

            i *= Vector<T>.Count;

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
