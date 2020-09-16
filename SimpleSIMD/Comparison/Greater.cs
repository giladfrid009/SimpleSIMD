using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool Greater(T[] left, T right)
        {
            var vVal = new Vector<T>(right);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                if (Vector.LessThanOrEqualAll(new Vector<T>(left, i), vVal))
                {
                    return false;
                }
            }

            for (; i < left.Length; i++)
            {
                if (MathOps<T>.LessOrEqual(left[i], right))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Greater(T[] left, T[] right)
        {
            if (right.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                if (Vector.LessThanOrEqualAll(new Vector<T>(left, i), new Vector<T>(right, i)))
                {
                    return false;
                }
            }

            for (; i < left.Length; i++)
            {
                if (MathOps<T>.LessOrEqual(left[i], right[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
