using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static T Dot(in ReadOnlySpan<T> left, T right)
        {        
            T dot = NumOps<T>.Zero;

            ref var rLeft = ref GetRef(left);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vDot = Vector<T>.Zero;
                var vRight = new Vector<T>(right);

                ref var vrLeft = ref AsVector(rLeft);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vDot += Offset(vrLeft, i) * vRight;
                }

                dot = Vector.Dot(vDot, Vector<T>.One);

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                dot = NumOps<T>.Add(dot, NumOps<T>.Multiply(Offset(rLeft, i), right));
            }

            return dot;
        }

        public static T Dot(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
            }

            T dot = NumOps<T>.Zero;

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
                    vDot += Offset(vrLeft, i) * Offset(vrRight, i);
                }

                dot = Vector.Dot(vDot, Vector<T>.One);

                i *= Vector<T>.Count;
            }  

            for (; i < left.Length; i++)
            {
                dot = NumOps<T>.Add(dot, NumOps<T>.Multiply(Offset(rLeft, i), Offset(rRight, i)));
            }

            return dot;
        }
    }
}
