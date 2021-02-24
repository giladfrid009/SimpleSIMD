using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Fill(in Span<T> span, T value)
        {
            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vValue = new Vector<T>(value);

                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;
                
                for (; i < length; i++)
                {
                    vrSpan.Offset(i) = vValue;
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                rSpan.Offset(i) = value;
            }
        }

        public static void Fill<F1, F2>(in Span<T> span, F1 vFunc, F2 func)

            where F1 : struct, IFunc<Vector<T>>
            where F2 : struct, IFunc<T>

        {
            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrSpan = ref AsVector(rSpan);
                
                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vrSpan.Offset(i) = vFunc.Invoke();
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                rSpan.Offset(i) = func.Invoke();
            }
        }
    }
}
