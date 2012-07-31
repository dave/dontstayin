using System;
using System.Web.UI;

namespace Spotted.Controls.PagedData
{
	internal class DelegateTemplate : ITemplate
	{
		private readonly Action<Control> instantiateIn;

		public DelegateTemplate(Action<Control> instantiateIn)
		{
			this.instantiateIn = instantiateIn;
		}

		public void InstantiateIn(Control container)
		{
			this.instantiateIn(container);
		}
	}
}
