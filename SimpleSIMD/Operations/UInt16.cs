namespace SimpleSimd.Operations
{
    public sealed class UInt16 : IOperation<ushort>
    {
        public override ushort MinVal { get; } = ushort.MinValue;
        public override ushort MaxVal { get; } = ushort.MaxValue;

        public override ushort Add(ushort left, ushort right) => (ushort)(left + right);
        public override ushort Subtract(ushort left, ushort right) => (ushort)(left - right);
        public override ushort Multiply(ushort left, ushort right) => (ushort)(left * right);
        public override ushort Divide(ushort left, ushort right) => (ushort)(left / right);

        public override bool Equal(ushort left, ushort right) => left == right;
        public override bool Less(ushort left, ushort right) => left < right;
        public override bool Greater(ushort left, ushort right) => left > right;
    }
}
