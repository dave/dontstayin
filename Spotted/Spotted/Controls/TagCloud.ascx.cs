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
using SpottedLibrary.Controls.TagCloud;
using Bobs;
using Common;
using SpottedLibrary.Controls.LinkCloud;
using SpottedLibrary.Pages.TagSearch;

namespace Spotted.Controls
{
	public partial class TagCloud : DsiUserControl, ITagCloudView, ITagCloud
	{
		TagCloudController controller;
		public TagCloud()
		{
			controller = new TagCloudController(this);
		}

		public ILinkCloud LinkCloud
		{
			get { return uiLinkCloud; }
		}

 

		public SpottedLibrary.Controls.SearchBoxControl.ISearchBoxControl SearchBoxControl
		{
			get { return uiSearchControl; }
		}

		public int NumberOfItems { get; set; }


		public UrlInfo UrlInfo
		{
			get { return this.ContainerPage.Url; }
		}

		public void Redirect(string url)
		{
			Response.Redirect(url);
		}
 
	}
}
