using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Or_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.BitwiseOr(left, right);
            }
        }

        private struct Or_Selector : IFunc<T, T, T>
        {
            public T Invoke(T left, T right)
            {
                return left | right;
            }
        }

        public static void Or(in ReadOnlySpan<T> left, T right, in Span<T> result)
        {
            Concat(left, right, new Or_VSelector(), new Or_Selector(), result);
        }

        public static void Or(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, in Span<T> result)
        {
            Concat(left, right, new Or_VSelector(), new Or_Selector(), result);
        }

        public static T[] Or(in ReadOnlySpan<T> left, T right)
        {
            T[] result = new T[left.Length];

            Or(left, right, result);

            return result;
        }

        public static T[] Or(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right)
        {
            T[] result = new T[left.Length];

            Or(left, right, result);

            return result;
        }
    }
}
