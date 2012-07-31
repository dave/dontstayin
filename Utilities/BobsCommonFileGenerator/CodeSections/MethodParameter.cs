using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	abstract class MethodParameter
	{
		protected string name;
		internal MethodParameter(string name)
		{
			this.name = name;
		}

		internal static string GetAsDeclarationList(List<MethodParameter> parameters){
			List<string> list = new List<string>();
			foreach (MethodParameter p in parameters)
			{
				list.Add(p.GetCSharpDeclaration());
			}
			return String.Join(", ", list.ToArray());
		}

		abstract internal string GetCSharpDeclaration();

		public string Name
		{
			get
			{
				return name;
			}
		}

		internal static string SetFirstLetterToLowerCase(string s)
		{
			return s.Substring(0, 1).ToLower() + s.Substring(1);
		}

	}
}
