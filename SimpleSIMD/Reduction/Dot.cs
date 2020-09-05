using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static T Dot(T[] left, T right)
        {
            var vVal = new Vector<T>(right);
            Vector<T> vDot = Vector<T>.Zero;
            T dot;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                vDot += new Vector<T>(left, i) * vVal;
            }

            dot = Vector.Dot(vDot, Vector<T>.One);

            for (; i < left.Length; i++)
            {
                dot = MathOps<T>.Add(dot, MathOps<T>.Multiply(left[i], right));
            }

            return dot;
        }

        public static T Dot(T[] left, T[] right)
        {
            if (right.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            Vector<T> vDot = Vector<T>.Zero;
            T dot;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                vDot += new Vector<T>(left, i) * new Vector<T>(right, i);
            }

            dot = Vector.Dot(vDot, Vector<T>.One);

            for (; i < left.Length; i++)
            {
                dot = MathOps<T>.Add(dot, MathOps<T>.Multiply(left[i], right[i]));
            }

            return dot;
        }
    }
}
