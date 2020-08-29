using SimpleSimd;
using System.Numerics;
using Xunit;
using static Tests.TestArrs<int>;

namespace Tests
{
    public class General
    {        
        [Fact]
        public void Any()
        {
            var vOne = new Vector<int>(1);
            var vTwo = new Vector<int>(2);

            Assert.True(ArrAsc.Any(X => Vector.LessThanAll(X, vOne), X => X < 1) == false);
            Assert.True(ArrAsc.Any(X => Vector.LessThanAny(X, vTwo), X => X < 2) == true);
        }

        [Fact]
        public void All()
        {
            var vOne = new Vector<int>(1);

            Assert.True(ArrAsc.All(X => Vector.LessThanAll(X, vOne), X => X < 1) == false);
            Assert.True(ArrAsc.All(X => Vector.GreaterThanOrEqualAll(X, vOne), X => X  >= 1) == true);
        }

        [Fact]
        public void Fill()
        {
            var arr = new int[Length];
            var arrOther = new int[Length];

            for (int i = 0; i < Length; i++)
            {
                arr[i] = Arr1[0];
            }

            arrOther.Fill(Arr1[0]);

            Assert.True(arr.Equal(arrOther));
        }

        [Fact]
        public void Foreach1()
        {
            Vector<int> vRes = Vector<int>.Zero;
            int res = 0;

            Arr1.Foreach(X => vRes += X, X => res += X);

            res += Vector.Dot(vRes, Vector<int>.One);

            Assert.True(res == Length);
        }

        [Fact]
        public void Foreach2()
        {
            Vector<int> vRes = Vector<int>.Zero;
            int res = 0;

            Arr1.Foreach((X, i) => vRes += X + Arr2.ToVector(i), (X, i) => res += X + Arr2[i]);

            res += Vector.Dot(vRes, Vector<int>.One);

            Assert.True(res == Length * 3);
        }

        [Fact]
        public void ToVector()
        {
            Assert.True(Arr1.ToVector(1).Equals(new Vector<int>(Arr1, 1)));
        }
    }
}
