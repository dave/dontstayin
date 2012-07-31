using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Common;
using SpottedLibrary.Controls.LinkCloud;
using SpottedLibrary.Controls.SearchBoxControl;

namespace SpottedLibrary.Controls.TagCloud
{
	public interface ITagCloudView : IView
	{
		ILinkCloud LinkCloud { get; }
		int NumberOfItems { get; }
	}
}
