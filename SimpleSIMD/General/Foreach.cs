using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Foreach(T[] array, Action<Vector<T>> vAction, Action<T> action)
        {
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vAction(vsArray[i]);
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                action(array[i]);
            }
        }

        public static void Foreach(T[] array, Action<Vector<T>, int> vAction, Action<T, int> action)
        {
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                vAction(vsArray[i], i * Vector<T>.Count);
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                action(array[i], i);
            }
        }
    }
}
