using System.Runtime.CompilerServices;

namespace SimpleSimd
{
    internal static class Impl
    {
        public const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        public const MethodImplOptions NoInline = MethodImplOptions.NoInlining;
    }
}
