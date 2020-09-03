namespace SimpleSimd.Operations
{
    public sealed class Byte : IOperation<byte>
    {
        public override byte MinVal { get; } = byte.MinValue;
        public override byte MaxVal { get; } = byte.MaxValue;

        public override byte Add(byte left, byte right) => (byte)(left + right);
        public override byte Subtract(byte left, byte right) => (byte)(left - right);
        public override byte Multiply(byte left, byte right) => (byte)(left * right);
        public override byte Divide(byte left, byte right) => (byte)(left / right);

        public override bool Equal(byte left, byte right) => left == right;
        public override bool Less(byte left, byte right) => left < right;
        public override bool Greater(byte left, byte right) => left > right;
    }
}
