using System;
using System.Numerics;

namespace SimpleSimd.General
{
    public static partial class GeneralExt
    {
        public static void Foreach<T>(this T[] source, Action<Vector<T>> vAction, Action<T> action) where T : unmanaged
        {
            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
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
            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
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
