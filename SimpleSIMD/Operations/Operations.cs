using System;

namespace SimpleSimd
{
    public static class Operations<T> where T : unmanaged
    {
        private static readonly IOperation<T> Ops;

        static Operations()
        {
            object? opsObj = Type.GetTypeCode(typeof(T)) switch
            {
                TypeCode.SByte => new Operations.SByte(),
                TypeCode.Byte => new Operations.Byte(),
                TypeCode.Int16 => new Operations.Int16(),
                TypeCode.UInt16 => new Operations.UInt16(),
                TypeCode.Int32 => new Operations.Int32(),
                TypeCode.UInt32 => new Operations.UInt32(),
                TypeCode.Int64 => new Operations.Int64(),
                TypeCode.UInt64 => new Operations.UInt64(),
                TypeCode.Single => new Operations.Single(),
                TypeCode.Double => new Operations.Double(),
                _ => null
            };

            Ops = opsObj as IOperation<T> ?? throw new NotSupportedException(typeof(T).Name);
        }

        public static T MinVal { get; } = Ops.MinVal;
        public static T MaxVal { get; } = Ops.MaxVal;
        public static T Zero { get; } = default;
        public static T One { get; } = Converter<int, T>.Convert(1);

        public static T Neg(T value) => Sub(Zero, value);
        public static T Abs(T value) => Less(value, Zero) ? Neg(value) : value;

        public static T Add(T left, T right) => Ops.Add(left, right);
        public static T Sub(T left, T right) => Ops.Sub(left, right);
        public static T Mul(T left, T right) => Ops.Mul(left, right);
        public static T Div(T left, T right) => Ops.Div(left, right);
        public static T Mod(T left, T right) => Ops.Mod(left, right);
        public static T Min(T left, T right) => Less(left, right) ? left : right;
        public static T Max(T left, T right) => Greater(left, right) ? left : right;

        public static bool Equal(T left, T right) => Ops.Equal(left, right);
        public static bool Less(T left, T right) => Ops.Less(left, right);
        public static bool Greater(T left, T right) => Ops.Greater(left, right);
        public static bool LessEqual(T left, T right) => !Greater(left, right);
        public static bool GreaterEqual(T left, T right) => !Less(left, right);
    }
}
