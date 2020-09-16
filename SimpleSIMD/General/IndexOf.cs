using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static int IndexOf(T[] array, T value)
        {
            var vVal = new Vector<T>(value);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                if (Vector.EqualsAny(new Vector<T>(array, i), vVal))
                {
                    for (int j = i; j < i + vLen; j++)
                    {
                        if (MathOps<T>.Equal(array[j], value))
                        {
                            return j;
                        }
                    }
                }
            }

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
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                if (vPredicate(new Vector<T>(array, i)))
                {
                    for (int j = i; j < i + vLen; j++)
                    {
                        if (predicate(array[j]))
                        {
                            return j;
                        }
                    }
                }
            }

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
