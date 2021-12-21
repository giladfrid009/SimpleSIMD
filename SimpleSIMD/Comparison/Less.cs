using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps<T>
	{
		private struct Less_VSelector : IFunc<Vector<T>, Vector<T>, bool>
		{
			public bool Invoke(Vector<T> left, Vector<T> right)
			{
				return Vector.LessThanAll(left, right);
			}
		}

		private struct Less_Selector : IFunc<T, T, bool>
		{
			public bool Invoke(T left, T right)
			{
				return NumOps<T>.Less(left, right);
			}
		}

		public static bool Less(ReadOnlySpan<T> left, T right)
		{
			return All(left, right, new Less_VSelector(), new Less_Selector());
		}

		public static bool Less(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return All(left, right, new Less_VSelector(), new Less_Selector());
		}
	}
}
