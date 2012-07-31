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

namespace Spotted.Admin
{
	public partial class DisableContent : AdminUserControl
	{
		protected TextBox PhotoK;
		protected Label OutLabel;

		public void Enable(object o, System.EventArgs e)
		{
			Photo p = new Photo(int.Parse(Request.QueryString["PhotoK"]));
			if (p.ContentDisabled)
			{
				p.ContentDisabled = false;
				p.Update();
				p.Gallery.UpdatePhotoOrder(null);
				OutLabel.Text = "Picture " + p.K.ToString() + " has had it's contents enabled";
			}
		}
		public void Disable(object o, System.EventArgs e)
		{
			Photo p = new Photo(int.Parse(Request.QueryString["PhotoK"]));
			if (!p.ContentDisabled)
			{
				p.ContentDisabled = true;
				p.Update();
				p.Gallery.UpdatePhotoOrder(null);
				OutLabel.Text = "Picture " + p.K.ToString() + " has had it's contents disabled";
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack && Request.QueryString["PhotoK"] != null)
				PhotoK.Text = Request.QueryString["PhotoK"];
		}
	}
}
