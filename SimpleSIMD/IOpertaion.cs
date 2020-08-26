namespace SimpleSimd
{
    public abstract class IOperation<T> where T : unmanaged
    {
        public abstract T MinVal { get; }
        public abstract T MaxVal { get; }

        public abstract T Add(T left, T right);
        public abstract T Sub(T left, T right);
        public abstract T Mul(T left, T right);
        public abstract T Div(T left, T right);
        public abstract T Mod(T left, T right);

        public abstract bool Equal(T left, T right);
        public abstract bool Less(T left, T right);
        public abstract bool Greater(T left, T right);
    }
}
