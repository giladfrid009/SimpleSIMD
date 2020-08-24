using System;
using System.Numerics;

namespace SimpleSimd.Comparison
{
    public static partial class ComparisonExt
    {
        public static bool GreaterEqual<T>(this T[] source, T value) where T : unmanaged
        {
            var vVal = new Vector<T>(value);
            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                if (Vector.LessThanAll(new Vector<T>(source, i), vVal))
                {
                    return false;
                }
            }

            for (; i < source.Length; i++)
            {
                if (Ops<T>.Less(source[i], value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool GreaterEqual<T>(this T[] source, T[] other) where T : unmanaged
        {
            if (other.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int vLength = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLength; i += vLength)
            {
                if (Vector.LessThanAll(new Vector<T>(source, i), new Vector<T>(other, i)))
                {
                    return false;
                }
            }

            for (; i < source.Length; i++)
            {
                if (Ops<T>.Less(source[i], other[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
