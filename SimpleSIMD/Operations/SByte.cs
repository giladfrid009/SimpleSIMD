namespace SimpleSimd.Operations
{
    public sealed class SByte : IOperation<sbyte>
    {
        public override sbyte MinVal { get; } = sbyte.MinValue;
        public override sbyte MaxVal { get; } = sbyte.MaxValue;

        public override sbyte FromInt(int value) => (sbyte)value;

        public override sbyte Add(sbyte left, sbyte right) => (sbyte)(left + right);
        public override sbyte Sub(sbyte left, sbyte right) => (sbyte)(left - right);
        public override sbyte Mul(sbyte left, sbyte right) => (sbyte)(left * right);
        public override sbyte Div(sbyte left, sbyte right) => (sbyte)(left / right);
        public override sbyte Mod(sbyte left, sbyte right) => (sbyte)(left % right);

        public override bool Equal(sbyte left, sbyte right) => left == right;
        public override bool Less(sbyte left, sbyte right) => left < right;
        public override bool Greater(sbyte left, sbyte right) => left > right;
    }
}
