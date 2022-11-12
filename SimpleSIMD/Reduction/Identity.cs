namespace SimpleSimd;

public static partial class SimdOps
{
	private struct ID_VSelector<T> : IFunc<Vector<T>, Vector<T>> where T : struct, INumber<T>
	{
		public Vector<T> Invoke(Vector<T> vec)
		{
			return vec;
		}
	}

	private struct ID_Selector<T> : IFunc<T, T> where T : struct, INumber<T>
	{
		public T Invoke(T val)
		{
			return val;
		}
	}
}
