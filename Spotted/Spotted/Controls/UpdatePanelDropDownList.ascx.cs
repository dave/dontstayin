using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Spotted.Controls
{
	public partial class UpdatePanelDropDownList : System.Web.UI.UserControl
	{
		public ListItemCollection Items
		{
			get { return uiList.Items; }
		}
		public string SelectedValue
		{
			get
			{
				ListItem item = uiList.Items.FindByValue(uiValue.Value);
				return item != null ? item.Value : "";
			}
			set { uiValue.Value = value; }
		}
		public int SelectedIndex
		{
			set { uiList.SelectedIndex = value; }
		}
		public string ClearFunctionName
		{
			get { return this.ClientID + "_Clear"; }
		}
		public string SetValueFunctionName
		{
			get { return this.ClientID + "_SetValue"; }
		}
		public WebControl TriggerControl { get { return this.uiList; } }
		public List<WebControl> TriggerControls = new List<WebControl>();

		public event EventHandler OnSelectedIndexChanged;

		public bool AutoPostBack
		{
			set { this.uiList.AutoPostBack = value; }
		}
		public void Page_PreRender(object o, EventArgs e)
		{
			// ensure that SetValue comes first out of any possible other onchange functions
			if (this.uiList.Attributes["onchange"] == null || !this.uiList.Attributes["onchange"].Contains(this.SetValueFunctionName))
				this.uiList.Attributes["onchange"] = this.SetValueFunctionName + "(this);" + this.uiList.Attributes["onchange"];

			foreach (WebControl c in TriggerControls)
			{
				AddOnChange(c);
			}

			if (this.SelectedValue != "")
				uiList.SelectedValue = this.SelectedValue;

			this.uiList.Attributes["onchange"] = this.uiList.Attributes["onchange"];

		}

		void AddOnChange(WebControl c)
		{
			// append clear function
			string eventName = "onchange";
			if (c is RadioButton)
			{
				eventName = "onclick";
			}
			if (this.uiList.Attributes[eventName] == null || !this.uiList.Attributes[eventName].Contains(this.ClearFunctionName))
				c.Attributes[eventName] += ClearFunctionName + "();";
		}

		protected void uiList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (OnSelectedIndexChanged != null)
				OnSelectedIndexChanged(sender, e);
		}
	}
}
