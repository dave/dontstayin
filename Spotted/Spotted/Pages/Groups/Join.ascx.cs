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

namespace Spotted.Pages.Groups
{
	public partial class Join : DsiUserControl
	{
		public void Page_Init(object o, System.EventArgs e)
		{

		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			Spotted.Controls.GroupJoin groupJoinControl = (Spotted.Controls.GroupJoin)this.LoadControl("~/Controls/GroupJoin.ascx");
			groupJoinControl.CurrentGroup = this.CurrentGroup;
			this.GroupJoinPlaceHolder.Controls.Add(groupJoinControl);
		}

		#region CurrentGroup
		public Group CurrentGroup
		{
			get
			{
				return ContainerPage.Url.ObjectFilterGroup;
			}
		}
		#endregion
	}
}
