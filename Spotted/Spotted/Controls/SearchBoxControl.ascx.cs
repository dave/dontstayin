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
using SpottedLibrary.Controls.SearchBoxControl;
using SpottedLibrary.Controls.PagedRepeater;
using SpottedLibrary.Controls.PhotoBrowserControl;
using System.Collections.Generic;
using Common.General;
using Common;
using Bobs;
using Bobs.CachedDataAccess;

namespace Spotted.Controls
{
	public partial class SearchBoxControl : EnhancedUserControl, ISearchBoxControl, ISearchBoxControlView
	{
		SearchBoxControlController controller;
		public SearchBoxControl()
		{
			controller = new SearchBoxControlController(this);
			this.Init += new EventHandler(SearchBoxControl_Init);
		}

		void SearchBoxControl_Init(object sender, EventArgs e)
		{
			this.uiSubmitSearchButton.Click +=new EventHandler(uiSubmitSearchButton_Click);
	//		this.uiSubmitSearchButton.Attributes.Add("OnClick", "alert('click');return false;");
	//		this.uiSubmitSearchButton.Attributes.Add("OnSubmit", "alert('submit');return false;");

		}

		void uiSubmitSearchButton_Click(object sender, EventArgs e)
		{
			if (this.SearchButtonClick != null) { SearchButtonClick(this, e); }
		}


		[NonSerialized]
		SearchQuery searchQuery;
 
 
 

		public string Title { protected get { return ViewState["Title"] as string; } set { ViewState["Title"] = value; } }



		public string Text
		{
			get { return uiSearchQuery.Text; }
			set { uiSearchQuery.Text = value; }
		}

	 

		public event EventHandler SearchButtonClick;
		public event EventHandler PagedDataServiceChanged;
	}
}
