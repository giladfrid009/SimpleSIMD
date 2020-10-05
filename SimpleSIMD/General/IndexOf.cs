using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static int IndexOf(in ReadOnlySpan<T> span, T value)
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
                        int length2 = (i + 1) * Vector<T>.Count;

                        for (int j = i * Vector<T>.Count; j < length2; j++)
                        {
                            if (NumOps<T>.Equal(Offset(rSpan, i) , value))
                            {
                                return j;
                            }
                        }
                    }
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                if (NumOps<T>.Equal(Offset(rSpan, i), value))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOf<F1, F2>(in ReadOnlySpan<T> span, F1 vPredicate, F2 predicate)

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
                    if (vPredicate.Invoke(Offset(vrSpan, i)))
                    {
                        int length2 = (i + 1) * Vector<T>.Count;

                        for (int j = i * Vector<T>.Count; j < length2; j++)
                        {
                            if (predicate.Invoke(Offset(rSpan, j)))
                            {
                                return j;
                            }
                        }
                    }
                }

                i *= Vector<T>.Count;
            }


            for (; i < span.Length; i++)
            {
                if (predicate.Invoke(Offset(rSpan, i)))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
