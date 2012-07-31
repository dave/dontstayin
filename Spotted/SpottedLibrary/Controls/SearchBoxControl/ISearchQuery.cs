using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.SearchBoxControl
{
	public interface ISearchQuery
	{
		IEnumerable<string> TagsQueryParts { get; }
		string SearchString { get; }
		string SearchUrl { get; }
	}
}
