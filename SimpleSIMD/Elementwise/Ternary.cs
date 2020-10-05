using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Ternary<F1, F2>(in ReadOnlySpan<T> span, F1 vCondition, F2 condition, T trueValue, T falseValue, in ReadOnlySpan<T> result)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, bool>

        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
            }

            ref var rSpan = ref GetRef(span);
            ref var rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                var vTrue = new Vector<T>(trueValue);
                var vFalse = new Vector<T>(falseValue);

                ref var vrSpan = ref AsVector(rSpan);
                ref var vrResult = ref AsVector(rResult);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    Offset(vrResult, i) = Vector.ConditionalSelect(vCondition.Invoke(Offset(vrSpan, i)), vTrue, vFalse);
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                Offset(rResult, i) = condition.Invoke(Offset(rSpan, i)) ? trueValue : falseValue;
            }
        }

        public static void Ternary<F1, F2, F3, F4, F5, F6>(in ReadOnlySpan<T> span, F1 vCondition, F2 vTrueSelector, F3 vFalseSelector, F4 condition, F5 trueSelector, F6 falseSelector, in ReadOnlySpan<T> result)

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

            ref var rSpan = ref GetRef(span);
            ref var rResult = ref GetRef(result);

            int i = 0;

            if (Vector.IsHardwareAccelerated)
            {
                ref var vrSpan = ref AsVector(rSpan);
                ref var vrResult = ref AsVector(rResult);

                int length = span.Length / Vector<T>.Count;

                for (; i < length; i++)
                {
                    Offset(vrResult, i) = Vector.ConditionalSelect(vCondition.Invoke(Offset(vrSpan, i)), vTrueSelector.Invoke(Offset(vrSpan, i)), vFalseSelector.Invoke(Offset(vrSpan, i)));
                }

                i *= Vector<T>.Count;
            }

            for (; i < span.Length; i++)
            {
                Offset(rResult, i) = condition.Invoke(Offset(rSpan, i)) ? trueSelector.Invoke(Offset(rSpan, i)) : falseSelector.Invoke(Offset(rSpan, i));
            }
        }

        public static T[] Ternary<F1, F2>(T[] array, F1 vCondition, F2 condition, T trueValue, T falseValue)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, bool>

        {
            var result = new T[array.Length];

            Ternary(array, vCondition, condition, trueValue, falseValue, result);

            return result;
        }

        public static T[] Ternary<F1, F2, F3, F4, F5, F6>(T[] array, F1 vCondition, F2 vTrueSelector, F3 vFalseSelector, F4 condition, F5 trueSelector, F6 falseSelector)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<Vector<T>, Vector<T>>
            where F3 : struct, IFunc<Vector<T>, Vector<T>>
            where F4 : struct, IFunc<T, bool>
            where F5 : struct, IFunc<T, T>
            where F6 : struct, IFunc<T, T>

        {
            var result = new T[array.Length];

            Ternary(array, vCondition, vTrueSelector, vFalseSelector, condition, trueSelector, falseSelector, result);

            return result;
        }
    }
}
