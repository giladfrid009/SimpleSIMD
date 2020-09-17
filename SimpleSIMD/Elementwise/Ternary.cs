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
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            var vTrue = new Vector<T>(trueValue);
            var vFalse = new Vector<T>(falseValue);
            int i;

            var vsArray = AsVectors(array);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsArray.Length; i++)
            {
                vsResult[i] = Vector.ConditionalSelect(vCondition(vsArray[i]), vTrue, vFalse);
            }

            i *= Vector<T>.Count;

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
                Exceptions.ArgOutOfRange(nameof(result));
                return;
            }

            int i;

            var vsArray = AsVectors(array);
            var vsResult = AsVectors(result);

            for (i = 0; i < vsArray.Length; i++)
            {
                vsResult[i] = Vector.ConditionalSelect(vCondition(vsArray[i]), vTrueSelector(vsArray[i]), vFalseSelector(vsArray[i]));
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                result[i] = condition(array[i]) ? trueSelector(array[i]) : falseSelector(array[i]);
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
