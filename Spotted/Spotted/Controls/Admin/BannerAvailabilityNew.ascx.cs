using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using Common.Clocks;
using Common;
using System.Drawing;

namespace Spotted.Controls.Admin
{
	public partial class BannerAvailabilityNew : System.Web.UI.UserControl
	{
		protected TableRow Row;
		public Banner.Positions Position { get; set; }

		private void Page_Load(object sender, System.EventArgs e)
		{
			for (int month = -1; month < 2; month++)
			{
				TableCell tc = new TableCell();
				tc.VerticalAlign = VerticalAlign.Top;

				System.Web.UI.WebControls.Calendar c = new System.Web.UI.WebControls.Calendar();
				c.VisibleDate = Time.Today.AddMonths(month);
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
			if (e.Day.IsOtherMonth)
			{
				e.Cell.Text = "";
				if (e.Day.Date.Day < 15)
				{
					e.Cell.Visible = false;
				}
				return;
			}

			DateTime date = e.Day.Date;

			int hits = 0;
			bool predictHits = date.CompareTo(Time.Today) >= 0;
			if (predictHits)
			{
				Bobs.BannerServer.Traffic.DataDrivenTrafficShape ts = new Bobs.BannerServer.Traffic.DataDrivenTrafficShape();
				hits = (int)Math.Floor(ts.GetPredictedCountOfLogItemBetweenDates(
					GetLogItemTypeFromPositionType(Position), date, date.AddDays(1)));
			}
			else
			{
				hits = Log.GetCount(Banner.GetLogItemTypeFromPositionType(Position), date);
			}

			// get total required impressions for all banners of this position for this day
			long requiredImpressions = Banner.GetPredictedRequiredImpressions(date, Position);

			e.Day.IsSelectable = false;

			if (hits > 0)
			{
				int perc = (int) Math.Round(100 * (((double) requiredImpressions) / ((double) hits)));
				e.Cell.Style["padding"] = "2px";
				e.Cell.Text = "<center><small>" + e.Day.DayNumberText + "</small>" +
				              "<br><font size=2><b><nobr>" + perc.ToString("n0") + "</nobr></b></font></center>";
				e.Cell.ToolTip = "Required impressions: " + requiredImpressions.ToString("N0") +
				                 (predictHits ? "\nPredicted" : "\nActual") + " hits: " + hits.ToString("N0");


				Color baseCol = Cambro.Misc.ColorHelp.HexStringToColor("FECA26");
				Color newCol = Cambro.Misc.RGBHSL.ModifyBrightness(baseCol, .2 + .8 * Math.Max(0, (100 - perc) / 100.0));
				e.Cell.Style["background-color"] = "#" + Cambro.Misc.ColorHelp.ColorToHexString(newCol);
			}
		}

		private static Bobs.Log.Items GetLogItemTypeFromPositionType(Banner.Positions position)
		{
			switch (position)
			{
				case Bobs.Banner.Positions.Hotbox: 
				case Bobs.Banner.Positions.Leaderboard:
				case Bobs.Banner.Positions.Skyscraper: return Log.Items.DsiPageRender;
				case Bobs.Banner.Positions.PhotoBanner: return Log.Items.PhotoImpressions;
				case Bobs.Banner.Positions.EmailBanner: return Log.Items.EmailsSent;
				default: throw new Exception("Unexpected banner position.");
			}
		}
	}
}
