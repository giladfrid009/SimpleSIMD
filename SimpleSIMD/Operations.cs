using System;
using System.Linq.Expressions;

namespace SimpleSimd
{

    public static class Operations<T> where T : unmanaged
    {
        private static readonly IOperation<T> ops;

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

            ops = opsObj as IOperation<T> ?? throw new NotSupportedException(typeof(T).Name);
        }

        public static T Zero { get; } = Operations<int, T>.Convert(0);
        public static T One { get; } = Operations<int, T>.Convert(1);
        public static T MinVal => ops.MinVal;
        public static T MaxVal => ops.MaxVal;

        public static T Neg(T value) => Sub(Zero, value);
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

    public static class Operations<TIn, TOut> where TIn : unmanaged where TOut : unmanaged
    {
        private static readonly Func<TIn, TOut> ConvFunc;

        static Operations()
        {
            ParameterExpression X = Expression.Parameter(typeof(TIn));
            ConvFunc = Expression.Lambda<Func<TIn, TOut>>(Expression.Convert(X, typeof(TOut)), X).Compile();
        }

        public static TOut Convert(TIn value) => ConvFunc(value);
    }
}
