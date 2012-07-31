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
	public partial class TopContent : System.Web.UI.UserControl
	{
		protected DataList PhotosDataList;
		protected HtmlGenericControl PhotosPageP1, PhotosPageP2;
		protected HyperLink PhotosPrevPage1, PhotosNextPage1, PhotosPrevPage2, PhotosNextPage2;

		private void Page_Load(object sender, System.EventArgs e)
		{
			int photosPerPage = 12;
			Query q = new Query();

			q.Paging.RecordsPerPage = photosPerPage;
			q.Paging.RequestedPage = PhotosPage;

			q.NoLock = true;
			if (ContainerPage.Url["yours"].Exists)
			{
				q.Columns = Templates.Photos.TopPhotoUserThumb.Columns;
				q.TableElement = Templates.Photos.TopPhotoUserThumb.PerformJoins(new TableElement(TablesEnum.Photo));
			}
			else
			{
				q.Columns = Templates.Photos.TopPhotoThumb.Columns;
				q.TableElement = Templates.Photos.TopPhotoThumb.PerformJoins(new TableElement(TablesEnum.Photo));
			}

			if (ContainerPage.Url["photos"].Exists || ContainerPage.Url["videos"].Exists)
			{
				q.QueryCondition = new And(
					new Q(Photo.Columns.PhotoOfWeek, true),
					new Q(Photo.Columns.MediaType, ContainerPage.Url["photos"].Exists ? Photo.MediaTypes.Image : Photo.MediaTypes.Video));
				q.OrderBy = new OrderBy(Photo.Columns.PhotoOfWeekDateTime, OrderBy.OrderDirection.Descending);
			}
			else if (ContainerPage.Url["all"].Exists)
			{
				q.QueryCondition = new Q(Photo.Columns.PhotoOfWeek, true);
				q.OrderBy = new OrderBy(Photo.Columns.PhotoOfWeekDateTime, OrderBy.OrderDirection.Descending);
			}
			else
			{
				q.QueryCondition = new Q(Photo.Columns.PhotoOfWeekUser, true);
				q.OrderBy = new OrderBy(Photo.Columns.PhotoOfWeekUserDateTime, OrderBy.OrderDirection.Descending);
			}
			q.TopRecords = (PhotosPage * photosPerPage) + 1;
			PhotoSet ps = new PhotoSet(q);

			int PageFromUrl = PhotosPage;

			if (PhotosPage != ps.Paging.ReturnedPage)
				PhotosPage = ps.Paging.ReturnedPage;

			if (ps.Paging.ShowNoLinks)
			{
				PhotosPageP1.Visible = false;
				PhotosPageP2.Visible = false;
			}
			else
			{
				PhotosPageP1.Visible = true;
				PhotosPageP2.Visible = true;

				string urlNextPage = ContainerPage.Url.CurrentUrl(((int)(PhotosPage + 1)).ToString(), "", PageFromUrl.ToString(), null);
				string urlPrevPage = "";
				if (PhotosPage == 2)
					urlPrevPage = ContainerPage.Url.CurrentUrl(PhotosPage.ToString(), null);
				else
					urlPrevPage = ContainerPage.Url.CurrentUrl(((int)(PhotosPage - 1)).ToString(), "", PageFromUrl.ToString(), null);

				PhotosNextPage1.Enabled = ps.Paging.ShowNextPageLink;
				PhotosNextPage2.Enabled = ps.Paging.ShowNextPageLink;
				PhotosPrevPage1.Enabled = ps.Paging.ShowPrevPageLink;
				PhotosPrevPage2.Enabled = ps.Paging.ShowPrevPageLink;

				PhotosNextPage1.NavigateUrl = urlNextPage;
				PhotosNextPage2.NavigateUrl = urlNextPage;
				PhotosPrevPage1.NavigateUrl = urlPrevPage;
				PhotosPrevPage2.NavigateUrl = urlPrevPage;
			}

			PhotosDataList.DataSource = ps;
			if (ContainerPage.Url["yours"].Exists)
				PhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/TopPhotoUserThumb.ascx");
			else
				PhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/TopPhotoThumb.ascx");
			PhotosDataList.DataBind();
		}

		int PhotosPage
		{
			get
			{
				if (photosPage == -1)
				{
					if (ContainerPage.Url[1].Exists)
						return ContainerPage.Url[1].KeyInt;
					else
						return 1;
				}
				else
					return photosPage;
			}
			set
			{
				photosPage = value;
			}
		}
		int photosPage = -1;

		#region ContainerPage
		public Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
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
	}
}
