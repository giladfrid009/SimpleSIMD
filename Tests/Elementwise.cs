using System.Numerics;

namespace Tests;
public class Elementwise_Tests
{
	private static readonly Random rnd = new(0);
	private static readonly int len = 100;
	private static readonly int lowerBound = -100;
	private static readonly int middleBound = 0;
	private static readonly int upperBound = 100;
	private readonly int[] negArr;
	private readonly int[] posArr;
	private readonly int[] zeroArr;

	public Elementwise_Tests()
	{
		negArr = Enumerable.Range(0, len).Select(X => rnd.Next(lowerBound, middleBound - 1)).ToArray();
		posArr = Enumerable.Range(0, len).Select(X => rnd.Next(middleBound + 1, upperBound)).ToArray();
		zeroArr = Enumerable.Repeat(middleBound, len).ToArray();
	}

	[Fact]
	public void Abs_Test()
	{
		Assert.True(Enumerable.SequenceEqual(negArr.Select(Math.Abs), SimdOps.Abs<int>(negArr)));

		int[] res = new int[len];

		SimdOps.Abs<int>(negArr, res);
		Assert.True(Enumerable.SequenceEqual(negArr.Select(Math.Abs), res));
	}

	[Fact]
	public void Negate_Test()
	{
		Assert.True(Enumerable.SequenceEqual(negArr.Select(X => -X), SimdOps.Negate<int>(negArr)));

		int[] res = new int[len];

		SimdOps.Negate<int>(negArr, res);
		Assert.True(Enumerable.SequenceEqual(negArr.Select(X => -X), res));
	}

	[Fact]
	public void Sqrt_Test()
	{
		double[] posArrDouble = posArr.Select(X => (double)X).ToArray();

		Assert.True(Enumerable.SequenceEqual(posArrDouble.Select(Math.Sqrt), SimdOps.Sqrt<double>(posArrDouble)));

		double[] res = new double[len];

		SimdOps.Sqrt<double>(posArrDouble, res);
		Assert.True(Enumerable.SequenceEqual(posArrDouble.Select(Math.Sqrt), res));
	}

	[Fact]
	public void Add_Test()
	{
		Assert.True(Enumerable.SequenceEqual(SimdOps.Add<int>(negArr, posArr), negArr.Zip(posArr, (N, P) => N + P)));

		Assert.True(Enumerable.SequenceEqual(SimdOps.Add<int>(posArr, upperBound), posArr.Select(X => X + upperBound)));

		int[] res = new int[len];

		SimdOps.Add<int>(negArr, posArr, res);
		Assert.True(Enumerable.SequenceEqual(res, negArr.Zip(posArr, (N, P) => N + P)));

		SimdOps.Add<int>(posArr, upperBound, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X + upperBound)));
	}

	[Fact]
	public void Subtract_Test()
	{
		Assert.True(Enumerable.SequenceEqual(SimdOps.Subtract<int>(negArr, posArr), negArr.Zip(posArr, (N, P) => N - P)));

		Assert.True(Enumerable.SequenceEqual(SimdOps.Subtract<int>(posArr, upperBound), posArr.Select(X => X - upperBound)));

		int[] res = new int[len];

		SimdOps.Subtract<int>(negArr, posArr, res);
		Assert.True(Enumerable.SequenceEqual(res, negArr.Zip(posArr, (N, P) => N - P)));

		SimdOps.Subtract<int>(posArr, upperBound, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X - upperBound)));
	}

	[Fact]
	public void Multiply_Test()
	{
		Assert.True(Enumerable.SequenceEqual(SimdOps.Multiply<int>(negArr, posArr), negArr.Zip(posArr, (N, P) => N * P)));

		Assert.True(Enumerable.SequenceEqual(SimdOps.Multiply<int>(posArr, upperBound), posArr.Select(X => X * upperBound)));

		int[] res = new int[len];

		SimdOps.Multiply<int>(negArr, posArr, res);
		Assert.True(Enumerable.SequenceEqual(res, negArr.Zip(posArr, (N, P) => N * P)));

		SimdOps.Multiply<int>(posArr, upperBound, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X * upperBound)));
	}

	[Fact]
	public void Divide_Test()
	{
		Assert.True(Enumerable.SequenceEqual(SimdOps.Divide<int>(negArr, posArr), negArr.Zip(posArr, (N, P) => N / P)));

		Assert.True(Enumerable.SequenceEqual(SimdOps.Divide<int>(posArr, upperBound), posArr.Select(X => X / upperBound)));

		int[] res = new int[len];

		SimdOps.Divide<int>(negArr, posArr, res);
		Assert.True(Enumerable.SequenceEqual(res, negArr.Zip(posArr, (N, P) => N / P)));

		SimdOps.Divide<int>(posArr, upperBound, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X / upperBound)));
	}

	[Fact]
	public void Not_Test()
	{
		Assert.True(Enumerable.SequenceEqual(SimdOps.Not<int>(negArr), negArr.Select(X => ~X)));

		int[] res = new int[len];

		SimdOps.Not<int>(negArr, res);
		Assert.True(Enumerable.SequenceEqual(res, negArr.Select(X => ~X)));
	}

	[Fact]
	public void And_Test()
	{
		Assert.True(Enumerable.SequenceEqual(SimdOps.And<int>(negArr, posArr), negArr.Zip(posArr, (N, P) => N & P)));

		Assert.True(Enumerable.SequenceEqual(SimdOps.And<int>(posArr, upperBound), posArr.Select(X => X & upperBound)));

		int[] res = new int[len];

		SimdOps.And<int>(negArr, posArr, res);
		Assert.True(Enumerable.SequenceEqual(res, negArr.Zip(posArr, (N, P) => N & P)));

		SimdOps.And<int>(posArr, upperBound, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X & upperBound)));
	}

	[Fact]
	public void Or_Test()
	{
		Assert.True(Enumerable.SequenceEqual(SimdOps.Or<int>(negArr, posArr), negArr.Zip(posArr, (N, P) => N | P)));

		Assert.True(Enumerable.SequenceEqual(SimdOps.Or<int>(posArr, upperBound), posArr.Select(X => X | upperBound)));

		int[] res = new int[len];

		SimdOps.Or<int>(negArr, posArr, res);
		Assert.True(Enumerable.SequenceEqual(res, negArr.Zip(posArr, (N, P) => N | P)));

		SimdOps.Or<int>(posArr, upperBound, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X | upperBound)));
	}

	[Fact]
	public void Xor_Test()
	{
		Assert.True(Enumerable.SequenceEqual(SimdOps.Xor<int>(negArr, posArr), negArr.Zip(posArr, (N, P) => N ^ P)));

		Assert.True(Enumerable.SequenceEqual(SimdOps.Xor<int>(posArr, upperBound), posArr.Select(X => X ^ upperBound)));

		int[] res = new int[len];

		SimdOps.Xor<int>(negArr, posArr, res);
		Assert.True(Enumerable.SequenceEqual(res, negArr.Zip(posArr, (N, P) => N ^ P)));

		SimdOps.Xor<int>(posArr, upperBound, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X ^ upperBound)));
	}

	[Fact]
	public void Concat_Test()
	{
		int[] res = new int[len];

		SimdOps.Concat<int, int>(posArr, negArr, (L, R) => L + 3 * R, (L, R) => L + 3 * R, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Zip(negArr, (L, R) => L + 3 * R)));

		SimdOps.Concat<int, int>(posArr, upperBound, (L, R) => L + 3 * R, (L, R) => L + 3 * R, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X + 3 * upperBound)));

		SimdOps.Concat<int, int>(upperBound, posArr, (L, R) => L + 3 * R, (L, R) => L + 3 * R, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => upperBound + 3 * X)));
	}

	[Fact]
	public void Select_Test()
	{
		int[] res = new int[len];

		Vector<int> mask = new(1010);

		SimdOps.Select<int, int>(posArr, X => -X * 2 + X | mask, X => -X * 2 + X | 1010, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => -X * 2 + X | 1010)));
	}

	[Fact]
	public void Ternary_Test()
	{
		int[] res = new int[len];

		Vector<int> mask = new(1010);

		SimdOps.Ternary(posArr, X => Vector.GreaterThan(X, new Vector<int>(50)), X => X > 50, 1, 0, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X > 50 ? 1 : 0)));

		SimdOps.Ternary<int>(posArr, X => Vector.GreaterThan(X, new Vector<int>(50)), X => 2 * X, X => 3 * X, X => X > 50, X => 2 * X, X => 3 * X, res);
		Assert.True(Enumerable.SequenceEqual(res, posArr.Select(X => X > 50 ? 2 * X : 3 * X)));
	}
}
