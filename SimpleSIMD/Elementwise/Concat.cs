﻿namespace SimpleSimd;

public static partial class SimdOps
{
	[ArrOverload]
	[DelOverload]
	public static void Concat<T, TRes, F1, F2>(ReadOnlySpan<T> left, T right, F1 vCombiner, F2 combiner, Span<TRes> result)
		where T : struct, INumber<T>
		where TRes : struct, INumber<TRes>
		where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
		where F2 : struct, IFunc<T, T, TRes>
	{
		if (result.Length != left.Length)
		{
			ThrowArgOutOfRange(nameof(result));
		}

		ref T rLeft = ref GetRef(left);
		ref TRes rResult = ref GetRef(result);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			Vector<T> vRight = new(right);

			ref Vector<T> vrLeft = ref AsVector(rLeft);
			ref Vector<TRes> vrResult = ref AsVector(rResult);

			int length = left.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vrResult.Offset(i) = vCombiner.Invoke(vrLeft.Offset(i), vRight);
			}

			i *= Vector<T>.Count;
		}

		for (; i < left.Length; i++)
		{
			rResult.Offset(i) = combiner.Invoke(rLeft.Offset(i), right);
		}
	}

	[ArrOverload]
	[DelOverload]
	public static void Concat<T, TRes, F1, F2>(T left, ReadOnlySpan<T> right, F1 vCombiner, F2 combiner, Span<TRes> result)
		where T : struct, INumber<T>
		where TRes : struct, INumber<TRes>
		where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
		where F2 : struct, IFunc<T, T, TRes>
	{
		if (result.Length != right.Length)
		{
			ThrowArgOutOfRange(nameof(result));
		}

		ref T rRight = ref GetRef(right);
		ref TRes rResult = ref GetRef(result);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			Vector<T> vLeft = new(left);

			ref Vector<T> vrRight = ref AsVector(rRight);
			ref Vector<TRes> vrResult = ref AsVector(rResult);

			int length = right.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vrResult.Offset(i) = vCombiner.Invoke(vLeft, vrRight.Offset(i));
			}

			i *= Vector<T>.Count;
		}

		for (; i < right.Length; i++)
		{
			rResult.Offset(i) = combiner.Invoke(left, rRight.Offset(i));
		}
	}

	[ArrOverload]
	[DelOverload]
	public static void Concat<T, TRes, F1, F2>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, F1 vCombiner, F2 combiner, Span<TRes> result)
		where T : struct, INumber<T>
		where TRes : struct, INumber<TRes>
		where F1 : struct, IFunc<Vector<T>, Vector<T>, Vector<TRes>>
		where F2 : struct, IFunc<T, T, TRes>
	{
		if (right.Length != left.Length)
		{
			ThrowArgOutOfRange(nameof(right));
		}

		if (result.Length != left.Length)
		{
			ThrowArgOutOfRange(nameof(result));
		}

		ref T rLeft = ref GetRef(left);
		ref T rRight = ref GetRef(right);
		ref TRes rResult = ref GetRef(result);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			ref Vector<T> vrLeft = ref AsVector(rLeft);
			ref Vector<T> vrRight = ref AsVector(rRight);
			ref Vector<TRes> vrResult = ref AsVector(rResult);

			int length = left.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vrResult.Offset(i) = vCombiner.Invoke(vrLeft.Offset(i), vrRight.Offset(i));
			}

			i *= Vector<T>.Count;
		}

		for (; i < left.Length; i++)
		{
			rResult.Offset(i) = combiner.Invoke(rLeft.Offset(i), rRight.Offset(i));
		}
	}
}
