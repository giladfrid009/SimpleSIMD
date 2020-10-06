using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool All<F1, F2>(in ReadOnlySpan<T> span, F1 vPredicate, F2 predicate)

            where F1 : struct, IFunc<Vector<T>, bool>
            where F2 : struct, IFunc<T, bool>

        {
            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    if (vPredicate.Invoke(vrSpan.Offset(i)) == false)
                    {
                        return false;
                    }
                }

                i *= Vector<T>.Count;
            }


            for (; i < span.Length; i++)
            {
                if (predicate.Invoke(rSpan.Offset(i)) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
