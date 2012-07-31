using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class Region : CodeSection 
	{
		string name;
		internal Region(string name)
		{
			this.name = name;
		}

		internal override string[] GetCSharpSourceCode()
		{
			List<string> list = new List<string>();
			list.Add("#region " + name );
			foreach (string s in base.GetCSharpSourceCode())
			{
				list.Add(s);
			}
			
			list.Add("#endregion");
			return list.ToArray();
		}
		

		internal void Add(CodeSection section)
		{
			base.Add(section);
		}
	}
}
