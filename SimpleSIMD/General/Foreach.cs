using System;
using System.Numerics;

namespace SimpleSimd;

public static partial class SimdOps
{
	[DelOverload]
	public static void Foreach<T, F1, F2>(ReadOnlySpan<T> span, F1 vAction, F2 action)
		where T : struct, INumber<T>
		where F1 : struct, IAction<Vector<T>>
		where F2 : struct, IAction<T>
	{
		ref T rSpan = ref GetRef(span);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			ref Vector<T> vrSpan = ref AsVector(rSpan);

			int length = span.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vAction.Invoke(vrSpan.Offset(i));
			}

			i *= Vector<T>.Count;
		}

		for (; i < span.Length; i++)
		{
			action.Invoke(rSpan.Offset(i));
		}
	}
}
