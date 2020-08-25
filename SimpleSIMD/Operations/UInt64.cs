namespace SimpleSimd.Operations
{
    public sealed class UInt64 : IOperation<ulong>
    {
        public override ulong MinVal { get; } = ulong.MinValue;
        public override ulong MaxVal { get; } = ulong.MaxValue;

        public override ulong FromInt(int value) => (ulong)value;

        public override ulong Add(ulong left, ulong right) => left + right;
        public override ulong Sub(ulong left, ulong right) => left - right;
        public override ulong Mul(ulong left, ulong right) => left * right;
        public override ulong Div(ulong left, ulong right) => left / right;
        public override ulong Mod(ulong left, ulong right) => left % right;

        public override bool Equal(ulong left, ulong right) => left == right;
        public override bool Less(ulong left, ulong right) => left < right;
        public override bool Greater(ulong left, ulong right) => left > right;
    }
}
