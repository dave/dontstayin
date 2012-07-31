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
using Bobs;
using System.Text;

namespace Spotted.Admin
{
	public partial class EventsWithNoSpendPromoters : AdminUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			bind();
		}
		protected void bind()
		{
			Query q = new Query();
			q.TableElement = Event.PromoterJoinAllWithVenue;
			q.CacheDuration = TimeSpan.FromHours(1);
			q.QueryCondition = new And(
				new StringQueryCondition("(select count(*) from [Invoice] where [Invoice].[PromoterK] = [Promoter].[K]) = 0"),
				new Q(Promoter.Columns.K, QueryOperator.GreaterThan, 0),
				Event.FutureEventsQueryCondition
				);
			q.OrderBy = Event.FutureEventOrder;
			q.TopRecords = 1000;
			q.Distinct = true;
			q.DistinctColumn = Event.Columns.K;

			EventSet es = new EventSet(q);
			StringBuilder sb = new StringBuilder();
			foreach (Event e in es)
			{
				bool doneOne = false;
				sb.Append("<p>");
				sb.Append(e.FriendlyHtml(true, true, true, true, true, 50));
				sb.Append(" - [");
				sb.Append(e.Venue.Capacity.ToString("#,##0"));
				sb.Append("] - ");
				if (e.Venue.PromoterK > 0)
				{
					if (e.Venue.Promoter.ChildInvoices().Count == 0)
					{
						appendPromoter(sb, e.Venue.Promoter, doneOne);
						doneOne = true;
					}
				}
				foreach (Brand b in e.Brands)
				{
					if (b.PromoterK > 0 && b.PromoterK != e.Venue.PromoterK)
					{
						if (b.Promoter.ChildInvoices().Count == 0)
						{
							appendPromoter(sb, b.Promoter, doneOne);
							doneOne = true;
						}
					}
				}
				sb.Append("</p>");
			}
			Ph.Controls.Add(new LiteralControl(sb.ToString()));
		}

		void appendPromoter(StringBuilder sb, Promoter p, bool doneOne)
		{
			if (doneOne)
				sb.Append(", ");
			sb.Append(p.Link());
			if (p.SalesUsrK > 0)
			{
				sb.Append(" (");
				sb.Append(p.SalesUsr.NickName);
				sb.Append(")");
			}
			sb.Append(" ");
			sb.Append("<a");
			sb.AppendAttribute("href", p.Url("callnow", "true"));
			sb.AppendAttribute("target", "_blank");
			sb.Append(">");
			sb.Append(p.PhoneNumber);
			sb.Append("</a>");
		}

		public void ReBind(object o, System.EventArgs e)
		{
			bind();
		}
	}
}
