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
        public void GreaterEqual1()
        {
            Assert.True(Arr2.GreaterEqual(Arr1));
            Assert.True(Arr2.GreaterEqual(Arr2));
            Assert.False(Arr2.GreaterEqual(Arr3));
        }

        [Fact]
        public void GreaterEqual2()
        {
            Assert.True(Arr2.GreaterEqual(1));
            Assert.True(Arr2.GreaterEqual(2));
            Assert.False(Arr2.GreaterEqual(3));
        }

        [Fact]
        public void LessEqual1()
        {
            Assert.True(Arr2.LessEqual(Arr3));
            Assert.True(Arr2.LessEqual(Arr2));
            Assert.False(Arr2.LessEqual(Arr1));
        }

        [Fact]
        public void LessEqual2()
        {
            Assert.True(Arr2.LessEqual(3));
            Assert.True(Arr2.LessEqual(2));
            Assert.False(Arr2.LessEqual(1));
        }
    }
}
