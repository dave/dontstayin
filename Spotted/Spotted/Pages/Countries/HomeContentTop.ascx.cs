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

namespace Spotted.Pages.Countries
{
	public partial class HomeContentTop : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{

		}

		#region ContainerPage
		public Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
			}
		}
		#endregion
		#region CountryK
		public int CountryK
		{
			get
			{
				if (ContainerPage.Url.HasCountryObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["K"];
			}
		}
		#endregion
		#region CurrentCountry
		public Country CurrentCountry
		{
			get
			{
				if (currentCountry == null)
				{
					if (ContainerPage.Url.HasCountryObjectFilter)
						currentCountry = ContainerPage.Url.ObjectFilterCountry;
					else if (CountryK > 0)
						currentCountry = new Country(CountryK);
				}
				return currentCountry;
			}
			set
			{
				currentCountry = value;
			}
		}
		private Country currentCountry;
		#endregion

		#region TopPlacesPanel
		protected Panel TopPlacesPanel, TopPlacesAnchorPanel;
		protected DataList TopPlacesDataList;
		protected HtmlAnchor TopPlacesAnchor;
		public void TopPlacesPanel_Load(object o, System.EventArgs e)
		{

			Query q = new Query();
			q.QueryCondition = new And(new Q(Place.Columns.CountryK, CurrentCountry.K), new Q(Place.Columns.Enabled, true));
			q.OrderBy = new OrderBy(new OrderBy(Place.Columns.TotalEvents, OrderBy.OrderDirection.Descending), new OrderBy(Place.Columns.Population, OrderBy.OrderDirection.Descending));
			q.TopRecords = 20;
			PlaceSet ps = new PlaceSet(q);
			if (ps.Count == 0)
			{
				TopPlacesPanel.Visible = false;
			}
			else
			{
				TopPlacesDataList.ItemTemplate = this.LoadTemplate("/Templates/Places/CountryTopPlacesList.ascx");
				TopPlacesDataList.DataSource = ps;
				TopPlacesDataList.DataBind();
			}
			if (ps.Count == 20)
			{
				TopPlacesAnchor.HRef = CurrentCountry.UrlApp("places");
				TopPlacesAnchor.InnerText = TopPlacesAnchor.InnerText.Replace("???", CurrentCountry.FriendlyName);
			}
			else
				TopPlacesAnchorPanel.Visible = false;
		}

		#endregion
		#region IntroPanel
		protected Panel IntroPanel;
		protected Panel CustomHtmlPanel;
		protected Spotted.CustomControls.h1 IntroHeader, CustomHtmlHeader;
		protected PlaceHolder CustomHtmlPlaceHolder;
		protected HtmlImage FlagImg;
		protected HtmlAnchor DiscussionLink, CalendarLink;
		protected Label DiscussionLinkCountryLabel, CalendarLinkCountryLabel;
		protected HtmlAnchor HomePageLink;
		protected HtmlAnchor HotTopicsLink;
		protected Label HotTopicsLinkCountryLabel;
		public void IntroPanel_Load(object o, System.EventArgs e)
		{
			IntroHeader.InnerText = CurrentCountry.Name + " home page";
			FlagImg.Src = CurrentCountry.FlagUrl();
			DiscussionLink.HRef = CurrentCountry.UrlDiscussion();
			DiscussionLinkCountryLabel.Text = CurrentCountry.FriendlyName;
			HotTopicsLink.HRef = CurrentCountry.UrlHotTopics();
			HotTopicsLinkCountryLabel.Text = CurrentCountry.FriendlyName;
			CalendarLink.HRef = CurrentCountry.UrlCalendar();
			CalendarLinkCountryLabel.Text = CurrentCountry.FriendlyName;
			HotTicketsLink.HRef = CurrentCountry.UrlApp("hottickets");
			HotTicketsLinkCountryLabel.Text = CurrentCountry.FriendlyName;
			TicketsLink.HRef = CurrentCountry.UrlCalendar(true, false);
			TicketsLinkCountryLabel.Text = CurrentCountry.FriendlyName;
			FreeGuestlistLink.HRef = CurrentCountry.UrlCalendar(false, true);
			FreeGuestlistLinkCountryLabel.Text = CurrentCountry.FriendlyName;
			HomePageLink.InnerText = HomePageLink.InnerText.Replace("?", CurrentCountry.FriendlyName);
			HomePageLink.HRef = CurrentCountry.Url() + "?ChangeHomeCountryK=" + CurrentCountry.K;
			if (CurrentCountry.CustomHtml.Length > 0)
			{
				CustomHtmlPanel.Visible = true;
				//CustomHtmlHeader.InnerText=CurrentCountry.FriendlyName;
				CustomHtmlPlaceHolder.Controls.Clear();
				CustomHtmlPlaceHolder.Controls.Add(new LiteralControl(CurrentCountry.CustomHtml));
			}
			else
				CustomHtmlPanel.Visible = false;
		}

		#endregion

		#region PhotoOfWeek
		protected Panel TopPhotosPanel;
		protected DataList TopPhotosDataList;
		private void PhotoOfWeek_Load(object sender, System.EventArgs e)
		{
			Query q = new Query();
			q.Columns = Templates.Photos.TopPhoto.Columns;
			q.TableElement = Photo.PlaceLeftJoin;
			q.QueryCondition = new And(new Q(Photo.Columns.PhotoOfWeek, true), new Q(Place.Columns.CountryK, CurrentCountry.K));
			q.OrderBy = new OrderBy(Photo.Columns.PhotoOfWeekDateTime, OrderBy.OrderDirection.Descending);
			q.TopRecords = 8;
			PhotoSet ps = new PhotoSet(q);
			if (ps.Count != 8)
			{
				TopPhotosPanel.Visible = false;
			}
			else
			{
				TopPhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/TopPhoto.ascx");
				TopPhotosDataList.DataSource = ps;
				TopPhotosDataList.DataBind();
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
			this.Load += new System.EventHandler(this.PhotoOfWeek_Load);
			this.Load += new System.EventHandler(this.IntroPanel_Load);
			this.Load += new System.EventHandler(this.TopPlacesPanel_Load);


		}
		#endregion
	}
}
