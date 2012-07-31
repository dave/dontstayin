using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Controls
{
	public partial class GalleriesBySpotter : System.Web.UI.UserControl
	{
		public Usr SpotterUsr { get; set; }
		public bool HasContent = false;

		public override void DataBind()
		{
			if (SpotterUsr != null && !SpotterUsr.IsSkeleton)
			{
				AllGalleriesLink.HRef = SpotterUsr.UrlGalleries();
				AllGalleriesLink.InnerText = "Show all " + SpotterUsr.NickName + "'s galleries";


				Query q = new Query();
	
				q.TableElement = Templates.Galleries.Default.PerformJoins(null, false);
				q.Columns = Templates.Galleries.Default.Columns;

				q.QueryCondition = new And(
					new Or(
						new Q(Gallery.Columns.ArticleK, 0),
						new Q(Gallery.Columns.ArticleK, QueryOperator.IsNull, null)
					),
					Gallery.ShowOnSiteQ,
					new Q(Gallery.Columns.OwnerUsrK, SpotterUsr.K)
				);
				q.OrderBy = new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending);

				q.TopRecords = 8;

				GallerySet gs = new GallerySet(q);

				HasContent = gs.Count > 0;

				uiGalleriesShowAllLinkPanel.Visible = gs.Count == q.TopRecords;

				if (gs.Count > 0)
				{
					this.Visible = true;
					uiGalleriesDataList.DataSource = gs;
					uiGalleriesDataList.ItemTemplate = this.LoadTemplate("/Templates/Galleries/Default.ascx");
					uiGalleriesDataList.DataBind();
					this.uiGalleriesDataList.Visible = true;
				}
				else
				{
					this.uiNoGalleriesForThisSpotterP.Visible = true;
					BindToNoRecords();
				}
			}
			else
			{
				this.Visible = false;
				BindToNoRecords();
			}
		}

		private void BindToNoRecords()
		{
			// annoying - if I don't kill this datasource, then on next postback, there are null hangovers in the datasource
			this.uiGalleriesDataList.DataSource = null;
			this.uiGalleriesDataList.DataBind();
			this.uiGalleriesDataList.Visible = false;
		}

	}
}
