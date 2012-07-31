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

namespace Spotted.Templates.Photos
{
	public partial class QuickBrowserPhotoList : System.Web.UI.UserControl
	{
		protected Label StatsLabel;
		protected PlaceHolder UsrPh;

		private void Page_Load(object sender, System.EventArgs e)
		{

		}

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Photo.UrlColumns,
					Photo.StatsTextColumns,
					Photo.Columns.ContentDisabled,
					Photo.Columns.Icon,
					Photo.Columns.Web,
					Photo.Columns.WebHeight,
					Photo.Columns.WebWidth,
					Photo.Columns.UsrCount,
					Photo.Columns.FirstUsrK,
					Photo.Columns.MediaType,
					Photo.Columns.VideoMed,
					Photo.Columns.VideoMedHeight,
					Photo.Columns.VideoMedWidth,
					Photo.Columns.Overlay,
					new JoinedColumnSet(Photo.Columns.FirstUsrK, Usr.LinkColumns)
				);
			}
		}
		public static TableElement PerformJoins(TableElement tIn)
		{
			TableElement t = new Join(tIn,
				new TableElement(new Column(Photo.Columns.FirstUsrK, Usr.Columns.K)),
				QueryJoinType.Left,
				Photo.Columns.FirstUsrK,
				new Column(Photo.Columns.FirstUsrK, Usr.Columns.K));
			return t;
		}

		protected string Attribs
		{
			get
			{
				string rolloverHtml = "";

				if (CurrentPhoto.UsrCount > 0)
				{
					if (CurrentPhoto.UsrString != null)
						rolloverHtml = CurrentPhoto.UsrString;
				}
				if (rolloverHtml.Length > 0)
					rolloverHtml = "This is: " + rolloverHtml;

				string totalsText = "";
				if (CurrentPhoto.Views > 0 || CurrentPhoto.TotalComments > 0)
				{
					if (CurrentPhoto.Views > 0)
						totalsText += CurrentPhoto.Views.ToString() + " view" + (CurrentPhoto.Views == 1 ? "" : "s");
					if (CurrentPhoto.Views > 0 && CurrentPhoto.TotalComments > 0)
						totalsText += ", ";
					if (CurrentPhoto.TotalComments > 0)
						totalsText += CurrentPhoto.TotalComments.ToString() + " comment" + (CurrentPhoto.TotalComments == 1 ? "" : "s");
				}
				if (totalsText.Length > 0)
					rolloverHtml = rolloverHtml + (rolloverHtml.Length > 0 ? "<br>" : "") + totalsText;
				if (rolloverHtml.Length > 0)
					rolloverHtml = HttpUtility.UrlEncodeUnicode("<b>" + rolloverHtml + "</b>").Replace("'", "\\'");

				int width = CurrentPhoto.WebWidth;
				int height = CurrentPhoto.WebHeight;

				string video = "";
				if (CurrentPhoto.MediaType.Equals(Photo.MediaTypes.Video))
				{
					video = CurrentPhoto.VideoMed.ToString();
					width = CurrentPhoto.VideoMedWidth;
					height = CurrentPhoto.VideoMedHeight;
				}

				string html = "onclick=\"SwitchPhoto(" + CurrentPhoto.K + ",'" + CurrentPhoto.Web.ToString() + "','" + video + "'," + CurrentPhoto.TotalComments + "," + CurrentPhoto.Views + "," + width.ToString() + "," + height.ToString() + "," + ((int)CurrentPhoto.Overlay).ToString() + ");return false;\";";
				if (rolloverHtml.Length > 0)
				{
					html += " onmouseover=\"stt('" + rolloverHtml + "');\" onmouseout=\"htm();\"";
				}
				return html;
			}
		}


		protected Photo CurrentPhoto
		{
			get
			{
				if (currentPhoto == null)
					currentPhoto = ((Photo)((RepeaterItem)NamingContainer).DataItem);
				return currentPhoto;
			}
		}
		Photo currentPhoto;



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
