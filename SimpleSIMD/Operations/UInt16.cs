namespace SimpleSimd.Operations
{
    public sealed class UInt16 : IOperation<ushort>
    {
        public override ushort MinVal { get; } = ushort.MinValue;
        public override ushort MaxVal { get; } = ushort.MaxValue;

        public override ushort FromInt(int value) => (ushort)value;

        public override ushort Add(ushort left, ushort right) => (ushort)(left + right);
        public override ushort Sub(ushort left, ushort right) => (ushort)(left - right);
        public override ushort Mul(ushort left, ushort right) => (ushort)(left * right);
        public override ushort Div(ushort left, ushort right) => (ushort)(left / right);
        public override ushort Mod(ushort left, ushort right) => (ushort)(left % right);

        public override bool Equal(ushort left, ushort right) => left == right;
        public override bool Less(ushort left, ushort right) => left < right;
        public override bool Greater(ushort left, ushort right) => left > right;
    }
}
