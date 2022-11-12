using System.Runtime.CompilerServices;

namespace SimpleSimd;

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
