using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Dot(ReadOnlySpan<T> left, T right)
        {
            var dot = NumOps<T>.Zero;

            ref var rLeft = ref GetRef(left);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vDot = Vector<T>.Zero;
                Vector<T> vRight = new(right);

                ref var vrLeft = ref AsVector(rLeft);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vDot += vrLeft.Offset(i) * vRight;
                }

                dot = Vector.Dot(vDot, Vector<T>.One);

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                dot = NumOps<T>.Add(dot, NumOps<T>.Multiply(rLeft.Offset(i), right));
            }

            return dot;
        }

        public static T Dot(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
            }

            var dot = NumOps<T>.Zero;

            ref var rLeft = ref GetRef(left);
            ref var rRight = ref GetRef(right);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vDot = Vector<T>.Zero;

                ref var vrLeft = ref AsVector(rLeft);
                ref var vrRight = ref AsVector(rRight);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vDot += vrLeft.Offset(i) * vrRight.Offset(i);
                }

                dot = Vector.Dot(vDot, Vector<T>.One);

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                dot = NumOps<T>.Add(dot, NumOps<T>.Multiply(rLeft.Offset(i), rRight.Offset(i)));
            }

            return dot;
        }
    }
}
