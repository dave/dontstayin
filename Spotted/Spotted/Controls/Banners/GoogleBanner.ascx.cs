using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using Spotted.Master;

namespace Spotted.Controls.Banners
{
	public partial class GoogleBanner : UserControl
	{
		UrlInfo Url
		{
			get { return ((DsiPage)Page).Url; }
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			this.uiPlaceHolder.Controls.Add(new Literal { Text = "google_page_url = \"http://www.dontstayin.com" + GetGoogleUrl() + "\";\r\n" });
		}

		public int Width { get; set; }
		public int Height { get; set; }
		private string GetGoogleUrl()
		{
			if (Url.CurrentApplication == "chat" && Url["k"].IsInt)
			{
				try
				{
					var t = new Thread(Url["k"]);

					return t.LastPage > 1 ? t.Url("c", t.LastPage.ToString()) : t.Url();
				}
				catch
				{
				}
			}
			else if (Url.HasObjectFilter)
			{
				switch (Url.ObjectFilterType)
				{
					case Model.Entities.ObjectType.Article:{return Url.ObjectFilterArticle.Url();}
					case Model.Entities.ObjectType.Brand:{return Url.ObjectFilterBrand.Url();}
					case Model.Entities.ObjectType.Group:{return Url.LogicalFilterGroup.Url();}
					case Model.Entities.ObjectType.Event:{return Url.ObjectFilterEvent.Url();}
					case Model.Entities.ObjectType.Venue:{return Url.ObjectFilterVenue.Url();}
					case Model.Entities.ObjectType.Place:{return Url.ObjectFilterPlace.Url();}
					case Model.Entities.ObjectType.Country:{return Url.ObjectFilterCountry.Url();}
					case Model.Entities.ObjectType.Usr:{return Url.ObjectFilterUsr.Url();}
					case Model.Entities.ObjectType.Photo:{return Url.ObjectFilterPhoto.Url();}
					default:{break;}
				}
			}
			return Url.CurrentUrl();
		}

	}
}
