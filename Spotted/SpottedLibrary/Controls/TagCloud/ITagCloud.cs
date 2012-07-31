using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using SpottedLibrary.Controls.SearchBoxControl;

namespace SpottedLibrary.Controls.TagCloud
{
	public interface ITagCloud 
	{
		bool Visible { set; }
		ISearchBoxControl SearchBoxControl { get; }
	}
}
