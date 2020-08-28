using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Foreach<T>(this T[] source, Action<Vector<T>> vAction, Action<T> action) where T : unmanaged
        {
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vAction(new Vector<T>(source, i));
            }

            for (; i < source.Length; i++)
            {
                action(source[i]);
            }
        }

        public static void Foreach<T>(this T[] source, Action<Vector<T>, int> vAction, Action<T, int> action) where T : unmanaged
        {
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vAction(new Vector<T>(source, i), i);
            }

            for (; i < source.Length; i++)
            {
                action(source[i], i);
            }
        }
    }
}
