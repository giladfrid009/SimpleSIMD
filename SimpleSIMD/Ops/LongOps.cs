using System;

namespace SimpleSimd.Operations
{
    [Serializable]
    public class LongOps : BaseOps<long>
    {
        public override long One { get; } = 1L;
        public override long MinVal { get; } = long.MinValue;
        public override long MaxVal { get; } = long.MaxValue;

        public override long Neg(long value) => -value;
        public override long FromInt(int value) => value;

        public override long Add(long left, long right) => left + right;
        public override long Sub(long left, long right) => left - right;
        public override long Mul(long left, long right) => left * right;
        public override long Div(long left, long right) => left / right;
        public override long Mod(long left, long right) => left % right;

        public override bool Equal(long left, long right) => left == right;
        public override bool Less(long left, long right) => left < right;
        public override bool Greater(long left, long right) => left > right;
    }
}
