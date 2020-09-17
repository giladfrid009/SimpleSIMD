using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool Less(T[] left, T right)
        {
            var vRight = new Vector<T>(right);
            int i;

            var vsLeft = AsVectors(left);

            for (i = 0; i < vsLeft.Length; i++)
            {
                if (Vector.LessThanAll(vsLeft[i], vRight) == false)
                {
                    return false;
                }
            }

            i *= Vector<T>.Count;
            
            for (; i < left.Length; i++)
            {
                if (MathOps<T>.Less(left[i], right) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Less(T[] left, T[] right)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
                return default;
            }

            int i;

            var vsLeft = AsVectors(left);
            var vsRight = AsVectors(right);

            for (i = 0; i < vsLeft.Length; i++)
            {
                if (Vector.LessThanAll(vsLeft[i], vsRight[i]) == false)
                {
                    return false;
                }
            }

            i *= Vector<T>.Count;

            for (; i < left.Length; i++)
            {
                if (MathOps<T>.Less(left[i], right[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
