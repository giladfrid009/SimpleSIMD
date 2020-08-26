namespace SimpleSimd.Operations
{
    public sealed class Double : IOperation<double>
    {
        public override double MinVal { get; } = double.MinValue;
        public override double MaxVal { get; } = double.MaxValue;

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
