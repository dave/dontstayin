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

namespace Spotted.Blank
{
	public partial class QuickBrowserPhotoListContent : System.Web.UI.UserControl
	{
		public Repeater PhotosDataList;
		Spotted.Master.BlankPage ContainerPage
		{
			get
			{
				return (Spotted.Master.BlankPage)Page;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScriptManager.RegisterStartupScript(Page, typeof(Page), "Tip", "mig_hand();", true);

			if (CurrentGallery.LivePhotos > 0)
			{
				OrderBy ob = Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
			//	if (ContainerPage.Url["Order"].Equals("Rating"))
			//		ob = Photo.RateOrder;

				Query q = new Query();
				q.NoLock = true;
				q.TableElement = Templates.Photos.QuickBrowserPhotoList.PerformJoins(new TableElement(TablesEnum.Photo));
				q.Columns = Templates.Photos.QuickBrowserPhotoList.Columns;
				q.QueryCondition = new And(new Q(Photo.Columns.GalleryK, CurrentGallery.K), Photo.EnabledQueryCondition);
				q.OrderBy = ob;
				PhotoSet ps = new PhotoSet(q);

				PhotosDataList.DataSource = ps;
				PhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/QuickBrowserPhotoList.ascx");
				PhotosDataList.DataBind();
			}
			this.DataBind();
		}

		#region CurrentGallery
		public Gallery CurrentGallery
		{
			get
			{
				if (currentGallery == null && ContainerPage.Url["GalleryK"] > 0)
					currentGallery = new Gallery(ContainerPage.Url["GalleryK"]);
				return currentGallery;
			}
			set
			{
				currentGallery = value;
			}
		}
		private Gallery currentGallery;
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
