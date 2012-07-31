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

namespace Spotted.Admin
{
	public partial class Stats : AdminUserControl
	{
		protected Label UsersOnline5MinLabel, MaxUsersOnline5MinLabel, MaxUsersOnline5MinDateLabel, UsersOnline30MinLabel, MaxUsersOnline30MinLabel;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Query q = new Query();
			q.QueryCondition = Usr.LoggedInQ;
			q.NoLock = true;
			q.ReturnCountOnly = true;
			UsrSet us = new UsrSet(q);
			UsersOnline5MinLabel.Text = (us.Count == 1 ? "is " : "are ") + us.Count.ToString("#,##0") + " user" + (us.Count == 1 ? "" : "s");

			Query q30min = new Query();
			q30min.QueryCondition = Usr.LoggedIn30MinQ;
			q30min.NoLock = true;
			q30min.ReturnCountOnly = true;
			UsrSet us30min = new UsrSet(q30min);
			UsersOnline30MinLabel.Text = us30min.Count.ToString("#,##0");

			Bobs.Global gMaxUsers = new Bobs.Global(Bobs.Global.Records.MaxUsers5Min);
			MaxUsersOnline5MinLabel.Text = gMaxUsers.ValueInt.ToString("#,##0");
			MaxUsersOnline5MinDateLabel.Text = Cambro.Misc.Utility.FriendlyDate(gMaxUsers.ValueDateTime, true, false);
			Bobs.Global gMaxUsers30Min = new Bobs.Global(Bobs.Global.Records.MaxUsers30Min);
			MaxUsersOnline30MinLabel.Text = gMaxUsers30Min.ValueInt.ToString("#,##0");

			Page.DataBind();
		}
		protected string Number(int DateSpan, int StatType)
		{
			TimeSpan ts = DateTime.Today.Subtract(new DateTime(1970, 1, 1));
			int dayIndexNow = ts.Days;
			//return dayIndexNow.ToString();
			int dayMin = dayIndexNow;
			int dayMax = dayIndexNow;
			DateTime dateMin = DateTime.Today;
			DateTime dateMax = DateTime.Today.AddDays(1);
			bool estimateWholeDay = false;
			if (DateSpan == 10)
				estimateWholeDay = true;
			else if (DateSpan == 1)
			{
				dayMin = dayIndexNow - 1;
				dayMax = dayIndexNow - 1;
				dateMin = DateTime.Today.AddDays(-1);
				dateMax = DateTime.Today;
			}
			else if (DateSpan == 2)
			{
				dayMin = dayIndexNow - 7;
				dayMax = dayIndexNow - 1;
				dateMin = DateTime.Today.AddDays(-7);
				dateMax = DateTime.Today;
			}
			else if (DateSpan == 3)
			{
				dayMin = dayIndexNow - 30;
				dayMax = dayIndexNow - 1;
				dateMin = DateTime.Today.AddMonths(-1);
				dateMax = DateTime.Today;
			}
			else if (DateSpan == 4)
			{
				dayMin = dayIndexNow - 365;
				dayMax = dayIndexNow - 1;
				dateMin = DateTime.Today.AddDays(-365);
				dateMax = DateTime.Today;
			}
			else if (DateSpan == 5)
			{
				dayMin = 0;
				dayMax = dayIndexNow;
				dateMin = new DateTime(2000, 1, 1);
				dateMax = new DateTime(3000, 1, 1);
			}
			else if (DateSpan == 15)
			{
				dayMin = 0;
				dayMax = dayIndexNow;
				dateMin = DateTime.Now.AddMinutes(-5);
				dateMax = DateTime.Now;
			}
			else if (DateSpan == 130)
			{
				dayMin = 0;
				dayMax = dayIndexNow;
				dateMin = DateTime.Now.AddMinutes(-30);
				dateMax = DateTime.Now;
			}

			int number = 0;


			if (StatType == 1) // Uniques
			{
				Query q = new Query();
				q.NoLock = true;
				q.Columns = new ColumnSet();
				q.ExtraSelectElements["count"] = "count(distinct Guid)";
				q.QueryCondition = new And(
					new Or(new Q(Visit.Columns.DateTimeStart, QueryOperator.GreaterThanOrEqualTo, dateMin), new Q(Visit.Columns.DateTimeLast, QueryOperator.GreaterThanOrEqualTo, dateMin)),
					new Or(new Q(Visit.Columns.DateTimeStart, QueryOperator.LessThan, dateMax), new Q(Visit.Columns.DateTimeLast, QueryOperator.LessThan, dateMax)),
					new Or(new Q(Visit.Columns.Pages, QueryOperator.GreaterThan, 1), new Q(Visit.Columns.IsNewGuid, false))
					);

				VisitSet vs = new VisitSet(q);

				number = (int)vs[0].ExtraSelectElements["count"];
			}
			else if (StatType == 2) //Pages
			{
				Query q = new Query();
				q.NoLock = true;
				q.Columns = new ColumnSet();
				q.ExtraSelectElements["sum"] = "sum(Count)";
				q.QueryCondition = new And(
					new Q(Log.Columns.Date, QueryOperator.GreaterThanOrEqualTo, dateMin),
					new Q(Log.Columns.Date, QueryOperator.LessThan, dateMax),
					new Q(Log.Columns.Item, Log.Items.DsiPages)
					);

				LogSet ls = new LogSet(q);

				number = (int)ls[0].ExtraSelectElements["sum"];
			}
			else if (StatType == 4) //Photos
			{
				if (DateSpan == 5)
				{
					Query q = new Query();
					q.NoLock = true;
					q.OrderBy = new OrderBy(Photo.Columns.K, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Photo.Columns.K);
					q.TopRecords = 1;
					PhotoSet ps = new PhotoSet(q);
					number = ps[0].K;
				}
				else
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new And(
						Photo.EnabledQueryCondition,
						new Q(Photo.Columns.EnabledDateTime, QueryOperator.GreaterThanOrEqualTo, dateMin),
						new Q(Photo.Columns.EnabledDateTime, QueryOperator.LessThan, dateMax)
						);
					q.ReturnCountOnly = true;
					PhotoSet ps = new PhotoSet(q);
					number = ps.Count;
				}
			}
			else if (StatType == 5) //Events
			{
				if (DateSpan == 5)
				{
					Query q = new Query();
					q.NoLock = true;
					q.OrderBy = new OrderBy(Event.Columns.K, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Event.Columns.K);
					q.TopRecords = 1;
					EventSet es = new EventSet(q);
					number = es[0].K;
				}
				else
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new And(
						new Q(Event.Columns.AddedDateTime, QueryOperator.GreaterThanOrEqualTo, dateMin),
						new Q(Event.Columns.AddedDateTime, QueryOperator.LessThan, dateMax)
						);
					q.ReturnCountOnly = true;
					EventSet es = new EventSet(q);
					number = es.Count;
				}
			}
			else if (StatType == 6) //New users
			{
				if (DateSpan == 5)
				{
					Query q = new Query();
					q.NoLock = true;
					q.OrderBy = new OrderBy(Usr.Columns.K, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Usr.Columns.K);
					q.TopRecords = 1;
					UsrSet us = new UsrSet(q);
					number = us[0].K;
				}
				else
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new And(
						new Q(Usr.Columns.DateTimeSignUp, QueryOperator.GreaterThanOrEqualTo, dateMin),
						new Q(Usr.Columns.DateTimeSignUp, QueryOperator.LessThan, dateMax)
						);
					q.ReturnCountOnly = true;
					UsrSet us = new UsrSet(q);
					number = us.Count;
				}
			}
			else if (StatType == 7) //Users logged on
			{
				if (DateSpan == 5)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = Usr.IsNotSkeletonQ;
					q.ReturnCountOnly = true;
					UsrSet us = new UsrSet(q);
					number = us.Count;
				}
				else
				{
					Query q = new Query();
					q.NoLock = true;
					if (DateSpan == 1)
					{
						q.QueryCondition = new Q(Usr.Columns.DateTimeLastPageRequest, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddDays(-1));
					}
					else
					{
						q.QueryCondition = new And(
							new Q(Usr.Columns.DateTimeLastPageRequest, QueryOperator.GreaterThanOrEqualTo, dateMin),
							new Q(Usr.Columns.DateTimeLastPageRequest, QueryOperator.LessThan, dateMax)
							);
					}
					q.QueryCondition = new And(Usr.IsNotSkeletonQ, q.QueryCondition);
					q.ReturnCountOnly = true;
					UsrSet us = new UsrSet(q);
					number = us.Count;
				}
			}
			else if (StatType == 8) //Comments
			{
				if (DateSpan == 5)
				{
					Query q = new Query();
					q.NoLock = true;
					q.OrderBy = new OrderBy(Comment.Columns.K, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Comment.Columns.K);
					q.TopRecords = 1;
					CommentSet cs = new CommentSet(q);
					number = cs[0].K;
				}
				else
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new And(
						new Q(Comment.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, dateMin),
						new Q(Comment.Columns.DateTime, QueryOperator.LessThan, dateMax)
						);
					q.ReturnCountOnly = true;
					CommentSet cs = new CommentSet(q);
					number = cs.Count;
				}
			}
			else if (StatType == 9) //PMs
			{
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = new And(
					new Q(Comment.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, dateMin),
					new Q(Comment.Columns.DateTime, QueryOperator.LessThan, dateMax),
					new Q(Thread.Columns.Private, true),
					new Q(Comment.Columns.UsrK, QueryOperator.NotEqualTo, 7646)
					);
				q.TableElement = Comment.ThreadJoin;
				q.ReturnCountOnly = true;
				CommentSet cs = new CommentSet(q);
				number = cs.Count;
			}
			else if (StatType == 10) //Chat massages
			{
				if (DateSpan == 5)
				{
					Query q = new Query();
					q.NoLock = true;
					q.OrderBy = new OrderBy(ChatMessage.Columns.K, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(ChatMessage.Columns.K);
					q.TopRecords = 1;
					ChatMessageSet cms = new ChatMessageSet(q);
					number = cms[0].K;
				}
				else
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new And(
						new Q(ChatMessage.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, dateMin),
						new Q(ChatMessage.Columns.DateTime, QueryOperator.LessThan, dateMax)
						);
					q.ReturnCountOnly = true;
					ChatMessageSet cms = new ChatMessageSet(q);
					number = cms.Count;
				}
			}
			else if (StatType == 11) //Photo views
			{
				Query q = new Query();
				q.NoLock = true;
				q.Columns = new ColumnSet();
				q.ExtraSelectElements["sum"] = "sum(Count)";
				q.QueryCondition = new And(
					new Q(Log.Columns.Date, QueryOperator.GreaterThanOrEqualTo, dateMin),
					new Q(Log.Columns.Date, QueryOperator.LessThan, dateMax),
					new Q(Log.Columns.Item, Log.Items.PhotoImpressions)
				);

				LogSet ls = new LogSet(q);

				number = (int)ls[0].ExtraSelectElements["sum"];
			}
			else if (StatType == 12) // Unique members from visit table
			{
				Query q = new Query();
				q.NoLock = true;
				q.Columns = new ColumnSet();
				q.ExtraSelectElements["count"] = "count(distinct UsrK)";
				q.QueryCondition = new And(
					new Or(new Q(Visit.Columns.DateTimeStart, QueryOperator.GreaterThanOrEqualTo, dateMin), new Q(Visit.Columns.DateTimeLast, QueryOperator.GreaterThanOrEqualTo, dateMin)),
					new Or(new Q(Visit.Columns.DateTimeStart, QueryOperator.LessThan, dateMax), new Q(Visit.Columns.DateTimeLast, QueryOperator.LessThan, dateMax))
					);

				VisitSet vs = new VisitSet(q);

				number = (int)vs[0].ExtraSelectElements["count"];
			}
			else if (StatType == 13) //New users
			{
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = new And(
					new Q(Usr.Columns.DateTimeSignUp, QueryOperator.GreaterThanOrEqualTo, dateMin),
					new Q(Usr.Columns.DateTimeSignUp, QueryOperator.LessThan, dateMax),
					new Or(new Q(Usr.Columns.IsSkeleton, false), new Q(Usr.Columns.IsSkeleton, QueryOperator.IsNull, null))
					);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				number = us.Count;

			}
			else
				number = 0;


			//if (estimateWholeDay)
			//{
			//    number = (int)Banner.EstimateFullDay(DateTime.Now, number);
			//}
			if (number == 0)
				return "<small>n/a</small>";
			else
				return number.ToString("###,##0");

		}
	}
}
