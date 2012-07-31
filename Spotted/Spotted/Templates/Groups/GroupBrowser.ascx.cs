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

namespace Spotted.Templates.Groups
{
	public partial class GroupBrowser : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Group.Columns.K,
					Group.Columns.BrandK,
					Group.Columns.Name,
					Group.Columns.Description,
					Group.Columns.TotalMembers,
					Group.Columns.TotalComments,
					Group.Columns.PrivateGroupPage,
					Group.Columns.UrlName,
					Group.Columns.Pic);
			}
		}
		//		public static TableElement PerformJoins(TableElement tIn)
		//		{
		//			TableElement t=new Join(tIn, 
		//				new TableElement(new Column(Photo.Columns.FirstUsrK,Usr.Columns.K)),
		//				QueryJoinType.Left,
		//				Photo.Columns.FirstUsrK,
		//				new Column(Photo.Columns.FirstUsrK,Usr.Columns.K));
		//			return t;
		//		}

		protected Group CurrentGroup
		{
			get
			{
				if (currentGroup == null)
					currentGroup = ((Group)((DataListItem)NamingContainer).DataItem);
				return currentGroup;
			}
		}
		Group currentGroup;

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
