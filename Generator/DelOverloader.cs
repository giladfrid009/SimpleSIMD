﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace Generator;

[Generator(LanguageNames.CSharp)]
public class DelOverloader : BaseGenerator
{
	public DelOverloader() : base("DelOverloadAttribute", "SimpleSimd")
	{
	}

	protected override void ProcessMethod(StringBuilder source, IMethodSymbol methodSymbol)
	{
		if (methodSymbol.TypeParameters.All(S => !IsValueDelegate(S)))
		{
			ReportDiagnostic(
				"DSG001",
				$"Invalid '{AttributeName}' attribute target - method skipped for source generation.",
				"The method does not have a parameter constrained to be a Value-Delegate. " +
				"At least one of the parameters must be a generic type constrained as IFunc or IAction.",
				methodSymbol);

			return;
		}

		string methodBody = GetMethodBody(methodSymbol);

		if (string.IsNullOrEmpty(methodBody))
		{
			return;
		}

		string methodName = methodSymbol.Name;
		string accessibility = GetAccessibility(methodSymbol);
		string staticModifier = GetStaticModifier(methodSymbol);
		string returnType = GetReturnType(methodSymbol);
		string parameters = GetParameters(methodSymbol);
		string generics = GetGenerics(methodSymbol);
		string constraints = GetConstraints(methodSymbol);

		_ = source.AppendLine($"\t{accessibility} {staticModifier} {returnType} {methodName} {generics} ({parameters}) {constraints}\n{methodBody}");
	}

	protected override string GetGenerics(IMethodSymbol methodSymbol)
	{
		IEnumerable<string> generics = methodSymbol.TypeParameters
			.Where(P => !IsValueDelegate(P))
			.Names();

		return generics.Any() ? $"<{generics.CommaSeperated()}>" : string.Empty;
	}

	protected override string GetParameters(IMethodSymbol methodSymbol)
	{
		string parameters = base.GetParameters(methodSymbol);

		foreach (ITypeParameterSymbol typeSymbol in methodSymbol.TypeParameters)
		{
			foreach (ITypeSymbol constraintSymbol in typeSymbol.ConstraintTypes)
			{
				if (constraintSymbol.Name == "IFunc")
				{
					parameters = parameters.Replace(typeSymbol.Name,
						constraintSymbol
						.ToDisplayString()
						.Replace("SimpleSimd.IFunc", "System.Func"));
				}
				else if (constraintSymbol.Name == "IAction")
				{
					parameters = parameters.Replace(typeSymbol.Name,
						constraintSymbol
						.ToDisplayString()
						.Replace("SimpleSimd.IAction", "System.Action"));
				}
			}
		}

		return parameters;
	}

	protected override string GetConstraints(IMethodSymbol methodSymbol)
	{
		StringBuilder constraints = new();

		foreach (ITypeParameterSymbol typeSymbol in methodSymbol.TypeParameters)
		{
			if (IsValueDelegate(typeSymbol) == false)
			{
				_ = constraints.Append(GetConstraints(typeSymbol));
			}
		}

		return constraints.ToString();
	}

	protected bool IsValueDelegate(ITypeParameterSymbol typeSymbol)
	{
		foreach (ITypeSymbol C in typeSymbol.ConstraintTypes)
		{
			if (C.Name is "IAction" or "IFunc")
			{
				return true;
			}
		}

		return false;
	}

	protected string GetMethodBody(IMethodSymbol methodSymbol)
	{
		MethodDeclarationSyntax? methodNode = methodSymbol.DeclaringSyntaxReferences[0].GetSyntax() as MethodDeclarationSyntax;

		return methodNode?.Body?.GetText().ToString() ?? string.Empty;
	}
}