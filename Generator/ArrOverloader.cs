using System.Collections.Immutable;
using System.Text;

namespace Generator;

[Generator(LanguageNames.CSharp)]
public class ArrOverloader : BaseGenerator
{
	public ArrOverloader() : base("ArrOverloadAttribute", "SimpleSimd")
	{
	}

	protected override void ProcessMethod(StringBuilder source, IMethodSymbol methodSymbol)
	{
		string returnType = GetReturnType(methodSymbol);

		if (string.IsNullOrEmpty(returnType))
		{
			ReportDiagnostic(
				"ASG001",
				$"Invalid '{AttributeName}' attribute target - method skipped for source generation.",
				"The method does not have a valid result span parameter. " +
				"The last parameter of the method must be of type Span<T>.",
				methodSymbol);

			return;
		}

		string lengthArgument = GetLengthArgument(methodSymbol);

		if (string.IsNullOrEmpty(lengthArgument))
		{
			ReportDiagnostic(
				"ASG002",
				$"Invalid '{AttributeName}' attribute target - method skipped for source generation.",
				"The method does not have a valid input span parameter. " +
				"The method must contain at least one parameter of type ReadOnlySpan<T>.",
				methodSymbol);

			return;
		}

		string methodName = methodSymbol.Name;
		string accessibility = GetAccessibility(methodSymbol);
		string staticModifier = GetStaticModifier(methodSymbol);
		string arguments = GetArguments(methodSymbol);
		string parameters = GetParameters(methodSymbol);
		string generics = GetGenerics(methodSymbol);
		string constraints = GetConstraints(methodSymbol);

		_ = source.AppendLine(
			$@"
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	{accessibility} {staticModifier} {returnType}[] {methodName} {generics} ({parameters}) {constraints}
	{{
		{returnType} [] result = new {returnType} [{lengthArgument}.Length];
		{methodName} {generics} ({arguments}, result);
		return result;
	}}"
			);
	}

	protected override string GetArguments(IMethodSymbol methodSymbol)
	{
		return methodSymbol.Parameters
			.Take(methodSymbol.Parameters.Length - 1)
			.Names()
			.CommaSeperated();
	}

	protected override string GetParameters(IMethodSymbol methodSymbol)
	{
		return methodSymbol.Parameters
			.Take(methodSymbol.Parameters.Length - 1)
			.TypesNames()
			.Select(S => $"{S.Type} {S.Name}")
			.CommaSeperated();
	}

	protected override string GetReturnType(IMethodSymbol methodSymbol)
	{
		if (methodSymbol.Parameters.Length == 0)
		{
			return string.Empty;
		}

		if (methodSymbol.Parameters[methodSymbol.Parameters.Length - 1].Type is not INamedTypeSymbol resultParameter)
		{
			return string.Empty;
		}

		if (resultParameter.Name != "Span")
		{
			return string.Empty;
		}

		if (resultParameter.IsGenericType == false)
		{
			return string.Empty;

		}

		return resultParameter.TypeArguments[0].Name;
	}

	private string GetLengthArgument(IMethodSymbol methodSymbol)
	{
		ImmutableArray<IParameterSymbol> parameterSymbols = methodSymbol.Parameters;

		for (int i = 0; i < parameterSymbols.Length - 1; i++)
		{
			if ((parameterSymbols[i].Type as INamedTypeSymbol)?.Name == "ReadOnlySpan")
			{
				return parameterSymbols[i].Name;
			}
		}

		return string.Empty;
	}
}