using System;

namespace SimpleSimd
{
    public static class Ops<T> where T : unmanaged
    {
        private static readonly BaseOps<T> ops;

        static Ops()
        {
            object? obj = Type.GetTypeCode(typeof(T)) switch
            {
                TypeCode.Int32 => new Operations.IntOps(),
                TypeCode.Single => new Operations.FloatOps(),
                TypeCode.Double => new Operations.DoubleOps(),
                TypeCode.Int64 => new Operations.LongOps(),
                _ => null
            };

            ops = obj as BaseOps<T> ?? throw new NotSupportedException(typeof(T).Name);
        }

        public static T Zero { get; } = default;
        public static T One => ops.One;
        public static T MinVal => ops.MinVal;
        public static T MaxVal => ops.MaxVal;

        public static T Neg(T value) => ops.Neg(value);
        public static T FromInt(int value) => ops.FromInt(value);
        public static T Abs(T value) => Less(value, Zero) ? Neg(value) : value;

        public static T Add(T left, T right) => ops.Add(left, right);
        public static T Sub(T left, T right) => ops.Sub(left, right);
        public static T Mul(T left, T right) => ops.Mul(left, right);
        public static T Div(T left, T right) => ops.Div(left, right);
        public static T Mod(T left, T right) => ops.Mod(left, right);
        public static T Min(T left, T right) => Less(left, right) ? left : right;
        public static T Max(T left, T right) => Greater(left, right) ? left : right;

        public static bool Equal(T left, T right) => ops.Equal(left, right);
        public static bool Less(T left, T right) => ops.Less(left, right);
        public static bool Greater(T left, T right) => ops.Greater(left, right);              
        public static bool LessEqual(T left, T right) => !Greater(left, right);
        public static bool GreaterEqual(T left, T right) => !Less(left, right);
    }
}
