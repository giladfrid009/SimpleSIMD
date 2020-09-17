using System;
using System.Runtime.CompilerServices;

namespace SimpleSimd
{
    /// <summary>
    /// Exception helpers. Using methods to throw exceptions helps generate better JIT assembly.
    /// </summary>
    internal static class Exceptions
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ArgOutOfRange(string name)
        {
            throw new ArgumentOutOfRangeException(name);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void InvalidCast(string name)
        {
            throw new InvalidCastException(name);
        }
    }
}
