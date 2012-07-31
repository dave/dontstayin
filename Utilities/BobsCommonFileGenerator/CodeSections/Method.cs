using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class Method : CodeSection 
	{
		string accessor;
		string name;
		string[] code;
		private string returnType;
        List<MethodParameter> parameters;

		internal Method(string accessor, string name, List<MethodParameter> parameters, string[] code, string returnType)  
		{
			this.returnType = returnType;
			this.name = name;
			this.code = code;
			this.parameters = parameters;
			this.accessor = accessor;
		}


		internal override string[] GetCSharpSourceCode()
		{
			List<string> list = new List<string>();
			list.Add(accessor + " static " + returnType + " " + name + "(" + GetParameterList() + ")");
			list.Add("{");
			foreach (string s in code)
			{
				list.Add('\t' + s);
			}
			list.Add("}");
			return list.ToArray();
		}

		private string GetParameterList()
		{
			return MethodParameter.GetAsDeclarationList(this.parameters);
		}
	}
}
