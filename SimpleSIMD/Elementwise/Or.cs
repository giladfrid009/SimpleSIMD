using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Or(in Span<T> left, T right, in Span<T> result)
        {
            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vRight = new Vector<T>(right);
                var vsLeft = AsVectors(left);
                var vsResult = AsVectors(result);

                for (; i < vsLeft.Length; i++)
                {
                    vsResult[i] = Vector.BitwiseOr(vsLeft[i], vRight);
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                result[i] = NumOps<T>.Or(left[i], right);
            }
        }

        public static void Or(in Span<T> left, in Span<T> right, in Span<T> result)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
                return;
            }

            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vsLeft = AsVectors(left);
                var vsRight = AsVectors(right);
                var vsResult = AsVectors(result);

                for (; i < vsLeft.Length; i++)
                {
                    vsResult[i] = Vector.BitwiseOr(vsLeft[i], vsRight[i]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                result[i] = NumOps<T>.Or(left[i], right[i]);
            }
        }

        public static T[] Or(T[] left, T right)
        {
            var result = new T[left.Length];

            Or(left, right, result);

            return result;
        }

        public static T[] Or(T[] left, T[] right)
        {
            var result = new T[left.Length];

            Or(left, right, result);

            return result;
        }
    }
}
