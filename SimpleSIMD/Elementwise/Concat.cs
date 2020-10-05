using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {   
        public static void Concat<TRes, F1, F2>(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, F1 vCombiner, F2 combiner, in ReadOnlySpan<TRes> result)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
            }

            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            if (Vector<TRes>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(TRes).Name);
            }        

            ref var rLeft = ref GetRef(left);
            ref var rRight = ref GetRef(right);
            ref var rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrLeft = ref AsVector(rLeft);
                ref var vrRight = ref AsVector(rRight);
                ref var vrResult = ref AsVector(rResult);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    Offset(vrResult, i) = vCombiner.Invoke(Offset(vrLeft, i), Offset(vrRight, i));
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                Offset(rResult, i) = combiner.Invoke(Offset(rLeft, i), Offset(rRight, i));
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
