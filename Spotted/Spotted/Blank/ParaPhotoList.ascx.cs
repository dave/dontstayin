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
	public partial class ParaPhotoList : BlankUserControl
	{
		public Repeater PhotosDataList;
		protected HtmlGenericControl NoPhotosDiv;
		private void Page_Load(object sender, System.EventArgs e)
		{
			PhotoSet ps = null;

			if (CurrentArticle != null)
				ps = new PhotoSet(new Query(new Q(Photo.Columns.ArticleK, CurrentArticle.K)));
			else
				ps = new PhotoSet(new Query(new Q(Photo.Columns.GalleryK, CurrentGallery.K)));

			NoPhotosDiv.Visible = ps.Count == 0;
			BgCol = ps.Count == 0 ? "FFFFFF" : "000000";
			PhotosDataList.DataSource = ps;
			PhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/ParaPhotoList.ascx");
			this.DataBind();
		}

		protected string BgCol;

		#region CurrentGallery
		public Gallery CurrentGallery
		{
			get
			{
				if (currentGallery == null && GalleryK > 0)
					currentGallery = new Gallery(GalleryK);
				return currentGallery;
			}
			set
			{
				currentGallery = value;
			}
		}
		private Gallery currentGallery;
		#endregion
		int GalleryK
		{
			get
			{
				return ContainerPage.Url["GalleryK"];
			}

		}

		#region CurrentArticle
		public Article CurrentArticle
		{
			get
			{
				if (currentArticle == null && ArticleK > 0)
					currentArticle = new Article(ArticleK);
				return currentArticle;
			}
			set
			{
				currentArticle = value;
			}
		}
		private Article currentArticle;
		#endregion
		int ArticleK
		{
			get
			{
				return ContainerPage.Url["K"];
			}

		}
	}
}
