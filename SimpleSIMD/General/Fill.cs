using System;
using System.Numerics;

namespace SimpleSimd.General
{
    public static partial class GeneralExt
    {
        public static void Fill<T>(this T[] source, T value) where T : unmanaged
        {
            var vVal = new Vector<T>(value);
            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                vVal.CopyTo(source, i);
            }

            for (; i < source.Length; i++)
            {
                source[i] = value;
            }
        }

        public static void Fill<T>(this T[] source, Func<Vector<T>> vFunc, Func<T> func) where T : unmanaged
        {
            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                vFunc().CopyTo(source, i);
            }

            for (; i < source.Length; i++)
            {
                source[i] = func();
            }
        }
    }
}
