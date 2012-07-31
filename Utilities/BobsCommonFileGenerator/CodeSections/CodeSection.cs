using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class CodeSection
	{
		protected List<CodeSection> codeSections;
		internal CodeSection(){
			this.codeSections = new List<CodeSection>();
		}

		internal void Add(CodeSection codeSection){
			this.codeSections.Add(codeSection);
		}
		
		internal virtual string[] GetCSharpSourceCode(){
			List<string> output = new List<string>();
			foreach(CodeSection cs in this.codeSections){
				foreach(string line in cs.GetCSharpSourceCode()){
					output.Add(line);
				}
			}
			return output.ToArray();
		}
		
	}
}
