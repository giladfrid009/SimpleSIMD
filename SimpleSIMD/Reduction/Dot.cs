using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static T Dot<T>(this T[] source, T value) where T : unmanaged
        {
            var vVal = new Vector<T>(value);
            Vector<T> vDot = Vector<T>.Zero;
            T dot;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vDot += new Vector<T>(source, i) * vVal;
            }

            dot = Vector.Dot(vDot, Vector<T>.One);

            for (; i < source.Length; i++)
            {
                dot = Operations<T>.Add(dot, Operations<T>.Mul(source[i], value));
            }

            return dot;
        }

        public static T Dot<T>(this T[] source, T[] other) where T : unmanaged
        {
            if (other.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(other));
            }

            Vector<T> vDot = Vector<T>.Zero;
            T dot;

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vDot += new Vector<T>(source, i) * new Vector<T>(other, i);
            }

            dot = Vector.Dot(vDot, Vector<T>.One);

            for (; i < source.Length; i++)
            {
                dot = Operations<T>.Add(dot, Operations<T>.Mul(source[i], other[i]));
            }

            return dot;
        }
    }
}
