using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Accum<T>(this T[] source, T seed, Func<Vector<T>, Vector<T>, Vector<T>> vAccum, Func<T, T, T> accumu) where T : unmanaged
        {
            var vRes = new Vector<T>(seed);
            T res = seed;

            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                vRes = vAccum(vRes, new Vector<T>(source[i]));
            }

            for (int j = 0; j < vLength; j++)
            {
                res = accumu(res, vRes[j]);
            }

            for (; i < source.Length; i++)
            {
                res = accumu(res, source[i]);
            }

            return res;
        }
    }
}
