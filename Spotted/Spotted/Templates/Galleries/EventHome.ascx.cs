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

namespace Spotted.Templates.Galleries
{
	public partial class EventHome : System.Web.UI.UserControl
	{
		protected Label LivePhotoCountLabel, LivePotosPlural;
		protected HtmlAnchor OwnerLink;


		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Gallery.Columns.K,
					Gallery.Columns.Name,
					Gallery.Columns.UrlFragment,
					Gallery.Columns.ArticleK,
					Gallery.Columns.MainPhotoK,
					Gallery.Columns.LivePhotos,
					Gallery.Columns.CreateDateTime,
					Gallery.Columns.EventK,
					Gallery.Columns.OwnerUsrK,
					new JoinedColumnSet(Gallery.Columns.MainPhotoK, Photo.Columns.K, Photo.Columns.Icon, Photo.Columns.ContentDisabled, Photo.Columns.Status),
					new JoinedColumnSet(Gallery.Columns.OwnerUsrK, Usr.LinkColumns),
					GalleryUsr.Columns.GalleryK,
					GalleryUsr.Columns.UsrK,
					GalleryUsr.Columns.Favourite,
					GalleryUsr.Columns.ViewPhotos,
					GalleryUsr.Columns.ViewPhotosLatest,
					GalleryUsr.Columns.ViewDateTime,
					GalleryUsr.Columns.ViewDateTimeLatest
				);
			}
		}

		protected string NewHtmlStart
		{
			get
			{
				if (CurrentGallery.IsNew)
				{
					string title = "New gallery";
					int newPhotos = CurrentGallery.LivePhotos - CurrentGallery.JoinedGalleryUsr.ViewPhotosInUse;
					if (CurrentGallery.JoinedGalleryUsr.ViewPhotosInUse > 0 &&
						newPhotos > 0)
					{
						title = newPhotos.ToString("#,##0") + " new photo" + (newPhotos == 1 ? "" : "s");
					}
					return "<div class=\"NewGalleryBox ClearAfter\" style=\"padding-top:3px;\"><div style=\"font-weight:bold;color:#ff0000;margin-bottom:3px;\">" + title + "!</div>";
				}
				return "<div class=\"NewGalleryBoxPadder\" style=\"padding-top:3px;\">";
			}
		}
		protected string NewHtmlEnd
		{
			get
			{
				if (CurrentGallery.IsNew)
				{
					return "</div>";
				}
				return "</div>";
			}
		}

		public static TableElement PerformJoins(TableElement tIn)
		{
			if (tIn == null)
				tIn = new TableElement(TablesEnum.Gallery);
			TableElement t = tIn;
			t = new Join(t, new TableElement(new Column(Gallery.Columns.MainPhotoK, Photo.Columns.K)), QueryJoinType.Left, Gallery.Columns.MainPhotoK, new Column(Gallery.Columns.MainPhotoK, Photo.Columns.K));
			t = new Join(t, new TableElement(new Column(Gallery.Columns.OwnerUsrK, Usr.Columns.K)), QueryJoinType.Inner, Gallery.Columns.OwnerUsrK, new Column(Gallery.Columns.OwnerUsrK, Usr.Columns.K));

			int usrK = 0;
			if (Usr.Current != null)
				usrK = Usr.Current.K;

			t = new Join(
				t,
				new TableElement(TablesEnum.GalleryUsr),
				QueryJoinType.Left,
				new And(
				new Q(Gallery.Columns.K, GalleryUsr.Columns.GalleryK, true),
				new Q(GalleryUsr.Columns.UsrK, usrK)));

			return t;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentGallery.LivePhotos != 1)
				LivePotosPlural.Text = "s";
		}

		protected Gallery CurrentGallery
		{
			get
			{
				if (currentGallery == null)
					currentGallery = (Gallery)((DataListItem)NamingContainer).DataItem;
				return currentGallery;
			}
		}
		Gallery currentGallery;

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
