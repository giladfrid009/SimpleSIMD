using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Select<U>(T[] array, Func<Vector<T>, Vector<U>> vSelector, Func<T, U> selector, U[] result) where U : unmanaged
        {
            if (result.Length != array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }       

            if (Vector<U>.Count != Vector<T>.Count)
            {
                throw new InvalidCastException(typeof(U).Name);
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vSelector(new Vector<T>(array, i)).CopyTo(result, i);
            }

            for (; i < array.Length; i++)
            {
                result[i] = selector(array[i]);
            }
        }

        public static void Select<U>(T[] array, Func<Vector<T>, int, Vector<U>> vSelector, Func<T, int, U> selector, U[] result) where U : unmanaged
        {
            if (result.Length != array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            if (Vector<U>.Count != Vector<T>.Count)
            {
                throw new InvalidCastException(typeof(U).Name);
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vSelector(new Vector<T>(array, i), i).CopyTo(result, i);
            }

            for (; i < array.Length; i++)
            {
                result[i] = selector(array[i], i);
            }
        }

        public static U[] Select<U>(T[] array, Func<Vector<T>, Vector<U>> vSelector, Func<T, U> selector) where U : unmanaged
        {
            var result = new U[array.Length];

            Select(array, vSelector, selector, result);

            return result;
        }

        public static U[] Select<U>(T[] array, Func<Vector<T>, int, Vector<U>> vSelector, Func<T, int, U> selector) where U : unmanaged
        {
            var result = new U[array.Length];

            Select(array, vSelector, selector, result);

            return result;
        }
    }
}
