﻿namespace SimpleSimd;

public static partial class SimdOps
{
	public static void Fill<T>(Span<T> span, T value) where T : struct, INumber<T>
	{
		ref T rSpan = ref GetRef(span);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			Vector<T> vValue = new(value);

			ref Vector<T> vrSpan = ref AsVector(rSpan);

			int length = span.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vrSpan.Offset(i) = vValue;
			}

			i *= Vector<T>.Count;
		}

		for (; i < span.Length; i++)
		{
			rSpan.Offset(i) = value;
		}
	}

	[DelOverload]
	public static void Fill<T, F1, F2>(Span<T> span, F1 vFunc, F2 func)
		where T : struct, INumber<T>
		where F1 : struct, IFunc<Vector<T>>
		where F2 : struct, IFunc<T>
	{
		ref T rSpan = ref GetRef(span);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			ref Vector<T> vrSpan = ref AsVector(rSpan);

			int length = span.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vrSpan.Offset(i) = vFunc.Invoke();
			}

			i *= Vector<T>.Count;
		}

		for (; i < span.Length; i++)
		{
			rSpan.Offset(i) = func.Invoke();
		}
	}
}
