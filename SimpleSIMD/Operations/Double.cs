namespace SimpleSimd.Operations
{
    public sealed class Double : IOperation<double>
    {
        public override double MinVal { get; } = double.MinValue;
        public override double MaxVal { get; } = double.MaxValue;

        public override double Add(double left, double right) => left + right;
        public override double Subtract(double left, double right) => left - right;
        public override double Multiply(double left, double right) => left * right;
        public override double Divide(double left, double right) => left / right;

        public override bool Equal(double left, double right) => left == right;
        public override bool Less(double left, double right) => left < right;
        public override bool Greater(double left, double right) => left > right;
    }
}
