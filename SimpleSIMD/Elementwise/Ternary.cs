using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Ternary(in Span<T> span, Func<Vector<T>, Vector<T>> vCondition, Func<T, bool> condition, T trueValue, T falseValue, in Span<T> result)
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            var vTrue = new Vector<T>(trueValue);
            var vFalse = new Vector<T>(falseValue);
            int i;

            var vsSpan = AsVectors(span);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vsResult[i] = Vector.ConditionalSelect(vCondition(vsSpan[i]), vTrue, vFalse);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = condition(span[i]) ? trueValue : falseValue;
            }
        }

        public static void Ternary
        (
            in Span<T> span,
            Func<Vector<T>, Vector<T>> vCondition,
            Func<Vector<T>, Vector<T>> vTrueSelector,
            Func<Vector<T>, Vector<T>> vFalseSelector,
            Func<T, bool> condition,
            Func<T, T> trueSelector,
            Func<T, T> falseSelector,
            in Span<T> result
        )
        {
            if (result.Length != span.Length)
            {
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int i;

            var vsSpan = AsVectors(span);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vsResult[i] = Vector.ConditionalSelect(vCondition(vsSpan[i]), vTrueSelector(vsSpan[i]), vFalseSelector(vsSpan[i]));
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = condition(span[i]) ? trueSelector(span[i]) : falseSelector(span[i]);
            }
        }

        public static T[] Ternary(T[] array, Func<Vector<T>, Vector<T>> vCondition, Func<T, bool> condition, T trueValue, T falseValue)
        {
            var result = new T[array.Length];

            Ternary(array, vCondition, condition, trueValue, falseValue, result);

            return result;
        }

        public static T[] Ternary
        (
            T[] array,
            Func<Vector<T>, Vector<T>> vCondition,
            Func<Vector<T>, Vector<T>> vTrueSelector,
            Func<Vector<T>, Vector<T>> vFalseSelector,
            Func<T, bool> condition,
            Func<T, T> trueSelector,
            Func<T, T> falseSelector
        )
        {
            var result = new T[array.Length];

            Ternary(array, vCondition, vTrueSelector, vFalseSelector, condition, trueSelector, falseSelector, result);

            return result;
        }
    }
}
