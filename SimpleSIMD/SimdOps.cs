﻿global using System;
global using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SimpleSimd;

public static partial class SimdOps
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	private static void ThrowArgOutOfRange(string name)
	{
		throw new ArgumentOutOfRangeException(name);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static ref Vector<T> AsVector<T>(in T value) where T : struct, INumber<T>
	{
		return ref Unsafe.As<T, Vector<T>>(ref Unsafe.AsRef(value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static ref T GetRef<T>(ReadOnlySpan<T> span) where T : struct, INumber<T>
	{
		return ref MemoryMarshal.GetReference(span);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static ref T GetRef<T>(Span<T> span) where T : struct, INumber<T>
	{
		return ref MemoryMarshal.GetReference(span);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static ref T Offset<T>(this ref T source, int count) where T : struct
	{
		return ref Unsafe.Add(ref source, count);
	}
}
