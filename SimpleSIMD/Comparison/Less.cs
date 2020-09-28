using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool Less(in ReadOnlySpan<T> left, T right)
        {
            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vRight = new Vector<T>(right);
                var vsLeft = AsVectors(left);

                for (; i < vsLeft.Length; i++)
                {
                    if (Vector.LessThanAll(vsLeft[i], vRight) == false)
                    {
                        return false;
                    }
                }

                i *= Vector<T>.Count;
            }
            
            for (; i < left.Length; i++)
            {
                if (NumOps<T>.Less(left[i], right) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Less(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
                return default;
            }

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vsLeft = AsVectors(left);
                var vsRight = AsVectors(right);

                for (; i < vsLeft.Length; i++)
                {
                    if (Vector.LessThanAll(vsLeft[i], vsRight[i]) == false)
                    {
                        return false;
                    }
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                if (NumOps<T>.Less(left[i], right[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
