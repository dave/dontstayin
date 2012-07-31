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

namespace Spotted.Templates.GroupPhotos
{
	public partial class Thumbnail : System.Web.UI.UserControl
	{
		protected Label StatsLabel;
		protected PlaceHolder UsrPh;

		private void Page_Load(object sender, System.EventArgs e)
		{
			StatsLabel.Text = CurrentPhoto.StatsText;

			if (CurrentPhoto.UsrCount > 0)
			{
				UsrPh.Controls.Add(new LiteralControl(CurrentPhoto.UsrHtml + "<br>"));
			}

		}

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Photo.UrlColumns,
					Photo.StatsTextColumns,
					Photo.Columns.ContentDisabled,
					Photo.Columns.Thumb,
					Photo.Columns.ThumbHeight,
					Photo.Columns.ThumbWidth,
					Photo.Columns.UsrCount,
					Photo.Columns.FirstUsrK,
					Photo.Columns.PhotoOfWeekCaption,
					Photo.Columns.PhotoOfWeekDateTime,
					new JoinedColumnSet(Photo.Columns.FirstUsrK, Usr.LinkColumns),
					GroupPhoto.Columns.GroupK,
					GroupPhoto.Columns.PhotoK,
					GroupPhoto.Columns.Caption,
					GroupPhoto.Columns.DateTime
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

		protected Photo CurrentPhoto
		{
			get
			{
				if (currentPhoto == null)
					currentPhoto = ((Photo)((DataListItem)NamingContainer).DataItem);
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
