namespace SimpleSimd;

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