namespace SimpleSimd
{
    public static partial class ArrayOps<T>
    {
        public static bool GreaterOrEqual(T[] left, T right)
        {
            return !Less(left, right);
        }

        public static bool GreaterOrEqual(T[] left, T[] right)
        {
            return !Less(left, right);
        }
    }
}
