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
            var methodNode = syntaxNode as MethodDeclarationSyntax;

            if (methodNode is null)
            {
                return;
            }

            if (methodNode.AttributeLists.Count == 0)
            {
                return;
            }

            CandidateMethods.Add(methodNode);
        }
    }
}