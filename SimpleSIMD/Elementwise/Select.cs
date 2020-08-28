using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Select<T, U>(this T[] source, Func<Vector<T>, Vector<U>> vSelector, Func<T, U> selector, U[] result) where T : unmanaged where U : unmanaged
        {
            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vSelector(new Vector<T>(source, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = selector(source[i]);
            }
        }

        public static void Select<T, U>(this T[] source, Func<Vector<T>, int, Vector<U>> vSelector, Func<T, int, U> selector, U[] result) where T : unmanaged where U : unmanaged
        {
            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vSelector(new Vector<T>(source, i), i).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = selector(source[i], i);
            }
        }

        public static U[] Select<T, U>(this T[] source, Func<Vector<T>, Vector<U>> vSelector, Func<T, U> selector) where T : unmanaged where U : unmanaged
        {
            var result = new U[source.Length];

            source.Select(vSelector, selector, result);

            return result;
        }

        public static U[] Select<T, U>(this T[] source, Func<Vector<T>, int, Vector<U>> vSelector, Func<T, int, U> selector) where T : unmanaged where U : unmanaged
        {
            var result = new U[source.Length];

            source.Select(vSelector, selector, result);

            return result;
        }
    }
}
