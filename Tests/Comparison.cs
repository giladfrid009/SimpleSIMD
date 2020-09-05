using SimpleSimd;
using Xunit;
using static Tests.TestArrs<int>;

namespace Tests
{
    public class Comparison
    {
        [Fact]
        public void Equals1()
        {
            Assert.True(ArrayOps<int>.Equals(Arr2, Arr2));
            Assert.False(ArrayOps<int>.Equals(Arr1, Arr2));
        }

        [Fact]
        public void Equals2()
        {
            Assert.True(ArrayOps<int>.Equals(Arr2, 2));
            Assert.False(ArrayOps<int>.Equals(Arr2, 2));
        }

        [Fact]
        public void Greater1()
        {
            Assert.True(ArrayOps<int>.Greater(Arr2, Arr1));
            Assert.False(ArrayOps<int>.Greater(Arr2, Arr3));
            Assert.False(ArrayOps<int>.Greater(Arr2, Arr2));
        }

        [Fact]
        public void Greater2()
        {
            Assert.True(ArrayOps<int>.Greater(Arr2, 1));
            Assert.False(ArrayOps<int>.Greater(Arr2, 3));
            Assert.False(ArrayOps<int>.Greater(Arr2, 2));
        }

        [Fact]
        public void Less1()
        {
            Assert.True(ArrayOps<int>.Less(Arr2, Arr3));
            Assert.False(ArrayOps<int>.Less(Arr2, Arr1));
            Assert.False(ArrayOps<int>.Less(Arr2, Arr2));
        }

        [Fact]
        public void Less2()
        {
            Assert.True(ArrayOps<int>.Less(Arr2, 3));
            Assert.False(ArrayOps<int>.Less(Arr2, 1));
            Assert.False(ArrayOps<int>.Less(Arr2, 2));
        }

        [Fact]
        public void GreaterOrEqual1()
        {
            Assert.True(ArrayOps<int>.GreaterOrEqual(Arr2, Arr1));
            Assert.True(ArrayOps<int>.GreaterOrEqual(Arr2, Arr2));
            Assert.False(ArrayOps<int>.GreaterOrEqual(Arr2, Arr3));
        }

        [Fact]
        public void GreaterOrEqual2()
        {
            Assert.True(ArrayOps<int>.GreaterOrEqual(Arr2, 1));
            Assert.True(ArrayOps<int>.GreaterOrEqual(Arr2, 2));
            Assert.False(ArrayOps<int>.GreaterOrEqual(Arr2, 3));
        }

        [Fact]
        public void LessOrEqual1()
        {
            Assert.True(ArrayOps<int>.LessOrEqual(Arr2, Arr3));
            Assert.True(ArrayOps<int>.LessOrEqual(Arr2, Arr2));
            Assert.False(ArrayOps<int>.LessOrEqual(Arr2, Arr1));
        }

        [Fact]
        public void LessOrEqual2()
        {
            Assert.True(ArrayOps<int>.LessOrEqual(Arr2, 3));
            Assert.True(ArrayOps<int>.LessOrEqual(Arr2, 2));
            Assert.False(ArrayOps<int>.LessOrEqual(Arr2, 1));
        }
    }
}
