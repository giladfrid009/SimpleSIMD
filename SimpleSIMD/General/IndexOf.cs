using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static int IndexOf(T[] array, T value)
        {
            var vValue = new Vector<T>(value);
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                if (Vector.EqualsAny(vsArray[i], vValue))
                {
                    var vec = vsArray[i];

                    for (int j = 0; j < Vector<T>.Count; j++)
                    {
                        if (MathOps<T>.Equal(vec[j], value))
                        {
                            return i * Vector<T>.Count + j;
                        }
                    }
                }
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                if (MathOps<T>.Equal(array[i], value))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public static int IndexOf(T[] array, Func<Vector<T>, bool> vPredicate, Func<T, bool> predicate)
        {
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                if (vPredicate(vsArray[i]))
                {
                    var vec = vsArray[i];

                    for (int j = 0; j < Vector<T>.Count; j++)
                    {
                        if (predicate(vec[j]))
                        {
                            return i * Vector<T>.Count + j;
                        }
                    }
                }
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                if (predicate(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
