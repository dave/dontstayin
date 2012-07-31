using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ScriptFileSplitter
{
	class LineOfJavascript
	{
		internal string Text { get; private set; }
		internal Namespace Namespace { get; private set; }
		public LineOfJavascript(string text, Namespace currentNamespace)
		{
			this.Text = text;
			if (this.Text.StartsWith(currentNamespace.Name + ".View."))
			{
				currentNamespace.SetIsAspControlNamespaceToTrue();
			}
			this.Namespace = currentNamespace;
		}
	}
}
