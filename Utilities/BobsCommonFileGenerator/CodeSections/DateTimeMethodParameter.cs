using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class DateTimeMethodParameter : MethodParameter 
	{
		internal DateTimeMethodParameter(string name): base(name)
		{
		}

		internal override string GetCSharpDeclaration()
		{
			return "DateTime? " + MethodParameter.SetFirstLetterToLowerCase(this.name);
		}

	}
}
