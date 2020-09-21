using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        public static void Ternary<F1, F2>(in Span<T> span, F1 vCondition, F2 condition, T trueValue, T falseValue, in Span<T> result)

            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, bool>

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
                vsResult[i] = Vector.ConditionalSelect(vCondition.Invoke(vsSpan[i]), vTrue, vFalse);
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = condition.Invoke(span[i]) ? trueValue : falseValue;
            }
        }

        public static void Ternary<F1, F2, F3, F4, F5, F6>(in Span<T> span, F1 vCondition, F2 vTrueSelector, F3 vFalseSelector, F4 condition, F5 trueSelector, F6 falseSelector, in Span<T> result)

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
                return;
            }

            int i;

            var vsSpan = AsVectors(span);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsSpan.Length; i++)
            {
                vsResult[i] = Vector.ConditionalSelect(vCondition.Invoke(vsSpan[i]), vTrueSelector.Invoke(vsSpan[i]), vFalseSelector.Invoke(vsSpan[i]));
            }

            i *= Vector<T>.Count;

            for (; i < span.Length; i++)
            {
                result[i] = condition.Invoke(span[i]) ? trueSelector.Invoke(span[i]) : falseSelector.Invoke(span[i]);
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
