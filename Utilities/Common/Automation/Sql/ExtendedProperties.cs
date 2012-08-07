using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Automation.Sql
{
	public abstract class ExtendedProperties
	{
		public abstract string this[string key] { get; set; }
	}
}
