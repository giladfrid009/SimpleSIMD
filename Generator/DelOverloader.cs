using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Linq;

namespace Generator
{
    [Generator]
    public class DelOverloader : BaseGenerator
    {
        public DelOverloader() : base("DelOverloadAttribute", "DelOverloads")
        {

        }

        protected override void ProcessMethod(StringBuilder source, IMethodSymbol method)
        {
            //var node = (MethodDeclarationSyntax)method.DeclaringSyntaxReferences[0].GetSyntax();

            //if(node == null)
            //{
            //    return;
            //}

            //var body = node.Body.GetText();

            if (method.IsGenericMethod == false) return;

            var paramSymbols = method.Parameters;

            var genericParams = method.TypeParameters;

            foreach (var typeSymbol in genericParams)
            {
                var constraints = TypeConstraints(typeSymbol);

                string valDelegate = constraints.FirstOrDefault(C => C.Contains("IFunc") || C.Contains("IAction"));

                if (string.IsNullOrEmpty(valDelegate) == false)
                {
                    string delegateStr = valDelegate.Replace("IFunc", "Func").Replace("IAction", "Action");

                    
                }
                else // doesnt have valDelegate constraint
                {

                }
            }

            

            string methodName = method.Name;

            string paramNames = string.Join(",", paramSymbols.Names());

            //string paramSignatures = string.Join(",", paramSymbols.TypesNames().Select(S => $"{S.Type} {S.Name}"));

            //string genericTypes = method.IsGenericMethod ? $"<{string.Join(",", method.TypeParameters.Names())}>" : string.Empty;

            



            string genericConstraints = AllConstraintsFormat(method.TypeParameters);

            /*
             * TODO: find which generic arguments are IFunc<> or IAction<>
             * Find generic arg in signature and replace with Func, Action
             * Remove from genericTypes - check if empty
             * Remove from genericConstraints - check if empty
             * Add original method body
            */
        }
    }
}