using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {        
        public static void Foreach<F1, F2>(in Span<T> span, F1 vAction, F2 action)

            where F1 : struct, IAction<Vector<T>>
            where F2 : struct, IAction<T>

        {
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vAction.Invoke(vsSpan[i]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                action.Invoke(span[i]);
            }
        }
    }
}
