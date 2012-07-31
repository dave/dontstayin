using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Collections;

namespace Js.ClientControls
{
	[Serializable]
	public partial class Suggestion : IHasPriority
	{

		#region IHasPriority Members

		public int Priority
		{
			get { return this.priority; }
		}

		#endregion
		public class SuggestionsComparer : IEqualityComparer<Suggestion>
		{
			public bool Equals(Suggestion x, Suggestion y)
			{
				return (x.html.Equals(y.html, StringComparison.InvariantCultureIgnoreCase) || x.value.Equals(y.value, StringComparison.InvariantCultureIgnoreCase));
			}
			public int GetHashCode(Suggestion obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
