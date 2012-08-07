using System;
using System.Collections.Generic;

using System.Text;

namespace Common.Automation.Sql
{
	public abstract class ClrAssembly
	{
		protected string name = null;
		public string Name
		{
			get
			{
				return name;
			}
		}

		public abstract string Mvid
		{
			get;
		}
	}
}
