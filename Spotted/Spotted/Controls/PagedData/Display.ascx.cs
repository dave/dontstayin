using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Spotted.Controls.PagedData
{
	public partial class Display : EnhancedUserControl
	{
		private IPagedDataDisplaySettings settings;
		protected EnhancedUserControl uiHeaderControl;
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.uiDefaultTop.Value = this.settings.DefaultTop.ToString();
			this.uiPageSize.Value = this.settings.PageSize.ToString();
			this.uiServicePath.Value = this.settings.ServicePath;
			this.uiServiceMethod.Value = this.settings.ServiceMethod;
			//this.uiParameterSourceNames.Value = String.Join(",", this.settings.ParameterSourceControls.Select(i => i.ControllerName).ToArray());
			this.uiTimeout.Value = this.settings.Timeout.ToString();
			this.uiTabName.Value = this.settings.TabName;
			//if (uiHeaderControl != null)
			//{
			//	this.uiParameterSourceNames.Value += "," + uiHeaderControl.ScriptSharpControlBehaviour.ControllerName;
			//}
		}
		public void Setup(IPagedDataDisplaySettings tabSettings)
		{
			this.settings = tabSettings;
			this.uiRepeater.Header = this.settings.Header;
			this.uiRepeater.Between = this.settings.Between;
			this.uiRepeater.Footer = this.settings.Footer;
			this.uiRepeater.ItemTemplateGetter = this.settings.GetItemTemplate;
			if (tabSettings.HeaderControl != null)
			{
				uiHeaderControl = tabSettings.HeaderControl;
				uiHeaderControl.ID = "uiHeaderControl";
				this.uiPanel.Controls.AddAt(0, tabSettings.HeaderControl);
			}
		}
	}

	static class Templating
	{

		public static DelegateTemplate ToTemplate(this string s)
		{
			return new DelegateTemplate(c => c.Controls.Add(new Literal() {Text = s}));
		}
	}
}
