using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Negate_VSelector : IFunc<Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> vec)
            {
                return Vector.Negate(vec);
            }
        }

        private struct Negate_Selector : IFunc<T, T>
        {
            public T Invoke(T val)
            {
                return NumOps<T>.Negate(val);
            }
        }

        [ArrOverload]
        public static void Negate(ReadOnlySpan<T> span, Span<T> result)
        {
            Select(span, new Negate_VSelector(), new Negate_Selector(), result);
        }
    }
}
