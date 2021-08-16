using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Generator
{
    class SyntaxReceiver : ISyntaxReceiver
    {
        public List<MethodDeclarationSyntax> CandidateMethods { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            var method = syntaxNode as MethodDeclarationSyntax;

            if (method is null)
            {
                return;
            }

            if (method.AttributeLists.Count == 0)
            {
                return;
            }

            CandidateMethods.Add(method);
        }
    }
}