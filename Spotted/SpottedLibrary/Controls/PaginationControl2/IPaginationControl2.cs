using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Controls.PaginationControl2
{
	public interface IPaginationControl2
	{
		string UrlPrefix { set; }
		int CurrentPage { get; set; }
		int LastPage { set; get; }
		KeyValuePair<string, string> UrlPart(int pageNumber);
		List<KeyValuePair<string, string>> UrlPartsThatShouldBeUsedWhenMakingNextAndPrevPageLinks { get; }
		event EventHandler<EventArgs<int>> PageChanged;
	}
}
