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
	public partial class Default : System.Web.UI.UserControl
	{
		protected PlaceHolder UsrPh;

		private void Page_Load(object sender, System.EventArgs e)
		{
			UsrPh.Controls.Add(new LiteralControl("<div style=\"margin-top:2px;\"><small>"));
			UsrPh.Controls.Add(new LiteralControl(CurrentPhoto.UsrHtml));
			if (CurrentPhoto.UsrHtml.Length > 0)
				UsrPh.Controls.Add(new LiteralControl("</small></div><div style=\"margin-top:1px;\"><small>"));
			UsrPh.Controls.Add(new LiteralControl(CurrentPhoto.StatsText));
			UsrPh.Controls.Add(new LiteralControl("</small></div>"));

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
