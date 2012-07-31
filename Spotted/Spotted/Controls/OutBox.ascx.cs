using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Controls
{
	public partial class OutBox : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			Q sexQ =
				Usr.Current == null ? new Q(true) :
				Usr.Current.IsMale ? new Q(Usr.Columns.IsMale, false) :
				new Q(Usr.Columns.IsMale, true);

			Query q = new Query();
			q.DataTableElement = new TableElement(TablesEnum.Usr);
			q.TableElement = Event.UsrAttendedJoin;
			q.QueryCondition = new And(
				sexQ,
				new Q(Usr.Columns.IsSkeleton, false),
				new Q(Usr.Columns.ExDirectory, false),
				new Q(Usr.Columns.PhotosMeCount, QueryOperator.GreaterThan, 0),
				new Q(Usr.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty),
				Event.FutureEventsQueryCondition,
				new Q(Event.Columns.DateTime, QueryOperator.LessThan, DateTime.Today.AddDays(7)));
			q.Distinct = true;
			q.DistinctColumn = Usr.Columns.K;
			q.OrderBy = new OrderBy(Usr.Columns.K);
			q.CacheDuration = new TimeSpan(1, 0, 0);
			q.Columns = new ColumnSet(Usr.Columns.IsMale, Usr.Columns.K, Usr.Columns.Pic, Usr.Columns.FacebookUID);
			UsrSet ueas = new UsrSet(q);

			if (ueas.Count >= 6)
			{
				Random r = new Random();
				int start = r.Next(ueas.Count);
				OutBoxHolder.InnerHtml += "<p>";
				for (int i = 0; i < 6; i++)
				{
					int j = start + i;
					if (j >= ueas.Count)
						j = start + i - ueas.Count;

					Usr u = ueas[j];

					string s = "<a href=\"/pages/out/" + (u.IsMale ? "boys" : "girls") + "/worldwide/" + u.K.ToString() + "\" class=\"NoStyle\"><img src=\"" + u.PicPath + "\" width=\"100\" height=\"100\" /></a>";
					OutBoxHolder.InnerHtml += s;
				}
				OutBoxHolder.InnerHtml += "</p>";
			}
			else
				this.Visible = false;

		}
	}
}
