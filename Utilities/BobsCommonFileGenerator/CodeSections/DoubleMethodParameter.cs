using System;

namespace BobsCommonFileGenerator.CodeSections
{
	internal class DoubleMethodParameter : MethodParameter
	{
		public DoubleMethodParameter(string name)
			: base(name)
		{
		}

		internal override string GetCSharpDeclaration()
		{
			return "double " + MethodParameter.SetFirstLetterToLowerCase(this.name);
		}
	}
}
