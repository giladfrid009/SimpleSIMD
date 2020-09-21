using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {   
        public static void Concat<F1, F2, TRes>(in Span<T> left, in Span<T> right, F1 vCombiner, F2 combiner, in Span<TRes> result)

            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>
            where TRes : unmanaged

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
                vsResult[i] = vCombiner.Invoke(vsLeft[i], vsRight[i]);
            }

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                result[i] = combiner.Invoke(left[i], right[i]);
            }
        }

        public static TRes[] Concat<F1, F2, TRes>(T[] left, T[] right, F1 vCombiner, F2 combiner)

            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>
            where TRes : unmanaged

        {
            var result = new TRes[left.Length];

            Concat<F1, F2, TRes>(left, right, vCombiner, combiner, result);

            return result;
        }
    }
}
