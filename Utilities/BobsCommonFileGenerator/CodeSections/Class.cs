using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class Class : CodeSection
	{
		string name;
		internal Class(string name) 
		{
			this.name = name;
		}

		internal override string[] GetCSharpSourceCode()
		{
			List<string> list = new List<string>();
			list.Add("public static partial class " + name);
			list.Add("{");
			foreach (string s in base.GetCSharpSourceCode())
			{
				list.Add("\t" + s);
			}
			
			list.Add("}");
			return list.ToArray();
		}

		public void Add(Class c){
			base.Add(c);
		}
		public void Add(Method c)
		{
			base.Add(c);
		}
		public Class GetClass(string name)
		{
			return (Class) this.codeSections.Find(delegate(CodeSection cs){
				return IsClassAndMatchesName(name, cs);
			});
		}
		
		static bool IsClassAndMatchesName(string name, CodeSection cs)
		{
			return IsClass(cs) && ((Class)cs).name == name;
		}

		private static bool IsClass(CodeSection cs)
		{
			return cs.GetType() == typeof(Class);
		}
	}
}
