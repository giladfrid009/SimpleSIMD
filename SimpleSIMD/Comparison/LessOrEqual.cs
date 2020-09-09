namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool LessOrEqual(T[] left, T right)
        {
            return !Greater(left, right);
        }

        public static bool LessOrEqual(T[] left, T[] right)
        {
            return !Greater(left, right);
        }
    }
}
