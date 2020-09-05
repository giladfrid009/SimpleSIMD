using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Concat(T[] left, T[] right, Func<Vector<T>, Vector<T>, Vector<T>> vCombiner, Func<T, T, T> combiner, T[] result)
        {
            if (right.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            if (result.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                vCombiner(new Vector<T>(left, i), new Vector<T>(right, i)).CopyTo(result, i);
            }

            for (; i < left.Length; i++)
            {
                result[i] = combiner(left[i], right[i]);
            }
        }

        public static T[] Concat(T[] left, T[] right, Func<Vector<T>, Vector<T>, Vector<T>> vCombiner, Func<T, T, T> combiner)
        {
            var result = new T[left.Length];

            Concat(left, right, vCombiner, combiner, result);

            return result;
        }
    }
}
