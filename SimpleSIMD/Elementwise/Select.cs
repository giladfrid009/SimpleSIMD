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
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }       

            if (Vector<U>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(U).Name);
                return;
            }

            int i;

            var vsArray = AsVectors(array);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsArray.Length; i++)
            {
                vsResult[i] = vSelector(vsArray[i]);
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                result[i] = selector(array[i]);
            }
        }

        public static void Select<U>(T[] array, Func<Vector<T>, int, Vector<U>> vSelector, Func<T, int, U> selector, U[] result) where U : unmanaged
        {
            if (result.Length != array.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            if (Vector<U>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(U).Name);
                return;
            }

            int i;

            var vsArray = AsVectors(array);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsArray.Length; i++)
            {
                vsResult[i] = vSelector(vsArray[i], i * Vector<T>.Count);
            }

            i *= Vector<T>.Count;

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
