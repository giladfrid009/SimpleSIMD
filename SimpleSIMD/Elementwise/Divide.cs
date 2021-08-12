using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Divide_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.Divide(left, right);
            }
        }

        private struct Divide_Selector : IFunc<T, T, T>
        {
            public T Invoke(T left, T right)
            {
                return left / right;
            }
        }

        public static void Divide(ReadOnlySpan<T> left, T right, Span<T> result)
        {
            Concat(left, right, new Divide_VSelector(), new Divide_Selector(), result);
        }

        public static void Divide(T left, ReadOnlySpan<T> right, Span<T> result)
        {
            Concat(left, right, new Divide_VSelector(), new Divide_Selector(), result);
        }

        public static void Divide(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result)
        {
            Concat(left, right, new Divide_VSelector(), new Divide_Selector(), result);
        }

        public static T[] Divide(ReadOnlySpan<T> left, T right)
        {
            T[] result = new T[left.Length];

            Divide(left, right, result);

            return result;
        }

        public static T[] Divide(T left, ReadOnlySpan<T> right)
        {
            T[] result = new T[right.Length];

            Divide(left, right, result);

            return result;
        }

        public static T[] Divide(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        {
            T[] result = new T[left.Length];

            Divide(left, right, result);

            return result;
        }
    }
}
