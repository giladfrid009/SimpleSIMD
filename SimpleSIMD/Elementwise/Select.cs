using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        [ArrOverload]
        [DelOverload]
        public static void Select<TRes, F1, F2>(ReadOnlySpan<T> span, F1 vSelector, F2 selector, Span<TRes> result)
            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, TRes>
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            if (Vector<TRes>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(TRes).Name);
            }

            ref var rSpan = ref GetRef(span);
            ref var rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrSpan = ref AsVector(rSpan);
                ref var vrResult = ref AsVector(rResult);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vrResult.Offset(i) = vSelector.Invoke(vrSpan.Offset(i));
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                rResult.Offset(i) = selector.Invoke(rSpan.Offset(i));
            }
        }
    }
}
