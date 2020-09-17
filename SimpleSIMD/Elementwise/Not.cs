﻿using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Not(in Span<T> span, in Span<T> result)
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int i;

            var vsSpan = AsVectors(span);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vsResult[i] = ~vsSpan[i];
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = NumOps<T>.Not(span[i]);
            }
        }

        public static T[] Not(T[] array)
        {
            var result = new T[array.Length];

            Not(array, result);

            return result;
        }
    }
}
