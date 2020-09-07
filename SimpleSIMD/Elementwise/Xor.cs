using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Xor(T[] left, T right, T[] result)
        {
            if (result.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            var vVal = new Vector<T>(right);
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                Vector.Xor(new Vector<T>(left, i), vVal).CopyTo(result, i);
            }

            for (; i < left.Length; i++)
            {
                result[i] = MathOps<T>.Xor(left[i], right);
            }
        }

        public static void Xor(T[] left, T[] right, T[] result)
        {
            if (right.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            if (result.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                Vector.Xor(new Vector<T>(left, i), new Vector<T>(right, i)).CopyTo(result, i);
            }

            for (; i < left.Length; i++)
            {
                result[i] = MathOps<T>.Xor(left[i], right[i]);
            }
        }

        public static T[] Xor(T[] left, T right)
        {
            var result = new T[left.Length];

            Xor(left, right, result);

            return result;
        }

        public static T[] Xor(T[] left, T[] right)
        {
            var result = new T[left.Length];

            Xor(left, right, result);

            return result;
        }
    }
}
