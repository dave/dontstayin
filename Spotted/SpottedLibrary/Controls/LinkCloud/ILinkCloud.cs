using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Bobs;

namespace SpottedLibrary.Controls.LinkCloud
{
	public interface ILinkCloud : IView
	{
		List<KeyValuePair<ILinkable, int>> Items { set; }
	}
}
