using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps
	{
		public static T Sum<T>(ReadOnlySpan<T> span) where T : struct, INumber<T>
		{
			return Sum(span, new ID_VSelector<T>(), new ID_Selector<T>());
		}

		[DelOverload]
		public static T Sum<T, F1, F2>(ReadOnlySpan<T> span, F1 vSelector, F2 selector)
			where T : struct, INumber<T>
			where F1 : struct, IFunc<Vector<T>, Vector<T>>
			where F2 : struct, IFunc<T, T>
		{
			T sum = T.Zero;

			ref T rSpan = ref GetRef(span);

			int i = 0;

			if (Vector.IsHardwareAccelerated)
			{
				Vector<T> vSum = Vector<T>.Zero;

				ref Vector<T> vrSpan = ref AsVector(rSpan);

				int length = span.Length / Vector<T>.Count;

				for (; i < length; i++)
				{
					vSum += vSelector.Invoke(vrSpan.Offset(i));
				}

				sum = Vector.Dot(vSum, Vector<T>.One);

				i *= Vector<T>.Count;
			}

			for (; i < span.Length; i++)
			{
				sum += selector.Invoke(rSpan.Offset(i));
			}

			return sum;
		}
	}
}