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
using Common.Clocks;
using Common;

namespace Spotted.Controls.Admin
{
	public partial class BannerSlots : System.Web.UI.UserControl
	{
		protected TableRow Row;
		#region Position
		public Banner.Positions Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
			}
		}
		private Banner.Positions position;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			for (int month = 0; month < 2; month++)
			{
				TableCell tc = new TableCell();
				tc.VerticalAlign = VerticalAlign.Top;

				System.Web.UI.WebControls.Calendar c = new System.Web.UI.WebControls.Calendar();
				c.VisibleDate = DateTime.Today.AddMonths(month);
				c.DayRender += new DayRenderEventHandler(DateCal_DayRender);
				c.DayStyle.Width = Unit.Parse("30px");
				c.DayStyle.Height = Unit.Parse("35px");
				c.DayStyle.VerticalAlign = VerticalAlign.Top;
				c.DayStyle.CssClass = "BannerCalendarDay";
				c.DayHeaderStyle.CssClass = "BannerCalendarDayHeader";
				c.OtherMonthDayStyle.CssClass = "BannerCalendarOtherMonthDay";
				c.CssClass = "BannerCalendar";
				c.TitleStyle.CssClass = "BannerCalendarTitle";
				c.CellPadding = 0;
				c.CellSpacing = 0;
				c.ShowNextPrevMonth = false;
				tc.Controls.Add(c);
				Row.Cells.Add(tc);
			}
		}
		public void Bind()
		{

		}

		public void DateCal_DayRender(object o, DayRenderEventArgs e)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				Banner.IsBookedQ,
				new Q(Banner.Columns.StatusEnabled, true),
				new Q(Banner.Columns.FirstDay, QueryOperator.LessThanOrEqualTo, Time.Now.Date),
				new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, Time.Now.Date));
			q.Columns = new ColumnSet();
			q.ExtraSelectElements.Add("sum", "SUM([Banner].[TotalRequiredImpressions] / DateDiff(day, [Banner].[FirstDay], DATEADD(d, 1, [Banner].[LastDay])))");
			BannerSet bs = new BannerSet(q);

			e.Cell.Style["padding"] = "2px";


			if (e.Day.IsOtherMonth)
			{
				e.Day.IsSelectable = false;
				e.Cell.Text = "";
				if (e.Day.Date.Day < 15)
				{
					e.Cell.Visible = false;
				}
			}
			else
			{
				int imps = 0;
				try
				{
					imps = (int)bs[0].ExtraSelectElements["sum"];
				}
				catch { }

				if (e.Day.Date == DateTime.Today)
					e.Cell.Style["background-color"] = "#CBA21E";

				e.Day.IsSelectable = false;
				e.Cell.Text = "<center><small>" + e.Day.DayNumberText + "</small>";
				if (imps > 0)
					e.Cell.Text += "<br><font size=2><b>" + imps.ToString("#,##0") + "</b></font>";

				e.Cell.Text += "</center>";
			}
		}
	}
}
