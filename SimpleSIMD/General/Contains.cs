using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool Contains(in ReadOnlySpan<T> span, T value)
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
                    if (Vector.EqualsAny(Offset(vrSpan, i), vValue))
                    {
                        return true;
                    }
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                if (NumOps<T>.Equal(Offset(rSpan, i), value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
