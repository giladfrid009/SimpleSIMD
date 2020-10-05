using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void And(in ReadOnlySpan<T> left, T right, in ReadOnlySpan<T> result)
        {
            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            ref var rLeft = ref GetRef(left);
            ref var rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vRight = new Vector<T>(right);

                ref var vrLeft = ref AsVector(rLeft);
                ref var vrResult = ref AsVector(rResult);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    Offset(vrResult, i) = Vector.BitwiseAnd(Offset(vrLeft, i), vRight);
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                Offset(rResult, i) = NumOps<T>.And(Offset(rLeft, i), right);
            }
        }

        public static void And(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, in ReadOnlySpan<T> result)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
            }

            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
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
                    Offset(vrResult, i) = Vector.BitwiseAnd(Offset(vrLeft, i), Offset(vrRight, i));
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                Offset(rResult, i) = NumOps<T>.And(Offset(rLeft, i), Offset(rRight, i));
            }
        }

        public static T[] And(T[] left, T right)
        {
            var result = new T[left.Length];

            And(left, right, result);

            return result;
        }

        public static T[] And(T[] left, T[] right)
        {
            var result = new T[left.Length];

            And(left, right, result);

            return result;
        }
    }
}
