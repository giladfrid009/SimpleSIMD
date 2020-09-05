using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static T Aggregate(T[] array, T seed, Func<Vector<T>, Vector<T>, Vector<T>> vAccumulator, Func<T, T, T> accumulator)
        {
            var vRes = new Vector<T>(seed);
            T res = seed;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vRes = vAccumulator(vRes, new Vector<T>(array[i]));
            }

            for (int j = 0; j < vLen; j++)
            {
                res = accumulator(res, vRes[j]);
            }

            for (; i < array.Length; i++)
            {
                res = accumulator(res, array[i]);
            }

            return res;
        }
    }
}
