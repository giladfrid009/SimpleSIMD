﻿namespace SimpleSimd;

public static partial class SimdOps
{
	public static bool Contains<T>(ReadOnlySpan<T> span, T value) where T : struct, INumber<T>
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
				if (Vector.EqualsAny(vrSpan.Offset(i), vValue))
				{
					return true;
				}
			}

			i *= Vector<T>.Count;
		}

		for (; i < span.Length; i++)
		{
			if (rSpan.Offset(i) == value)
			{
				return true;
			}
		}

		return false;
	}
}
