using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        [MethodImpl(MaxOpt)]
        public static T Sum(in ReadOnlySpan<T> span)
        {
            T sum = NumOps<T>.Zero;

            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vSum = Vector<T>.Zero;

                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vSum = Vector.Add(vSum, vrSpan.Offset(i));
                }

                sum = Vector.Dot(vSum, Vector<T>.One);

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                sum = NumOps<T>.Add(sum, rSpan.Offset(i));
            }

            return sum;
        }

        [MethodImpl(MaxOpt)]
        public static T Sum<F1, F2>(in ReadOnlySpan<T> span, F1 vSelector, F2 selector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>

        {
            T sum = NumOps<T>.Zero;

            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vSum = Vector<T>.Zero;

                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vSum = Vector.Add(vSum, vSelector.Invoke(vrSpan.Offset(i)));
                }

                sum = Vector.Dot(vSum, Vector<T>.One);

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                sum = NumOps<T>.Add(sum, selector.Invoke(rSpan.Offset(i)));
            }

            return sum;
        }
    }
}