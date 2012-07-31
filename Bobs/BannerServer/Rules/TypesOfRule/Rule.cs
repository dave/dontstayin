using System;
using Bobs;

namespace Bobs.BannerServer.Rules.TypesOfRule
{
	internal abstract class Rule : IComparable<Rule>
	{
		public abstract Q Q { get; }

		public int CompareTo(Rule other)
		{
			return this.ToString().CompareTo(other.ToString());
		}

		
	}
}
