using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Dot(in Span<T> left, T right)
        {
            var vRight = new Vector<T>(right);
            Vector<T> vDot = Vector<T>.Zero;
            T dot;
            int i;

            var vsLeft = AsVectors(left);

            for (i = 0; i < vsLeft.Length; i++)
            {
                vDot += vsLeft[i] * vRight;
            }

            dot = Vector.Dot(vDot, Vector<T>.One);

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                dot = NumOps<T>.Add(dot, NumOps<T>.Multiply(left[i], right));
            }

            return dot;
        }

        public static T Dot(in Span<T> left, in Span<T> right)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
                return default;
            }

            Vector<T> vDot = Vector<T>.Zero;
            T dot;
            int i;

            var vsLeft = AsVectors(left);
            var vsRight = AsVectors(right);

            for (i = 0; i < vsLeft.Length; i++)
            {
                vDot += vsLeft[i] * vsRight[i];
            }

            dot = Vector.Dot(vDot, Vector<T>.One);

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                dot = NumOps<T>.Add(dot, NumOps<T>.Multiply(left[i], right[i]));
            }

            return dot;
        }
    }
}
