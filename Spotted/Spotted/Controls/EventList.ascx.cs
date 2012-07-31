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

namespace Spotted.Controls
{
	public partial class EventList : System.Web.UI.UserControl
	{
		#region Properties
		public Model.Entities.ObjectType ParentObjectType { get; set; }
		public bool Future { get; set; }
		public Q Filter { get; set; }
		public TableElement Join { get; set; }
		public int Size { get; set; }
		public bool OnlyPhotos { get; set; }
		public ICalendar Calendar { get; set; }
		public string AddEventUrl { get; set; }
		public bool MusicFilterDropDown { get; set; }
		#endregion

		#region Date QueryOperators
		QueryOperator dateOpNow
		{
			get
			{
				return Future ? QueryOperator.GreaterThanOrEqualTo : QueryOperator.LessThan;
			}
		}
		QueryOperator dateOpNotNow
		{
			get
			{
				return Future ? QueryOperator.LessThan : QueryOperator.GreaterThanOrEqualTo;
			}
		}
		#endregion
		#region Relevant DateTimes
		DateTime dateNow
		{
			get
			{
				return DateTime.Now.AddDays(-1);
			}
		}
		DateTime dateWeek
		{
			get
			{
				return DateTime.Now.AddDays(-1).AddDays(Future ? 7 : -7);
			}
		}
		#endregion
		#region dateQ
		Q dateQ
		{
			get
			{
				return new Q(Event.Columns.DateTime, dateOpNow, dateNow);
			}
		}
		#endregion

		#region FilterByMusic
		protected bool FilterByMusic
		{
			get
			{
				return MusicFilterDropDown && Prefs.Current["MusicPref"].Exists && Prefs.Current["MusicPref"] != 1;
			}
		}
		#endregion
		#region MusicFilterQ
		protected Q MusicFilterQ
		{
			get
			{
				if (!FilterByMusic)
					return new Q(true);
				else
				{
					ArrayList al = new ArrayList();
					MusicType mt = new MusicType(Prefs.Current["MusicPref"]);
					//al.Add(new Q(EventMusicType.Columns.MusicTypeK,1));
					al.Add(new Q(EventMusicType.Columns.MusicTypeK, mt.K));
					MusicTypeSet mts = new MusicTypeSet(new Query(new Q(MusicType.Columns.ParentK, mt.K)));
					foreach (MusicType mtChild in mts)
					{
						al.Add(new Q(EventMusicType.Columns.MusicTypeK, mtChild.K));
					}
					Q[] qArr = (Q[])al.ToArray(typeof(Q));
					return new Or(qArr);
				}
			}
		}
		#endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			MusicFilterP.Visible = MusicFilterDropDown;
			MusicFilterP1.Visible = MusicFilterDropDown;
		}
		#endregion

		#region Bind()
		public void Bind()
		{
			//Header.InnerText = Future ? "Next events" : "Past events";

			OnlyPhotosP.Visible = OnlyPhotos;

			#region Get weekCount
			Query qWeek = new Query();
			qWeek.QueryCondition = new And(
				Filter,
				dateQ,
				new Q(Event.Columns.DateTime, dateOpNotNow, dateWeek),
				MusicFilterQ
			);
			if (FilterByMusic)
			{
				qWeek.TableElement = new Join(
					Join,
					new TableElement(TablesEnum.EventMusicType),
					QueryJoinType.Left,
					Event.Columns.K,
					EventMusicType.Columns.EventK);
				qWeek.Distinct = true;
				qWeek.DistinctColumn = Event.Columns.K;
			}
			else
				qWeek.TableElement = Join;
			qWeek.ReturnCountOnly = true;
			EventSet esWeek = new EventSet(qWeek);
			int weekCount = esWeek.Count;
			#endregion

			int maxSize1 = Size;
			int maxSize2 = Size * 2;
			int maxSize3 = Size * 4;

			#region Determine template type and toprecords
			int type = 1;
			int top = maxSize1;

			if (weekCount > maxSize1)
			{
				type = 2;
				top = maxSize2;
			}

			if (weekCount > maxSize2)
			{
				type = 3;
				top = maxSize3;
			}
			#endregion

			#region Get EventSet
			Query eventsShowQuery = new Query();
			eventsShowQuery.QueryCondition = new And(
				Filter,
				dateQ,
				MusicFilterQ
			);
			if (Join == null)
				Join = new TableElement(TablesEnum.Event);
			if (FilterByMusic)
			{
				eventsShowQuery.TableElement = new Join(
					Join,
					new TableElement(TablesEnum.EventMusicType),
					QueryJoinType.Left,
					Event.Columns.K,
					EventMusicType.Columns.EventK);
				eventsShowQuery.Distinct = true;
				eventsShowQuery.DistinctColumn = Event.Columns.K;
			}
			else
				eventsShowQuery.TableElement = Join;

			if (eventsShowQuery.Distinct)
			{
				eventsShowQuery.DataTableElement = Join;
			}
			eventsShowQuery.Columns = Event.EventsForDisplay.EventListColumnSet;
			eventsShowQuery.TopRecords = top;
			eventsShowQuery.OrderBy = Future ? Event.FutureEventOrder : Event.PastEventOrder;
			EventSet esShow = new EventSet(eventsShowQuery);
			#endregion

			if (esShow.Count > 0)
			{

				if (AddEventUrl != null)
					AddEventLink1.HRef = AddEventUrl;

				MoreEventsP.Visible = (esShow.Count == top) && !OnlyPhotos;

				DateTime dtLast = esShow[esShow.Count - 1].DateTime;
				CalendarLinkBottomDateLabel.Text = Cambro.Misc.Utility.FriendlyDate(dtLast, false, true);

				if (Calendar != null)
				{
					CalendarLink.HRef = Calendar.UrlCalendar();
					CalendarLink1.HRef = Calendar.UrlCalendar();

					CalendarLinkBottom.HRef = Calendar.UrlCalendarDay(dtLast.Year, dtLast.Month, dtLast.Day);
				}
				else
				{
					CalendarLink.HRef = Calendar.UrlCalendar();
					CalendarLink1.HRef = Calendar.UrlCalendar();

					CalendarLinkBottom.HRef = Calendar.UrlCalendarDay(dtLast.Year, dtLast.Month, dtLast.Day);
				}

				EventsPanel.Visible = true;
				NoEventsPanel.Visible = false;
				EventsDataList.DataSource = esShow;
				if (type == 1)
					EventsDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventList1.ascx");
				else if (type == 2)
					EventsDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventList2.ascx");
				else
					EventsDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventList3.ascx");


				//EventsDataList.DataBinding
				EventsDataList.ItemDataBound += new DataListItemEventHandler(EventsDataList_ItemDataBound);
				EventsDataList.PreRender += new EventHandler(EventsDataList_PreRender);
				EventsDataList.DataBind();

			}
			else
			{
				if (AddEventUrl != null)
					AddEventLink.HRef = AddEventUrl;

				EventsPanel.Visible = false;
				NoEventsPanel.Visible = true;
			}
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		public bool ShowDateTitles = false;
		Hashtable items = new Hashtable();

		#region EventsDataList_PreRender
		private void EventsDataList_PreRender(object sender, EventArgs ev)
		{
			int totalDays = 0;
			DateTime lastDate = new DateTime(1, 1, 1);
			foreach (DataListItem di in EventsDataList.Items)
			{
				if (items[di.ItemIndex] != null)
				{
					if ((DateTime)items[di.ItemIndex] != lastDate)
					{
						totalDays++;
						lastDate = (DateTime)items[di.ItemIndex];
					}
				}
			}
			double itemCount = (double)EventsDataList.Items.Count;
			double days = (double)totalDays;
			if ((itemCount / days) >= 2.0)
			{
				ShowDateTitles = true;
				DateTime lastDate1 = new DateTime(1, 1, 1);
				foreach (DataListItem di in EventsDataList.Items)
				{
					if (items[di.ItemIndex] != null)
					{
						DateTime date = (DateTime)items[di.ItemIndex];
						if (date != lastDate1)
						{
							lastDate1 = date;
							di.Controls.AddAt(0, new LiteralControl("<h2>" + Cambro.Misc.Utility.FriendlyDate(date, true) + "</h2>"));

						}
					}
				}
			}
			else
			{
				MoreEventsBottomP.Visible = false;
			}
		}
		#endregion
		#region EventsDataList_ItemDataBound
		private void EventsDataList_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if (e.Item.DataItem != null && !items.ContainsKey(e.Item.ItemIndex))
				items.Add(e.Item.ItemIndex, ((Event)e.Item.DataItem).DateTime);
		}
		#endregion
	}
}
