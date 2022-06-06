using System;
using System.Numerics;

namespace SimpleSimd;

public static partial class SimdOps
{
	[DelOverload]
	public static bool All<T, F1, F2>(ReadOnlySpan<T> span, F1 vPredicate, F2 predicate)
		where T : struct, INumber<T>
		where F1 : struct, IFunc<Vector<T>, bool>
		where F2 : struct, IFunc<T, bool>
	{
		ref T rSpan = ref GetRef(span);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			ref Vector<T> vrSpan = ref AsVector(rSpan);

			int length = span.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				if (vPredicate.Invoke(vrSpan.Offset(i)) == false)
				{
					return false;
				}
			}

			i *= Vector<T>.Count;
		}

		for (; i < span.Length; i++)
		{
			if (predicate.Invoke(rSpan.Offset(i)) == false)
			{
				return false;
			}
		}

		return true;
	}

	[DelOverload]
	public static bool All<T, F1, F2>(ReadOnlySpan<T> left, T right, F1 vPredicate, F2 predicate)
		where T : struct, INumber<T>
		where F1 : struct, IFunc<Vector<T>, Vector<T>, bool>
		where F2 : struct, IFunc<T, T, bool>
	{
		ref T rLeft = ref GetRef(left);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			Vector<T> vRight = new(right);

			ref Vector<T> vrLeft = ref AsVector(rLeft);

			int length = left.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				if (vPredicate.Invoke(vrLeft.Offset(i), vRight) == false)
				{
					return false;
				}
			}

			i *= Vector<T>.Count;
		}

		for (; i < left.Length; i++)
		{
			if (predicate.Invoke(rLeft.Offset(i), right) == false)
			{
				return false;
			}
		}

		return true;
	}

	[DelOverload]
	public static bool All<T, F1, F2>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, F1 vPredicate, F2 predicate)
		where T : struct, INumber<T>
		where F1 : struct, IFunc<Vector<T>, Vector<T>, bool>
		where F2 : struct, IFunc<T, T, bool>
	{
		if (right.Length != left.Length)
		{
			Exceptions.ArgOutOfRange(nameof(right));
		}

		ref T rLeft = ref GetRef(left);
		ref T rRight = ref GetRef(right);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			ref Vector<T> vrLeft = ref AsVector(rLeft);
			ref Vector<T> vrRight = ref AsVector(rRight);

			int length = left.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				if (vPredicate.Invoke(vrLeft.Offset(i), vrRight.Offset(i)) == false)
				{
					return false;
				}
			}

			i *= Vector<T>.Count;
		}

		for (; i < left.Length; i++)
		{
			if (predicate.Invoke(rLeft.Offset(i), rRight.Offset(i)) == false)
			{
				return false;
			}
		}

		return true;
	}
}
