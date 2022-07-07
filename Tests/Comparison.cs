namespace Tests;

public class Comparison_Tests
{
	static Random rnd = new(0);

	static int len = 100;
	static int lowerBound = -100;
	static int middleBound = 100;
	static int upperBound = 300;

	int[] smallerArr;
	int[] largerArr;
	int[] constArr;

	public Comparison_Tests()
	{
		smallerArr =Enumerable.Range(0, len).Select(X => rnd.Next(lowerBound, middleBound)).ToArray();
		largerArr = Enumerable.Range(0, len).Select(X => rnd.Next(middleBound, upperBound)).ToArray();
		constArr = Enumerable.Repeat(middleBound, len).ToArray();
	}

	[Fact]
	public void Equal_Test()
	{
		Assert.True(SimdOps.Equal<int>(smallerArr, smallerArr));
		Assert.True(SimdOps.Equal<int>(constArr, middleBound));

		Assert.False(SimdOps.Equal<int>(smallerArr, largerArr));
		Assert.False(SimdOps.Equal<int>(constArr, upperBound));
	}

	[Fact]
	public void Greater_Test()
	{
		Assert.True(SimdOps.Greater<int>(largerArr, smallerArr));
		Assert.True(SimdOps.Greater<int>(largerArr, lowerBound));

		Assert.False(SimdOps.Greater<int>(smallerArr, smallerArr));
		Assert.False(SimdOps.Greater<int>(constArr, middleBound));

		Assert.False(SimdOps.Greater<int>(smallerArr, largerArr));
		Assert.False(SimdOps.Greater<int>(constArr, upperBound));
	}

	[Fact]
	public void GreaterOrEqual_Test()
	{
		Assert.True(SimdOps.GreaterOrEqual<int>(largerArr, smallerArr));
		Assert.True(SimdOps.GreaterOrEqual<int>(largerArr, lowerBound));

		Assert.True(SimdOps.GreaterOrEqual<int>(smallerArr, smallerArr));
		Assert.True(SimdOps.GreaterOrEqual<int>(constArr, middleBound));

		Assert.False(SimdOps.GreaterOrEqual<int>(smallerArr, largerArr));
		Assert.False(SimdOps.GreaterOrEqual<int>(constArr, upperBound));
	}

	[Fact]
	public void Less_Test()
	{
		Assert.False(SimdOps.Less<int>(largerArr, smallerArr));
		Assert.False(SimdOps.Less<int>(largerArr, lowerBound));

		Assert.False(SimdOps.Less<int>(smallerArr, smallerArr));
		Assert.False(SimdOps.Less<int>(constArr, middleBound));

		Assert.True(SimdOps.Less<int>(smallerArr, largerArr));
		Assert.True(SimdOps.Less<int>(constArr, upperBound));
	}

	[Fact]
	public void LessOrEqual_Test()
	{
		Assert.False(SimdOps.LessOrEqual<int>(largerArr, smallerArr));
		Assert.False(SimdOps.LessOrEqual<int>(largerArr, lowerBound));

		Assert.True(SimdOps.LessOrEqual<int>(smallerArr, smallerArr));
		Assert.True(SimdOps.LessOrEqual<int>(constArr, middleBound));

		Assert.True(SimdOps.LessOrEqual<int>(smallerArr, largerArr));
		Assert.True(SimdOps.LessOrEqual<int>(constArr, upperBound));
	}
}