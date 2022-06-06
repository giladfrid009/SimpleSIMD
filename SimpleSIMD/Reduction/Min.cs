using System;
using System.Numerics;

namespace SimpleSimd
{
	public static partial class SimdOps
	{
		public static T Min<T>(ReadOnlySpan<T> span) where T : struct, INumber<T>, IMinMaxValue<T>
		{
			return Min(span, new ID_VSelector<T>(), new ID_Selector<T>());
		}

		[DelOverload]
		public static T Min<T, F1, F2>(ReadOnlySpan<T> span, F1 vSelector, F2 selector)
			where T : struct, INumber<T>, IMinMaxValue<T>
			where F1 : struct, IFunc<Vector<T>, Vector<T>>
			where F2 : struct, IFunc<T, T>
		{
			T min = T.MaxValue;

			ref T rSpan = ref GetRef(span);

			int i = 0;

			if (Vector.IsHardwareAccelerated)
			{
				Vector<T> vMin = new(min);

				ref Vector<T> vrSpan = ref AsVector(rSpan);

				int length = span.Length / Vector<T>.Count;

				for (; i < length; i++)
				{
					vMin = Vector.Min(vMin, vSelector.Invoke(vrSpan.Offset(i)));
				}

				for (int j = 0; j < Vector<T>.Count; j++)
				{
					min = T.Min(min, vMin[j]);
				}

				i *= Vector<T>.Count;
			}

			for (; i < span.Length; i++)
			{
				min = T.Min(min, selector.Invoke(rSpan.Offset(i)));
			}

			return min;
		}
	}
}
