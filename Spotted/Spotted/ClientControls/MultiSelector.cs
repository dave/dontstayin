using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Collections;
using Js.ClientControls;
using Microsoft.JScript;
using Spotted;
 

namespace JsWebControls
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:MultiSelector runat=server></{0}:MultiSelector>")]
	[ClientScript]
	public class MultiSelector : EnhancedWebControl, IHasParameters, IIncludesJs
	{
		
		TextBox htmlAutoCompleteInput;
		HiddenField htmlAutoCompleteHiddenInput;
		HiddenField hiddenOuput;
		HasParametersController iHasParametersController;
		public MultiSelector()
			: base(HtmlTextWriterTag.Ul)
		{
			this.CssClass = "MultiSelector";
			WebControl listItem = new WebControl(HtmlTextWriterTag.Li);
			this.htmlAutoCompleteHiddenInput = new HiddenField();
			listItem.Controls.Add(htmlAutoCompleteHiddenInput);
			this.htmlAutoCompleteInput = new TextBox();
			this.htmlAutoCompleteInput.TextMode = TextBoxMode.MultiLine;
			this.htmlAutoCompleteInput.BorderStyle = BorderStyle.None;
			this.htmlAutoCompleteInput.BorderWidth = 0;
			
			
			this.htmlAutoCompleteInput.Style["overflow"] = "hidden";
			
			listItem.Controls.Add(htmlAutoCompleteInput);

			this.htmlAutoCompleteInput.Attributes["autocomplete"] = "off";
			this.hiddenOuput = new HiddenField();
			listItem.Controls.Add(hiddenOuput);
			this.Parameters = new Dictionary<string, string>();
			this.ParametersHiddenField = new HiddenField();
			listItem.Controls.Add(ParametersHiddenField);
			this.Controls.Add(listItem);
			this.iHasParametersController = new HasParametersController(this);
			
		}

		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			ScriptSharp.Include(page, "/ClientControls");
		
			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		public int TextBoxTabIndex { get; set; }
		

		private bool AreSame(List<KeyValuePair<string, string>> a, List<KeyValuePair<string, string>> b)
		{
			if (a.Count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				if (a[i].Key != b[i].Key || a[i].Value != b[i].Value) { return false; }
			}
			return true;

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
		public string PopupMenuClassName
		{
			get { return ViewState[HtmlAutoCompleteAttributes.PopupMenuClassName] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.PopupMenuClassName] = value; }
		}
		[Bindable(true), Category("Display"), Localizable(true)]
		public string PopupMenuSelectedItemClassName
		{
			get { return ViewState[HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName] = value; }
		}

	

		public IEnumerable<KeyValuePair<string, string>> Selections { get { return selections; } }
		List<KeyValuePair<string, string>> _selections  = null	;
		List<KeyValuePair<string, string>> selections
		{
			get
			{
				if (_selections == null)
				{
					_selections = new List<KeyValuePair<string, string>>();
					if (this.Page.IsPostBack && Page.Request.Params[this.hiddenOuput.UniqueID] == null)
					{
						this._selections = ViewState["Selections"] as List<KeyValuePair<string, string>> ?? new List<KeyValuePair<string, string>>();
					}
					else if (this.hiddenOuput.Value.Length > 0)
					{
						List<string> unsplitParts = new List<string>(this.hiddenOuput.Value.Split(';'));
						foreach (var unsplitPart in unsplitParts)
						{
							string[] splitParts = unsplitPart.Split(':');
							Add(GlobalObject.unescape(splitParts[0]), GlobalObject.unescape(splitParts[1]));
						}
					}
					
				}
				return _selections;
			}
		}

		public void Add(string text, string value)
		{
			if (!keyDictionary.ContainsKey(text) && !valDictionary.ContainsKey(value))
			{
				keyDictionary.Add(text, value);
				valDictionary.Add(value, text);
				selections.Add(new KeyValuePair<string, string>(text, value));
				ViewState["Selections"] = selections;
			}
		}
		public void Remove(string value)
		{
			string key = valDictionary[value];
			keyDictionary.Remove(key);
			valDictionary.Remove(value);
			selections.Remove(new KeyValuePair<string, string>(key, value));
		}
		Dictionary<string, string> keyDictionary = new Dictionary<string, string>();
		Dictionary<string, string> valDictionary = new Dictionary<string, string>();
		 
		[Bindable(true), Category("Display"), Localizable(true)]
		public string Watermark
		{
			get { return ViewState[HtmlAutoCompleteAttributes.Watermark] as string; }
			set { ViewState[HtmlAutoCompleteAttributes.Watermark] = value; }
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
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (WebServiceUrl != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.WebServiceUrl, WebServiceUrl);
			if (WebServiceMethod != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.WebServiceMethod, WebServiceMethod);
			//if (CometServiceUrl != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.CometServiceUrl, CometServiceUrl);

			if (MaxNumberOfItemsToGet != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet, MaxNumberOfItemsToGet.ToString());
			if (PopupMenuClassName != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.PopupMenuClassName, PopupMenuClassName);
			if (PopupMenuSelectedItemClassName != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName, PopupMenuSelectedItemClassName);
			if (Watermark != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.Watermark, Watermark);
			if (onselectionmade != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.onselectionmade, onselectionmade);
			if (onpopupcancel != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.onpopupcancel, onpopupcancel);
			if (onhighlight != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.onhighlight, onhighlight);
			if (MinimumPopupWidth != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.MinimumPopupWidth, MinimumPopupWidth.Value.ToString());
			if (MaximumPopupWidth != null) this.htmlAutoCompleteInput.Attributes.Add(HtmlAutoCompleteAttributes.MaximumPopupWidth, MaximumPopupWidth.Value.ToString());
			if (Watermark != null)
			{
				if (htmlAutoCompleteInput.Text == "" || htmlAutoCompleteInput.Text == Watermark)
				{
					htmlAutoCompleteInput.CssClass = "Watermark";
					htmlAutoCompleteInput.Text = Watermark;
					htmlAutoCompleteInput.ReadOnly = true;
				}
				htmlAutoCompleteInput.Attributes.Add("Watermark", Watermark);
			};
			base.RenderChildren(writer);
		}

		public int Count
		{
			get
			{
				return this.selections.Count;
			}
		}
 
		protected override void OnPreRender(EventArgs e)
		{
			this.htmlAutoCompleteInput.TabIndex = (short)TextBoxTabIndex;
			this.hiddenOuput.Value = String.Join(";", selections.ConvertAll(s => Microsoft.JScript.GlobalObject.escape(s.Key) + ":" + Microsoft.JScript.GlobalObject.escape(s.Value)).ToArray());
			foreach (var selection in Selections)
			{
				WebControl li = new WebControl(HtmlTextWriterTag.Li);

				li.CssClass = MultiSelectorAttributes.MultiSelectorListBoxCss;
				li.Controls.Add(new LiteralControl(selection.Key));
				li.Attributes["val"] = selection.Value;
				li.Attributes["text"] = selection.Key;
				WebControl span = new WebControl(HtmlTextWriterTag.Span);
				span.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
				span.CssClass = MultiSelectorAttributes.MultiSelectorDelButtonCss;
				
				li.Controls.Add(span);

				this.Controls.AddAt(this.Controls.Count - 1, li);
			}
			base.OnPreRender(e);
#if DEBUG
			string ending = ".debug.js";
#else
			string ending = ".js";
#endif
			if (ScriptManager.GetCurrent(Page) != null && ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
			{
				ScriptSharp.Include(Page, "/ClientControls");
				//ScriptManager.RegisterClientScriptInclude(Page, typeof(System.Web.UI.Page), "/misc/Js/Desktop/ClientControls" + ending, "/misc/Js/Desktop/ClientControls" + ending);
				ScriptManager.RegisterStartupScript(this, this.GetType(), this.ClientID + "Init", GenerateInitialisationScript(), true);
			}
			else
			{
				this.Controls.Add(new LiteralControl("<script src=\"/misc/Js/Desktop/ClientControls" + ending + "\" type=\"text/javascript\"></script>"));
				this.Controls.Add(new LiteralControl(String.Format("<script>{0}</script>", GenerateInitialisationScript())));
			}
		}
		private string GenerateInitialisationScript()
		{
			return this.ClientID + "Behaviour = new Js.ClientControls.MultiSelectorBehaviour("
					+ "$get('" + this.ClientID + "'),"
					+ "new Js.ClientControls.HtmlAutoCompleteBehaviour("
					+ "$get('" + this.htmlAutoCompleteInput.ClientID + "'),"
					+ "$get('" + this.htmlAutoCompleteHiddenInput.ClientID + "'),"
					+ "$get('" + this.ClientID + "'),"
					+ "false,"
					+ "$get('" + this.ParametersHiddenField.ClientID + "')"
					+ "),"
					+ "$get('" + this.hiddenOuput.ClientID + "')"
					+ ");";
		}
		protected override void Render(HtmlTextWriter writer)
		{

			base.Render(writer);
		}
	 

		public void Clear()
		{
			selections.Clear();
			valDictionary.Clear();
			keyDictionary.Clear();
		}



		public Dictionary<string, string> Parameters { get; private set; }
		public HiddenField ParametersHiddenField { get; private set; }
	}
}
