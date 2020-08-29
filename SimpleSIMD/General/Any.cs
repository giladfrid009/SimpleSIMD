using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static bool Any<T>(this T[] source, Func<Vector<T>, bool> vPredicate, Func<T, bool> predicate) where T : unmanaged
        {
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                if (vPredicate(new Vector<T>(source, i)))
                {
                    return true;
                }
            }

            for (; i < source.Length; i++)
            {
                if (predicate(source[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
