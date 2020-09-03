namespace SimpleSimd.Operations
{
    public sealed class Int64 : IOperation<long>
    {
        public override long MinVal { get; } = long.MinValue;
        public override long MaxVal { get; } = long.MaxValue;

        public override long Add(long left, long right) => left + right;
        public override long Subtract(long left, long right) => left - right;
        public override long Multiply(long left, long right) => left * right;
        public override long Divide(long left, long right) => left / right;

        public override bool Equal(long left, long right) => left == right;
        public override bool Less(long left, long right) => left < right;
        public override bool Greater(long left, long right) => left > right;
    }
}
