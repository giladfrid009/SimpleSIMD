namespace SimpleSimd
{
    public interface IAction<T> where T : struct
    {
        void Invoke(T param);
    }

    public interface IFunc<TResult> where TResult : struct
    {
        TResult Invoke();
    }
    
    public interface IFunc<T, TResult> where T : struct where TResult : struct
    {
        TResult Invoke(T param);
    }

    public interface IFunc<T1, T2, TResult> where T1 : struct where T2 : struct where TResult : struct
    {
        TResult Invoke(T1 param1, T2 param2);
    }
}
