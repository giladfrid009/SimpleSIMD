﻿using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Not_VSelector : IFunc<Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> vec)
            {
                return ~vec;
            }
        }

        private struct Not_Selector : IFunc<T, T>
        {
            public T Invoke(T val)
            {
                return NumOps<T>.Not(val);
            }
        }

        public static void Not(in ReadOnlySpan<T> span, in Span<T> result)
        {
            Select(span, new Not_VSelector(), new Not_Selector(), result);
        }

        public static T[] Not(T[] array)
        {
            var result = new T[array.Length];

            Not(array, result);

            return result;
        }
    }
}
