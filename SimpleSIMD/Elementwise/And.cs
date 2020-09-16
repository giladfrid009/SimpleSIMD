using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void And(T[] left, T right, T[] result)
        {
            if (result.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            var vVal = new Vector<T>(right);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                Vector.BitwiseAnd(new Vector<T>(left, i), vVal).CopyTo(result, i);
            }

            for (; i < left.Length; i++)
            {
                result[i] = MathOps<T>.And(left[i], right);
            }
        }

        public static void And(T[] left, T[] right, T[] result)
        {
            if (right.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            if (result.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                Vector.BitwiseAnd(new Vector<T>(left, i), new Vector<T>(right, i)).CopyTo(result, i);
            }

            for (; i < left.Length; i++)
            {
                result[i] = MathOps<T>.And(left[i], right[i]);
            }
        }

        public static T[] And(T[] left, T right)
        {
            var result = new T[left.Length];

            And(left, right, result);

            return result;
        }

        public static T[] And(T[] left, T[] right)
        {
            var result = new T[left.Length];

            And(left, right, result);

            return result;
        }
    }
}
