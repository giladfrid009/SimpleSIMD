using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class Extensions
    {
        public static void Concat<T>(this T[] source, T[] other, Func<Vector<T>, Vector<T>, Vector<T>> vCombiner, Func<T, T, T> combiner, T[] result) where T : unmanaged
        {
            if(other.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(other));
            }

            if (result.Length != source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int vLen = Vector<T>.Count;
            int i;

            for (i = 0; i <= source.Length - vLen; i += vLen)
            {
                vCombiner(new Vector<T>(source, i), new Vector<T>(other, i)).CopyTo(result, i);
            }

            for (; i < source.Length; i++)
            {
                result[i] = combiner(source[i], other[i]);
            }
        }

        public static T[] Concat<T>(this T[] source, T[] other, Func<Vector<T>, Vector<T>, Vector<T>> vCombiner, Func<T, T, T> combiner) where T : unmanaged
        {
            var result = new T[source.Length];

            source.Concat(other, vCombiner, combiner, result);

            return result;
        }
    }
}
