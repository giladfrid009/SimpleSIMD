using System;
using System.Linq.Expressions;

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

        public static T Zero { get; } = default;
        public static T MinVal => Ops.MinVal;
        public static T MaxVal => Ops.MaxVal;

        public static T Negate(T value) => Subtract(Zero, value);
        public static T Abs(T value) => Less(value, Zero) ? Negate(value) : value;

        public static T Add(T left, T right) => Ops.Add(left, right);
        public static T Subtract(T left, T right) => Ops.Subtract(left, right);
        public static T Multiply(T left, T right) => Ops.Multiply(left, right);
        public static T Divide(T left, T right) => Ops.Divide(left, right);
        public static T Min(T left, T right) => Less(left, right) ? left : right;
        public static T Max(T left, T right) => Greater(left, right) ? left : right;

        public static bool Equal(T left, T right) => Ops.Equal(left, right);
        public static bool Less(T left, T right) => Ops.Less(left, right);
        public static bool Greater(T left, T right) => Ops.Greater(left, right);
        public static bool LessOrEqual(T left, T right) => !Greater(left, right);
        public static bool GreaterOrEqual(T left, T right) => !Less(left, right);
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
