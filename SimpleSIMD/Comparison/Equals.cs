using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool Equals(T[] left, T right)
        {
            var vVal = new Vector<T>(right);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i < left.Length - vLen; i += vLen)
            {
                if (new Vector<T>(left, i) != vVal)
                {
                    return false;
                }
            }

            for (; i < left.Length; i++)
            {
                if (MathOps<T>.Equals(left[i], right) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Equals(T[] left, T[] right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (right.Length != left.Length)
            {
                return false;
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i < left.Length - vLen; i += vLen)
            {
                if (new Vector<T>(left, i) != new Vector<T>(right, i))
                {
                    return false;
                }
            }

            for (; i < left.Length; i++)
            {
                if (MathOps<T>.Equals(left[i], right[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
