using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static bool Contains<T>(this T[] source, T value) where T : unmanaged
        {
            var vVal = new Vector<T>(value);
            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                if (Vector.EqualsAny(new Vector<T>(source, i), vVal))
                {
                    return true;
                }
            }

            for (; i < source.Length; i++)
            {
                if (Operations<T>.Equal(source[i], value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
