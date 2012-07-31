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

namespace Spotted.Pages.Usrs
{
	public partial class FavouritePhotos2 : UsrUserControl
	{
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Inbox))
					ChangePanel(PanelPhotos);
			}

			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}

		#region InfoPanel
		protected Spotted.CustomControls.UsrIntro UsrIntro;
		public void InfoPanel_Load(object o, System.EventArgs e)
		{
			ContainerPage.SetPageTitle(ThisUsr.NickName + "'s favourite photos");
			UsrIntro.Header = ThisUsr.NickName + "'s favourite photos";
		}
		#endregion

		#region Cal
		protected Controls.Cal Cal;
		public void Cal_Load(object o, System.EventArgs e)
		{
			Cal.MonthUrlGetter = new Controls.Cal.MonthUrlDelegate(GetMonthUrl);
			Cal.DayUrlGetter = new Controls.Cal.DayUrlDelegate(GetDayUrl);
			Cal.DateTimeColumn = new Column(Photo.Columns.ParentDateTime);
			Cal.TableElement = new Join(Photo.Columns.K, UsrPhotoFavourite.Columns.PhotoK);
			Cal.QueryCondition = new And(Photo.EnabledQueryCondition, new Q(UsrPhotoFavourite.Columns.UsrK, ThisUsr.K));
		}
		public string GetMonthUrl(DateTime d, params object[] par)
		{
			return ThisUsr.UrlFavouritePhotosMonth(d);
		}
		public string GetDayUrl(DateTime d, params object[] par)
		{
			return ThisUsr.UrlFavouritePhotosDate(d);
		}
		#endregion

		#region PanelPhotos
		protected Panel PanelPhotos;
		protected Panel PhotosPanel, NoPhotosPanel;
		protected HtmlGenericControl PhotosPageP, PhotosPageP1;
		protected HyperLink PhotosNextPageLink, PhotosNextPageLink1,
			PhotosPrevPageLink, PhotosPrevPageLink1;
		protected DataList PhotosDataList;

		private void PanelPhotos_Load(object sender, System.EventArgs e)
		{
			this.BindPhotos();
		}
		#region BindPhotos()
		void BindPhotos()
		{
			if (PhotosPage != Photos.Paging.ReturnedPage)
				PhotosPage = Photos.Paging.ReturnedPage;


			if (Photos.Count == 0)
			{
				PhotosPanel.Visible = false;
				NoPhotosPanel.Visible = true;
			}
			else
			{
				PhotosPanel.Visible = true;
				NoPhotosPanel.Visible = false;

				if (Photos.Paging.ShowNoLinks)
				{
					PhotosPageP.Visible = false;
					PhotosPageP1.Visible = false;
				}
				else
				{
					PhotosPageP.Visible = true;
					PhotosPageP1.Visible = true;

					string urlNextPage = ContainerPage.Url.CurrentUrl("p", ((int)(PhotosPage + 1)).ToString());
					string urlPrevPage = "";
					if (PhotosPage == 2)
						urlPrevPage = ContainerPage.Url.CurrentUrl("p", null);
					else
						urlPrevPage = ContainerPage.Url.CurrentUrl("p", ((int)(PhotosPage - 1)).ToString());

					PhotosNextPageLink1.Enabled = Photos.Paging.ShowNextPageLink;
					PhotosNextPageLink1.NavigateUrl = urlNextPage;
					PhotosPrevPageLink1.Enabled = Photos.Paging.ShowPrevPageLink;
					PhotosPrevPageLink1.NavigateUrl = urlPrevPage;

					PhotosNextPageLink.Enabled = Photos.Paging.ShowNextPageLink;
					PhotosNextPageLink.NavigateUrl = urlNextPage;
					PhotosPrevPageLink.Enabled = Photos.Paging.ShowPrevPageLink;
					PhotosPrevPageLink.NavigateUrl = urlPrevPage;

					if (!PhotosNextPageLink1.Enabled)
						PhotosNextPageLink1.CssClass = "DisabledAnchor";
					if (!PhotosNextPageLink.Enabled)
						PhotosNextPageLink.CssClass = "DisabledAnchor";
					if (!PhotosPrevPageLink1.Enabled)
						PhotosPrevPageLink1.CssClass = "DisabledAnchor";
					if (!PhotosPrevPageLink.Enabled)
						PhotosPrevPageLink.CssClass = "DisabledAnchor";
				}

				PhotosDataList.DataSource = Photos;
				PhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/Default.ascx");
				PhotosDataList.DataBind();

			}

		}
		#endregion
		#region Photos
		PhotoSet Photos
		{
			get
			{
				if (photos == null)
				{

					int photosPerPage = 15;
					Query q = new Query();

					q.Paging.RecordsPerPage = photosPerPage;
					q.Paging.RequestedPage = PhotosPage;

					q.Columns = Templates.Photos.Default.Columns;
					q.TableElement = Templates.Photos.Default.PerformJoins(new Join(Photo.Columns.K, UsrPhotoFavourite.Columns.PhotoK));

					if (ContainerPage.Url.HasDayFilter ||
						ContainerPage.Url.HasMonthFilter ||
						ContainerPage.Url.HasYearFilter)
						q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
					else
						q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Descending);


					Q dateQ = new Q(true);
					if (ContainerPage.Url.HasDayFilter)
						dateQ = new And(
							new Q(Photo.Columns.ParentDateTime, QueryOperator.LessThan, ContainerPage.Url.DateFilter.AddDays(1)),
							new Q(Photo.Columns.ParentDateTime, QueryOperator.GreaterThanOrEqualTo, ContainerPage.Url.DateFilter));
					else if (ContainerPage.Url.HasMonthFilter)
						dateQ = new And(
							new Q(Photo.Columns.ParentDateTime, QueryOperator.LessThan, ContainerPage.Url.DateFilter.AddMonths(1)),
							new Q(Photo.Columns.ParentDateTime, QueryOperator.GreaterThanOrEqualTo, ContainerPage.Url.DateFilter));
					else if (ContainerPage.Url.HasYearFilter)
						dateQ = new And(
							new Q(Photo.Columns.ParentDateTime, QueryOperator.LessThan, ContainerPage.Url.DateFilter.AddYears(1)),
							new Q(Photo.Columns.ParentDateTime, QueryOperator.GreaterThanOrEqualTo, ContainerPage.Url.DateFilter));

					q.QueryCondition = new And(
						dateQ,
						Photo.EnabledQueryCondition,
						new Q(UsrPhotoFavourite.Columns.UsrK, ThisUsr.K)
					);

					photos = new PhotoSet(q);

				}
				return photos;
			}
			set
			{
				photos = value;
			}
		}
		PhotoSet photos;
		#endregion
		#region PhotosPage
		protected int PhotosPage
		{
			get
			{
				if (threadPage == -1)
				{
					if (ContainerPage.Url["p"].IsInt)
						threadPage = ContainerPage.Url["p"];
					else
						threadPage = 1;
				}
				return threadPage;
			}
			set
			{
				threadPage = value;
			}
		}
		int threadPage = -1;
		#endregion

		protected object CurrentDate
		{
			get
			{
				if (ContainerPage.Url.HasDayFilter)
					return ContainerPage.Url.DateFilter.ToString("yyyyMMdd");
				else if (ContainerPage.Url.HasMonthFilter)
					return ContainerPage.Url.DateFilter.ToString("yyyyMM");
				else if (ContainerPage.Url.HasYearFilter)
					return ContainerPage.Url.DateFilter.ToString("yyyy");
				else
					return null;
			}
		}
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("xxx"))
					return Modes.XXX;
				else
					return Modes.Inbox;
			}
		}
		public enum Modes
		{
			Inbox,
			XXX
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelPhotos.Visible = p.Equals(PanelPhotos);
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
			this.Load += new System.EventHandler(this.PanelPhotos_Load);
			this.Load += new System.EventHandler(this.InfoPanel_Load);
			this.Load += new System.EventHandler(this.Cal_Load);

		}
		#endregion
	}
}
