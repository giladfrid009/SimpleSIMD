using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {   
        public static void Concat<TRes, F1, F2>(in Span<T> left, in Span<T> right, F1 vCombiner, F2 combiner, in Span<TRes> result)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

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

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vsLeft = AsVectors(left);
                var vsRight = AsVectors(right);
                var vsResult = AsVectors(result);

                for (; i < vsLeft.Length; i++)
                {
                    vsResult[i] = vCombiner.Invoke(vsLeft[i], vsRight[i]);
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                result[i] = combiner.Invoke(left[i], right[i]);
            }
        }

        public static TRes[] Concat<TRes, F1, F2>(T[] left, T[] right, F1 vCombiner, F2 combiner)
            
            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

        {
            var result = new TRes[left.Length];

            Concat<TRes, F1, F2>(left, right, vCombiner, combiner, result);

            return result;
        }
    }
}
