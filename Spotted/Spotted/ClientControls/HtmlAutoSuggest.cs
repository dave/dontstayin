using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Js.ClientControls;
using Spotted;

namespace JsWebControls
{
	[ToolboxData("<{0}:HtmlAutoSuggest runat=server></{0}:HtmlAutoSuggest>")]
	[ClientScript]
	public class HtmlAutoSuggest : EnhancedWebControl, IHasParameters
	{
		TextBox input;
		HiddenField hiddenInput;
		public event EventHandler ValueChanged;
		HasParametersController iHasParametersController;
		public HtmlAutoSuggest()
		{
			this.input = new TextBox();
			this.Controls.Add(input);
			this.ParametersHiddenField = new HiddenField();
			this.Controls.Add(this.ParametersHiddenField);
			this.Parameters = new Dictionary<string, string>();
			this.iHasParametersController = new HasParametersController(this);
			this.hiddenInput = new HiddenField();
			this.Controls.Add(hiddenInput);
			
			this.hiddenInput.ValueChanged +=new EventHandler(hiddenInput_ValueChanged);
			
			input.Attributes["autocomplete"] = "off";
			
		}

		void hiddenInput_ValueChanged(object sender, EventArgs e)
		{
			this.Text = hiddenInput.Value;
			if (ValueChanged != null) {ValueChanged(this, EventArgs.Empty);}
		}
		[Bindable(true), Category("Data"), Localizable(true)]
		public string WebServiceUrl
		{
			get { return ViewState[HtmlAutoCompleteAttributes.WebServiceUrl] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.WebServiceUrl] = value; }
		}
		[Bindable(true), Category("Data"), Localizable(true)]
		public string WebServiceMethod
		{
			get { return ViewState[HtmlAutoCompleteAttributes.WebServiceMethod] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.WebServiceMethod] = value; }
		}
		//[Bindable(true), Category("Data"), Localizable(true)]
		//public string CometServiceUrl
		//{
		//    get { return ViewState[HtmlAutoCompleteAttributes.CometServiceUrl] as string; }
		//    set { ViewState[HtmlAutoCompleteAttributes.CometServiceUrl] = value; }
		//}

		[Bindable(true), Category("Data"),  Localizable(true)]
		public int? MaxNumberOfItemsToGet
		{
			get { return ViewState[HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet] as int?; }
			set { ViewState[HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet] = value; }
		}
	 
		[Bindable(true), Category("Display"), Localizable(true)]
		public string PopupListClassName
		{
			get { return ViewState[HtmlAutoCompleteAttributes.PopupMenuClassName] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.PopupMenuClassName] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public string PopupListSelectedItemClassName
		{
			get { return ViewState[HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public string Watermark
		{
			get { return ViewState[HtmlAutoCompleteAttributes.Watermark] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.Watermark] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public int? MinimumPopupWidth
		{
			get { return ViewState[HtmlAutoCompleteAttributes.MinimumPopupWidth] as int?; }
			set { ViewState[HtmlAutoCompleteAttributes.MinimumPopupWidth] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public int? MaximumPopupWidth
		{
			get { return ViewState[HtmlAutoCompleteAttributes.MaximumPopupWidth] as int?; }
			set { ViewState[HtmlAutoCompleteAttributes.MaximumPopupWidth] = value; }
		}
		[Bindable(true), Category("JsHooks"), Localizable(true)]
		public string onhighlight
		{
			get { return ViewState[HtmlAutoCompleteAttributes.onhighlight] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.onhighlight] = value; }
		}
		[Bindable(true), Category("JsHooks"), Localizable(true)]
		public string onpopupcancel
		{
			get { return ViewState[HtmlAutoCompleteAttributes.onpopupcancel] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.onpopupcancel] = value; }
		}
		[Bindable(true), Category("JsHooks"), Localizable(true)]
		public string onselectionmade
		{
			get { return ViewState[HtmlAutoCompleteAttributes.onselectionmade] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.onselectionmade] = value; }
		}

		[Bindable(true), Localizable(true)]
		public bool AutoPostBack
		{
			get { return (ViewState["AutoPostBack"] as bool? ?? false); }
			set { ViewState["AutoPostBack"] = value; }
		}



		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (WebServiceUrl != null) input.Attributes.Add(HtmlAutoCompleteAttributes.WebServiceUrl, WebServiceUrl);
			if (WebServiceMethod != null) input.Attributes.Add(HtmlAutoCompleteAttributes.WebServiceMethod, WebServiceMethod);
			//if (CometServiceUrl != null) input.Attributes.Add(HtmlAutoCompleteAttributes.CometServiceUrl, CometServiceUrl);
			if (MaxNumberOfItemsToGet != null) input.Attributes.Add(HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet, MaxNumberOfItemsToGet.ToString());
			if (PopupListClassName != null) input.Attributes.Add(HtmlAutoCompleteAttributes.PopupMenuClassName, PopupListClassName);
			if (PopupListSelectedItemClassName != null) input.Attributes.Add(HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName, PopupListSelectedItemClassName);
			if (AutoPostBack) input.Attributes.Add("AutoPostBackFunction",  Page.ClientScript.GetPostBackEventReference(this, "")  );
			if (MinimumPopupWidth != null) input.Attributes.Add(HtmlAutoCompleteAttributes.MinimumPopupWidth, MinimumPopupWidth.ToString());
			if (MaximumPopupWidth != null) input.Attributes.Add(HtmlAutoCompleteAttributes.MaximumPopupWidth, MaximumPopupWidth.ToString());
			if (Watermark != null)
			{
				if (input.Text == "" || input.Text == Watermark)
				{
					input.CssClass = "Watermark";
					input.Text = Watermark;
					input.ReadOnly = true;
				}
				input.Attributes.Add(HtmlAutoCompleteAttributes.Watermark, Watermark);
			}
			if (onselectionmade != null) input.Attributes.Add(HtmlAutoCompleteAttributes.onselectionmade, onselectionmade);
			if (onpopupcancel != null) input.Attributes.Add(HtmlAutoCompleteAttributes.onpopupcancel, onpopupcancel);
			if (onhighlight != null) input.Attributes.Add(HtmlAutoCompleteAttributes.onhighlight, onhighlight);

			base.RenderChildren(writer);
		}
		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write("<span class='HtmlAutoSuggest'>");
			base.Render(writer);
			writer.Write("</span>");
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public Unit Width
		{
			get { return input.Width; }
			set { input.Width = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public Unit Height
		{
			get { return input.Height; }
			set { input.Height = value; }
		}
		[Bindable(true), Category("Data"), Localizable(true)]
		public string Text
		{
			get 
			{
				if (Watermark == input.Text) { return ""; }
				return ViewState["Text"] as string ?? ""; 
			}
			set
			{
				ViewState["Text"]  = value;
			}
		}





		protected override void OnPreRender(EventArgs e)
		{
			this.input.Text = Text;
			this.hiddenInput.Value = Text;

			base.OnPreRender(e);

			ScriptManager sm = ScriptManager.GetCurrent(Page);
			string script = "<script>"
						+ this.ClientID + "Behaviour = new Js.ClientControls.HtmlAutoCompleteBehaviour"
						+ "("
						+ "$get('" + this.input.ClientID + "'),"
						+ "$get('" + this.hiddenInput.ClientID + "'),"
						+ "null,"
						+ "true,"
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




		public Dictionary<string, string> Parameters { get; private set; }

		public HiddenField ParametersHiddenField { get; private set; }
	}
}
