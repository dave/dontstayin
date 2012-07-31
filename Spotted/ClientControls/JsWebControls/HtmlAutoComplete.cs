 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace JsWebControls
{
	[ToolboxData("<{0}:HtmlAutoComplete runat=server></{0}:HtmlAutoComplete>"), ValidationProperty("Value")]
	public class HtmlAutoComplete : WebControl, IHasParameters, IPostBackEventHandler
	{
		TextBox input;
		HiddenField hiddenInput;
		public event EventHandler ValueChanged;
		HasParametersController iHasParametersController;
		public string CssClass = "HtmlAutoComplete";
		public HtmlAutoComplete()
		{
			this.input = new TextBox();
			this.Controls.Add(input);
			this.ParametersHiddenField = new HiddenField();
			this.Controls.Add(this.ParametersHiddenField);
			this.Parameters = new Dictionary<string, string>();
			this.iHasParametersController = new HasParametersController(this);
			//this.CssClass = "HtmlAutoComplete";

			this.hiddenInput = new HiddenField();
			this.Controls.Add(hiddenInput);

			this.input.TextChanged += new EventHandler(input_TextChanged);
			this.hiddenInput.ValueChanged += new EventHandler(hiddenInput_ValueChanged);

			input.Attributes["autocomplete"] = "off";
		}
		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write("<span");
			if (CssClass.Length > 0)
			{
				writer.Write(" class=\"");
				writer.Write(CssClass);
				writer.Write("\"");
			}
			writer.Write(">");
			base.Render(writer);
			writer.Write("</span>");
		}
		void hiddenInput_ValueChanged(object sender, EventArgs e)
		{
			Value = Page.Request.Form[hiddenInput.UniqueID];
			if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
		}

		void input_TextChanged(object sender, EventArgs e)
		{
			Text = Page.Request.Form[input.UniqueID];
		}
 
		[Bindable(true), Category("Data"), Localizable(true)]
		public string WebServiceUrl
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.WebServiceUrl] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.WebServiceUrl] = value; }
		}
		[Bindable(true), Category("Data"), Localizable(true)]
		public string WebServiceMethod
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.WebServiceMethod] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.WebServiceMethod] = value; }
		}
		[Bindable(true), Category("Data"), Localizable(true)]
		public int PopupLeftOffset
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupLeftOffset] as int? ?? 0; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupLeftOffset] = value; }
		}
		[Bindable(true), Category("Data"), Localizable(true)]
		public int PopupTopOffset
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupTopOffset] as int? ?? 0; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupTopOffset] = value; }
		}
		[Bindable(true), Category("Data"), Localizable(true)]
		public string CometServiceUrl
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.CometServiceUrl] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.CometServiceUrl] = value; }
		}

		[Bindable(true), Category("Data"),  Localizable(true)]
		public int? MaxNumberOfItemsToGet
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet] as int?; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet] = value; }
		}
	 
		[Bindable(true), Category("Display"), Localizable(true)]
		public string PopupListClassName
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupMenuClassName] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupMenuClassName] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public string PopupListSelectedItemClassName
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public new Unit Width
		{
			get { return input.Width; }
			set { input.Width = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public new Unit Height
		{
			get { return input.Height; }
			set { input.Height = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public string Watermark
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.Watermark] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.Watermark] = value; }
		}
		[Bindable(true), Category("JsHooks"), Localizable(true)]
		public string onhighlight
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.onhighlight] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.onhighlight] = value; }
		}
		[Bindable(true), Category("JsHooks"), Localizable(true)]
		public string onpopupcancel
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.onpopupcancel] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.onpopupcancel] = value; }
		}
		[Bindable(true), Category("JsHooks"), Localizable(true)]
		public string onselectionmade
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.onselectionmade] as string; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.onselectionmade] = value; }
		}
		[Bindable(true), Localizable(true)]
		public bool AutoPostBack
		{
			get { return (ViewState["AutoPostBack"] as bool? ?? false); }
			set { ViewState["AutoPostBack"] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public int? MinimumPopupWidth
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.MinimumPopupWidth] as int?; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.MinimumPopupWidth] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public int? MaximumPopupWidth
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.MaximumPopupWidth] as int?; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.MaximumPopupWidth] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public bool RightAlign
		{
			get { return ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.RightAlign] as bool? ?? false; }
			set { ViewState[ScriptSharpLibrary.HtmlAutoCompleteAttributes.RightAlign] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public string Border
		{
			get { return ViewState["Border"] as string ; }
			set { ViewState["Border"] = value; }
		}
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (WebServiceUrl != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.WebServiceUrl, WebServiceUrl);
			if (WebServiceMethod != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.WebServiceMethod, WebServiceMethod);
			if (CometServiceUrl != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.CometServiceUrl, CometServiceUrl);

			if (MaxNumberOfItemsToGet != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet, MaxNumberOfItemsToGet.ToString());
			if (PopupListClassName != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupMenuClassName, PopupListClassName);
			if (PopupListSelectedItemClassName != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName, PopupListSelectedItemClassName);
			if (MinimumPopupWidth != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.MinimumPopupWidth, MinimumPopupWidth.Value.ToString());
			if (MaximumPopupWidth != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.MaximumPopupWidth, MaximumPopupWidth.Value.ToString());
			if (PopupLeftOffset != 0) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupLeftOffset, PopupLeftOffset.ToString());
			if (PopupTopOffset != 0) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.PopupTopOffset, PopupTopOffset.ToString());
			if (RightAlign) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.RightAlign, RightAlign.ToString());
			if (AutoPostBack) input.Attributes.Add("AutoPostBackFunction", Page.ClientScript.GetPostBackEventReference(this, ""));
			if (Border != null) input.Style["border"] = Border;
			if (Watermark != null)
			{
				if (input.Text == "" || input.Text == Watermark)
				{
					input.CssClass = "Watermark";
					input.Text = Watermark;
					input.ReadOnly = true;
				}
				input.Attributes.Add("Watermark", Watermark);
			}
			if (onselectionmade != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.onselectionmade, onselectionmade);
			if (onpopupcancel != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.onpopupcancel, onpopupcancel);
			if (onhighlight != null) input.Attributes.Add(ScriptSharpLibrary.HtmlAutoCompleteAttributes.onhighlight, onhighlight);

			base.RenderChildren(writer);
		}

		[Bindable(true), Category("Data"), Localizable(true)]
		public string Value
		{
			get { return this.ViewState["Value"] as string ?? ""; }
			set { ViewState["Value"] = value; }
		}
		[Bindable(true), Category("Data"), Localizable(true)]
		public string Text
		{
			get { return ((ViewState["Text"] as string == Watermark) ? "" : ViewState["Text"] as string) ?? ""; }
			set { ViewState["Text"] = (value == Watermark) ? "" : value; }
		}



	 
	 
		protected override void OnPreRender(EventArgs e)
		{
			input.Text = Text;
			hiddenInput.Value = Value;

			base.OnPreRender(e);

			ScriptManager sm = ScriptManager.GetCurrent(Page);
			string script = "<script>"
						+ this.ClientID + "Behaviour = new ScriptSharpLibrary.HtmlAutoCompleteBehaviour"
						+ "("
						+ "$get('" + this.input.ClientID + "'),"
						+ "$get('" + this.hiddenInput.ClientID + "'),"
						+ "null,"
						+ "false,"
						+ "$get('" + this.ParametersHiddenField.ClientID + "')"
						+ ");</script>";

			if (sm != null && sm.IsInAsyncPostBack)
			{
				ScriptManager.RegisterStartupScript
				(
					this,
					this.GetType(),
					this.ClientID + "init",
					script,
					false);
			}
			else
			{
				this.Controls.Add(new Literal()
				{
					Text = script
				});
			}
		}






		public HiddenField ParametersHiddenField { get; set; }
		public Dictionary<string, string> Parameters { get; set; }

		public void RaisePostBackEvent(string eventArgument)
		{
			if (ValueChanged != null)
				ValueChanged(this, new EventArgs<string>(eventArgument));
		}

	}
}
