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
using System.Text;

namespace Spotted.Admin
{
	public partial class SalesCalls : AdminUserControl
	{
		#region CurrentUsr
		public Usr CurrentUsr
		{
			get
			{
				if (currentUsr == null && ContainerPage.Url["UsrK"].IsInt)
					currentUsr = new Usr(ContainerPage.Url["UsrK"]);
				return currentUsr;
			}
			set
			{
				currentUsr = value;
			}
		}
		public Usr currentUsr;
		#endregion

		protected Controls.Cal Cal;
		protected PlaceHolder SalesCallsPh;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!ContainerPage.Url.HasMonthFilter || !ContainerPage.Url["UsrK"].IsInt)
				Response.Redirect("/admin/salescalls/usrk-1/year-" + DateTime.Now.Year + "/month-" + DateTime.Now.Month);

			Query q1 = new Query();
			q1.QueryCondition = new Q(Usr.Columns.IsSalesPerson, true);
			q1.OrderBy = new OrderBy(Usr.Columns.K);
			UsrSet us = new UsrSet(q1);
			foreach (Usr u in us)
			{
				if (CurrentUsr != null && u.K == CurrentUsr.K)
					UsersPh.Controls.Add(new LiteralControl("<b>" + u.NickName + "</b>"));
				else
				{
					if (ContainerPage.Url.HasDayFilter)
					{
						UsersPh.Controls.Add(new LiteralControl("<a href=\"/admin/salescalls/usrk-" + u.K + "/year-" + ContainerPage.Url.DateFilter.Year + "/month-" + ContainerPage.Url.DateFilter.Month + "/day-" + ContainerPage.Url.DateFilter.Day + "\" " + u.Rollover + ">" + u.NickName + "</a>"));
					}
					else
					{
						UsersPh.Controls.Add(new LiteralControl("<a href=\"/admin/salescalls/usrk-" + u.K + "/year-" + ContainerPage.Url.DateFilter.Year + "/month-" + ContainerPage.Url.DateFilter.Month + "\" " + u.Rollover + ">" + u.NickName + "</a>"));
					}
				}
				UsersPh.Controls.Add(new LiteralControl("&nbsp;|&nbsp;"));

			}
			
			Cal.MonthUrlGetter = new Controls.Cal.MonthUrlDelegate(GetMonthUrl);
			Cal.DayUrlGetter = new Controls.Cal.DayUrlDelegate(GetDayUrl);
			Cal.DateTimeColumn = new Column(SalesCall.Columns.DateTimeStart);
			Cal.TableElement = new TableElement(TablesEnum.SalesCall);
			Cal.QueryCondition = new And(new Q(SalesCall.Columns.UsrK, CurrentUsr.K), new Q(SalesCall.Columns.IsCall, true));


			DateTime dtStart = new DateTime(ContainerPage.Url["Year"], ContainerPage.Url["Month"], 1);
			DateTime dtEnd = new DateTime(ContainerPage.Url["Year"], ContainerPage.Url["Month"], 1).AddMonths(1);
			if (ContainerPage.Url["Day"].Exists)
			{
				dtStart = new DateTime(ContainerPage.Url["Year"], ContainerPage.Url["Month"], ContainerPage.Url["Day"]);
				dtEnd = new DateTime(ContainerPage.Url["Year"], ContainerPage.Url["Month"], ContainerPage.Url["Day"]).AddDays(1);
			}

			

			Query q = new Query();
			q.QueryCondition = new And(
				new Q(SalesCall.Columns.UsrK, CurrentUsr.K),
				new Q(SalesCall.Columns.DateTimeStart, QueryOperator.GreaterThanOrEqualTo, dtStart),
				new Q(SalesCall.Columns.DateTimeStart, QueryOperator.LessThan, dtEnd)
			);
			q.OrderBy = new OrderBy(SalesCall.Columns.DateTimeStart);
			SalesCallSet scs = new SalesCallSet(q);

			StringBuilder sb = new StringBuilder();

			foreach (SalesCall sc in scs)
			{
				SalesCallToString(sc, sb);
			}
			SalesCallsPh.Controls.Add(new LiteralControl(sb.ToString()));



		}
		bool doneOne = false;
		bool lastColor = false;
		DateTime lastTime = DateTime.MinValue;

		#region SalesCallToString
		public void SalesCallToString(SalesCall sc, StringBuilder sb)
		{
			if (!doneOne || lastTime.Hour != sc.DateTimeStart.Hour)
			{
				if (doneOne)
					sb.Append("</div>");
				if (!ContainerPage.Url.HasDayFilter && lastTime.Day != sc.DateTimeStart.Day)
				{
					sb.Append("<h2 style=\"padding:2px 8px 2px 8px;\">" + sc.DateTimeStart.ToString("ddd dd") + "</h2>");
				}
				sb.Append("<div style=\"padding:2px 8px 2px 8px; background-color:#" + (lastColor ? "FECA26" : "FED551") + ";\">");
				lastColor = !lastColor;
			}
			lastTime = sc.DateTimeStart;
			doneOne = true;
			sb.Append("<p>");
			
			sb.Append("<small>");

			sb.Append(sc.Promoter.Link());
			sb.Append(", ");
			if (ContainerPage.Url.HasDayFilter)
				sb.Append(sc.DateTimeStart.ToString("HH:mm"));
			else
				sb.Append(sc.DateTimeStart.ToString("ddd dd HH:mm"));
			//sb.Append(Cambro.Misc.Utility.FriendlyDate(sc.DateTimeStart, false));
			sb.Append(", ");
			if (!sc.IsCall)
				sb.Append("note");
			else if (sc.Direction.Equals(SalesCall.Directions.Incoming))
				sb.Append("incoming call");
			else
			{
				if (sc.Effective)
					sb.Append("</small><b>effective</b> <small>");

				sb.Append("outgoing");

				if (sc.Type.Equals(SalesCall.Types.Cold))
					sb.Append(" cold call");
				else if (sc.Type.Equals(SalesCall.Types.ProactiveFollowUp))
					sb.Append(" follow up call");
				else
					sb.Append(" active call");
			}

			if (sc.IsCall)
			{
				sb.Append(", " + sc.Duration.ToString("0") + " min ");
				int stars = int.Parse(sc.Duration.ToString("0"));
				if (stars > 60)
					stars = 60;
				for (int i = 0; i < stars; i++)
					sb.Append("*");
			}

			if (sc.Note.Length > 0)
			{
				sb.Append("</small>");

				sb.Append("<br>");
				sb.Append(sc.Note.Replace("\n", "<br>"));
			}
			else
				sb.Append("</small>");

			sb.Append("</p>");
		}
		#endregion


		#region Cal
		public string GetMonthUrl(DateTime d, params object[] par)
		{
			return "/admin/salescalls/usrk-" + CurrentUsr.K + "/year-" + d.Year + "/month-" + d.Month;
		}
		public string GetDayUrl(DateTime d, params object[] par)
		{
			return "/admin/salescalls/usrk-" + CurrentUsr.K + "/year-" + d.Year + "/month-" + d.Month + "/day-" + d.Day;
		}
		#endregion
	}
}
