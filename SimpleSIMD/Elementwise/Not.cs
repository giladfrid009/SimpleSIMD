using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Not_VSelector : IFunc<Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> vec)
            {
                return ~vec;
            }
        }

        private struct Not_Selector : IFunc<T, T>
        {
            public T Invoke(T val)
            {
                return ~val;
            }
        }

        public static void Not(ReadOnlySpan<T> span, Span<T> result)
        {
            Select(span, new Not_VSelector(), new Not_Selector(), result);
        }

        public static T[] Not(ReadOnlySpan<T> span)
        {
            T[] result = new T[span.Length];

            Not(span, result);

            return result;
        }
    }
}
