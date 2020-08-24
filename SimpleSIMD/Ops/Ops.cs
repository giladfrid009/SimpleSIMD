using System;

namespace SimpleSimd
{
    public static class Ops<T> where T : unmanaged
    {
        private static readonly BaseOps<T> baseOps;

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

            baseOps = obj as BaseOps<T> ?? throw new NotSupportedException(typeof(T).Name);
        }

        public static T Zero => default;
        public static T One => baseOps.One;
        public static T MinVal => baseOps.MinVal;
        public static T MaxVal => baseOps.MaxVal;

        public static T Neg(T value) => baseOps.Neg(value);
        public static T FromInt(int value) => baseOps.FromInt(value);
        public static T Abs(T value) => Less(value, Zero) ? Neg(value) : value;

        public static T Add(T left, T right) => baseOps.Add(left, right);
        public static T Sub(T left, T right) => baseOps.Sub(left, right);
        public static T Mul(T left, T right) => baseOps.Mul(left, right);
        public static T Div(T left, T right) => baseOps.Div(left, right);
        public static T Mod(T left, T right) => baseOps.Mod(left, right);
        public static T Min(T left, T right) => Less(left, right) ? left : right;
        public static T Max(T left, T right) => Greater(left, right) ? left : right;

        public static bool Equal(T left, T right) => baseOps.Equal(left, right);
        public static bool Less(T left, T right) => baseOps.Less(left, right);
        public static bool Greater(T left, T right) => baseOps.Greater(left, right);              
        public static bool LessEqual(T left, T right) => !Greater(left, right);
        public static bool GreaterEqual(T left, T right) => !Less(left, right);
    }
}
