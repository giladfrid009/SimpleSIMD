namespace SimpleSimd.Operations
{
    public sealed class UInt32 : IOperation<uint>
    {
        public override uint MinVal { get; } = uint.MinValue;
        public override uint MaxVal { get; } = uint.MaxValue;

        public override uint Add(uint left, uint right) => left + right;
        public override uint Subtract(uint left, uint right) => left - right;
        public override uint Multiply(uint left, uint right) => left * right;
        public override uint Divide(uint left, uint right) => left / right;

        public override bool Equal(uint left, uint right) => left == right;
        public override bool Less(uint left, uint right) => left < right;
        public override bool Greater(uint left, uint right) => left > right;
    }
}
