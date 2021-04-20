using System;

namespace SimpleSimd
{
    public static class Extensions
    {
        public static AWrapper<T> ToStruct<T>(this Action<T> delRef) where T : struct
        {
            return new AWrapper<T>(delRef);
        }

        public static FWrapper<T> ToStruct<T>(this Func<T> delRef) where T : struct
        {
            return new FWrapper<T>(delRef);
        }

        public static FWrapper<T, TRes> ToStruct<T, TRes>(this Func<T, TRes> delRef) where T : struct where TRes : struct
        {
            return new FWrapper<T, TRes>(delRef);
        }

        public static FWrapper<T1, T2, TRes> ToStruct<T1, T2, TRes>(this Func<T1, T2, TRes> delRef) where T1 : struct where T2 : struct where TRes : struct
        {
            return new FWrapper<T1, T2, TRes>(delRef);
        }
    }
}
