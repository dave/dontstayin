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

namespace Spotted.Pages
{
	public partial class HotForums : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			BindBoards();
		}

		#region HotTopicsCountryK
		public int HotTopicsCountryK
		{
			get
			{
				if (ContainerPage.Url[0].Exists)
					return ContainerPage.Url.GetCountryK(ContainerPage.Url[0].Raw);
				else
					return 0;
			}
		}
		#endregion
		#region HotTopicsCountry
		public Country HotTopicsCountry
		{
			get
			{
				if (hotTopicsCountry == null && HotTopicsCountryK > 0)
					hotTopicsCountry = new Country(HotTopicsCountryK);
				return hotTopicsCountry;
			}
			set
			{
				hotTopicsCountry = value;
			}
		}
		private Country hotTopicsCountry;
		#endregion

		#region PanelBoardList
		protected Panel PanelBoardList, BoardPlacePanel, BoardEventPanel, BoardThreadPanel;
		protected DataGrid BoardDataGrid, BoardPlaceDataGrid, BoardEventDataGrid, BoardThreadDataGrid;
		protected HtmlAnchor HotTopicsHomeCountryLink, HotTopicsCountryLink;
		protected Panel HotTopicsCountryPanel, HotTopicsWorldwidePanel;
		void BindBoards()
		{
			if (HotTopicsCountry == null)
			{
				HotTopicsHomeCountryLink.InnerText = HotTopicsHomeCountryLink.InnerText.Replace("???", Country.Current.FriendlyName);
				HotTopicsHomeCountryLink.HRef = Country.Current.UrlHotTopics();

				HotTopicsCountryPanel.Visible = false;
				HotTopicsWorldwidePanel.Visible = true;

				SetPageTitle("Hot forums worldwide");
			}
			else
			{
				HotTopicsCountryLink.InnerText = HotTopicsCountry.FriendlyName;
				HotTopicsCountryLink.HRef = HotTopicsCountry.Url();

				HotTopicsCountryPanel.Visible = true;
				HotTopicsWorldwidePanel.Visible = false;

				SetPageTitle("Hot forums in " + HotTopicsCountry.FriendlyName);
			}

			Q HotTopicsCountryPlaceFilter = new Q(true);
			Q HotTopicsCountryThreadFilter = new Q(true);
			if (HotTopicsCountry != null)
			{
				HotTopicsCountryPlaceFilter = new Q(Place.Columns.CountryK, HotTopicsCountry.K);
				HotTopicsCountryThreadFilter = new Q(Thread.Columns.CountryK, HotTopicsCountry.K);
			}

			Query qPlace = new Query();
			qPlace.TopRecords = 20;
			qPlace.QueryCondition = new And(
				new Q(Place.Columns.TotalComments, QueryOperator.GreaterThan, 0),
				HotTopicsCountryPlaceFilter
				);
			qPlace.OrderBy = new OrderBy("(TotalComments - (Population/12.0)) DESC");
			PlaceSet ts = new PlaceSet(qPlace);
			if (ts.Count > 0)
			{
				BoardPlaceDataGrid.DataSource = ts;
				BoardPlaceDataGrid.DataBind();
			}
			else
				BoardPlacePanel.Visible = false;

			Query qEvent = new Query();
			qEvent.TopRecords = 20;
			qEvent.TableElement = Event.PlaceAllJoin;
			qEvent.QueryCondition = new And(
				new Q(Event.Columns.TotalComments, QueryOperator.GreaterThan, 0),
				HotTopicsCountryPlaceFilter
				);
			qEvent.OrderBy = new OrderBy("(Event.TotalComments - (CASE SIGN(DATEDIFF(day, Event.DateTime, GetDate())*2) WHEN 1 THEN DATEDIFF(day, Event.DateTime, GetDate())*2 WHEN 0 THEN 0 ELSE 0 END)) DESC");
			EventSet es = new EventSet(qEvent);
			if (es.Count > 0)
			{
				BoardEventDataGrid.DataSource = es;
				BoardEventDataGrid.DataBind();
			}
			else
				BoardEventPanel.Visible = false;

			Query qThread = new Query();
			qThread.TopRecords = 20;
			qThread.QueryCondition = new And(
				new Q(Thread.Columns.TotalComments, QueryOperator.GreaterThan, 2),
				new Q(Thread.Columns.Enabled, true),
				new Q(Thread.Columns.Private, false),
				new Q(Thread.Columns.GroupPrivate, false),
				new Q(Thread.Columns.PrivateGroup, false),
				HotTopicsCountryThreadFilter,
				new Q(Thread.Columns.HideFromHighlights, false)
				);
			qThread.OrderBy = Thread.HotTopicsOrderBy;
			ThreadSet threadSet = new ThreadSet(qThread);
			if (threadSet.Count > 0)
			{
				BoardThreadDataGrid.DataSource = threadSet;
				BoardThreadDataGrid.DataBind();
			}
			else
				BoardThreadPanel.Visible = false;
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
	}
}
