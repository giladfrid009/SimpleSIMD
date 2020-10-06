using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {        
        public static void Foreach<F1, F2>(in ReadOnlySpan<T> span, F1 vAction, F2 action)

            where F1 : struct, IAction<Vector<T>>
            where F2 : struct, IAction<T>

        {
            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vAction.Invoke(vrSpan.Offset(i));
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                action.Invoke(rSpan.Offset(i));
            }
        }
    }
}
