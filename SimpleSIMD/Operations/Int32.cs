namespace SimpleSimd.Operations
{
    public sealed class Int32 : IOperation<int>
    {
        public override int MinVal { get; } = int.MinValue;
        public override int MaxVal { get; } = int.MaxValue;

        public override int FromInt(int value) => value;

        public override int Add(int left, int right) => left + right;
        public override int Sub(int left, int right) => left - right;
        public override int Mul(int left, int right) => left * right;
        public override int Div(int left, int right) => left / right;
        public override int Mod(int left, int right) => left % right;

        public override bool Equal(int left, int right) => left == right;
        public override bool Less(int left, int right) => left < right;
        public override bool Greater(int left, int right) => left > right;
    }
}
