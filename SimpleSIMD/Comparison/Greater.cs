﻿using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct Greater_VSelector : IFunc<Vector<T>, Vector<T>, bool>
        {
            public bool Invoke(Vector<T> left, Vector<T> right)
            {
                return Vector.GreaterThanAll(left, right);
            }
        }

        private struct Greater_Selector : IFunc<T, T, bool>
        {
            public bool Invoke(T left, T right)
            {
                return NumOps<T>.Greater(left, right);
            }
        }

        public static bool Greater(in ReadOnlySpan<T> left, T right)
        {
            return All(left, right, new Greater_VSelector(), new Greater_Selector());
        }

        public static bool Greater(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right)
        {
            return All(left, right, new Greater_VSelector(), new Greater_Selector());
        }
    }
}
