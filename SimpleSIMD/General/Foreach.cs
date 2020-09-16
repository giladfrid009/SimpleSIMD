using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Foreach(T[] array, Action<Vector<T>> vAction, Action<T> action)
        {
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vAction(new Vector<T>(array, i));
            }

            for (; i < array.Length; i++)
            {
                action(array[i]);
            }
        }

        public static void Foreach(T[] array, Action<Vector<T>, int> vAction, Action<T, int> action)
        {
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                vAction(new Vector<T>(array, i), i);
            }

            for (; i < array.Length; i++)
            {
                action(array[i], i);
            }
        }
    }
}
