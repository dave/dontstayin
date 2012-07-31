using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class GuidMethodParameter : MethodParameter 
	{
		internal GuidMethodParameter(string name) : base(name)
		{ 
		}

		internal override string GetCSharpDeclaration()
		{
			return "Guid? " + MethodParameter.SetFirstLetterToLowerCase(this.name);
		}
	}
}
