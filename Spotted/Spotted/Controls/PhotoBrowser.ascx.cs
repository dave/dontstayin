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
using SpottedLibrary.Controls;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary;
using Common;
using SpottedLibrary.Controls.PagedRepeater;
using System.Collections.Generic;
using SpottedLibrary.Controls.PaginationControl2;
using Bobs;
using Js.Controls.PhotoControl;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class PhotoBrowser : EnhancedUserControl, IPhotoBrowser, IPagedDataControlView<Photo>, IIncludesJs
	{
		PagedDataControlController<Photo> controller;
		public PhotoBrowser()
		{
			this.Visible = false;
			controller = new PagedDataControlController<Photo>(this);
		}


		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			Spotted.Controls.PhotoControl.IncludeJs(page);

			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			
			this.uiPaginationControl.UrlPartsThatShouldBeUsedWhenMakingNextAndPrevPageLinks.Add(new KeyValuePair<string, string>("photo", null));
		}
		
		public int PageSize
		{
			set { ViewState["PageSize"] = value; }
			get { return (int?)ViewState["PageSize"] ?? Common.Properties.PhotoBrowser.IconsPerPage; }
		}

		protected int RowLength
		{
			get { return Common.Properties.PhotoBrowser.IconsPerRow; }
		}

		public event EventHandler<EventArgs<int>> ItemClicked;
		IPagedDataService<Photo> pagedDataService;
		public IPagedDataService<Photo> PagedDataService
		{
			set
			{
				pagedDataService = value;
				if (this.PagedDataServiceChanged != null) { this.PagedDataServiceChanged(this, new EventArgs<IPagedDataService<Photo>>(value)); }
			}
			get { return pagedDataService; }
		}


		private Photo[] currentPageItems;
		public Photo[] CurrentPageItems
		{
			set
			{
				if (value.Length < this.PageSize)
				{
					// make sure we generate template for all cells
					Photo[] pbi = new Photo[this.PageSize];
					int i = 0;
					while (i < value.Length)
					{
						pbi[i] = value[i];
						i++;
					}
					while (i < pbi.Length)
					{
						pbi[i] = new Photo { K = 0 };
						i++;
					}
					value = pbi;
				}
				currentPageItems = value;
			}
			get { return currentPageItems; }
		}


		public int NumberOfItems { get; set; }

		public IPaginationControl2 PaginationControl { get { return this.uiPaginationControl; } }
		public bool IsValid { get { return true; } }
		public string Title { protected get { return ViewState["Title"] as string; } set { ViewState["Title"] = value; } }


		public event EventHandler<EventArgs<IPagedDataService<Photo>>> PagedDataServiceChanged;

		protected string GetUrlForPhoto(int K)
		{
			var urlPart = uiPaginationControl.UrlPart(uiPaginationControl.CurrentPage);
			if (((Spotted.Master.DsiPage)this.Page).Url.ObjectFilterType == Model.Entities.ObjectType.Photo)
			{
				return ((Spotted.Master.DsiPage)this.Page).Url.ObjectFilterPhoto.UrlOfPhotoInSameParent(K, urlPart.Key, urlPart.Value);
			}
			else
			{
				return ((Spotted.Master.DsiPage)this.Page).Url.CurrentUrl(urlPart.Key, urlPart.Value, "photo", K);
			}
		}

		public bool DisplayNoResultsMessage
		{
			set { ; }
		}

		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs eventArgs)
		{

			this.uiIconsPerPage.Value = Common.Properties.PhotoBrowser.IconsPerPage.ToString();
			this.uiIconsPerRow.Value = Common.Properties.PhotoBrowser.IconsPerRow.ToString();
			this.uiIconSize.Value = Common.Properties.PhotoBrowser.IconSize.ToString();
			this.uiTableCellsPrefix.Value = TableCellsPrefix;
		}
		#endregion

		protected string TableCellsPrefix
		{
			get { return this.ClientID + "_td"; }
		}
	}
}
