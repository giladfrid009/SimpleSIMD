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
                return NumOps<double, T>.Convert(Math.Sqrt(NumOps<T, double>.Convert(val)));
            }
        }

        [ArrOverload]
        public static void Sqrt(ReadOnlySpan<T> span, Span<T> result)
        {
            Select(span, new Sqrt_VSelector(), new Sqrt_Selector(), result);
        }
    }
}
