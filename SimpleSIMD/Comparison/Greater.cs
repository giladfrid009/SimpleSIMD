using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool Greater(in ReadOnlySpan<T> left, T right)
        {
            ref var rLeft = ref GetRef(left);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vRight = new Vector<T>(right);     

                ref var vrLeft = ref AsVector(rLeft);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    if (Vector.GreaterThanAll(vrLeft.Offset(i), vRight) == false)
                    {
                        return false;
                    }
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                if (NumOps<T>.Greater(rLeft.Offset(i), right) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Greater(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
            }

            ref var rLeft = ref GetRef(left);
            ref var rRight = ref GetRef(right);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrLeft = ref AsVector(rLeft);
                ref var vrRight = ref AsVector(rRight);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    if (Vector.GreaterThanAll(vrLeft.Offset(i), vrRight.Offset(i)) == false)
                    {
                        return false;
                    }
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                if (NumOps<T>.Greater(rLeft.Offset(i), rRight.Offset(i)) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
