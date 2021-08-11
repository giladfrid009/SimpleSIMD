using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Sqrt_VSelector : IFunc<Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> vec)
            {
                return Vector.SquareRoot(vec);
            }
        }

        private struct Sqrt_Selector : IFunc<T, T>
        {
            public T Invoke(T val)
            {
                return T.Create(Math.Sqrt(Convert<double>(val)));
            }
        }

        public static void Sqrt(in ReadOnlySpan<T> span, in Span<T> result)
        {
            Select(span, new Sqrt_VSelector(), new Sqrt_Selector(), result);
        }

        public static T[] Sqrt(in ReadOnlySpan<T> span)
        {
            T[] result = new T[span.Length];

            Sqrt(span, result);

            return result;
        }
    }
}
