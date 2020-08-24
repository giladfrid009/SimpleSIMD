using System;

namespace SimpleSimd.Operations
{
    [Serializable]
    public class FloatOps : BaseOps<float>
    {
        public override float One { get; } = 1f;
        public override float MinVal { get; } = float.MinValue;
        public override float MaxVal { get; } = float.MaxValue;

        public override float Neg(float value) => -value;
        public override float FromInt(int value) => value;

        public override float Add(float left, float right) => left + right;
        public override float Sub(float left, float right) => left - right;
        public override float Mul(float left, float right) => left * right;
        public override float Div(float left, float right) => left / right;
        public override float Mod(float left, float right) => left % right;

        public override bool Equal(float left, float right) => left == right;
        public override bool Less(float left, float right) => left < right;
        public override bool Greater(float left, float right) => left > right;
    }
}
