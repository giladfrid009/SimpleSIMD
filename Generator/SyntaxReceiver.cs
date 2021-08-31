using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator
{
    class SyntaxReceiver : ISyntaxReceiver
    {
        public List<MethodDeclarationSyntax> MethodCandidates { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not MethodDeclarationSyntax methodNode)
            {
                return;
            }

            if (methodNode.AttributeLists.Count == 0)
            {
                return;
            }

            MethodCandidates.Add(methodNode);            
        }
    }
}