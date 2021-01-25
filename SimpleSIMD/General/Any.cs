using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool Any<F1, F2>(in ReadOnlySpan<T> span, F1 vPredicate, F2 predicate)

            where F1 : struct, IFunc<Vector<T>, bool>
            where F2 : struct, IFunc<T, bool>

        {
            ref var rSpan = ref GetRef(span);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrSpan = ref AsVector(rSpan);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    if (vPredicate.Invoke(vrSpan.Offset(i)))
                    {
                        return true;
                    }
                }

                i *= Vector<T>.Count;
            }


            for (; i < span.Length; i++)
            {
                if (predicate.Invoke(rSpan.Offset(i)))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Any<F1, F2>(in ReadOnlySpan<T> left, T right, F1 vPredicate, F2 predicate)

            where F1 : struct, IFunc<Vector<T>, Vector<T>, bool>
            where F2 : struct, IFunc<T, T, bool>

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
                    if (vPredicate.Invoke(vrLeft.Offset(i), vRight))
                    {
                        return true;
                    }
                }

                i *= Vector<T>.Count;
            }


            for (; i < left.Length; i++)
            {
                if (predicate.Invoke(rLeft.Offset(i), right))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Any<F1, F2>(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, F1 vPredicate, F2 predicate)

            where F1 : struct, IFunc<Vector<T>, Vector<T>, bool>
            where F2 : struct, IFunc<T, T, bool>

        {
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
                    if (vPredicate.Invoke(vrLeft.Offset(i), vrRight.Offset(i)))
                    {
                        return true;
                    }
                }

                i *= Vector<T>.Count;
            }


            for (; i < left.Length; i++)
            {
                if (predicate.Invoke(rLeft.Offset(i), rRight.Offset(i)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
