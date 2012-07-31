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
using System.Web.Services;
using System.Text;
using System.IO;
using Bobs;

namespace Spotted.Controls
{
	[ValidationPropertyAttribute("ValidationPropertyValue")]
	[ClientScript]
	public partial class Html : DsiUserControl, IIncludesJs
	{

		public Html()
		{
		}

		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			Spotted.Controls.CommentsDisplay.IncludeJs(page);

			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		public int TabIndexBase { get; set; }

		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{

			

			if (!ScriptManager.GetCurrent(Page).Services.Contains(new ServiceReference("/WebServices/Controls/Html/Service.asmx")))
				ScriptManager.GetCurrent(Page).Services.Add(new ServiceReference("/WebServices/Controls/Html/Service.asmx"));

			LinkAnchor.Attributes["onmousedown"] = "HtmlControlShowHide('Link', '" + this.ClientID + "');return false;";
			ImageAnchor.Attributes["onmousedown"] = "HtmlControlShowHide('Image', '" + this.ClientID + "');return false;";
			VideoAnchor.Attributes["onmousedown"] = "HtmlControlShowHide('Video', '" + this.ClientID + "');return false;";
			MixmagAnchor.Attributes["onmousedown"] = "HtmlControlShowHide('Mixmag', '" + this.ClientID + "');return false;";
			FlashAnchor.Attributes["onmousedown"] = "HtmlControlShowHide('Flash', '" + this.ClientID + "');return false;";
			AdvancedAnchor.Attributes["onmousedown"] = "HtmlControlShowHide('Advanced', '" + this.ClientID + "');return false;";

			setHelperPanelVisibility(LinkAnchor, LinkDiv, HelperPanelDisplayState.Value == "Link");
			setHelperPanelVisibility(ImageAnchor, ImageDiv, HelperPanelDisplayState.Value == "Image");
			setHelperPanelVisibility(VideoAnchor, VideoDiv, HelperPanelDisplayState.Value == "Video");
			setHelperPanelVisibility(MixmagAnchor, MixmagDiv, HelperPanelDisplayState.Value == "Mixmag");
			setHelperPanelVisibility(FlashAnchor, FlashDiv, HelperPanelDisplayState.Value == "Flash");
			setHelperPanelVisibility(AdvancedAnchor, AdvancedDiv, HelperPanelDisplayState.Value == "Advanced");

			HtmlTextBox.TabIndex = (short)(TabIndexBase + 1);
			SaveButton.TabIndex = (short)(TabIndexBase + 2);
			PreviewButton.Attributes["tabindex"] = (TabIndexBase + 3).ToString();
			HidePreviewButton.Attributes["tabindex"] = (TabIndexBase + 4).ToString();

			HelpersDiv.Style["width"] = Width.ToString() + "px";
			TextBoxDiv.Style["width"] = (Width - 2).ToString() + "px";
			HtmlTextBox.Style["width"] = (Width - 6).ToString() + "px";
			DisabledMessageDiv.Style["width"] = (Width - 6).ToString() + "px";
			PreviewPanelContainer.Style["width"] = (Width - 2).ToString() + "px";
			ButtonsContainer.Style["width"] = Width.ToString() + "px";

			//SaveButton.OnClientClick = "SaveClick_" + this.ClientID + "(); return false;";
			
		}
		#endregion
		#region setHelperPanelVisibility
		void setHelperPanelVisibility(HtmlControl anchor, HtmlControl div, bool state)
		{
			if (state)
			{
				div.Style.Remove("display");
				anchor.Style["border-bottom"] = "1px solid #FED551";
				anchor.Style["background-color"] = "#FED551";
			}
			else
			{
				div.Style["display"] = "none";
				anchor.Style.Remove("border-bottom");
				anchor.Style.Remove("background-color");
			}
		}
		#endregion
		
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs eventArgs)
		{
			

			foreach (object c in Page.Validators)
			{
			    if (c is BaseValidator)
			    {
			        BaseValidator val = (BaseValidator)c;
			        //if (val.NamingContainer.UniqueID == this.NamingContainer.UniqueID && val.ControlToValidate == this.ID)
			        if (val.NamingContainer.UniqueID == this.NamingContainer.UniqueID && val.ControlToValidate == this.ID)
			        {
						val.ControlToValidate = this.ID + ":HtmlTextBox";
			            //val.Attributes["controltovalidate"] = this.ClientID + "_HtmlTextBox";

			            //ScriptManager.RegisterStartupScript(this, typeof(Page), val.ClientID + "_Validator", "var " + val.ClientID + " = document.all ? document.all[\"" + val.ClientID + "\"] : document.getElementById(\"" + val.ClientID + "\");" + val.ClientID + ".controltovalidate=\"" + this.ClientID + "_HtmlTextBox" + "\";", true);
			        }
			    }
			}
		}
		#endregion
		
		#region Enabled
		public bool Enabled
		{
			get
			{
				return uiEnabled.Value == "" || bool.Parse(uiEnabled.Value);
			}
			set
			{
				uiEnabled.Value = value.ToString();
				SaveButton.Enabled = value;
				PreviewButton.Disabled = !value;
				HidePreviewButton.Disabled = !value;
				HtmlTextBox.Enabled = value;
				HelpersDiv.Visible = value;
				DisabledMessageDiv.Visible = !value;
				TextBoxDiv.Visible = value;
			}
		}
		#endregion
		#region TextBoxClientID
		public string TextBoxClientID
		{
			get
			{
				return HtmlTextBox.ClientID;
			}
		}
		#endregion
		#region CausesValidation
		public bool CausesValidation
		{
			get
			{
				return ViewState["CausesValidation"] as bool? ?? true;
			}
			set
			{
				ViewState["CausesValidation"] = value;
				SaveButton.CausesValidation = value;
			}
		}
		#endregion
	
		#region DisableSaveButton
		public bool DisableSaveButton
		{
			get
			{
				return ViewState["DisableSaveButton"] as bool? ?? false;
			}
			set
			{
				ViewState["DisableSaveButton"] = value;
				SaveButton.Visible = !value;
			}
		}
		#endregion
		#region SaveButtonText
		public string SaveButtonText
		{
			get
			{
				return ViewState["SaveButtonText"] as string ?? "Save";
			}
			set
			{
				ViewState["SaveButtonText"] = value;
				SaveButton.Text = value;
			}
		}
		#endregion

		#region DisableContainer
		public bool DisableContainer
		{
			get
			{
				return ViewState["DisableContainer"] as bool? ?? false;
			}
			set
			{
				ViewState["DisableContainer"] = value.ToString();
				AdvancedContainerPanel.Visible = !value;
			}
		}
		#endregion
		#region Container
		public bool Container
		{
			get
			{
				return AdvancedContainerTrueRadio.Checked;
			}
			set
			{
				AdvancedContainerTrueRadio.Checked = value;
				AdvancedContainerFalseRadio.Checked = !value;
			}
		}
		
		#endregion

		#region Formatting
		public bool Formatting
		{
			get
			{
				return AdvancedFormattingTrueRadio.Checked;
			}
			set
			{
				AdvancedFormattingTrueRadio.Checked = value;
				AdvancedFormattingFalseRadio.Checked = !value;
			}
		}
		#endregion

		#region Width
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
			}
		}
		int width = 600;
		#endregion

		#region LoadHtml
		public void LoadHtml(string html)
		{
			Bobs.HtmlRenderer r = new Bobs.HtmlRenderer();
			r.LoadHtml(html);

			Formatting = r.Formatting;

			if (!DisableContainer)
				Container = r.Container;

			HtmlTextBox.Text = r.GetHtmlForEditorControl();
		}
		#endregion
		#region Text
		public string Text
		{
			set
			{
				HtmlTextBox.Text = value;
				DisabledMessageDiv.InnerText = value;
			}
		}
		#endregion
		#region Clear()
		public void Clear()
		{
			LoadHtml("");
			HidePreview();
		}
		#endregion

		#region Save_Click
		public event EventHandler Save;
		protected void Save_Click(object sender, EventArgs eventArgs)
		{
			//HidePreview_Click(null, null);
			if (Save != null)
				Save(this, eventArgs);
		}
		#endregion
		#region GetHtml
		public string GetHtml()
		{
			return Comment.ParseCommentHtml(HtmlTextBox.Text, Formatting, Container);
		}
		#endregion
		#region GetPlainText
		public string GetPlainText()
		{
			return Cambro.Web.Helpers.Strip(Cambro.Web.Helpers.CleanHtml(HtmlTextBox.Text));
		}
		#endregion
		#region GetCleanHtml
		public string GetCleanHtml()
		{
			return Cambro.Web.Helpers.CleanHtml(HtmlTextBox.Text);
		}
		#endregion

		#region getDsiHtmlTag()
		string getDsiHtmlTag()
		{
			string dsiHtmlTagStart = "<dsi:html formatting=\"" + Formatting.ToString().ToLower() + "\"";

			if (!DisableContainer)
				dsiHtmlTagStart += " container=\"" + Container.ToString().ToLower() + "\"";

			dsiHtmlTagStart += ">";

			return dsiHtmlTagStart;
		}
		#endregion
		#region GetParaHtml()
		public string[] GetParaHtml() // Only used by the articles system
		{
			string startTag = getDsiHtmlTag();
			string endTag = "</dsi:html>";

			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("\\n\\W*\\n");
			string[] paraAry = r.Split(HtmlTextBox.Text);
			for (int i = 0; i < paraAry.Length; i++)
			{
				if (paraAry[i].Trim().Length == 0)
					paraAry[i] = "";
				else
				{
					paraAry[i] = startTag + Cambro.Web.Helpers.CleanHtml(paraAry[i]) + endTag;
				}
			}
			return paraAry;
		}
		#endregion

		#region ValidationPropertyValue
		public string ValidationPropertyValue
		{
			get
			{
				return this.GetCleanHtml();
			}
		}
		#endregion

		#region HidePreview_Click
		protected void HidePreview()
		{
			PreviewButton.InnerText = "Preview";
			HidePreviewButton.Style["display"] = "none";
			PreviewPanelContainer.Style["display"] = "none";
		}
		#endregion

		#region PreviewType
		public PreviewTypes PreviewType
		{
			get
			{
				int previewType;
				if (int.TryParse(uiPreviewType.Value, out previewType)) return (PreviewTypes)previewType;
				else return PreviewTypes.Normal;
				//return ViewState["PreviewType"] as PreviewTypes? ?? PreviewTypes.Normal;
			}
			set
			{
				uiPreviewType.Value = ((int)value).ToString();
				//ViewState["PreviewType"] = value;
			}
		}
		#endregion
		#region PreviewTypes
		public enum PreviewTypes
		{
			Normal,
			Comment,
			Article,
			Competition
		}
		#endregion
		public string MixmagWidth
		{
			get{
				return PreviewType == PreviewTypes.Comment ? "485" : PreviewType == PreviewTypes.Competition ? "300" : "600";
			}
		}
	}
}
