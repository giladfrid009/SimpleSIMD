using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct And_VSelector : IFunc<Vector<T>, Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.BitwiseAnd(left, right);
            }
        }

        private struct And_Selector : IFunc<T, T, T>
        {
            public T Invoke(T left, T right)
            {
                return NumOps<T>.And(left, right);
            }
        }

        public static void And(ReadOnlySpan<T> left, T right, Span<T> result)
        {
            Concat(left, right, new And_VSelector(), new And_Selector(), result);
        }

        public static void And(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result)
        {
            Concat(left, right, new And_VSelector(), new And_Selector(), result);
        }       

        public static T[] And(ReadOnlySpan<T> left, T right)
        {
            T[] result = new T[left.Length];

            And(left, right, result);

            return result;
        }

        public static T[] And(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        {
            T[] result = new T[left.Length];

            And(left, right, result);

            return result;
        }
    }
}
