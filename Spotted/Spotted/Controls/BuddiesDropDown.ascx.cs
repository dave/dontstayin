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

namespace Spotted.Controls
{
	public partial class BuddiesDropDown : System.Web.UI.UserControl
	{
		protected DropDownList UsrPhotoBuddyDropDown;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.HasBuddiesFull)
			{
				Query q = new Query();
				q.TableElement = Usr.BuddyJoin;
				q.QueryCondition = Usr.Current.BuddiesFullQ;
				q.OrderBy = new OrderBy(Usr.Columns.NickName);
				q.NoLock = true;
				q.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName);
				UsrSet usBuddys = new UsrSet(q);
				UsrPhotoBuddyDropDown.DataSource = usBuddys;
				UsrPhotoBuddyDropDown.DataTextField = "NickName";
				UsrPhotoBuddyDropDown.DataValueField = "K";
				UsrPhotoBuddyDropDown.DataBind();
				UsrPhotoBuddyDropDown.Items.Insert(0, new ListItem("Select buddy", "-1"));
			}
			else
			{
				this.Visible = false;
			}
		}
		public void Test(object o, CommandEventArgs e)
		{
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
