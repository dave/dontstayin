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

namespace Spotted.Master
{
	public partial class BlankPage : GenericPage
	{
		public HtmlForm TemplateForm;
		public HtmlGenericControl BodyTag;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (this.SslPage == ContentUserControl.SslPage)
			{
				Bobs.Log.Increment(Bobs.Log.Items.BlankPages);
			}
			else
			{
			    this.SslPage = ContentUserControl.SslPage;
			}
		}
		public BlankUserControl ContentUserControl
		{
			get
			{
				return (BlankUserControl)GenericUserControl;
			}
		}

		public override void VerifyRenderingInServerForm(Control control)
		{
			// Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
