namespace SimpleSimd.Operations
{
    public sealed class Int32 : IOperation<int>
    {
        public override int MinVal { get; } = int.MinValue;
        public override int MaxVal { get; } = int.MaxValue;

        public override int Add(int left, int right) => left + right;
        public override int Subtract(int left, int right) => left - right;
        public override int Multiply(int left, int right) => left * right;
        public override int Divide(int left, int right) => left / right;

        public override bool Equal(int left, int right) => left == right;
        public override bool Less(int left, int right) => left < right;
        public override bool Greater(int left, int right) => left > right;
    }
}
