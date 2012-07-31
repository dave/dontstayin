using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class Int32MethodParameter : MethodParameter 
	{

		internal Int32MethodParameter(string name) : base(name)
		{
		}

		internal override string GetCSharpDeclaration()
		{
			return "int " + MethodParameter.SetFirstLetterToLowerCase(this.name);
		}

	}
}
