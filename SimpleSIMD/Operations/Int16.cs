namespace SimpleSimd.Operations
{
    public sealed class Int16 : IOperation<short>
    {
        public override short MinVal { get; } = short.MinValue;
        public override short MaxVal { get; } = short.MaxValue;

        public override short Add(short left, short right) => (short)(left + right);
        public override short Subtract(short left, short right) => (short)(left - right);
        public override short Multiply(short left, short right) => (short)(left * right);
        public override short Divide(short left, short right) => (short)(left / right);

        public override bool Equal(short left, short right) => left == right;
        public override bool Less(short left, short right) => left < right;
        public override bool Greater(short left, short right) => left > right;
    }
}
