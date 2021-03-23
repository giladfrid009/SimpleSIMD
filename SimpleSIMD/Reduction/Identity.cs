using System.Numerics;

namespace SimpleSimd
{
    public static partial class SimdOps<T>
    {
        private struct ID_VSelector : IFunc<Vector<T>, Vector<T>>
        {
            public Vector<T> Invoke(Vector<T> vec)
            {
                return vec;
            }
        }

        private struct ID_Selector : IFunc<T, T>
        {
            public T Invoke(T val)
            {
                return val;
            }
        }
    }
}
