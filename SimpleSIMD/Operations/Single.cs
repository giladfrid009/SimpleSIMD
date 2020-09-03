namespace SimpleSimd.Operations
{
    public sealed class Single : IOperation<float>
    {
        public override float MinVal { get; } = float.MinValue;
        public override float MaxVal { get; } = float.MaxValue;

        public override float Add(float left, float right) => left + right;
        public override float Subtract(float left, float right) => left - right;
        public override float Multiply(float left, float right) => left * right;
        public override float Divide(float left, float right) => left / right;

        public override bool Equal(float left, float right) => left == right;
        public override bool Less(float left, float right) => left < right;
        public override bool Greater(float left, float right) => left > right;
    }
}
