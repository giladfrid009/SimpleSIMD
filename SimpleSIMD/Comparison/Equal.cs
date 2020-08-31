﻿using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static bool Equal<T>(this T[] source, T value) where T : unmanaged
        {
            var vVal = new Vector<T>(value);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i < source.Length - vLen; i += vLen)
            {
                if (new Vector<T>(source, i) != vVal)
                {
                    return false;
                }
            }

            for (; i < source.Length; i++)
            {
                if (NOperations<T>.Equal(source[i], value) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Equal<T>(this T[] source, T[] other) where T : unmanaged
        {
            if (ReferenceEquals(source, other))
            {
                return true;
            }

            if (other.Length != source.Length)
            {
                return false;
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i < source.Length - vLen; i += vLen)
            {
                if (new Vector<T>(source, i) != new Vector<T>(other, i))
                {
                    return false;
                }
            }

            for (; i < source.Length; i++)
            {
                if (NOperations<T>.Equal(source[i], other[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
