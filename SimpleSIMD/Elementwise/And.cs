﻿using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void And(in Span<T> left, T right, in Span<T> result)
        {
            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            var vRight = new Vector<T>(right);
            int i;

            var vsLeft = AsVectors(left);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsLeft.Length; i++)
            {
                vsResult[i] = Vector.BitwiseAnd(vsLeft[i], vRight);
            }

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                result[i] = NumOps<T>.And(left[i], right);
            }
        }

        public static void And(in Span<T> left, in Span<T> right, in Span<T> result)
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

            int i;

            var vsLeft = AsVectors(left);
            var vsRight = AsVectors(right);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsLeft.Length; i++)
            {
                vsResult[i] = Vector.BitwiseAnd(vsLeft[i], vsRight[i]);
            }

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                result[i] = NumOps<T>.And(left[i], right[i]);
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