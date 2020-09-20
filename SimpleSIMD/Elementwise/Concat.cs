using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Concat<TRes>(in Span<T> left, in Span<T> right, Func<Vector<T>, Vector<T>, Vector<TRes>> vCombiner, Func<T, T, TRes> combiner, in Span<TRes> result) where TRes : unmanaged
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

            if (Vector<TRes>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(TRes).Name);
                return;
            }

            int i;

            var vsLeft = AsVectors(left);
            var vsRight = AsVectors(right);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsLeft.Length; i++)
            {
                vsResult[i] = vCombiner(vsLeft[i], vsRight[i]);
            }

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                result[i] = combiner(left[i], right[i]);
            }
        }

        public static TRes[] Concat<TRes>(T[] left, T[] right, Func<Vector<T>, Vector<T>, Vector<TRes>> vCombiner, Func<T, T, TRes> combiner) where TRes : unmanaged
        {
            var result = new TRes[left.Length];

            Concat(left, right, vCombiner, combiner, result);

            return result;
        }
    }
}
