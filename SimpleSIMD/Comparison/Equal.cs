using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps<T>
	{
		private struct Equal_VSelector : IFunc<Vector<T>, Vector<T>, bool>
		{
			public bool Invoke(Vector<T> left, Vector<T> right)
			{
				return Vector.EqualsAll(left, right);
			}
		}

		private struct Equal_Selector : IFunc<T, T, bool>
		{
			public bool Invoke(T left, T right)
			{
				return NumOps<T>.Equal(left, right);
			}
		}

		public static bool Equal(ReadOnlySpan<T> left, T right)
		{
			return All(left, right, new Equal_VSelector(), new Equal_Selector());
		}

		public static bool Equal(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return All(left, right, new Equal_VSelector(), new Equal_Selector());
		}
	}
}
