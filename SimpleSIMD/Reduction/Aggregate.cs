using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Aggregate(in Span<T> span, T seed, Func<Vector<T>, Vector<T>, Vector<T>> vAccumulator, Func<T, T, T> accumulator)
        {
            var vRes = new Vector<T>(seed);
            T res = seed;
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vRes = vAccumulator(vRes, vsSpan[i]);
            }

            for (int j = 0; j < Vector<T>.Count; j++)
            {
                res = accumulator(res, vRes[j]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                res = accumulator(res, span[i]);
            }

            return res;
        }
    }
}
