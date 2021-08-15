namespace SimpleSimd
{
    public struct ActionWrapper<T> : IAction<T> where T : struct
    {
        private readonly Action<T> delRef;

        public ActionWrapper(Action<T> del) => delRef = del;

        public void Invoke(T param) => delRef(param);
    }

    public struct FuncWrapper<TRes> : IFunc<TRes> where TRes : struct
    {
        private readonly Func<TRes> delRef;

        public FuncWrapper(Func<TRes> del) => delRef = del;

        public TRes Invoke() => delRef();
    }

    public struct FuncWrapper<T, TRes> : IFunc<T, TRes> where T : struct where TRes : struct
    {
        private readonly Func<T, TRes> delRef;

        public FuncWrapper(Func<T, TRes> del) => delRef = del;

        public TRes Invoke(T param) => delRef(param);
    }

    public struct FuncWrapper<T1, T2, TRes> : IFunc<T1, T2, TRes> where T1 : struct where T2 : struct where TRes : struct
    {
        private readonly Func<T1, T2, TRes> delRef;

        public FuncWrapper(Func<T1, T2, TRes> del) => delRef = del;

        public TRes Invoke(T1 param1, T2 param2) => delRef(param1, param2);
    }

    public static class Wrapper
    {
        public static ActionWrapper<T> Wrap<T>(this Action<T> delRef) where T : struct
        {
            return new ActionWrapper<T>(delRef);
        }

        public static FuncWrapper<T> Wrap<T>(this Func<T> delRef) where T : struct
        {
            return new FuncWrapper<T>(delRef);
        }

        public static FuncWrapper<T, TRes> Wrap<T, TRes>(this Func<T, TRes> delRef) where T : struct where TRes : struct
        {
            return new FuncWrapper<T, TRes>(delRef);
        }

        public static FuncWrapper<T1, T2, TRes> Wrap<T1, T2, TRes>(this Func<T1, T2, TRes> delRef) where T1 : struct where T2 : struct where TRes : struct
        {
            return new FuncWrapper<T1, T2, TRes>(delRef);
        }
    }
}