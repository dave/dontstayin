using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Spotted.CustomControls
{
	public class DbButton : HtmlControl
	{
		public string ImageFileNameTrue { get; set; }
		public string ImageFileNameFalse { get; set; }
		public string AltTrue { get; set; }
		public string AltFalse { get; set; }
		public string TextTrue { get; set; }
		public string TextFalse { get; set; }
		public string CssClass { get; set; }
		public string CssStyle { get; set; }
		public string Align { get; set; }
		public int ImageWidth { get; set; }
		public int ImageHeight { get; set; }
		public string FunctionName { get; set; }
		public string FunctionArgs { get; set; }
		public bool InitialState { get; set; }
		public string DbButtonId { get; set; }
		public string OnReturn { get; set; }
		public string ConfirmStringTrue { get; set; }
		public string ConfirmStringFalse { get; set; }

		public DbButton() 
		{
			this.Load += new EventHandler(RegisterInit);
			this.PreRender += new EventHandler(RegisterScript);
		}

		public void RegisterInit(object o, EventArgs e)
		{
			ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}
		public void RegisterScript(object o, EventArgs e)
		{
			string s = String.Format(@"DbButtonFull(""{0}"",""{1}"",""{2}"",""{3}"",""{4}"",""{5}"",""{6}"",""{7}"",""{8}"",{9},{10},""{11}"",""{12}"",{13},""{14}"",""{15}"",""{16}"",""{17}"",""-"");",
				ImageFileNameTrue,
				ImageFileNameFalse,
				AltTrue,
				AltFalse,
				TextTrue,
				TextFalse,
				CssClass,
				CssStyle,
				Align,
				ImageWidth,
				ImageHeight,
				FunctionName,
				FunctionArgs,
				InitialState ? "1" : "0",
				DbButtonId,
				OnReturn,
				ConfirmStringTrue,
				ConfirmStringFalse);
			ScriptManager.RegisterStartupScript(this, typeof(Control), "DbButtonScript", s, true);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			string initialAlt = InitialState ? AltTrue : AltFalse;
			string initialText = InitialState ? TextTrue : TextFalse;
			string alignAttribute = Align.Length == 0 ? "" : ("align=\"" + Align + "\"");

			string styleAttributeLink = CssStyle.Length == 0 ? "" : ("style=\"" + CssStyle + "\"");
			string styleTrue = CssStyle + (InitialState ? "" : ((CssStyle.Length == 0 || CssStyle.EndsWith(";") ? "" : ";") + "display : none;"));
			string styleFalse = CssStyle + (!InitialState ? "" : ((CssStyle.Length == 0 || CssStyle.EndsWith(";") ? "" : ";") + "display : none;"));
			string styleAttributeTrue = styleTrue.Length == 0 ? "" : ("style=\"" + styleTrue + "\"");
			string styleAttributeFalse = styleFalse.Length == 0 ? "" : ("style=\"" + styleFalse + "\"");

			string classAttribute = CssClass.Length == 0 ? "" : ("class=\"" + CssClass + "\"");
			string mouseOverAttribute = initialAlt.Length == 0 ? "" : ("onmouseover=\"stt('" + initialAlt.Replace("'","\\'") + "');\" onmouseout=\"htm();\"");
			string textSpan = initialText.Length == 0 ? "" : ("<span id=\"" + DbButtonId + "-span\">" + initialText + "</span>");

			var buttonHtml = "<span id=\"" + DbButtonId + "\" " + mouseOverAttribute + "><a href=\"\" " + styleAttributeLink + " " + classAttribute + " onclick=\"DbButtonClick('" + DbButtonId + "');return false;\"><img src=\"" + ImageFileNameTrue + "\" border=\"0\" " + alignAttribute + " " + styleAttributeTrue + " " + classAttribute + " height=\"" + ImageHeight.ToString() + "\" width=\"" + ImageWidth.ToString() + "\" id=\"" + DbButtonId + "-true\"><img src=\"" + ImageFileNameFalse + "\" border=\"0\" " + alignAttribute + " " + styleAttributeFalse + " " + classAttribute + " height=\"" + ImageHeight.ToString() + "\" width=\"" + ImageWidth.ToString() + "\" id=\"" + DbButtonId + "-false\">" + textSpan + "</a></span>";
			writer.Write(buttonHtml);
		}


	}
}
