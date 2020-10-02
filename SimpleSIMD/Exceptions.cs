using System;

namespace SimpleSimd
{
    internal static class Exceptions
    {
        internal static void ArgOutOfRange(string name)
        {
            throw new ArgumentOutOfRangeException(name);
        }

        internal static void InvalidCast(string name)
        {
            throw new InvalidCastException(name);
        }
    }
}
