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
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vsSpan = AsVectors(span);

                for (; i < vsSpan.Length; i++)
                {
                    vAction.Invoke(vsSpan[i]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                action.Invoke(span[i]);
            }
        }
    }
}
