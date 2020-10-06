using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Divide(in ReadOnlySpan<T> left, T right, in ReadOnlySpan<T> result)
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
                    vrResult.Offset(i) = Vector.Divide(vrLeft.Offset(i), vRight);
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                rResult.Offset(i) = NumOps<T>.Divide(rLeft.Offset(i), right);
            }
        }

        public static void Divide(T left, in ReadOnlySpan<T> right, in ReadOnlySpan<T> result)
        {
            if (result.Length != right.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            ref var rRight = ref GetRef(right);
            ref var rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vLeft = new Vector<T>(right);

                ref var vrRight = ref AsVector(rRight);
                ref var vrResult = ref AsVector(rResult);

                int length = right.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vrResult.Offset(i) = Vector.Divide(vLeft, vrRight.Offset(i));
                }

                i *= Vector<T>.Count;
            }      

            for (; i < right.Length; i++)
            {
                rResult.Offset(i) = NumOps<T>.Divide(left, rRight.Offset(i));
            }
        }

        public static void Divide(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, in ReadOnlySpan<T> result)
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
                    vrResult.Offset(i) = Vector.Divide(vrLeft.Offset(i), vrRight.Offset(i));
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                rResult.Offset(i) = NumOps<T>.Divide(rLeft.Offset(i), rRight.Offset(i));
            }
        }

        public static T[] Divide(T[] left, T right)
        {
            var result = new T[left.Length];

            Divide(left, right, result);

            return result;
        }

        public static T[] Divide(T left, T[] right)
        {
            var result = new T[right.Length];

            Divide(left, right, result);

            return result;
        }

        public static T[] Divide(T[] left, T[] right)
        {
            var result = new T[left.Length];

            Divide(left, right, result);

            return result;
        }
    }
}
