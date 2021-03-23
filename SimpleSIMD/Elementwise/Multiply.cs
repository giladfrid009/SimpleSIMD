﻿using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Multiply_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.Multiply(left, right);
            }
        }

        private struct Multiply_Selector : IFunc<T, T, T>
        {
            public T Invoke(T left, T right)
            {
                return NumOps<T>.Multiply(left, right);
            }
        }

        public static void Multiply(in ReadOnlySpan<T> left, T right, in Span<T> result)
        {
            Concat(left, right, new Multiply_VSelector(), new Multiply_Selector(), result);
        }

        public static void Multiply(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, in Span<T> result)
        {
            Concat(left, right, new Multiply_VSelector(), new Multiply_Selector(), result);
        }

        public static T[] Multiply(T[] left, T right)
        {
            var result = new T[left.Length];

            Multiply(left, right, result);

            return result;
        }

        public static T[] Multiply(T[] left, T[] right)
        {
            var result = new T[left.Length];

            Multiply(left, right, result);

            return result;
        }
    }
}
