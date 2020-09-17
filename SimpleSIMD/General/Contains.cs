using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool Contains(in Span<T> span, T value)
        {
            Vector<T> vValue = new Vector<T>(value);
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                if (Vector.EqualsAny(vsSpan[i], vValue))
                {
                    return true;
                }
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                if (NumOps<T>.Equal(span[i], value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
