using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool All(in Span<T> span, Func<Vector<T>, bool> vPredicate, Func<T, bool> predicate)
        {
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                if (vPredicate(vsSpan[i]) == false)
                {
                    return false;
                }
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                if (predicate(span[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
