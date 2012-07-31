using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SpottedLibrary.Controls.PaginationControl2;
using Common;
using Spotted.Master;
using System.Collections.Generic;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class PaginationControl2 : EnhancedUserControl, IPaginationControl2
	{
		public PaginationControl2()
		{

			this.UrlPartsThatShouldBeUsedWhenMakingNextAndPrevPageLinks = new List<KeyValuePair<string, string>>();
			this.Init += new EventHandler(PaginationControl2_Init);
		}

		void PaginationControl2_Init(object sender, EventArgs e)
		{
			this.uiNextPage.Click += (o, s) => CurrentPage = NextPage;
			this.uiPrevPage.Click += (o, s) => CurrentPage = PrevPage;
		}

		private void ChangePage(int pageIndex)
		{
			if (PageChanged != null) { PageChanged(this, new EventArgs<int>(pageIndex)); }
		}
		public int CurrentPage
		{
			get
			{
				return ViewState["CurrentPage"] as int? ?? 1;
			}
			set
			{
				ViewState["CurrentPage"] = value;
				if (PageChanged != null) { PageChanged(this, new EventArgs<int>(value)); }
			}
		}
		public int LastPage
		{
			get { return (int?)ViewState["LastPage"] ?? 1; }
			set
			{
				ViewState["LastPage"] = value;
				uiContainer.Style["display"] = value > 1 ? "" : "none";
			}
		}

		public bool HideBorder
		{
			set
			{
				if (value)
				{
					this.uiContainer.Style["border-left"] = "";
					this.uiContainer.Style["border-right"] = "";
					this.uiContainer.Style["border-bottom"] = "";
				}
			}
		}
 

		protected int NextPage { get { return CurrentPage + 1 > LastPage ? 1 : CurrentPage + 1; } }
		protected int PrevPage { get { return CurrentPage - 1 < 1 ? LastPage : CurrentPage - 1; } }


		protected string PageUrl(int pageNumber, params KeyValuePair<string, string>[] urlParts)
		{
			List<string> parts = new List<string>();
			var kvp = UrlPart(pageNumber);
			parts.Add(kvp.Key);
			parts.Add(kvp.Value);
			foreach (var pair in urlParts)
			{
				parts.Add(pair.Key);
				parts.Add(pair.Value);	
			}
			return ((DsiPage)Page).Url.CurrentUrl(parts.ToArray());
		}

		public string UrlPrefix { private get; set; }
		public List<KeyValuePair<string, string>> UrlPartsThatShouldBeUsedWhenMakingNextAndPrevPageLinks { get; private set; }

		public KeyValuePair<string, string> UrlPart(int pageNumber)
		{
			return new KeyValuePair<string, string>(UrlPrefix.ToLower() + "page", pageNumber == 1 ? null : pageNumber.ToString());
		}

		public event EventHandler<EventArgs<int>> PageChanged;

	}
}
