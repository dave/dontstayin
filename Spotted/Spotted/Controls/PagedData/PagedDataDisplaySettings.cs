using System;
using System.Collections.Generic;
using System.Web.UI;
using Spotted.Controls.ClientSideRepeater;

namespace Spotted.Controls.PagedData
{
	internal class PagedDataDisplaySettings : IPagedDataDisplaySettings
	{
		public Func<Page, Template> GetItemTemplate { get; private set; }
		public ITemplate Header { get; private set; }
		public ITemplate Between { get; private set; }
		public ITemplate Footer { get; private set; }
		public int PageSize { get; private set; }
		public int DefaultTop { get; private set; }
		public string ServicePath { get; private set; }
		public string ServiceMethod { get; private set; }
		public int Timeout { get; private set; }
		public string TabName { get; private set; }
		public EnhancedUserControl HeaderControl { get; private set; }
		public PagedDataDisplaySettings(string tabName, EnhancedUserControl headerControl, ITemplate header, Func<Page, Template> item, ITemplate between, ITemplate footer, int pageSize, int defaultTop, string servicePath, string serviceMethod, int timeout)
		{
			this.Header = header;
			this.HeaderControl = headerControl;
			this.GetItemTemplate = item;
			this.Between = between;
			this.Footer = footer;
			this.PageSize = pageSize;
			this.DefaultTop = defaultTop;
			this.ServicePath = servicePath;
			this.ServiceMethod = serviceMethod;
			this.Timeout = timeout;
			this.TabName = tabName;
		}







		
	}
}
