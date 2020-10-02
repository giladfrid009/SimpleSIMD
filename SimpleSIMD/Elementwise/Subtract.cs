using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Subtract(in ReadOnlySpan<T> left, T right, in Span<T> result)
        {
            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vRight = new Vector<T>(right);
                var vsLeft = AsVectors(left);
                var vsResult = AsVectors(result);

                for (; i < vsLeft.Length; i++)
                {
                    vsResult[i] = Vector.Subtract(vsLeft[i], vRight);
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                result[i] = NumOps<T>.Subtract(left[i], right);
            }
        }

        public static void Subtract(T left, in ReadOnlySpan<T> right, in Span<T> result)
        {
            if (result.Length != right.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vLeft = new Vector<T>(right);
                var vsRight = AsVectors(right);
                var vsResult = AsVectors(result);

                for (; i < vsRight.Length; i++)
                {
                    vsResult[i] = Vector.Subtract(vLeft, vsRight[i]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < right.Length; i++)
            {
                result[i] = NumOps<T>.Subtract(left, right[i]);
            }
        }

        public static void Subtract(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, in Span<T> result)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
            }

            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vsLeft = AsVectors(left);
                var vsRight = AsVectors(right);
                var vsResult = AsVectors(result);

                for (; i < vsLeft.Length; i++)
                {
                    vsResult[i] = Vector.Subtract(vsLeft[i], vsRight[i]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                result[i] = NumOps<T>.Subtract(left[i], right[i]);
            }
        }

        public static T[] Subtract(T[] left, T right)
        {
            var result = new T[left.Length];

            Subtract(left, right, result);

            return result;
        }

        public static T[] Subtract(T left, T[] right)
        {
            var result = new T[right.Length];

            Subtract(left, right, result);

            return result;
        }

        public static T[] Subtract(T[] left, T[] right)
        {
            var result = new T[left.Length];

            Subtract(left, right, result);

            return result;
        }
    }
}
