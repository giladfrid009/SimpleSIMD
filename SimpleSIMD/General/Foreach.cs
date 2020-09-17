using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Foreach(in Span<T> span, Action<Vector<T>> vAction, Action<T> action)
        {
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vAction(vsSpan[i]);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                action(span[i]);
            }
        }

        public static void Foreach(in Span<T> span, Action<Vector<T>, int> vAction, Action<T, int> action)
        {
            int i;

            var vsSpan = AsVectors(span);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vAction(vsSpan[i], i * Vector<T>.Count);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                action(span[i], i);
            }
        }
    }
}
