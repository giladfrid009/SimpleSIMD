using SimpleSimd;
using Xunit;
using static Tests.TestArrs<int>;

namespace Tests
{
    public class Comparison
    {
        [Fact]
        public void Equal1()
        {
            Assert.True(Arr2.Equal(Arr2));
            Assert.False(Arr1.Equal(Arr2));
        }

        [Fact]
        public void Equal2()
        {
            Assert.True(Arr2.Equal(2));
            Assert.False(Arr1.Equal(2));
        }

        [Fact]
        public void Greater1()
        {
            Assert.True(Arr2.Greater(Arr1));
            Assert.False(Arr2.Greater(Arr3));
            Assert.False(Arr2.Greater(Arr2));
        }

        [Fact]
        public void Greater2()
        {
            Assert.True(Arr2.Greater(1));
            Assert.False(Arr2.Greater(3));
            Assert.False(Arr2.Greater(2));
        }

        [Fact]
        public void Less1()
        {
            Assert.True(Arr2.Less(Arr3));
            Assert.False(Arr2.Less(Arr1));
            Assert.False(Arr2.Less(Arr2));
        }

        [Fact]
        public void Less2()
        {
            Assert.True(Arr2.Less(3));
            Assert.False(Arr2.Less(1));
            Assert.False(Arr2.Less(2));
        }

        [Fact]
        public void GreaterOrEqual1()
        {
            Assert.True(Arr2.GreaterOrEqual(Arr1));
            Assert.True(Arr2.GreaterOrEqual(Arr2));
            Assert.False(Arr2.GreaterOrEqual(Arr3));
        }

        [Fact]
        public void GreaterOrEqual2()
        {
            Assert.True(Arr2.GreaterOrEqual(1));
            Assert.True(Arr2.GreaterOrEqual(2));
            Assert.False(Arr2.GreaterOrEqual(3));
        }

        [Fact]
        public void LessOrEqual1()
        {
            Assert.True(Arr2.LessOrEqual(Arr3));
            Assert.True(Arr2.LessOrEqual(Arr2));
            Assert.False(Arr2.LessOrEqual(Arr1));
        }

        [Fact]
        public void LessOrEqual2()
        {
            Assert.True(Arr2.LessOrEqual(3));
            Assert.True(Arr2.LessOrEqual(2));
            Assert.False(Arr2.LessOrEqual(1));
        }
    }
}
