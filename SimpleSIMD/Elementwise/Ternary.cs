using System;
using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static void Ternary(T[] array, Func<Vector<T>, Vector<T>> vCondition, Func<T, bool> condition, T trueValue, T falseValue, T[] result)
        {
            if (result.Length != array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            var vTrue = new Vector<T>(trueValue);
            var vFalse = new Vector<T>(falseValue);
            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                Vector.ConditionalSelect(vCondition(new Vector<T>(array, i)), vTrue, vFalse).CopyTo(result, i);
            }

            for (; i < array.Length; i++)
            {
                result[i] = condition(array[i]) ? trueValue : falseValue;
            }
        }

        public static void Ternary
        (
            T[] array,
            Func<Vector<T>, Vector<T>> vCondition,
            Func<Vector<T>, Vector<T>> vTrueSelector,
            Func<Vector<T>, Vector<T>> vFalseSelector,
            Func<T, bool> condition,
            Func<T, T> trueSelector,
            Func<T, T> falseSelector,
            T[] result
        )
        {
            if (result.Length != array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            int i;

            for (i = 0; i <= array.Length - vLen; i += vLen)
            {
                var vec = new Vector<T>(array, i);
                Vector.ConditionalSelect(vCondition(vec), vTrueSelector(vec), vFalseSelector(vec)).CopyTo(result, i);
            }

            for (; i < array.Length; i++)
            {
                T val = array[i];
                result[i] = condition(val) ? trueSelector(val) : falseSelector(val);
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
