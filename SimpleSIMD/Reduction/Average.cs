using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps
	{
		public static T Average<T>(ReadOnlySpan<T> span) where T : struct, INumber<T>
		{
			return Average(span, new ID_VSelector<T>(), new ID_Selector<T>());
		}

		[DelOverload]
		public static T Average<T, F1, F2>(ReadOnlySpan<T> span, F1 vSelector, F2 selector)
			where T : struct, INumber<T>
			where F1 : struct, IFunc<Vector<T>, Vector<T>>
			where F2 : struct, IFunc<T, T>
		{
			return Sum(span, vSelector, selector) / Convert<int, T>(span.Length);
		}

		private static TDst Convert<TSrc, TDst>(TSrc val) where TSrc : INumber<TSrc> where TDst : INumber<TDst>
		{
			return TDst.Create(val);
		}
	}
}
