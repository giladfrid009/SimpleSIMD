using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Abs_VSelector : IFunc<Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> vec)
            {
                return Vector.Abs(vec);
            }
        }

        private struct Abs_Selector : IFunc<T, T>
        {
            public T Invoke(T val)
            {
                return T.Abs(val);
            }
        }

        public static void Abs(ReadOnlySpan<T> span, Span<T> result)
        {
            Select(span, new Abs_VSelector(), new Abs_Selector(), result);
        }

        public static T[] Abs(ReadOnlySpan<T> span)
        {
            T[] result = new T[span.Length];

            Abs(span, result);

            return result;
        }
    }
}
