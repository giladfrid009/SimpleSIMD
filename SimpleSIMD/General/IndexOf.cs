using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static int IndexOf<T>(this T[] source, T value) where T : unmanaged
        {
            var vVal = new Vector<T>(value);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                if (Vector.EqualsAny(new Vector<T>(source, i), vVal))
                {
                    for (int j = i; j < i + vLen; j++)
                    {
                        if (NOperations<T>.Equal(source[j], value))
                        {
                            return j;
                        }
                    }
                }
            }

            for (; i < source.Length; i++)
            {
                if (NOperations<T>.Equal(source[i], value))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
