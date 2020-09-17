using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool Less(T[] left, T right)
        {
            var vVal = new Vector<T>(right);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                if (Vector.LessThanAll(new Vector<T>(left, i), vVal) == false)
                {
                    return false;
                }
            }

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

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                if (Vector.LessThanAll(new Vector<T>(left, i), new Vector<T>(right, i)) == false)
                {
                    return false;
                }
            }

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
