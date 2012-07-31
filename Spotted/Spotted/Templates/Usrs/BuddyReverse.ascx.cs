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

namespace Spotted.Templates.Usrs
{
	public partial class BuddyReverse : System.Web.UI.UserControl
	{
		protected PlaceHolder NamePh, EmailPh, NotCanInvitePh;
		protected HtmlAnchor PicAnchor;

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Bobs.Buddy.Columns.BuddyUsrK,
					Bobs.Buddy.Columns.UsrK,
					Bobs.Buddy.Columns.CanBuddyInvite,
					Bobs.Buddy.Columns.FullBuddy,
					Usr.Columns.AddedByUsrK,
					Usr.Columns.Email,
					Usr.LinkColumns
				);
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			NamePh.Visible = (CurrentUsr.NickName.Length > 0);
			NotCanInvitePh.Visible = !CurrentBuddy.CanBuddyInvite;
			EmailPh.Visible = (CurrentUsr.NickName.Length == 0 && Usr.Current != null && CurrentUsr.AddedByUsrK == Usr.Current.K);
			if (CurrentUsr.NickName.Length > 0)
				PicAnchor.HRef = CurrentUsr.Url();
		}

		protected Usr CurrentUsr
		{
			get
			{
				return CurrentBuddy.Usr;
			}
		}

		protected Bobs.Buddy CurrentBuddy
		{
			get
			{
				if (currentBuddy == null)
					currentBuddy = ((Bobs.Buddy)((DataListItem)NamingContainer).DataItem);
				return currentBuddy;
			}
		}
		Bobs.Buddy currentBuddy;


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
