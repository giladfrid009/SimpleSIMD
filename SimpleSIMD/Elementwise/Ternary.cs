namespace SimpleSimd;

public static partial class SimdOps
{
	[ArrOverload]
	[DelOverload]
	public static void Ternary<T, F1, F2>(ReadOnlySpan<T> span, F1 vCondition, F2 condition, T trueValue, T falseValue, Span<T> result)
		where T : struct, INumber<T>
		where F1 : struct, IFunc<Vector<T>, Vector<T>>
		where F2 : struct, IFunc<T, bool>
	{
		if (result.Length != span.Length)
		{
			Exceptions.ArgOutOfRange(nameof(result));
		}

		ref T rSpan = ref GetRef(span);
		ref T rResult = ref GetRef(result);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			Vector<T> vTrue = new(trueValue);
			Vector<T> vFalse = new(falseValue);

			ref Vector<T> vrSpan = ref AsVector(rSpan);
			ref Vector<T> vrResult = ref AsVector(rResult);

			int length = span.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vrResult.Offset(i) = Vector.ConditionalSelect(vCondition.Invoke(vrSpan.Offset(i)), vTrue, vFalse);
			}

			i *= Vector<T>.Count;
		}

		for (; i < span.Length; i++)
		{
			rResult.Offset(i) = condition.Invoke(rSpan.Offset(i)) ? trueValue : falseValue;
		}
	}

	[ArrOverload]
	[DelOverload]
	public static void Ternary<T, F1, F2, F3, F4, F5, F6>(ReadOnlySpan<T> span, F1 vCondition, F2 vTrueSelector, F3 vFalseSelector, F4 condition, F5 trueSelector, F6 falseSelector, Span<T> result)
		where T : struct, INumber<T>
		where F1 : struct, IFunc<Vector<T>, Vector<T>>
		where F2 : struct, IFunc<Vector<T>, Vector<T>>
		where F3 : struct, IFunc<Vector<T>, Vector<T>>
		where F4 : struct, IFunc<T, bool>
		where F5 : struct, IFunc<T, T>
		where F6 : struct, IFunc<T, T>
	{
		if (result.Length != span.Length)
		{
			Exceptions.ArgOutOfRange(nameof(result));
		}

		ref T rSpan = ref GetRef(span);
		ref T rResult = ref GetRef(result);

		int i = 0;

		if (Vector.IsHardwareAccelerated)
		{
			ref Vector<T> vrSpan = ref AsVector(rSpan);
			ref Vector<T> vrResult = ref AsVector(rResult);

			int length = span.Length / Vector<T>.Count;

			for (; i < length; i++)
			{
				vrResult.Offset(i) = Vector.ConditionalSelect(vCondition.Invoke(vrSpan.Offset(i)), vTrueSelector.Invoke(vrSpan.Offset(i)), vFalseSelector.Invoke(vrSpan.Offset(i)));
			}

			i *= Vector<T>.Count;
		}

		for (; i < span.Length; i++)
		{
			rResult.Offset(i) = condition.Invoke(rSpan.Offset(i)) ? trueSelector.Invoke(rSpan.Offset(i)) : falseSelector.Invoke(rSpan.Offset(i));
		}
	}
}
