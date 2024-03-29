﻿namespace SimpleSimd;

public static partial class SimdOps
{
	[ArrOverload]
	[DelOverload]
	public static void Select<T, TRes, F1, F2>(ReadOnlySpan<T> span, F1 vSelector, F2 selector, Span<TRes> result)
		where T : struct, INumber<T>
		where TRes : struct, INumber<TRes>
		where F1 : struct, IFunc<Vector<T>, Vector<TRes>>
		where F2 : struct, IFunc<T, TRes>
	{
		if (result.Length != span.Length)
		{
			ThrowArgOutOfRange(nameof(result));
		}

		ref T rSpan = ref GetRef(span);
		ref TRes rResult = ref GetRef(result);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			ref Vector<T> vrSpan = ref AsVector(rSpan);
			ref Vector<TRes> vrResult = ref AsVector(rResult);

			int length = span.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vrResult.Offset(i) = vSelector.Invoke(vrSpan.Offset(i));
			}

			i *= Vector<T>.Count;
		}

		for (; i < span.Length; i++)
		{
			rResult.Offset(i) = selector.Invoke(rSpan.Offset(i));
		}
	}
}
