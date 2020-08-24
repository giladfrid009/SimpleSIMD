using System;

namespace SimpleSimd.Operations
{
    [Serializable]
    public class DoubleOps : BaseOps<double>
    {
        public override double One { get; } = 1.0;
        public override double MinVal { get; } = double.MinValue;
        public override double MaxVal { get; } = double.MaxValue;

        public override double Neg(double value) => -value;
        public override double FromInt(int value) => value;

        public override double Add(double left, double right) => left + right;
        public override double Sub(double left, double right) => left - right;
        public override double Mul(double left, double right) => left * right;
        public override double Div(double left, double right) => left / right;
        public override double Mod(double left, double right) => left % right;

        public override bool Equal(double left, double right) => left == right;
        public override bool Less(double left, double right) => left < right;
        public override bool Greater(double left, double right) => left > right;
    }
}
