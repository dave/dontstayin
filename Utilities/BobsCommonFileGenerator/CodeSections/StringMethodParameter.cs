using System;

namespace BobsCommonFileGenerator.CodeSections
{
	internal class StringMethodParameter : MethodParameter
	{
		public StringMethodParameter(string name)
			: base(name)
		{
		}

		internal override string GetCSharpDeclaration()
		{
			return "string " + MethodParameter.SetFirstLetterToLowerCase(this.name);
		}
	}
}
