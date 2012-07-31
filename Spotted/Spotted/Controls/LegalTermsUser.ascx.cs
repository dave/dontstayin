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

namespace Spotted.Controls
{
	public partial class LegalTermsUser : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (UsePopups)
			{
				InformationPolicyAnchor.HRef = "/popup/legalinformationpolicy";
				InformationPolicyAnchor.Attributes["onclick"] = "openPopup('/popup/legalinformationpolicy');return false;";
			}
		}
		#region UsePopups
		public bool UsePopups
		{
			get
			{
				return usePopups;
			}
			set
			{
				usePopups = value;
			}
		}
		bool usePopups;
		#endregion
	}
}
