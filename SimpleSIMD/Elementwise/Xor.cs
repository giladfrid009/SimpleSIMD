using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Xor_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.Xor(left, right);
            }
        }

        private struct Xor_Selector : IFunc<T, T, T>
        {
            public T Invoke(T left, T right)
            {
                return left ^ right;
            }
        }

        public static void Xor(ReadOnlySpan<T> left, T right, Span<T> result)
        {
            Concat(left, right, new Xor_VSelector(), new Xor_Selector(), result);
        }

        public static void Xor(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result)
        {
            Concat(left, right, new Xor_VSelector(), new Xor_Selector(), result);
        }

        public static T[] Xor(ReadOnlySpan<T> left, T right)
        {
            T[] result = new T[left.Length];

            Xor(left, right, result);

            return result;
        }

        public static T[] Xor(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        {
            T[] result = new T[left.Length];

            Xor(left, right, result);

            return result;
        }
    }
}
