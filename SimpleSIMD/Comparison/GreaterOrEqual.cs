﻿using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool GreaterOrEqual(T[] left, T right)
        {
            var vVal = new Vector<T>(right);
            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                if (Vector.LessThanAll(new Vector<T>(left, i), vVal))
                {
                    return false;
                }
            }

            for (; i < left.Length; i++)
            {
                if (MathOps<T>.Less(left[i], right))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool GreaterOrEqual(T[] left, T[] right)
        {
            if (right.Length != left.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int i;

            for (i = 0; i <= left.Length - vLen; i += vLen)
            {
                if (Vector.LessThanAll(new Vector<T>(left, i), new Vector<T>(right, i)))
                {
                    return false;
                }
            }

            for (; i < left.Length; i++)
            {
                if (MathOps<T>.Less(left[i], right[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}