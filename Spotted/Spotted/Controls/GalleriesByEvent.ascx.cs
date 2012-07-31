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
	public partial class GalleriesByEvent : System.Web.UI.UserControl
	{
		#region ThisEvent
		public Event ThisEvent
		{
			get
			{
				if (thisEvent == null && ViewState["EventK"] != null)
				{
					thisEvent = new Event((int)ViewState["EventK"]);
				}
				return thisEvent;
			}
			set
			{
				thisEvent = value;
				ViewState["EventK"] = value.K;
			}
		}
		private Event thisEvent;
		#endregion

		public override void DataBind()
		{
			if (ThisEvent != null)
			{
				//uiAttendedEvent.ThisEvent = ThisEvent;
				//uiAttendedEvent.DataBind();

				Query q = new Query();

				q.TableElement = Templates.Galleries.Default.PerformJoins(null, false);
				q.Columns = Templates.Galleries.Default.Columns;

				q.QueryCondition = new And(
					new Or(
						new Q(Gallery.Columns.ArticleK, 0),
						new Q(Gallery.Columns.ArticleK, QueryOperator.IsNull, null)
					),
					Gallery.ShowOnSiteQ,
					new Q(Gallery.Columns.EventK, ThisEvent.K)
				);
				q.OrderBy = new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending);

				q.TopRecords = 8;

				GallerySet gs = new GallerySet(q);

				uiGalleriesShowAllLinkPanel.Visible = gs.Count == q.TopRecords;

				this.Visible = true;
				if (gs.Count > 0)
				{
					this.uiNoGalleriesForThisEventP.Visible = false;
					this.uiGalleriesDataList.DataSource = gs;
					this.uiGalleriesDataList.ItemTemplate = this.LoadTemplate("/Templates/Galleries/Default.ascx");
					this.uiGalleriesDataList.DataBind();
					this.uiGalleriesDataList.Visible = true;
				}
				else
				{
					this.uiNoGalleriesForThisEventP.Visible = true;
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
