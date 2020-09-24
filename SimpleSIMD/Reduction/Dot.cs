using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Dot(in Span<T> left, T right)
        {        
            T dot = NumOps<T>.Zero;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vDot = Vector<T>.Zero;
                var vRight = new Vector<T>(right);
                var vsLeft = AsVectors(left);

                for (; i < vsLeft.Length; i++)
                {
                    vDot += vsLeft[i] * vRight;
                }

                dot = Vector.Dot(vDot, Vector<T>.One);

                i *= Vector<T>.Count;
            }

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

            T dot = NumOps<T>.Zero;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vDot = Vector<T>.Zero;
                var vsLeft = AsVectors(left);
                var vsRight = AsVectors(right);

                for (; i < vsLeft.Length; i++)
                {
                    vDot += vsLeft[i] * vsRight[i];
                }

                dot = Vector.Dot(vDot, Vector<T>.One);

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                dot = NumOps<T>.Add(dot, NumOps<T>.Multiply(left[i], right[i]));
            }

            return dot;
        }
    }
}
