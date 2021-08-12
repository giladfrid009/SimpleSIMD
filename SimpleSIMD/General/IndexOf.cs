using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static int IndexOf(ReadOnlySpan<T> span, T value)
        {
            ref T rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                Vector<T> vValue = new(value);

                ref Vector<T> vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    if (Vector.EqualsAny(vrSpan.Offset(i), vValue))
                    {
                        int j = i * Vector<T>.Count;
                        int l = j + Vector<T>.Count;

                        for (; j < l; j++)
                        {
                            if (NumOps<T>.Equal(rSpan.Offset(j) , value))
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
                if (NumOps<T>.Equal(rSpan.Offset(i), value))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOf<F1, F2>(ReadOnlySpan<T> span, F1 vPredicate, F2 predicate)

            where F1 : struct, IFunc<Vector<T>, bool>
            where F2 : struct, IFunc<T, bool>

        {
            ref T rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref Vector<T> vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    if (vPredicate.Invoke(vrSpan.Offset(i)))
                    {
                        int j = i * Vector<T>.Count;
                        int l = j + Vector<T>.Count;

                        for (; j < l; j++)
                        {
                            if (predicate.Invoke(rSpan.Offset(j)))
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
                if (predicate.Invoke(rSpan.Offset(i)))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
