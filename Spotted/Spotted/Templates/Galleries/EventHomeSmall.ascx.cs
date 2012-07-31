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
	public partial class EventHomeSmall : System.Web.UI.UserControl
	{
		protected Label LivePhotoCountLabel, LivePotosPlural;
		protected HtmlAnchor OwnerLink;

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

		protected string NewHtmlStart
		{
			get
			{
				if (CurrentGallery.IsNew)
					return "<div class=\"NewGalleryBox ClearAfter\"><div style=\"height:42px;\">";
				else
					return "<div class=\"NewGalleryBoxPadder\">";
			}
		}

		protected string NewHtmlTitle
		{
			get
			{
				if (CurrentGallery.IsNew)
				{
					string title = "New!";
					int newPhotos = CurrentGallery.LivePhotos - CurrentGallery.JoinedGalleryUsr.ViewPhotosInUse;
					if (CurrentGallery.JoinedGalleryUsr.ViewPhotosInUse > 0 && newPhotos > 0)
					{
						title = newPhotos.ToString("#,##0") + " new!";
					}
					return "<b style=\"color:#ff0000;\">" + title + "</b> ";
				}
				else
					return "";
			}
		}
		protected string NewHtmlEnd
		{
			get
			{
				if (CurrentGallery.IsNew)
					return "</div></div>";
				else
					return "</div>";
			}
		}

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
