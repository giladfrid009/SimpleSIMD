namespace SimpleSimd.Operations
{
    public sealed class UInt64 : IOperation<ulong>
    {
        public override ulong MinVal { get; } = ulong.MinValue;
        public override ulong MaxVal { get; } = ulong.MaxValue;

        public override ulong Add(ulong left, ulong right) => left + right;
        public override ulong Subtract(ulong left, ulong right) => left - right;
        public override ulong Multiply(ulong left, ulong right) => left * right;
        public override ulong Divide(ulong left, ulong right) => left / right;

        public override bool Equal(ulong left, ulong right) => left == right;
        public override bool Less(ulong left, ulong right) => left < right;
        public override bool Greater(ulong left, ulong right) => left > right;
    }
}
