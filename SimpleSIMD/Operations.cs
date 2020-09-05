using System;

namespace SimpleSimd
{
    #pragma warning disable IDE0011
    public static class Operations<T> where T : unmanaged
    {
        public static T Zero => default;

        public static T MinValue
        {
            get
            {
                if (typeof(T) == typeof(byte)) return (T)(object)byte.MinValue;
                if (typeof(T) == typeof(sbyte)) return (T)(object)sbyte.MinValue;
                if (typeof(T) == typeof(ushort)) return (T)(object)ushort.MinValue;
                if (typeof(T) == typeof(short)) return (T)(object)short.MinValue;
                if (typeof(T) == typeof(uint)) return (T)(object)uint.MinValue;
                if (typeof(T) == typeof(int)) return (T)(object)int.MinValue;
                if (typeof(T) == typeof(ulong)) return (T)(object)ulong.MinValue;
                if (typeof(T) == typeof(long)) return (T)(object)long.MinValue;
                if (typeof(T) == typeof(float)) return (T)(object)float.MinValue;
                if (typeof(T) == typeof(double)) return (T)(object)double.MinValue;
                if (typeof(T) == typeof(decimal)) return (T)(object)decimal.MinValue;

                throw new NotSupportedException(typeof(T).Name);
            }
        }

        public static T MaxValue
        {
            get
            {
                if (typeof(T) == typeof(byte)) return (T)(object)byte.MaxValue;
                if (typeof(T) == typeof(sbyte)) return (T)(object)sbyte.MaxValue;
                if (typeof(T) == typeof(ushort)) return (T)(object)ushort.MaxValue;
                if (typeof(T) == typeof(short)) return (T)(object)short.MaxValue;
                if (typeof(T) == typeof(uint)) return (T)(object)uint.MaxValue;
                if (typeof(T) == typeof(int)) return (T)(object)int.MaxValue;
                if (typeof(T) == typeof(ulong)) return (T)(object)ulong.MaxValue;
                if (typeof(T) == typeof(long)) return (T)(object)long.MaxValue;
                if (typeof(T) == typeof(float)) return (T)(object)float.MaxValue;
                if (typeof(T) == typeof(double)) return (T)(object)double.MaxValue;
                if (typeof(T) == typeof(decimal)) return (T)(object)decimal.MaxValue;

                throw new NotSupportedException(typeof(T).Name);
            }
        }

        public static T Add(T left, T right)
        {
            if (typeof(T) == typeof(byte)) return (T)(object)((byte)(object)left + (byte)(object)right);
            if (typeof(T) == typeof(sbyte)) return (T)(object)((sbyte)(object)left + (sbyte)(object)right);
            if (typeof(T) == typeof(ushort)) return (T)(object)((ushort)(object)left + (ushort)(object)right);
            if (typeof(T) == typeof(short)) return (T)(object)((short)(object)left + (short)(object)right);
            if (typeof(T) == typeof(uint)) return (T)(object)((uint)(object)left + (uint)(object)right);
            if (typeof(T) == typeof(int)) return (T)(object)((int)(object)left + (int)(object)right);
            if (typeof(T) == typeof(ulong)) return (T)(object)((ulong)(object)left + (ulong)(object)right);
            if (typeof(T) == typeof(long)) return (T)(object)((long)(object)left + (long)(object)right);
            if (typeof(T) == typeof(float)) return (T)(object)((float)(object)left + (float)(object)right);
            if (typeof(T) == typeof(double)) return (T)(object)((double)(object)left + (double)(object)right);
            if (typeof(T) == typeof(decimal)) return (T)(object)((decimal)(object)left + (decimal)(object)right);

            throw new NotSupportedException(typeof(T).Name);
        }

        public static T Subtract(T left, T right)
        {
            if (typeof(T) == typeof(byte)) return (T)(object)((byte)(object)left - (byte)(object)right);
            if (typeof(T) == typeof(sbyte)) return (T)(object)((sbyte)(object)left - (sbyte)(object)right);
            if (typeof(T) == typeof(ushort)) return (T)(object)((ushort)(object)left - (ushort)(object)right);
            if (typeof(T) == typeof(short)) return (T)(object)((short)(object)left - (short)(object)right);
            if (typeof(T) == typeof(uint)) return (T)(object)((uint)(object)left - (uint)(object)right);
            if (typeof(T) == typeof(int)) return (T)(object)((int)(object)left - (int)(object)right);
            if (typeof(T) == typeof(ulong)) return (T)(object)((ulong)(object)left - (ulong)(object)right);
            if (typeof(T) == typeof(long)) return (T)(object)((long)(object)left - (long)(object)right);
            if (typeof(T) == typeof(float)) return (T)(object)((float)(object)left - (float)(object)right);
            if (typeof(T) == typeof(double)) return (T)(object)((double)(object)left - (double)(object)right);
            if (typeof(T) == typeof(decimal)) return (T)(object)((decimal)(object)left - (decimal)(object)right);

            throw new NotSupportedException(typeof(T).Name);
        }

        public static T Multiply(T left, T right)
        {
            if (typeof(T) == typeof(byte)) return (T)(object)((byte)(object)left * (byte)(object)right);
            if (typeof(T) == typeof(sbyte)) return (T)(object)((sbyte)(object)left * (sbyte)(object)right);
            if (typeof(T) == typeof(ushort)) return (T)(object)((ushort)(object)left * (ushort)(object)right);
            if (typeof(T) == typeof(short)) return (T)(object)((short)(object)left * (short)(object)right);
            if (typeof(T) == typeof(uint)) return (T)(object)((uint)(object)left * (uint)(object)right);
            if (typeof(T) == typeof(int)) return (T)(object)((int)(object)left * (int)(object)right);
            if (typeof(T) == typeof(ulong)) return (T)(object)((ulong)(object)left * (ulong)(object)right);
            if (typeof(T) == typeof(long)) return (T)(object)((long)(object)left * (long)(object)right);
            if (typeof(T) == typeof(float)) return (T)(object)((float)(object)left * (float)(object)right);
            if (typeof(T) == typeof(double)) return (T)(object)((double)(object)left * (double)(object)right);
            if (typeof(T) == typeof(decimal)) return (T)(object)((decimal)(object)left * (decimal)(object)right);

            throw new NotSupportedException(typeof(T).Name);
        }

        public static T Divide(T left, T right)
        {
            if (typeof(T) == typeof(byte)) return (T)(object)((byte)(object)left / (byte)(object)right);
            if (typeof(T) == typeof(sbyte)) return (T)(object)((sbyte)(object)left / (sbyte)(object)right);
            if (typeof(T) == typeof(ushort)) return (T)(object)((ushort)(object)left / (ushort)(object)right);
            if (typeof(T) == typeof(short)) return (T)(object)((short)(object)left / (short)(object)right);
            if (typeof(T) == typeof(uint)) return (T)(object)((uint)(object)left / (uint)(object)right);
            if (typeof(T) == typeof(int)) return (T)(object)((int)(object)left / (int)(object)right);
            if (typeof(T) == typeof(ulong)) return (T)(object)((ulong)(object)left / (ulong)(object)right);
            if (typeof(T) == typeof(long)) return (T)(object)((long)(object)left / (long)(object)right);
            if (typeof(T) == typeof(float)) return (T)(object)((float)(object)left / (float)(object)right);
            if (typeof(T) == typeof(double)) return (T)(object)((double)(object)left / (double)(object)right);
            if (typeof(T) == typeof(decimal)) return (T)(object)((decimal)(object)left / (decimal)(object)right);

            throw new NotSupportedException(typeof(T).Name);
        }

        public static bool Equal(T left, T right)
        {
            if (typeof(T) == typeof(byte)) return (byte)(object)left == (byte)(object)right;
            if (typeof(T) == typeof(sbyte)) return (sbyte)(object)left == (sbyte)(object)right;
            if (typeof(T) == typeof(ushort)) return (ushort)(object)left == (ushort)(object)right;
            if (typeof(T) == typeof(short)) return (short)(object)left == (short)(object)right;
            if (typeof(T) == typeof(uint)) return (uint)(object)left == (uint)(object)right;
            if (typeof(T) == typeof(int)) return (int)(object)left == (int)(object)right;
            if (typeof(T) == typeof(ulong)) return (ulong)(object)left == (ulong)(object)right;
            if (typeof(T) == typeof(long)) return (long)(object)left == (long)(object)right;
            if (typeof(T) == typeof(float)) return (float)(object)left == (float)(object)right;
            if (typeof(T) == typeof(double)) return (double)(object)left == (double)(object)right;
            if (typeof(T) == typeof(decimal)) return (decimal)(object)left == (decimal)(object)right;

            throw new NotSupportedException(typeof(T).Name);
        }

        public static bool Greater(T left, T right)
        {
            if (typeof(T) == typeof(byte)) return (byte)(object)left > (byte)(object)right;
            if (typeof(T) == typeof(sbyte)) return (sbyte)(object)left > (sbyte)(object)right;
            if (typeof(T) == typeof(ushort)) return (ushort)(object)left > (ushort)(object)right;
            if (typeof(T) == typeof(short)) return (short)(object)left > (short)(object)right;
            if (typeof(T) == typeof(uint)) return (uint)(object)left > (uint)(object)right;
            if (typeof(T) == typeof(int)) return (int)(object)left > (int)(object)right;
            if (typeof(T) == typeof(ulong)) return (ulong)(object)left > (ulong)(object)right;
            if (typeof(T) == typeof(long)) return (long)(object)left > (long)(object)right;
            if (typeof(T) == typeof(float)) return (float)(object)left > (float)(object)right;
            if (typeof(T) == typeof(double)) return (double)(object)left > (double)(object)right;
            if (typeof(T) == typeof(decimal)) return (decimal)(object)left > (decimal)(object)right;

            throw new NotSupportedException(typeof(T).Name);
        }

        public static bool Less(T left, T right)
        {
            if (typeof(T) == typeof(byte)) return (byte)(object)left < (byte)(object)right;
            if (typeof(T) == typeof(sbyte)) return (sbyte)(object)left < (sbyte)(object)right;
            if (typeof(T) == typeof(ushort)) return (ushort)(object)left < (ushort)(object)right;
            if (typeof(T) == typeof(short)) return (short)(object)left < (short)(object)right;
            if (typeof(T) == typeof(uint)) return (uint)(object)left < (uint)(object)right;
            if (typeof(T) == typeof(int)) return (int)(object)left < (int)(object)right;
            if (typeof(T) == typeof(ulong)) return (ulong)(object)left < (ulong)(object)right;
            if (typeof(T) == typeof(long)) return (long)(object)left < (long)(object)right;
            if (typeof(T) == typeof(float)) return (float)(object)left < (float)(object)right;
            if (typeof(T) == typeof(double)) return (double)(object)left < (double)(object)right;
            if (typeof(T) == typeof(decimal)) return (decimal)(object)left < (decimal)(object)right;

            throw new NotSupportedException(typeof(T).Name);
        }

        public static T Negate(T value) => Subtract(Zero, value);
        public static T Abs(T value) => Less(value, Zero) ? Negate(value) : value;

        public static bool LessOrEqual(T left, T right) => !Greater(left, right);
        public static bool GreaterOrEqual(T left, T right) => !Less(left, right);
    }
}
