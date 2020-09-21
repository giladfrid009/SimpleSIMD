namespace SimpleSimd
{
    public interface IAction<T>
    {
        void Invoke(T param);
    }

    public interface IFunc<TResult>
    {
        TResult Invoke();
    }
    
    public interface IFunc<T, TResult>
    {
        TResult Invoke(T param);
    }

    public interface IFunc<T1, T2, TResult>
    {
        TResult Invoke(T1 param1, T2 param2);
    }    
}
