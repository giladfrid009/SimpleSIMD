using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static bool Greater<T>(this T[] source, T value) where T : unmanaged
        {
            var vVal = new Vector<T>(value);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                if (Vector.LessThanOrEqualAll(new Vector<T>(source, i), vVal))
                {
                    return false;
                }
            }

            for (; i < source.Length; i++)
            {
                if (NOperations<T>.LessEqual(source[i], value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Greater<T>(this T[] source, T[] other) where T : unmanaged
        {
            if (other.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                if (Vector.LessThanOrEqualAll(new Vector<T>(source, i), new Vector<T>(other, i)))
                {
                    return false;
                }
            }

            for (; i < source.Length; i++)
            {
                if (NOperations<T>.LessEqual(source[i], other[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
