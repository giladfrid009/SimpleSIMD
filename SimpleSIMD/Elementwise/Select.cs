using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Select<TIn, TOut>(this TIn[] source, Func<Vector<TIn>, Vector<TOut>> vSelector, Func<TIn, TOut> selector, TOut[] result)
        where TIn : unmanaged
        where TOut : unmanaged
        {
            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }       

            int vLen = Vector<TIn>.Count;

            if (Vector<TOut>.Count != vLen)
            {
                throw new InvalidCastException(typeof(TOut).Name);
            }

            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vSelector(new Vector<TIn>(source, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = selector(source[i]);
            }
        }

        public static void Select<TIn, TOut>(this TIn[] source, Func<Vector<TIn>, int, Vector<TOut>> vSelector, Func<TIn, int, TOut> selector, TOut[] result) 
        where TIn : unmanaged 
        where TOut : unmanaged
        {
            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLen = Vector<TIn>.Count;

            if (Vector<TOut>.Count != vLen)
            {
                throw new InvalidCastException(typeof(TOut).Name);
            }

            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vSelector(new Vector<TIn>(source, i), i).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = selector(source[i], i);
            }
        }

        public static TOut[] Select<TIn, TOut>(this TIn[] source, Func<Vector<TIn>, Vector<TOut>> vSelector, Func<TIn, TOut> selector) 
        where TIn : unmanaged 
        where TOut : unmanaged
        {
            var result = new TOut[source.Length];

            source.Select(vSelector, selector, result);

            return result;
        }

        public static TOut[] Select<TIn, TOut>(this TIn[] source, Func<Vector<TIn>, int, Vector<TOut>> vSelector, Func<TIn, int, TOut> selector) 
        where TIn : unmanaged 
        where TOut : unmanaged
        {
            var result = new TOut[source.Length];

            source.Select(vSelector, selector, result);

            return result;
        }
    }
}
