using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Xml;

using Bobs;

namespace Spotted.Pages.Promoters
{
	public partial class StyledSetup : PromoterUserControl
	{
		Misc logoFile = null;
		Misc cssFile = null;
		Misc backgroundFile = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.TemplateForm.Enctype = "multipart/form-data";

			if (StyledObject == null)
				throw new DsiUserFriendlyException("Not a valid styleable object. Please go back to your promoter page and try again.");

			if(StyledObject.PromoterK != CurrentPromoter.K)
				throw new DsiUserFriendlyException("This styleable object does not belong to promoter account: " + CurrentPromoter.Name);

			if (!this.IsPostBack)
			{
				SetupDropDownLists();

				H1Header.InnerHtml = "Styled setup for " + StyledObject.TypeName.ToLower() + ": " + StyledObject.FriendlyName;

				// If StyleObject already has a saved StyledCss, then load it.
				//if (StyledObject.StyledCss.Length > 0)
				//{
				//    this.CssDropDownList.Value = "";
				//    this.CssUrlHiddenTextBox.Text = StyledObject.UrlCss();
				//}

				LoadFromStylesXml();
				try
				{
					//this.CssDropDownList.Value = "";
					//if(StyledObject.StyledCss.Length > 0)
					//    this.CssDropDownList.Value = StyledObject.UrlCss();
					if (this.CssUrlHiddenTextBox.Text != "")
						this.CssDropDownList.Value = this.CssUrlHiddenTextBox.Text;
				}
				catch{}
			}
			//RemoveLogoLinkButton.Visible = LogoUrlHiddenTextBox.Text.Length > 0;
			LinkToStyledPages.Visible = StyledObject.StyledCss.Length > 0;
			LinkToStyledPages.HRef = StyledObject.UrlStyled();
			//LinkToStyledPages.InnerHtml = "Click here to go to your styled pages.";

		}

		public IStyledEventHolder StyledObject
		{
			get
			{
				if (styledObject == null)
				{
					int k = Convert.ToInt32(ContainerPage.Url["K"].Value);
					Model.Entities.ObjectType objType = (Model.Entities.ObjectType)Convert.ToInt32(ContainerPage.Url["objType"].Value);
					var bob = Bob.Get(objType, k);
					if (bob is IStyledEventHolder)
						styledObject = (IStyledEventHolder)bob;
				}
				return styledObject;
			}
		}
		private IStyledEventHolder styledObject;

		#region Setup DropDownLists
		private void SetupDropDownLists()
		{
			SetupCssDropDownList();
			SetupFontFamilyDropDownList();
			SetupFontSizeDropDownLists();
			SetupFontWeightDropDownLists();
			SetupTextDecorationDropDownLists();
			SetupTextAlignDropDownLists();
		}

		private void SetupCssDropDownList()
		{
			CssDropDownList.Items.Clear();
			CssDropDownList.Items.Add(new ListItem("Default", "/misc/styled/default.css"));
			CssDropDownList.Items.Add(new ListItem("Black", "/misc/styled/black.css"));
			CssDropDownList.Items.Add(new ListItem("Blue", "/misc/styled/blue.css"));
			CssDropDownList.Items.Add(new ListItem("Green", "/misc/styled/green.css"));
			CssDropDownList.Items.Add(new ListItem("Pink", "/misc/styled/pink.css"));
			//CssDropDownList.Items.Add(new ListItem("Frantic", "/misc/styled/frantic.css"));
			//CssDropDownList.Items.Add(new ListItem("Pacha", "/misc/styled/pacha.css"));
			//CssDropDownList.Items.Add(new ListItem("Tasty", "/misc/styled/tasty.css"));
			//if(StyledObject.StyledCss.Length > 0)
			//    CssDropDownList.Items.Add(new ListItem("Your saved style", StyledObject.UrlCss()));
			CssDropDownList.Items.Add(new ListItem("Upload your own", ""));
		}

		private void SetupFontFamilyDropDownList()
		{
			List<ListItem> fontListItems = new List<ListItem>();
			fontListItems.Add(new ListItem("", ""));
			fontListItems.Add(new ListItem("Arial", "arial"));
			//fontListItems.Add(new ListItem("Courier New", "courier"));
			fontListItems.Add(new ListItem("Times New Roman", "times"));
			fontListItems.Add(new ListItem("Verdana", "verdana"));

			FontFamilyDropDownList.Items.Clear();
			FontFamilyDropDownList.Items.AddRange(fontListItems.ToArray());
		}

		private void SetupFontSizeDropDownLists()
		{	
			BodyFontSizeDropDownList.Items.Clear();
			BodyFontSizeDropDownList.Items.AddRange(GetFontSizeOptions());

			HeaderFontSizeDropDownList.Items.Clear();
			HeaderFontSizeDropDownList.Items.AddRange(GetFontSizeOptions());

			LinksFontSizeDropDownList.Items.Clear();
			LinksFontSizeDropDownList.Items.AddRange(GetFontSizeOptions());

			LinksHoverFontSizeDropDownList.Items.Clear();
			LinksHoverFontSizeDropDownList.Items.AddRange(GetFontSizeOptions());
		}

		private ListItem[] GetFontSizeOptions()
		{
			List<ListItem> fontListItems = new List<ListItem>();
			fontListItems.Add(new ListItem("", ""));
			fontListItems.Add(new ListItem("XX-Small", "xx-small"));
			fontListItems.Add(new ListItem("X-Small", "x-small"));
			fontListItems.Add(new ListItem("Small", "small"));
			fontListItems.Add(new ListItem("Medium", "medium"));
			fontListItems.Add(new ListItem("Large", "large"));
			fontListItems.Add(new ListItem("X-Large", "x-large"));
			fontListItems.Add(new ListItem("XX-Large", "xx-large"));

			return fontListItems.ToArray();
		}

		private void SetupFontWeightDropDownLists()
		{
			BodyFontWeightDropDownList.Items.Clear();
			BodyFontWeightDropDownList.Items.AddRange(GetFontWeightOptions());

			HeaderFontWeightDropDownList.Items.Clear();
			HeaderFontWeightDropDownList.Items.AddRange(GetFontWeightOptions());

			LinksFontWeightDropDownList.Items.Clear();
			LinksFontWeightDropDownList.Items.AddRange(GetFontWeightOptions());

			LinksHoverFontWeightDropDownList.Items.Clear();
			LinksHoverFontWeightDropDownList.Items.AddRange(GetFontWeightOptions());
		}

		private ListItem[] GetFontWeightOptions()
		{
			List<ListItem> fontListItems = new List<ListItem>();
			fontListItems.Add(new ListItem("", ""));
			fontListItems.Add(new ListItem("Normal", "normal"));
			fontListItems.Add(new ListItem("Bold", "bold"));
			fontListItems.Add(new ListItem("Italic", "italic"));
			fontListItems.Add(new ListItem("Bold italic", "bold italic"));

			return fontListItems.ToArray();
		}

		private void SetupTextDecorationDropDownLists()
		{
			BodyTextDecorationDropDownList.Items.Clear();
			BodyTextDecorationDropDownList.Items.AddRange(GetTextDecorationOptions());

			HeaderTextDecorationDropDownList.Items.Clear();
			HeaderTextDecorationDropDownList.Items.AddRange(GetTextDecorationOptions());


			LinksTextDecorationDropDownList.Items.Clear();
			LinksTextDecorationDropDownList.Items.AddRange(GetTextDecorationOptions());

			LinksHoverTextDecorationDropDownList.Items.Clear();
			LinksHoverTextDecorationDropDownList.Items.AddRange(GetTextDecorationOptions());
		}

		private ListItem[] GetTextDecorationOptions()
		{
			List<ListItem> fontListItems = new List<ListItem>();
			fontListItems.Add(new ListItem("", ""));
			fontListItems.Add(new ListItem("None", "none"));
			fontListItems.Add(new ListItem("Underline", "underline"));

			return fontListItems.ToArray();
		}

		private void SetupTextAlignDropDownLists()
		{
			LogoAlignDropDownList.Items.Clear();
			LogoAlignDropDownList.Items.AddRange(GetTextAlignOptions());

			BodyTextAlignDropDownList.Items.Clear();
			BodyTextAlignDropDownList.Items.AddRange(GetTextAlignOptions());

			HeaderTextAlignDropDownList.Items.Clear();
			HeaderTextAlignDropDownList.Items.AddRange(GetTextAlignOptions());

			LinksTextAlignDropDownList.Items.Clear();
			LinksTextAlignDropDownList.Items.AddRange(GetTextAlignOptions());

			LinksHoverTextAlignDropDownList.Items.Clear();
			LinksHoverTextAlignDropDownList.Items.AddRange(GetTextAlignOptions());			
		}

		private ListItem[] GetTextAlignOptions()
		{
			List<ListItem> fontListItems = new List<ListItem>();
			fontListItems.Add(new ListItem("", ""));
			fontListItems.Add(new ListItem("Left", "left"));
			fontListItems.Add(new ListItem("Centre", "center"));
			fontListItems.Add(new ListItem("Right", "right"));

			return fontListItems.ToArray();
		}
		#endregion

		#region Upload Files
		protected void UploadLogoButton_Click(object sender, EventArgs eventArgs)
		{
			logoFile = Misc.UploadFile(InputLogoFile, Usr.Current, CurrentPromoter, new List<string>(){ "jpg", "gif", "png" });
			LogoUrlHiddenTextBox.Text = logoFile != null ? "div.MainImage{background-image: url('" + logoFile.Url() + "'); height: " + logoFile.Height.ToString() + "px;}" : "";
		}
		protected void UploadCssButton_Click(object sender, EventArgs eventArgs)
		{
			cssFile = Misc.UploadFile(InputCssFile, Usr.Current, CurrentPromoter, new List<string>() { "css" });
			CssUrlHiddenTextBox.Text = cssFile != null ? cssFile.Url() : "";
		}
		protected void UploadBackgroundButton_Click(object sender, EventArgs eventArgs)
		{
			backgroundFile = Misc.UploadFile(InputBackgroundFile, Usr.Current, CurrentPromoter, new List<string>() { "jpg", "gif", "png" });
			BackgroundUrlHiddenTextBox.Text = backgroundFile != null ? backgroundFile.Url() : "";
		}
		#endregion

		#region Save
		protected void SaveCustomStyleButton_Click(object sender, EventArgs e)
		{
			// Validate user	

			StyledObject.StyledXml = CaptureUserInputsToXmlString();

			// parse CssFile
			StyledObject.StyledCss = ParseCssFile();

			// Capture inputs
			StyledObject.StyledCss += " " + GenerateCustomCssFromInputFields();
			
			// Save
			StyledObject.Update();
			LinkToStyledPages.Visible = StyledObject.StyledCss.Length > 0;
		}

		private string CaptureUserInputsToXmlString()
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlNode rootNode = xmlDoc.CreateNode(XmlNodeType.Element, "root", "");
			xmlDoc.AppendChild(rootNode);
			CaptureUserInputsToXmlDoc(xmlDoc, rootNode, CustomiseOptionsPanel);

			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			XmlTextWriter xw = new XmlTextWriter(sw);
			
			xmlDoc.WriteTo(xw);
			return sb.ToString();
		}

		private void CaptureUserInputsToXmlDoc(XmlDocument xmlDoc, XmlNode rootNode, Control control)
		{
			string value = "";
			if (control is HtmlInputText)
			{
				value = ((HtmlInputText)control).Value;			
			}
			else if (control is TextBox)
			{
				value = ((TextBox)control).Text;
			}
			else if (control is HtmlSelect)
			{
				value = ((HtmlSelect)control).Value;
			}
			else if (control is HtmlInputCheckBox)
			{
				value = ((HtmlInputCheckBox)control).Checked.ToString();
			}

			if (value != "")
			{
				XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, control.ID, "");
				node.InnerText = value;
				rootNode.AppendChild(node);
			}

			foreach (Control ctl in control.Controls)
			{
				CaptureUserInputsToXmlDoc(xmlDoc, rootNode, ctl);
			}
		}

		private string ParseCssFile()
		{
			string output = "";
			string inheritedCss = "";

			if (CssDropDownList.Value != "" && CssDropDownList.Value != StyledObject.UrlCss())
				inheritedCss = CssDropDownList.Value;
			else if (CssUrlHiddenTextBox.Text != "")
				inheritedCss = CssUrlHiddenTextBox.Text;

			if (inheritedCss.EndsWith(".css"))
			{
				WebClient webClient = new WebClient();
				Stream strm = webClient.OpenRead("http://" + Vars.DomainName + inheritedCss);
				StreamReader sr = new StreamReader(strm);
				try
				{
					output = sr.ReadToEnd();
				}
				finally
				{
					sr.Close();
					strm.Close();
					webClient.Dispose();
				}
			}
			return output;
		}

		private string GenerateCustomCssFromInputFields()
		{
			string newStyle = "";

			if (LogoUrlHiddenTextBox.Text != "")
			{
				newStyle += LogoUrlHiddenTextBox.Text;
				if (LogoAlignDropDownList.Value != "")
					newStyle += ".MainImage{background-position: " + LogoAlignDropDownList.Value + ";} ";
			}
			else
			{
				newStyle += "div.MainImage{background-image: none; height:0px; width:0px;}";
			}
			if (BackgroundUrlHiddenTextBox.Text != "")
			{
				newStyle += "body{background: url('" + BackgroundUrlHiddenTextBox.Text + "') " + (NoRepeatBackgroundCheckBox.Checked ? "no-repeat" : "repeat") + ";}";
			}
			
			string bodyStyle = GenerateElementStyle(BodyTextColourInput, BodyBackgroundColourInput, BodyFontSizeDropDownList, BodyFontWeightDropDownList, BodyTextDecorationDropDownList, BodyTextAlignDropDownList);
			if(FontFamilyDropDownList.Value != "")
				bodyStyle += "font-family: " + FontFamilyDropDownList.Value + "; ";
			if(bodyStyle != "")
				newStyle += "body, #form1, body div, div.BodyDiv, body div.OuterDiv, body div.WelcomeDiv, body div.WelcomeDiv a:link, body div.WelcomeDiv a:visited, body div.WelcomeDiv a:hover {" + bodyStyle + "} ";

			if (BodyTextAlignDropDownList.Value == "left")
				newStyle += "div.OuterDiv{left:0%;margin-left:0px;}";
			else if (BodyTextAlignDropDownList.Value == "center")
				newStyle += "div.OuterDiv{left:50%;margin-left:-400px;}";
			else if (BodyTextAlignDropDownList.Value == "right")
				newStyle += "div.OuterDiv{left:100%;margin-left:-800px;}";

			string headerStyle = GenerateElementStyle(HeaderTextColourInput, HeaderBackgroundColourInput, HeaderFontSizeDropDownList, HeaderFontWeightDropDownList, HeaderTextDecorationDropDownList, HeaderTextAlignDropDownList);
			if(headerStyle != "")
				newStyle += "div h2{" + headerStyle + "} ";
			
			string linksStyle = GenerateElementStyle(LinksTextColourInput, LinksBackgroundColourInput, LinksFontSizeDropDownList, LinksFontWeightDropDownList, LinksTextDecorationDropDownList, LinksTextAlignDropDownList);
			if(linksStyle != "")
				newStyle += "div.Link, div a.Link:link, div a.Link:visited{" + linksStyle + "} ";

			string linksHoverStyle = GenerateElementStyle(LinksHoverTextColourInput, LinksHoverBackgroundColourInput, LinksHoverFontSizeDropDownList, LinksHoverFontWeightDropDownList, LinksHoverTextDecorationDropDownList, LinksHoverTextAlignDropDownList);
			if(linksHoverStyle != "")
				newStyle += "div a.Link:hover{" + linksHoverStyle + "} ";

			return newStyle;			
		}

		private string GenerateElementStyle(HtmlInputText fontColourTextBox, HtmlInputText backgroundColourTextBox, HtmlSelect fontSizeDropDownList, HtmlSelect fontWeightDropDownList, HtmlSelect fontTextDecorationDropDownList, HtmlSelect fontTextAlignDropDownList)
		{
			string output = "";
			if(fontColourTextBox.Value != "")
				output += "color: " + fontColourTextBox.Value + "; ";
			if(backgroundColourTextBox.Value != "")
				output += "background-color: " + backgroundColourTextBox.Value + "; ";
			if (fontSizeDropDownList.Value != "")
				output += "font-size: " + fontSizeDropDownList.Value + "; ";
			if (fontWeightDropDownList.Value != "")
			{
				if (fontWeightDropDownList.Value.IndexOf("normal") >= 0)
					output += "font-style: normal; font-weight: normal;";
				else if (fontWeightDropDownList.Value.IndexOf("bold italic") >= 0)
					output += "font-style: italic; font-weight: bold;";
				else if (fontWeightDropDownList.Value.IndexOf("italic") >= 0)
					output += "font-style: italic; font-weight: normal;";
				else if (fontWeightDropDownList.Value.IndexOf("bold") >= 0)
					output += "font-style: normal; font-weight: bold;";
			}
			if (fontTextDecorationDropDownList.Value != "")
				output += "text-decoration: " + fontTextDecorationDropDownList.Value + "; ";
			if (fontTextAlignDropDownList.Value != "")
				output += "text-align: " + fontTextAlignDropDownList.Value + "; ";
					
			return output;
		}
		#endregion

		#region Load
		private void LoadFromStylesXml()
		{
			if (StyledObject != null && StyledObject.StyledXml.Length > 0)
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(StyledObject.StyledXml);
				LoadUserInputsFromXmlDoc(xmlDoc, CustomiseOptionsPanel);
			}
		}

		private void LoadUserInputsFromXmlDoc(XmlDocument xmlDoc, Control control)
		{
			try
			{
				if (xmlDoc.DocumentElement[control.ID] != null && xmlDoc.DocumentElement[control.ID].InnerText.Length > 0)
				{
					if (control is HtmlInputText)
					{
						((HtmlInputText)control).Value = xmlDoc.DocumentElement[control.ID].InnerText;
					}
					else if (control is HtmlSelect)
					{
						((HtmlSelect)control).Value = xmlDoc.DocumentElement[control.ID].InnerText;
					}
					else if (control is DropDownList)
					{
						((DropDownList)control).Text = xmlDoc.DocumentElement[control.ID].InnerText;
					}
					else if (control is TextBox)
					{
						((TextBox)control).Text = xmlDoc.DocumentElement[control.ID].InnerText;
					}
					else if (control is HtmlInputCheckBox)
					{
						((HtmlInputCheckBox)control).Checked = Convert.ToBoolean(xmlDoc.DocumentElement[control.ID].InnerText);
					}

				}
			}
			catch { }
			foreach (Control ctl in control.Controls)
			{
				LoadUserInputsFromXmlDoc(xmlDoc, ctl);
			}
		}

		#endregion
	}
}
