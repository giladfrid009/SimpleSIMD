using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static int IndexOf(in Span<T> span, T value)
        {
            var vValue = new Vector<T>(value);
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                if (Vector.EqualsAny(vsSpan[i], vValue))
                {
                    var vec = vsSpan[i];

                    for (int j = 0; j < Vector<T>.Count; j++)
                    {
                        if (NumOps<T>.Equal(vec[j], value))
                        {
                            return i * Vector<T>.Count + j;
                        }
                    }
                }
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                if (NumOps<T>.Equal(span[i], value))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOf<F1, F2>(in Span<T> span, F1 vPredicate, F2 predicate)

            where F1 : struct, IFunc<Vector<T>, bool>
            where F2 : struct, IFunc<T, bool>

        {
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                if (vPredicate.Invoke(vsSpan[i]))
                {
                    var vec = vsSpan[i];

                    for (int j = 0; j < Vector<T>.Count; j++)
                    {
                        if (predicate.Invoke(vec[j]))
                        {
                            return i * Vector<T>.Count + j;
                        }
                    }
                }
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                if (predicate.Invoke(span[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
