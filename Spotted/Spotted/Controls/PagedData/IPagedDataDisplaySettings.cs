using System;
using System.Collections.Generic;
using System.Web.UI;
using Spotted.Controls.ClientSideRepeater;

namespace Spotted.Controls.PagedData
{
	public interface IPagedDataDisplaySettings
	{
		EnhancedUserControl HeaderControl { get; }

		ITemplate Header { get; }
		Func<Page, Template> GetItemTemplate { get; }
		ITemplate Between { get; }
		ITemplate Footer { get; }
		int DefaultTop { get; }
		int PageSize { get; }
		string ServicePath { get; }
		string ServiceMethod { get; }
		int Timeout { get; }
		string TabName { get; }
	}
}
