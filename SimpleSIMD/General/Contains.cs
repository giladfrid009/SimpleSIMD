using System.Numerics;

namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool Contains(T[] array, T value)
        {
            Vector<T> vValue = new Vector<T>(value);
            int i;

            var vsArray = AsVectors(array);

            for (i = 0; i < vsArray.Length; i++)
            {
                if (Vector.EqualsAny(vsArray[i], vValue))
                {
                    return true;
                }
            }

            i *= Vector<T>.Count;

            for (; i < array.Length; i++)
            {
                if (MathOps<T>.Equal(array[i], value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
