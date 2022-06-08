namespace Generator;

internal static class Extensions
{
	internal static IEnumerable<(string Type, string Name)> TypesNames(this IEnumerable<ISymbol>? symbols)
	{
		return symbols?.Select(S => (S.ToDisplayString(), S.Name)) ?? Enumerable.Empty<(string, string)>();
	}

	internal static IEnumerable<string> Names(this IEnumerable<ISymbol>? symbols)
	{
		return symbols?.Select(S => S.Name) ?? Enumerable.Empty<string>();
	}

	internal static IEnumerable<string> Types(this IEnumerable<ISymbol>? symbols)
	{
		return symbols?.Select(S => S.ToDisplayString()) ?? Enumerable.Empty<string>();
	}

	internal static string CommaSeperated(this IEnumerable<string>? strings)
	{
		return string.Join(",", strings);
	}
}
