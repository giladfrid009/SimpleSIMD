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
                return left * right;
            }
        }

        [ArrOverload]
        public static void Multiply(ReadOnlySpan<T> left, T right, Span<T> result)
        {
            Concat(left, right, new Multiply_VSelector(), new Multiply_Selector(), result);
        }

        [ArrOverload]
        public static void Multiply(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> result)
        {
            Concat(left, right, new Multiply_VSelector(), new Multiply_Selector(), result);
        }
    }
}
