using System;
using System.Collections;
using System.Collections.Generic;
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

namespace Spotted.Controls
{
	public partial class OptionsList : System.Web.UI.UserControl
	{
		public OptionsList()
		{
			OptionItems = new List<Option>();
		}
		public string OptionPanelsContainerID { get; set; }
		public int RepeatColumns { get; set; }

		public void Page_PreRender(object o, EventArgs e)
		{
			if (RepeatColumns > 0) this.uiList.RepeatColumns = RepeatColumns;
			ScriptManager.RegisterStartupScript(Page, typeof(LiteralControl), this.ClientID + "_StartupScript", TriggerShowHidePanelsScript, true);
		}

		private string TriggerShowHidePanelsScript
		{
			get { return this.ClientID + "_ShowHidePanels('" + SelectedPanelID + "');"; }
		}
		public string SelectedPanelID
		{
			get
			{
				foreach (ListItem item in uiList.Items)
				{
					if (item.Selected) return item.Value;
				}
				return "";
			}
		}

		public override void DataBind()
		{
			base.DataBind();
			uiList.Items.Clear();
			uiList.Items.AddRange(OptionItems.ConvertAll(o => o.ToListItem(this.ClientID)).ToArray());
		}

		public List<Option> OptionItems { get; set; }

		public class Option
		{
			public string PanelID { get; set; }
			public string Text { get; set; }
			public string ImgSrc { get; set; }
			public bool Checked { get; set; }
			public bool TextAsHeaders { get; set; }
			public string LinkUrl { get; set; }

			private string TextSpanOpenTag { get { return TextAsHeaders ? "<span style=\"font-size:14px;font-weight:bold;\">" : "<span>"; } }
			private string TextSpanCloseTag { get { return "</span>"; } }
			private string TextHtml { get { return (Text != null && Text.Length > 0) ? TextSpanOpenTag + Text + TextSpanCloseTag : ""; } }
			private string ImgHtml { get { return (ImgSrc != null && ImgSrc.Length > 0) ? "<img src=\"" + ImgSrc + "\" />" : ""; } }

			public string ItemText
			{
				get { return ImgHtml + TextHtml; }
			}

			public Option(string panelID, string text) : this(panelID, text, "", false, false) { }
			/// <param name="description">default blank</param>
			/// <param name="imgSrc">default blank</param>
			/// <param name="isChecked">default false</param>
			/// <param name="textAsHeaders">default false</param>
			public Option(string panelID, string text, string imgSrc, bool isChecked, bool textAsHeaders)
			{
				this.PanelID = panelID;
				this.Text = text;
				this.ImgSrc = imgSrc;
				this.Checked = isChecked;
				this.TextAsHeaders = textAsHeaders;
			}
			internal ListItem ToListItem(string clientID)
			{
				ListItem item = new ListItem();
				item.Text = this.ItemText;
				item.Value = this.PanelID;
				item.Attributes["onclick"] = 
					(this.LinkUrl != null) ? "window.location = '" + this.LinkUrl + "';":
					clientID + "_ShowHidePanels('" + this.PanelID + "');";
				item.Selected = this.Checked;
				return item;
			}

		}
	}
}
