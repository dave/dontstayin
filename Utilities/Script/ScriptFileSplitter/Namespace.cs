using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptFileSplitter
{
	class Namespace
	{
		private Namespace(string name){
			this.Lines = new List<LineOfJavascript>();
			this.IsAspControlNamespace = false;
			this.Name = name;
		}

		static Dictionary<string, Namespace> namespaces = new Dictionary<string, Namespace>();
		internal static IEnumerable<Namespace> Namespaces { get { return namespaces.Values; } }
		internal string Name { get; private set; }
		internal bool IsAspControlNamespace { get; private set; }



		internal Namespace ParentAspControlNamespace
		{
			get
			{
				Namespace current = this;
				while (current.Parent != null)
				{
					if (current.IsAspControlNamespace)
					{
						return current;
					}
					current = current.Parent;
				}
				return null;
			}
		}
		internal static Namespace GetNamespace(string name)
		{
			if (!namespaces.ContainsKey(name))
			{
				var ns = new Namespace(name);
				namespaces.Add(name, ns);
				return ns;
			}
			else
			{
				return namespaces[name];
			}
			
		}
		internal static Namespace GetNamespace(string name, bool add)
		{
			if (!namespaces.ContainsKey(name))
			{
				var ns = new Namespace(name);
				if (add)
					namespaces.Add(name, ns);
				return ns;
			}
			else
			{
				return namespaces[name];
			}

		}
		internal Namespace Parent
		{
			get
			{
				if (Name.Contains('.'))
				{
					return GetNamespace(this.Name.Substring(0, this.Name.LastIndexOf('.')));
				}
				else
				{
					return null;
				}

			}
		}
		internal List<LineOfJavascript> Lines { get; set; }
		internal void SetIsAspControlNamespaceToTrue() { this.IsAspControlNamespace = true; }
	}
}
