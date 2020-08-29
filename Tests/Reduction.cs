using SimpleSimd;
using Xunit;
using static Tests.TestArrs<int>;

namespace Tests
{
    public class Reduction
    {
        [Fact]
        public void Sum1() => Assert.True(Arr1.Sum() == Length);

        [Fact]
        public void Sum2() => Assert.True(Arr1.Sum(X => X * 2, X => X * 2) == Length * 2);

        [Fact]
        public void Avg1() => Assert.True(ArrAsc.Avg() == (ArrAsc[Length - 1] + ArrAsc[0]) / 2);

        [Fact]
        public void Avg2() => Assert.True(ArrAsc.Avg(X => X * 2, X => X * 2) == (ArrAsc[Length - 1] + ArrAsc[0]));

        [Fact]
        public void Dot1() => Assert.True(Arr2.Dot(Arr3) == Length * 6);

        [Fact]
        public void Dot2() => Assert.True(Arr2.Dot(3) == Length * 6);

        [Fact]
        public void Max1() => Assert.True(ArrAsc.Max() == Length);

        [Fact]
        public void Max2() => Assert.True(ArrAsc.Max(X => X * 2, X => X * 2) == Length * 2);

        [Fact]
        public void Min1() => Assert.True(ArrAsc.Min() == 1);

        [Fact]
        public void Min2() => Assert.True(ArrAsc.Min(X => X * 2, X => X * 2) == 2);

        [Fact]
        public void Accumulate() => Assert.True(Arr1.Accumulate(0, (R, X) => R + X, (R, X) => R + X) == Length);
    }
}
