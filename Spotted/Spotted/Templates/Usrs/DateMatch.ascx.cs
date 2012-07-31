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
	public partial class DateMatch : System.Web.UI.UserControl
	{
		protected PlaceHolder NamePh, EmailPh;

		private void Page_Load(object sender, System.EventArgs e)
		{
			NamePh.Visible = (CurrentUsr.NickName.Length > 0);
			EmailPh.Visible = (CurrentUsr.NickName.Length == 0 && Usr.Current.K == CurrentUsr.AddedByUsrK);
		}
		protected string NameHtml
		{
			get
			{
				if (CurrentUsr.HasBuddy(Usr.Current.K) || CurrentUsr.K == Usr.Current.K)
					return " (" + CurrentUsr.FirstName + " " + CurrentUsr.LastName + ")";
				else
					return "";
			}
		}
		protected string FriendlyMatchDateTime
		{
			get
			{
				return Cambro.Misc.Utility.FriendlyTime(CurrentUsrDate.MatchDateTime, false);
			}
		}
		protected UsrDate CurrentUsrDate
		{
			get
			{
				if (currentUsrDate == null)
					currentUsrDate = ((UsrDate)((DataListItem)NamingContainer).DataItem);
				return currentUsrDate;
			}
		}
		UsrDate currentUsrDate;

		protected Usr CurrentUsr
		{
			get
			{
				return CurrentUsrDate.DateUsr;
			}
		}

	}
}
