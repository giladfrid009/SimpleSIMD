﻿using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Aggregate<F1, F2>(ReadOnlySpan<T> span, T seed, F1 vAccumulator, F2 accumulator)

            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T, T>

        {
            T res = seed;

            ref T rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                Vector<T> vRes = new(seed);

                ref Vector<T> vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vRes = vAccumulator.Invoke(vRes, vrSpan.Offset(i));
                }

                for (int j = 0; j < Vector<T>.Count; j++)
                {
                    res = accumulator.Invoke(res, vRes[j]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                res = accumulator.Invoke(res, rSpan.Offset(i));
            }

            return res;
        }
    }
}
