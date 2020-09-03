namespace SimpleSimd.Operations
{
    public sealed class SByte : IOperation<sbyte>
    {
        public override sbyte MinVal { get; } = sbyte.MinValue;
        public override sbyte MaxVal { get; } = sbyte.MaxValue;

        public override sbyte Add(sbyte left, sbyte right) => (sbyte)(left + right);
        public override sbyte Subtract(sbyte left, sbyte right) => (sbyte)(left - right);
        public override sbyte Multiply(sbyte left, sbyte right) => (sbyte)(left * right);
        public override sbyte Divide(sbyte left, sbyte right) => (sbyte)(left / right);

        public override bool Equal(sbyte left, sbyte right) => left == right;
        public override bool Less(sbyte left, sbyte right) => left < right;
        public override bool Greater(sbyte left, sbyte right) => left > right;
    }
}
