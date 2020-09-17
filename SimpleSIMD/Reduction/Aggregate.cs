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

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vRes = vAccumulator(vRes, vsArray[i]);
            }

            for (int j = 0; j < Vector<T>.Count; j++)
            {
                res = accumulator(res, vRes[j]);
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                res = accumulator(res, array[i]);
            }

            return res;
        }
    }
}
