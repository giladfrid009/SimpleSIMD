namespace SimpleSimd.Operations
{
    public sealed class UInt32 : IOperation<uint>
    {
        public override uint MinVal { get; } = uint.MinValue;
        public override uint MaxVal { get; } = uint.MaxValue;

        public override uint FromInt(int value) => (uint)value;

        public override uint Add(uint left, uint right) => left + right;
        public override uint Sub(uint left, uint right) => left - right;
        public override uint Mul(uint left, uint right) => left * right;
        public override uint Div(uint left, uint right) => left / right;
        public override uint Mod(uint left, uint right) => left % right;

        public override bool Equal(uint left, uint right) => left == right;
        public override bool Less(uint left, uint right) => left < right;
        public override bool Greater(uint left, uint right) => left > right;
    }
}
