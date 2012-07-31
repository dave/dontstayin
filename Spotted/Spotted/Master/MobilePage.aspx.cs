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
using Bobs.Main;

namespace Spotted.Master
{
	public partial class MobilePage : GenericPage
	{

		private void Page_Load(object sender, System.EventArgs e)
		{

			Bobs.Log.Increment(Bobs.Log.Items.MobilePages);
			if (Bobs.Visit.HasCurrent && !Bobs.Visit.Current.IsCrawler)
				Bobs.Log.Increment(Bobs.Log.Items.MobilePagesNoCrawlers);


//            #region SpottedScript
//            if (ScriptManager.GetCurrent(Page) == null || !ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
//            {
//#if DEBUG
//                HeaderScriptPlaceHolder.Controls.Add(new LiteralControl(JsLinkCreator.GetRegisterScriptHtml(@"/misc/SpottedScript/spottedscript.debug.js")));
//#else
//                HeaderScriptPlaceHolder.Controls.Add(new LiteralControl(JsLinkCreator.GetRegisterScriptHtml(@"/misc/SpottedScript/spottedscript.js")));
//#endif
//            }
//            #endregion

		}
		public MobileUserControl ContentUserControl
		{
			get
			{
				return (MobileUserControl)GenericUserControl;
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
