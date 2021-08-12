using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Add_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.Add(left, right);
            }
        }

        private struct Add_Selector : IFunc<T, T, T>
        {
            public T Invoke(T left, T right)
            {
                return left + right;
            }
        }

        public static void Add(ReadOnlySpan<T> left, T right, Span<T> result)
        {
            Concat(left, right, new Add_VSelector(), new Add_Selector(), result);
        }

        public static void Add(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result)
        {
            Concat(left, right, new Add_VSelector(), new Add_Selector(), result);
        }

        public static T[] Add(ReadOnlySpan<T> left, T right)
        {
            T[] result = new T[left.Length];

            Add(left, right, result);

            return result;
        }

        public static T[] Add(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        {
            T[] result = new T[left.Length];

            Add(left, right, result);

            return result;
        }
    }
}
