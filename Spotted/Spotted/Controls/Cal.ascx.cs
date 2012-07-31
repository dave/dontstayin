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
using System.Drawing;

namespace Spotted.Controls
{
	public partial class Cal : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.Calendar_Load(sender, e);
		}

		#region Public items
		public delegate string MonthUrlDelegate(DateTime d, params object[] par);
		public delegate string DayUrlDelegate(DateTime d, params object[] par);
		public MonthUrlDelegate MonthUrlGetter;
		public DayUrlDelegate DayUrlGetter;
		public Column DateTimeColumn { get; set; }
		public TableElement TableElement { get; set; }
		public Q QueryCondition { get; set; }
		#endregion

		private Hashtable Traffic;
		#region CalendarPanel
		 
		#region Calendar_Load
		public void Calendar_Load(object o, System.EventArgs e)
		{
			CalendarPanel.Visible = ContainerPage.Url.HasDayFilter || ContainerPage.Url.HasMonthFilter;

			#region Set up calendar control
			if (ContainerPage.Url.HasDayFilter)
			{
				DateCal.SelectedDate = ContainerPage.Url.DateFilter;
				DateCal.VisibleDate = ContainerPage.Url.DateFilter;
				//CalendarTitleCell.Attributes["onmouseover"] = "this.style.backgroundColor='#FED551';";
				CalendarTitleCell.Style["cursor"] = "pointer";
				//CalendarTitleCell.Attributes["onmouseout"] = "this.style.backgroundColor='#FECA26';";
				CalendarTitleCell.Attributes["onclick"] = "document.location='" + MonthUrlGetter(ContainerPage.Url.DateFilter) + "';";
			}
			else if (ContainerPage.Url.HasMonthFilter)
			{
				DateCal.VisibleDate = ContainerPage.Url.DateFilter;

				CalendarTitleCell.Attributes["class"] = "BorderBlack All";
				CalendarTitleCell.Style["border-width"] = "2px";
			}
			#endregion
			if (ContainerPage.Url.HasDayFilter || ContainerPage.Url.HasMonthFilter)
			{
				#region Find number of comments on each day
				Query q = new Query();

				q.Columns = new ColumnSet();
				q.ExtraSelectElements.Add("count", "COUNT(*)");
				q.ExtraSelectElements.Add("day", "DAY(" + DateTimeColumn.InternalSqlName + ")");
				q.ExtraSelectElements.Add("month", "MONTH(" + DateTimeColumn.InternalSqlName + ")");
				q.ExtraSelectElements.Add("year", "YEAR(" + DateTimeColumn.InternalSqlName + ")");

				q.TableElement = this.TableElement;

				q.GroupBy = new GroupBy("DAY(" + DateTimeColumn.InternalSqlName + "), MONTH(" + DateTimeColumn.InternalSqlName + "), YEAR(" + DateTimeColumn.InternalSqlName + ")");

				q.QueryCondition = new And(
					new Q(DateTimeColumn, QueryOperator.GreaterThanOrEqualTo, new DateTime(ContainerPage.Url.DateFilter.Year, ContainerPage.Url.DateFilter.Month, 1).AddDays(-8)),
					new Q(DateTimeColumn, QueryOperator.LessThan, new DateTime(ContainerPage.Url.DateFilter.Year, ContainerPage.Url.DateFilter.Month, 1).AddMonths(1).AddDays(8)),
					this.QueryCondition);
				CommentSet days = new CommentSet(q);
				Traffic = new Hashtable();
				int TotalCount = 0;
				foreach (Comment c in days)
				{
					string date = c.ExtraSelectElements["year"] + "-" + c.ExtraSelectElements["month"] + "-" + c.ExtraSelectElements["day"];
					int val = (int)c.ExtraSelectElements["count"];
					Traffic[date] = val;
					TotalCount += val;
				}
				#endregion

				bool ShowPrevMonth = true;
				bool ShowNextMonth = true;
				DateTime NextMonth = ContainerPage.Url.DateFilter.AddMonths(1);
				DateTime PrevMonth = ContainerPage.Url.DateFilter.AddMonths(-1);

				if (TotalCount == 0)
				{
					Query PrevQ = new Query();
					PrevQ.TopRecords = 1;
					PrevQ.OrderBy = new OrderBy(DateTimeColumn.ColumnEnum, OrderBy.OrderDirection.Descending);
					PrevQ.TableElement = this.TableElement;
					PrevQ.Columns = new ColumnSet();
					PrevQ.ExtraSelectElements.Add("datetime", DateTimeColumn.InternalSqlName);
					PrevQ.QueryCondition = new And(this.QueryCondition,
						new Q(this.DateTimeColumn, QueryOperator.LessThan, ContainerPage.Url.DateFilter));
					CommentSet PrevBobSet = new CommentSet(PrevQ);
					if (PrevBobSet.Count == 0)
						ShowPrevMonth = false;
					else
						PrevMonth = (DateTime)PrevBobSet[0].ExtraSelectElements["datetime"];

					Query NextQ = new Query();
					NextQ.TopRecords = 1;
					NextQ.OrderBy = new OrderBy(DateTimeColumn.ColumnEnum, OrderBy.OrderDirection.Ascending);
					NextQ.TableElement = this.TableElement;
					NextQ.Columns = new ColumnSet();
					NextQ.ExtraSelectElements.Add("datetime", DateTimeColumn.InternalSqlName);
					NextQ.QueryCondition = new And(this.QueryCondition,
						new Q(this.DateTimeColumn, QueryOperator.GreaterThan, ContainerPage.Url.DateFilter));
					CommentSet NextBobSet = new CommentSet(NextQ);
					if (NextBobSet.Count == 0)
						ShowNextMonth = false;
					else
						NextMonth = (DateTime)NextBobSet[0].ExtraSelectElements["datetime"];

				}


				#region Back, month and forward links
				if (ShowPrevMonth)
				{
					if (PrevMonth.Year == DateTime.Today.Year)
						CalendarBackLabel.Text = PrevMonth.ToString("MMMM");
					else
						CalendarBackLabel.Text = PrevMonth.ToString("MMMM yy");

					CalendarBackLink.HRef = MonthUrlGetter(PrevMonth);
				}
				else
					CalendarBackLink.Visible = false;

				if (ContainerPage.Url.DateFilter.Year == DateTime.Today.Year)
					CalendarMiddleLink.InnerText = ContainerPage.Url.DateFilter.ToString("MMMM");
				else
					CalendarMiddleLink.InnerText = ContainerPage.Url.DateFilter.ToString("MMMM yy");

				if (ContainerPage.Url.HasDayFilter)
					CalendarMiddleLink.HRef = MonthUrlGetter(ContainerPage.Url.DateFilter);


				if (ShowNextMonth)
				{
					if (NextMonth.Year == DateTime.Today.Year)
						CalendarForwardLabel.Text = NextMonth.ToString("MMMM");
					else
						CalendarForwardLabel.Text = NextMonth.ToString("MMMM yy");

					CalendarForwardLink.HRef = MonthUrlGetter(NextMonth);
				}
				else
					CalendarForwardLink.Visible = false;
				#endregion

			}
		}
		#endregion
		#region DateCal_DayRender
		public void DateCal_DayRender(object o, DayRenderEventArgs e)
		{
			int items = 0;
			e.Cell.Controls.Clear();

			string start = "";
			string end = "";
			string backColor = "FFFFFF";
			string date = e.Day.Date.Year + "-" + e.Day.Date.Month + "-" + e.Day.Date.Day;
			int perc = 0;
			if (Traffic[date] != null && (int)Traffic[date] > 0)
			{
				items = (int)Traffic[date];
				perc = 40 + (int)(items * 1.5);
			}
			else
			{
				perc = 0;
			}


			if (perc > 90)
				perc = 90;
			float frac = (float)2 - ((float)perc / (float)100);
			//float frac = (float)perc / (float)100;

			Color baseCol = Cambro.Misc.ColorHelp.HexStringToColor(backColor);
			Color newCol = Cambro.Misc.RGBHSL.ModifyBrightness(baseCol, frac);
			backColor = Cambro.Misc.ColorHelp.ColorToHexString(newCol);

			//if (e.Day.IsOtherMonth)
			//{
			//    Color textCol = Cambro.Misc.RGBHSL.ModifyBrightness(newCol, (float)0.66);
			//    textColor = Cambro.Misc.ColorHelp.ColorToHexString(textCol);
			//}


			e.Cell.Style["background-color"] = "#" + backColor;
			e.Cell.Style["font-size"] = "12px";

			if (items > 0)
			{
				e.Cell.Attributes["onmouseover"] = "stt('" + items.ToString("#,##0") + " item" + (items == 1 ? "" : "s") + "');";
				e.Cell.Attributes["onmouseout"] = "htm();";
			}

			if (!e.Day.IsSelected)
			{
				if (e.Day.IsOtherMonth)
					e.Cell.Controls.Add(new LiteralControl("<small>" + e.Day.DayNumberText + "</small>"));
				else
					e.Cell.Controls.Add(new LiteralControl("<a href=\"" + DayUrlGetter(e.Day.Date) + "\">" + start + e.Day.DayNumberText + end + "</a>"));
				//e.Cell.Attributes["onmouseover"] = "this.className='BackgroundColorLight';";
				//e.Cell.Attributes["onmouseout"] = "this.className='';";
				e.Cell.Attributes["onclick"] = "document.location='" + DayUrlGetter(e.Day.Date) + "'";
				e.Cell.Style["cursor"] = "pointer";
			}
			else
			{
				e.Cell.Attributes["class"] = "BorderBlack All";
				e.Cell.Style["border-width"] = "2px";
				e.Cell.Controls.Add(new LiteralControl("<b>" + e.Day.DayNumberText + "</b>"));
			}
		}
		#endregion
		#endregion

		#region ContainerPage
		protected Spotted.GenericPage ContainerPage
		{
			get
			{
				return (Spotted.GenericPage)Page;
			}
		}
		#endregion


	 
	}
}
