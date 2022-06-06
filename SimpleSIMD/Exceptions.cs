using System;
using System.Runtime.CompilerServices;

namespace SimpleSimd;

internal static class Exceptions
{
	[MethodImpl(Impl.NoInline)]
	internal static void ArgOutOfRange(string name)
	{
		throw new ArgumentOutOfRangeException(name);
	}

	[MethodImpl(Impl.NoInline)]
	internal static void InvalidCast(string name)
	{
		throw new InvalidCastException(name);
	}
}
