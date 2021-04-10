#pragma warning disable IDE0011

using System;
using System.Runtime.CompilerServices;

namespace SimpleSimd
{
    public static class NumOps<T> where T : unmanaged
    {
        private const MethodImplOptions MaxOpt = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        public static T Zero => default;

        public static T MinValue
        {
            [MethodImpl(MaxOpt)]
            get
            {
                return default(T) switch
                {
                    byte => (T)(object)byte.MinValue,
                    sbyte => (T)(object)sbyte.MinValue,
                    ushort => (T)(object)ushort.MinValue,
                    short => (T)(object)short.MinValue,
                    uint => (T)(object)uint.MinValue,
                    int => (T)(object)int.MinValue,
                    ulong => (T)(object)ulong.MinValue,
                    long => (T)(object)long.MinValue,
                    float => (T)(object)float.MinValue,
                    double => (T)(object)double.MinValue,
                    _ => throw new NotSupportedException(typeof(T).Name)
                };
            }
        }

        public static T MaxValue
        {
            [MethodImpl(MaxOpt)]
            get
            {
                return default(T) switch
                {
                    byte => (T)(object)byte.MaxValue,
                    sbyte => (T)(object)sbyte.MaxValue,
                    ushort => (T)(object)ushort.MaxValue,
                    short => (T)(object)short.MaxValue,
                    uint => (T)(object)uint.MaxValue,
                    int => (T)(object)int.MaxValue,
                    ulong => (T)(object)ulong.MaxValue,
                    long => (T)(object)long.MaxValue,
                    float => (T)(object)float.MaxValue,
                    double => (T)(object)double.MaxValue,
                    _ => throw new NotSupportedException(typeof(T).Name)
                };
            }
        }

        [MethodImpl(MaxOpt)]
        public static T Not(T value)
        {
            return value switch
            {
                byte X => (T)(object)(byte)~X,
                sbyte X => (T)(object)(sbyte)~X,
                ushort X => (T)(object)(ushort)~X,
                short X => (T)(object)(short)~X,
                uint X => (T)(object)(uint)~X,
                int X => (T)(object)(int)~X,
                ulong X => (T)(object)(ulong)~X,
                long X => (T)(object)(long)~X,
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T Add(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => (T)(object)(byte)(L + R),
                (sbyte L, sbyte R) => (T)(object)(sbyte)(L + R),
                (ushort L, ushort R) => (T)(object)(ushort)(L + R),
                (short L, short R) => (T)(object)(short)(L + R),
                (uint L, uint R) => (T)(object)(uint)(L + R),
                (int L, int R) => (T)(object)(int)(L + R),
                (ulong L, ulong R) => (T)(object)(ulong)(L + R),
                (long L, long R) => (T)(object)(long)(L + R),
                (float L, float R) => (T)(object)(float)(L + R),
                (double L, double R) => (T)(object)(double)(L + R),
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T Subtract(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => (T)(object)(byte)(L - R),
                (sbyte L, sbyte R) => (T)(object)(sbyte)(L - R),
                (ushort L, ushort R) => (T)(object)(ushort)(L - R),
                (short L, short R) => (T)(object)(short)(L - R),
                (uint L, uint R) => (T)(object)(uint)(L - R),
                (int L, int R) => (T)(object)(int)(L - R),
                (ulong L, ulong R) => (T)(object)(ulong)(L - R),
                (long L, long R) => (T)(object)(long)(L - R),
                (float L, float R) => (T)(object)(float)(L - R),
                (double L, double R) => (T)(object)(double)(L - R),
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T Multiply(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => (T)(object)(byte)(L * R),
                (sbyte L, sbyte R) => (T)(object)(sbyte)(L * R),
                (ushort L, ushort R) => (T)(object)(ushort)(L * R),
                (short L, short R) => (T)(object)(short)(L * R),
                (uint L, uint R) => (T)(object)(uint)(L * R),
                (int L, int R) => (T)(object)(int)(L * R),
                (ulong L, ulong R) => (T)(object)(ulong)(L * R),
                (long L, long R) => (T)(object)(long)(L * R),
                (float L, float R) => (T)(object)(float)(L * R),
                (double L, double R) => (T)(object)(double)(L * R),
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T Divide(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => (T)(object)(byte)(L / R),
                (sbyte L, sbyte R) => (T)(object)(sbyte)(L / R),
                (ushort L, ushort R) => (T)(object)(ushort)(L / R),
                (short L, short R) => (T)(object)(short)(L / R),
                (uint L, uint R) => (T)(object)(uint)(L / R),
                (int L, int R) => (T)(object)(int)(L / R),
                (ulong L, ulong R) => (T)(object)(ulong)(L / R),
                (long L, long R) => (T)(object)(long)(L / R),
                (float L, float R) => (T)(object)(float)(L / R),
                (double L, double R) => (T)(object)(double)(L / R),
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T Modulus(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => (T)(object)(byte)(L % R),
                (sbyte L, sbyte R) => (T)(object)(sbyte)(L % R),
                (ushort L, ushort R) => (T)(object)(ushort)(L % R),
                (short L, short R) => (T)(object)(short)(L % R),
                (uint L, uint R) => (T)(object)(uint)(L % R),
                (int L, int R) => (T)(object)(int)(L % R),
                (ulong L, ulong R) => (T)(object)(ulong)(L % R),
                (long L, long R) => (T)(object)(long)(L % R),
                (float L, float R) => (T)(object)(float)(L % R),
                (double L, double R) => (T)(object)(double)(L % R),
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T And(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => (T)(object)(byte)(L & R),
                (sbyte L, sbyte R) => (T)(object)(sbyte)(L & R),
                (ushort L, ushort R) => (T)(object)(ushort)(L & R),
                (short L, short R) => (T)(object)(short)(L & R),
                (uint L, uint R) => (T)(object)(uint)(L & R),
                (int L, int R) => (T)(object)(int)(L & R),
                (ulong L, ulong R) => (T)(object)(ulong)(L & R),
                (long L, long R) => (T)(object)(long)(L & R),
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T Or(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => (T)(object)(byte)(L | R),
                (sbyte L, sbyte R) => (T)(object)(sbyte)(L | R),
                (ushort L, ushort R) => (T)(object)(ushort)(L | R),
                (short L, short R) => (T)(object)(short)(L | R),
                (uint L, uint R) => (T)(object)(L | R),
                (int L, int R) => (T)(object)(int)(L | R),
                (ulong L, ulong R) => (T)(object)(ulong)(L | R),
                (long L, long R) => (T)(object)(long)(L | R),
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T Xor(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => (T)(object)(byte)(L ^ R),
                (sbyte L, sbyte R) => (T)(object)(sbyte)(L ^ R),
                (ushort L, ushort R) => (T)(object)(ushort)(L ^ R),
                (short L, short R) => (T)(object)(short)(L ^ R),
                (uint L, uint R) => (T)(object)(uint)(L ^ R),
                (int L, int R) => (T)(object)(int)(L ^ R),
                (ulong L, ulong R) => (T)(object)(ulong)(L ^ R),
                (long L, long R) => (T)(object)(long)(L ^ R),
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static bool Equal(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => L == R,
                (sbyte L, sbyte R) => L == R,
                (ushort L, ushort R) => L == R,
                (short L, short R) => L == R,
                (uint L, uint R) => L == R,
                (int L, int R) => L == R,
                (ulong L, ulong R) => L == R,
                (long L, long R) => L == R,
                (float L, float R) => L == R,
                (double L, double R) => L == R,
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static bool Greater(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => L > R,
                (sbyte L, sbyte R) => L > R,
                (ushort L, ushort R) => L > R,
                (short L, short R) => L > R,
                (uint L, uint R) => L > R,
                (int L, int R) => L > R,
                (ulong L, ulong R) => L > R,
                (long L, long R) => L > R,
                (float L, float R) => L > R,
                (double L, double R) => L > R,
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static bool Less(T left, T right)
        {
            return (left, right) switch
            {
                (byte L, byte R) => L < R,
                (sbyte L, sbyte R) => L < R,
                (ushort L, ushort R) => L < R,
                (short L, short R) => L < R,
                (uint L, uint R) => L < R,
                (int L, int R) => L < R,
                (ulong L, ulong R) => L < R,
                (long L, long R) => L < R,
                (float L, float R) => L < R,
                (double L, double R) => L < R,
                _ => throw new NotSupportedException(typeof(T).Name)
            };
        }

        [MethodImpl(MaxOpt)]
        public static T Min(T left, T right)
        {
            return Less(left, right) ? left : right;
        }

        [MethodImpl(MaxOpt)]
        public static T Max(T left, T right)
        {
            return Greater(left, right) ? left : right;
        }

        [MethodImpl(MaxOpt)]
        public static T Negate(T value)
        {
            return Subtract(Zero, value);
        }

        [MethodImpl(MaxOpt)]
        public static T Abs(T value)
        {
            return Less(value, Zero) ? Negate(value) : value;
        }

        [MethodImpl(MaxOpt)]
        public static bool LessOrEqual(T left, T right)
        {
            return !Greater(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static bool GreaterOrEqual(T left, T right)
        {
            return !Less(left, right);
        }
    }
}
