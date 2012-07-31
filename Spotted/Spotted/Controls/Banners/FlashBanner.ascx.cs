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

namespace Spotted.Controls.Banners
{
	public partial class FlashBanner : FileBanner
	{
		
		private void Page_PreRender(object sender, System.EventArgs eargs)
		{
			this.DataBind();
		}

		public string Guid
		{
			get
			{
				if (guid.Length==0)
					guid = System.Guid.NewGuid().ToString().Replace("-",String.Empty);
				return guid;
			}
		}
		string guid = "";

		protected string FlashVersionString
		{
			get
			{
				if (FlashVersion.Equals(String.Empty))
					return "7";
				else
					return FlashVersion;
			}
		}

		public string TargetTag
		{
			get
			{
				return LinkTargetBlank ? "_blank" : "_self";
			}
		}

		public string LinkTag
		{
			get
			{
				if (LinkUrl.StartsWith("http://"))
					return LinkUrl;
				else
					return "http://" + Vars.DomainName + LinkUrl;
			}
		}

		protected string FlashVarsString
		{
			get
			{
				if (LinkUrl.StartsWith("http://"))
					return "targetTag=" + (LinkTargetBlank ? "_blank" : "_self") + "&linkTag=" + LinkUrl;
				else
					return "targetTag=" + (LinkTargetBlank ? "_blank" : "_self") + "&linkTag=http://" + Vars.DomainName + LinkUrl;
			}
		}

		protected string WidthString
		{
			get { return Width.ToString(); }
		}
		protected string HeightString
		{
			get { return Height.ToString(); }
		}

	 
	}
}
