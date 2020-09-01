using SimpleSimd;
using System.Linq;
using Xunit;
using static Tests.TestArrs<int>;

namespace Tests
{
    public class Elementwise
    {
        [Fact]
        public void Abs() => Assert.True(Arr1N.Abs().Equal(Arr1));

        [Fact]
        public void Neg() => Assert.True(Arr1.Neg().Equal(Arr1N));

        [Fact]
        public void Add1() => Assert.True(Arr1.Add(Arr2).Equal(Arr3));

        [Fact]
        public void Add2() => Assert.True(Arr1.Add(2).Equal(Arr3));

        [Fact]
        public void Sub1() => Assert.True(Arr3.Sub(Arr2).Equal(Arr1));

        [Fact]
        public void Sub2() => Assert.True(Arr3.Sub(2).Equal(Arr1));

        [Fact]
        public void Mul1() => Assert.True(Arr2.Mul(Arr3).Equal(Arr6));

        [Fact]
        public void Mul2() => Assert.True(Arr2.Mul(3).Equal(Arr6));

        [Fact]
        public void Div1() => Assert.True(Arr6.Div(Arr3).Equal(Arr2));

        [Fact]
        public void Div2() => Assert.True(Arr6.Div(3).Equal(Arr2));

        [Fact]
        public void Select1() => Assert.True(Arr1.Select(X => X * 2, X => X * 2).Equal(Arr2));

        [Fact]
        public void Select2() => Assert.True(Arr6.Select((X, i) => X / Arr3.ToVector(i), (X, i) => X / Arr3[i]).Equal(Arr2));

        [Fact]
        public void Concat() => Assert.True(Arr1.Concat(Arr2, (X, Y) => X + Y, (X, Y) => X + Y).Equal(Arr3));
    }
}
