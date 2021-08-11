using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Subtract_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.Divide(left, right);
            }
        }

        private struct Subtract_Selector : IFunc<T, T, T>
        {
            public T Invoke(T left, T right)
            {
                return left / right;
            }
        }

        public static void Subtract(in ReadOnlySpan<T> left, T right, in Span<T> result)
        {
            Concat(left, right, new Subtract_VSelector(), new Subtract_Selector(), result);
        }

        public static void Subtract(T left, in ReadOnlySpan<T> right, in Span<T> result)
        {
            Concat(left, right, new Subtract_VSelector(), new Subtract_Selector(), result);
        }

        public static void Subtract(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, in Span<T> result)
        {
            Concat(left, right, new Subtract_VSelector(), new Subtract_Selector(), result);
        }

        public static T[] Subtract(in ReadOnlySpan<T> left, T right)
        {
            T[] result = new T[left.Length];

            Subtract(left, right, result);

            return result;
        }

        public static T[] Subtract(T left, in ReadOnlySpan<T> right)
        {
            T[] result = new T[right.Length];

            Subtract(left, right, result);

            return result;
        }

        public static T[] Subtract(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right)
        {
            T[] result = new T[left.Length];

            Subtract(left, right, result);

            return result;
        }
    }
}
