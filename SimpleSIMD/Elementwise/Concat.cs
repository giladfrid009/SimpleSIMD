﻿using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Concat<TRes, F1, F2>(in ReadOnlySpan<T> left, T right, F1 vSelector, F2 selector, in Span<TRes> result)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

        {
            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            if (Vector<TRes>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(TRes).Name);
            }

            ref T rLeft = ref GetRef(left);
            ref TRes rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                Vector<T> vRight = new(right);

                ref Vector<T> vrLeft = ref AsVector(rLeft);
                ref Vector<TRes> vrResult = ref AsVector(rResult);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vrResult.Offset(i) = vSelector.Invoke(vrLeft.Offset(i), vRight);
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                rResult.Offset(i) = selector.Invoke(rLeft.Offset(i), right);
            }
        }

        public static void Concat<TRes, F1, F2>(T left, ReadOnlySpan<T> right, F1 vSelector, F2 selector, in Span<TRes> result)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

        {
            if (result.Length != right.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            if (Vector<TRes>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(TRes).Name);
            }

            ref T rRight = ref GetRef(right);
            ref TRes rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                Vector<T> vLeft = new(left);

                ref Vector<T> vrRight = ref AsVector(rRight);
                ref Vector<TRes> vrResult = ref AsVector(rResult);

                int length = right.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vrResult.Offset(i) = vSelector.Invoke(vLeft, vrRight.Offset(i));
                }

                i *= Vector<T>.Count;
            }

            for (; i < right.Length; i++)
            {
                rResult.Offset(i) = selector.Invoke(left, rRight.Offset(i));
            }
        }

        public static void Concat<TRes, F1, F2>(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, F1 vCombiner, F2 combiner, in Span<TRes> result)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

        {
            if (right.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(right));
            }

            if (result.Length != left.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            if (Vector<TRes>.Count != Vector<T>.Count)
            {
                Exceptions.InvalidCast(typeof(TRes).Name);
            }        

            ref T rLeft = ref GetRef(left);
            ref T rRight = ref GetRef(right);
            ref TRes rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref Vector<T> vrLeft = ref AsVector(rLeft);
                ref Vector<T> vrRight = ref AsVector(rRight);
                ref Vector<TRes> vrResult = ref AsVector(rResult);

                int length = left.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    vrResult.Offset(i) = vCombiner.Invoke(vrLeft.Offset(i), vrRight.Offset(i));
                }

                i *= Vector<T>.Count;
            }

            for (; i < left.Length; i++)
            {
                rResult.Offset(i) = combiner.Invoke(rLeft.Offset(i), rRight.Offset(i));
            }
        }

        public static TRes[] Concat<TRes, F1, F2>(in ReadOnlySpan<T> left, T right, F1 vCombiner, F2 combiner)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

        {
            TRes[] result = new TRes[left.Length];

            Concat<TRes, F1, F2>(left, right, vCombiner, combiner, result);

            return result;
        }

        public static TRes[] Concat<TRes, F1, F2>(T left, in ReadOnlySpan<T> right, F1 vCombiner, F2 combiner)

            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

        {
            TRes[] result = new TRes[right.Length];

            Concat<TRes, F1, F2>(left, right, vCombiner, combiner, result);

            return result;
        }

        public static TRes[] Concat<TRes, F1, F2>(in ReadOnlySpan<T> left, in ReadOnlySpan<T> right, F1 vCombiner, F2 combiner)
            
            where TRes : unmanaged
            where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
            where F2 : struct, IFunc<T, T, TRes>

        {
            TRes[] result = new TRes[left.Length];

            Concat<TRes, F1, F2>(left, right, vCombiner, combiner, result);

            return result;
        }
    }
}
