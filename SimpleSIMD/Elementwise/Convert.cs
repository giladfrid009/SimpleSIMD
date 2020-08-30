using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Convert<TIn, TOut>(this TIn[] source, Func<Vector<TIn>, Vector<TOut>> vConverter, TOut[] result)
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
                vConverter(new Vector<TIn>(source, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = Converter<TIn, TOut>.Change(source[i]);
            }
        }

        public static TOut[] Convert<TIn, TOut>(this TIn[] source, Func<Vector<TIn>, Vector<TOut>> vConverter)
        where TIn : unmanaged
        where TOut : unmanaged
        {
            var result = new TOut[source.Length];

            source.Convert(vConverter, result);

            return result;
        }
    }
}
