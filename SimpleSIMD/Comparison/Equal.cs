using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static bool Equal(in Span<T> left, T right)
        {
            var vRight = new Vector<T>(right);
            int i;

            var vsLeft = AsVectors(left);

            for (i = 0; i < vsLeft.Length; i++)
            {
                if (vsLeft[i].Equals(vRight) == false)
                {
                    return false;
                }
            }

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                if (NumOps<T>.Equal(left[i], right) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Equal(in Span<T> left, in Span<T> right)
        {
            if (right.Length != left.Length)
            {
                return false;
            }

            int i;

            var vsLeft = AsVectors(left);
            var vsRight = AsVectors(right);

            for (i = 0; i < vsLeft.Length; i++)
            {
                if (vsLeft[i].Equals(vsRight[i]) == false)
                {
                    return false;
                }
            }

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                if (NumOps<T>.Equal(left[i], right[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
