using System;

namespace SimpleSimd
{
    public interface IAction<T> where T : struct
    {
        void Invoke(T param);
    }

    public interface IFunc<TRes> where TRes : struct
    {
        TRes Invoke();
    }
    
    public interface IFunc<T, TRes> where T : struct where TRes : struct
    {
        TRes Invoke(T param);
    }

    public interface IFunc<T1, T2, TRes> where T1 : struct where T2 : struct where TRes : struct
    {
        TRes Invoke(T1 param1, T2 param2);
    }

    public struct AWrapper<T> : IAction<T> where T : struct
    {
        private readonly Action<T> delRef;

        public AWrapper(Action<T> del) => delRef = del;

        public void Invoke(T param) => delRef(param);
    }

    public struct FWrapper<TRes> : IFunc<TRes> where TRes : struct
    {
        private readonly Func<TRes> delRef;

        public FWrapper(Func<TRes> del) => delRef = del;

        public TRes Invoke() => delRef();
    }

    public struct FWrapper<T, TRes> : IFunc<T, TRes> where T : struct where TRes : struct
    {
        private readonly Func<T, TRes> delRef;

        public FWrapper(Func<T, TRes> del) => delRef = del;

        public TRes Invoke(T param) => delRef(param);
    }

    public struct FWrapper<T1, T2, TRes> : IFunc<T1, T2, TRes> where T1 : struct where TRes : struct where T2 : struct
    {
        private readonly Func<T1, T2, TRes> delRef;

        public FWrapper(Func<T1, T2, TRes> del) => delRef = del;

        public TRes Invoke(T1 param1, T2 param2) => delRef(param1, param2);
    }
}
