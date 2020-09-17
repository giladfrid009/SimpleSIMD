using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Add(T[] left, T right, T[] result)
        {
            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            var vVal = new Vector<T>(right);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                Vector.Add(new Vector<T>(left, i), vVal).CopyTo(result, i);
            }

            for (; i < left.Length; i++)
            {
                result[i] = MathOps<T>.Add(left[i], right);
            }
        }

        public static void Add(T[] left, T[] right, T[] result)
        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
                return;
            }

            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                Vector.Add(new Vector<T>(left, i), new Vector<T>(right, i)).CopyTo(result, i);
            }

            for (; i < left.Length; i++)
            {
                result[i] = MathOps<T>.Add(left[i], right[i]);
            }
        }

        public static T[] Add(T[] left, T right)
        {
            var result = new T[left.Length];

            Add(left, right, result);

            return result;
        }

        public static T[] Add(T[] left, T[] right)
        {
            var result = new T[left.Length];

            Add(left, right, result);

            return result;
        }
    }
}
