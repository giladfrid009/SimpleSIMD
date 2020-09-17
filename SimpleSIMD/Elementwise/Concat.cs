using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Concat<U>(T[] left, T[] right, Func<Vector<T>, Vector<T>, Vector<U>> vCombiner, Func<T, T, U> combiner, U[] result) where U : unmanaged
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
                return;
            }

            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            if (Vector<U>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(U).Name);
                return;
            }

            int vLen = Vector<T>.Count;
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

        public static U[] Concat<U>(T[] left, T[] right, Func<Vector<T>, Vector<T>, Vector<U>> vCombiner, Func<T, T, U> combiner) where U : unmanaged
        {
            var result = new U[left.Length];

            Concat(left, right, vCombiner, combiner, result);

            return result;
        }
    }
}
