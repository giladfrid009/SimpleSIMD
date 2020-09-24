using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Aggregate<F1, F2>(in Span<T> span, T seed, F1 vAccumulator, F2 accumulator)

            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T, T>

        {
            T res = seed;
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vRes = new Vector<T>(seed);
                var vsSpan = AsVectors(span);

                for (; i < vsSpan.Length; i++)
                {
                    vRes = vAccumulator.Invoke(vRes, vsSpan[i]);
                }

                for (int j = 0; j < Vector<T>.Count; j++)
                {
                    res = accumulator.Invoke(res, vRes[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                res = accumulator.Invoke(res, span[i]);
            }

            return res;
        }
    }
}
