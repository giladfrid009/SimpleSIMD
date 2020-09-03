namespace SimpleSimd
{
    public abstract class IOperation<T> where T : unmanaged
    {
        public abstract T MinVal { get; }
        public abstract T MaxVal { get; }

        public abstract T Add(T left, T right);
        public abstract T Subtract(T left, T right);
        public abstract T Multiply(T left, T right);
        public abstract T Divide(T left, T right);

        public abstract bool Equal(T left, T right);
        public abstract bool Less(T left, T right);
        public abstract bool Greater(T left, T right);
    }
}
