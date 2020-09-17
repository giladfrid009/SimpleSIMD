using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Fill(in Span<T> span, T value)
        {
            var vValue = new Vector<T>(value);
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vsSpan[i] = vValue;
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                span[i] = value;
            }
        }

        public static void Fill(in Span<T> span, Func<Vector<T>> vFunc, Func<T> func)
        {
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vsSpan[i] = vFunc();
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                span[i] = func();
            }
        }
    }
}
