using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Web.Util;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace Spotted.CustomControls
{
	/// <devdoc>
	/// <para>Interacts with the parser to build a <see cref='Spotted.Controls.UrlButton'/> control.</para> 
	/// </devdoc>
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UrlButtonControlBuilder : ControlBuilder
	{


		/// <internalonly/> 
		/// <devdoc>
		///    <para>Specifies whether white space literals are allowed.</para> 
		/// </devdoc>
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}



	/// <devdoc> 
	///    <para>Constructs a link button and defines its properties.</para>
	/// </devdoc>
	[
	ControlBuilderAttribute(typeof(UrlButtonControlBuilder)),
	DefaultEvent("Click"),
	DefaultProperty("Text"),
	ToolboxData("<{0}:UrlButton runat=\"server\">UrlButton</{0}:UrlButton>"),
	ParseChildren(false),
	SupportsEventValidation
	]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UrlButton : WebControl, IButtonControl, IPostBackEventHandler
	{

		private static readonly object EventClick = new object();
		private static readonly object EventCommand = new object();


		/// <devdoc>
		/// <para>Initializes a new instance of the <see cref='Spotted.Controls.UrlButton'/> class.</para> 
		/// </devdoc>
		public UrlButton()
			: base(HtmlTextWriterTag.A)
		{
		}


		/// <devdoc>
		///    <para>Specifies the command name that is propagated in the
		///    <see cref='Spotted.Controls.UrlButton.Command'/>event along with the associated <see cref='Spotted.Controls.UrlButton.CommandArgument'/>
		///    property.</para> 
		/// </devdoc>
		[
		DefaultValue(""),
		Themeable(false)
		]
		public string CommandName
		{
			get
			{
				string s = (string)ViewState["CommandName"];
				return ((s == null) ? String.Empty : s);
			}
			set
			{
				ViewState["CommandName"] = value;
			}
		}



		/// <devdoc> 
		///    <para>Specifies the command argument that is propagated in the 
		///    <see langword='Command '/>event along with the associated <see cref='Spotted.Controls.UrlButton.CommandName'/>
		///    property.</para> 
		/// </devdoc>
		[
		Bindable(true),
		DefaultValue(""),
		Themeable(false)
		]
		public string CommandArgument
		{
			get
			{
				string s = (string)ViewState["CommandArgument"];
				return ((s == null) ? String.Empty : s);
			}
			set
			{
				ViewState["CommandArgument"] = value;
			}
		}


		/// <devdoc>
		///    <para>Gets or sets whether pressing the button causes page validation to fire. This defaults to True so that when
		///          using validation controls, the validation state of all controls are updated when the button is clicked, both 
		///          on the client and the server. Setting this to False is useful when defining a cancel or reset button on a page
		///          that has validators.</para> 
		/// </devdoc> 
		[
		DefaultValue(true),
		Themeable(false)
		]
		public virtual bool CausesValidation
		{
			get
			{
				object b = ViewState["CausesValidation"];
				return ((b == null) ? true : (bool)b);
			}
			set
			{
				ViewState["CausesValidation"] = value;
			}
		}
#if SITECOUNTERS
 
        [ 
        DefaultValue("UrlButton"),
        Themeable(false), 
        WebCategory("SiteCounters"),
        WebSysDescription(SR.Control_For_SiteCounters_CounterGroup),
        ]
        public String CounterGroup { 
            get {
                String s = (String)ViewState["CounterGroup"]; 
                return((s == null) ? "UrlButton" : s); 
            }
            set { 
                ViewState["CounterGroup"] = value;
            }
        }
 

        [ 
        DefaultValue(""), 
        Themeable(false),
        WebCategory("SiteCounters"), 
        WebSysDescription(SR.Control_For_SiteCounters_CounterName),
        ]
        public String CounterName {
            get { 
                String s = (String)ViewState["CounterName"];
                return((s == null) ? String.Empty : s); 
            } 
            set {
                ViewState["CounterName"] = value; 
            }
        }

 
        [
        DefaultValue(false), 
        Themeable(false), 
        WebCategory("SiteCounters"),
        WebSysDescription(SR.Control_For_SiteCounters_CountClicks), 
        ]
        public bool CountClicks {
            get {
                object b = ViewState["CountClicks"]; 
                return((b == null) ? false : (bool)b);
            } 
            set { 
                ViewState["CountClicks"] = value;
            } 
        }
#endif

		/// <devdoc> 
		///    The script that is executed on a client-side click.
		/// </devdoc> 
		[
		DefaultValue(""),
		Themeable(false)
		]
		public virtual string OnClientClick
		{
			get
			{
				string s = (string)ViewState["OnClientClick"];
				if (s == null)
				{
					return String.Empty;
				}
				return s;
			}
			set
			{
				ViewState["OnClientClick"] = value;
			}
		}


		/// <devdoc> 
		///  The Href
		/// </devdoc> 
		[
		DefaultValue(""),
		Themeable(false)
		]
		public virtual string Href
		{
			get
			{
				return ViewState["Href"] as string ?? String.Empty;
			}
			set
			{
				ViewState["Href"] = value;
			}
		}

		//internal override bool RequiresLegacyRendering
		//{
		//    get
		//    {
		//        return true;
		//    }
		//}


		/// <devdoc> 
		///    <para>Gets or sets the text display for the link button.</para>
		/// </devdoc> 
		[
		Localizable(true),
		Bindable(true),
		DefaultValue(""),
		PersistenceMode(PersistenceMode.InnerDefaultProperty)
		]
		public virtual string Text
		{
			get
			{
				object o = ViewState["Text"];
				return ((o == null) ? String.Empty : (string)o);
			}
			set
			{
				if (HasControls())
				{
					Controls.Clear();
				}
				ViewState["Text"] = value;
			}
		}


		[
		DefaultValue(""),
		Editor("System.Web.UI.Design.UrlEditor, ", typeof(UITypeEditor)),
		Themeable(false),
		UrlProperty("*.aspx")
		]
		public virtual string PostBackUrl
		{
			get
			{
				string s = (string)ViewState["PostBackUrl"];
				return s == null ? String.Empty : s;
			}
			set
			{
				ViewState["PostBackUrl"] = value;
			}
		}
#if SITECOUNTERS 

        [ 
        DefaultValue(-1), 
        Themeable(false),
        WebCategory("SiteCounters"), 
        WebSysDescription(SR.Control_For_SiteCounters_RowsPerDay),
        ]
        public int RowsPerDay {
            get { 
                Object o = ViewState["RowsPerDay"];
                return((o == null) ? -1 : (int) o); 
            } 
            set {
                if (value == 0) { 
                    throw new ArgumentOutOfRangeException("value");
                }
                ViewState["RowsPerDay"] = value;
            } 
        }
 
 
        [
        DefaultValue(""), 
        Themeable(false),
        WebCategory("SiteCounters"),
        WebSysDescription(SR.Control_For_SiteCounters_SiteCountersProvider)
        ] 
        public String SiteCountersProvider {
            get { 
                String s = (String) ViewState["SiteCountersProvider"]; 
                return((s != null) ? s : String.Empty);
            } 
            set {
                ViewState["SiteCountersProvider"] = value;
            }
        } 

 
        [ 
        DefaultValue(true),
        Themeable(false), 
        WebCategory("SiteCounters"),
        WebSysDescription(SR.Control_For_SiteCounters_TrackApplicationName),
        ]
        public bool TrackApplicationName { 
            get {
                object b = ViewState["TrackApplicationName"]; 
                return((b == null) ? true : (bool)b); 
            }
            set { 
                ViewState["TrackApplicationName"] = value;
            }
        }
 

        [ 
        DefaultValue(true), 
        Themeable(false),
        WebCategory("SiteCounters"), 
        WebSysDescription(SR.Control_For_SiteCounters_TrackPageUrl),
        ]
        public bool TrackPageUrl {
            get { 
                object b = ViewState["TrackPageUrl"];
                return((b == null) ? true : (bool)b); 
            } 
            set {
                ViewState["TrackPageUrl"] = value; 
            }
        }
#endif

		[
		Themeable(false),
		DefaultValue("")
		]
		public virtual string ValidationGroup
		{
			get
			{
				string s = (string)ViewState["ValidationGroup"];
				return ((s == null) ? String.Empty : s);
			}
			set
			{
				ViewState["ValidationGroup"] = value;
			}
		}


		/// <devdoc> 
		///    <para>Occurs when the link button is clicked.</para>
		/// </devdoc> 
		public event EventHandler Click
		{
			add
			{
				Events.AddHandler(EventClick, value);
			}
			remove
			{
				Events.RemoveHandler(EventClick, value);
			}
		}



		/// <devdoc> 
		/// <para>Occurs when any item is clicked within the <see cref='Spotted.Controls.UrlButton'/> control tree.</para>
		/// </devdoc> 
		public event CommandEventHandler Command
		{
			add
			{
				Events.AddHandler(EventCommand, value);
			}
			remove
			{
				Events.RemoveHandler(EventCommand, value);
			}
		}


		/// <internalonly/>
		/// <devdoc> 
		///    Render the attributes on the begin tag.
		/// </devdoc> 
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool effectiveEnabled = IsEnabled;

			// Make sure we are in a form tag with runat=server.
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}

			
			if (effectiveEnabled && Page != null)
			{

				writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Href);
			}

			if (Enabled && !effectiveEnabled)
			{
				// We need to do the cascade effect on the server, because the browser 
				// only renders as disabled, but doesn't disable the functionality. 
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}

			// Need to merge the onclick attribute with the OnClientClick 
			string onClick = Util.EnsureEndWithSemiColon(OnClientClick);

			if (HasAttributes)
			{
				string userOnClick = Attributes["onclick"];
				if (userOnClick != null)
				{
					// We don't use Util.MergeScript because OnClientClick or
					// onclick attribute are set by page developer directly.  We
					// should preserve the value without adding javascript prefix.
					onClick += Util.EnsureEndWithSemiColon(userOnClick);
					Attributes.Remove("onclick");
				}
			}
			

			if (effectiveEnabled && Page != null)
			{
				PostBackOptions options = GetPostBackOptions();
				string postBackEventReference = null;
				if (options != null)
				{
					postBackEventReference = Page.ClientScript.GetPostBackEventReference(options, true);
				}

				onClick += Util.EnsureEndWithSemiColon(postBackEventReference);
				onClick += "return false;";
			}

			if (onClick.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
			}

			base.AddAttributesToRender(writer);


		}


		/// <internalonly/> 
		/// <devdoc> 
		/// </devdoc>
		protected override void AddParsedSubObject(object obj)
		{
			if (HasControls())
			{
				base.AddParsedSubObject(obj);
			}
			else
			{
				if (obj is LiteralControl)
				{
					Text = ((LiteralControl)obj).Text;
				}
				else
				{
					string currentText = Text;
					if (currentText.Length != 0)
					{
						Text = String.Empty;
						base.AddParsedSubObject(new LiteralControl(currentText));
					}
					base.AddParsedSubObject(obj);
				}
			}
		}

		// Returns the client post back options.
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions options = new PostBackOptions(this, String.Empty);
			options.RequiresJavaScriptProtocol = false;

			if (!String.IsNullOrEmpty(PostBackUrl))
			{
				// VSWhidbey 424614: Since the url is embedded as javascript in attribute, 
				// we should match the same encoding as done on HyperLink.NavigateUrl value.
				options.ActionUrl = HttpUtility.UrlPathEncode(ResolveClientUrl(PostBackUrl));

				// Also, there is a specific behavior in IE that when the script
				// is triggered in href attribute, the whole string will be
				// decoded once before the code is run.  This doesn't happen to 
				// onclick or other event attributes.  So here we do an extra
				// encoding to compensate the weird behavior on IE. 
				if (!DesignMode && Page != null &&
					String.Equals(Page.Request.Browser.Browser, "IE", StringComparison.OrdinalIgnoreCase))
				{
					options.ActionUrl = Util.QuoteJScriptString(options.ActionUrl, true);
				}
			}

			if (CausesValidation && Page.GetValidators(ValidationGroup).Count > 0)
			{
				options.PerformValidation = true;
				options.ValidationGroup = ValidationGroup;
			}

			return options;
		}


		/// <internalonly/> 
		/// <devdoc>
		///    Load previously saved state. 
		///    Overridden to synchronize Text property with LiteralContent. 
		/// </devdoc>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				string s = (string)ViewState["Text"];
				if (s != null)
					Text = s;
			}
		}


		/// <devdoc>
		/// <para>Raises the <see langword='Click '/> event.</para>
		/// </devdoc>
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler onClickHandler = (EventHandler)Events[EventClick];
			if (onClickHandler != null) onClickHandler(this, e);
		}


		/// <devdoc>
		/// <para>Raises the <see langword='Command'/> event.</para>
		/// </devdoc>
		protected virtual void OnCommand(CommandEventArgs e)
		{
			CommandEventHandler onCommandHandler = (CommandEventHandler)Events[EventCommand];
			if (onCommandHandler != null)
				onCommandHandler(this, e);

			// Command events are bubbled up the control heirarchy 
			RaiseBubbleEvent(this, e);
		}


		/// <internalonly/>
		/// <devdoc> 
		/// <para>Raises a <see langword='Click '/>event upon postback 
		/// to the server, and a <see langword='Command'/> event if the <see cref='Spotted.Controls.UrlButton.CommandName'/>
		/// is defined.</para> 
		/// </devdoc>
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			RaisePostBackEvent(eventArgument);
		}

		internal void ValidateEvent(string uniqueID, string eventArgument)
		{
			if ((this.Page != null) && this.SupportsEventValidation)
			{
				this.Page.ClientScript.ValidateEvent(uniqueID, eventArgument);
			}
		}
		private bool SupportsEventValidation
		{
			get
			{
				return SupportsEventValidationAttribute.SupportsEventValidation(base.GetType());
			}
		}
 

 
		/// <internalonly/> 
		/// <devdoc>
		/// <para>Raises a <see langword='Click '/>event upon postback 
		/// to the server, and a <see langword='Command'/> event if the <see cref='Spotted.Controls.UrlButton.CommandName'/>
		/// is defined.</para>
		/// </devdoc>
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			ValidateEvent(this.UniqueID, eventArgument);
#if ORCAS 
            SiteCounters siteCounters = Context.SiteCounters; 
            if (siteCounters.Enabled && CountClicks) {
                String counterName = CounterName; 
                if (counterName.Length == 0) {
                    counterName = ID;
                }
                siteCounters.Write(CounterGroup, counterName, SiteCounters.ClickEventText, 
                                   null, TrackApplicationName, TrackPageUrl,
                                   SiteCountersProvider, RowsPerDay); 
            } 
#endif
			if (CausesValidation)
			{
				Page.Validate(ValidationGroup);
			}
			OnClick(EventArgs.Empty);
			OnCommand(new CommandEventArgs(CommandName, CommandArgument));
		}


		/// <internalonly/>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (Page != null && Enabled)
			{
				//Page.RegisterPostBackScript();

				if ((CausesValidation && Page.GetValidators(ValidationGroup).Count > 0) ||
					 !String.IsNullOrEmpty(PostBackUrl))
				{
					//Page.RegisterWebFormsScript();  // VSWhidbey 489577 
				}
			}
		}


		/// <internalonly/> 
		/// <devdoc>
		/// </devdoc> 
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (HasRenderingData())
			{
				base.RenderContents(writer);
			}
			else
			{
				writer.Write(Text);
			}
		}
		internal bool HasRenderingData()
		{
			if (!this.HasControls())
			{
				return false;
			}
			return true;
		}

 
		internal static class Util
		{
			// Fields
			internal const char DeviceFilterSeparator = ':';
			private static char[] invalidFileNameChars = new char[] { '/', '\\', '?', '*', ':' };
			internal const string XmlnsAttribute = "xmlns:";

			
			internal static string EnsureEndWithSemiColon(string value)
			{
				if (value != null)
				{
					int length = value.Length;
					if ((length > 0) && (value[length - 1] != ';'))
					{
						return (value + ";");
					}
				}
				return value;
			}

			internal static string QuoteJScriptString(string value)
			{
				return QuoteJScriptString(value, false);
			}

			internal static string QuoteJScriptString(string value, bool forUrl)
			{
				StringBuilder builder = null;
				if (string.IsNullOrEmpty(value))
				{
					return string.Empty;
				}
				int startIndex = 0;
				int count = 0;
				for (int i = 0; i < value.Length; i++)
				{
					switch (value[i])
					{
						case '%':
							{
								if (!forUrl)
								{
									break;
								}
								if (builder == null)
								{
									builder = new StringBuilder(value.Length + 6);
								}
								if (count > 0)
								{
									builder.Append(value, startIndex, count);
								}
								builder.Append("%25");
								startIndex = i + 1;
								count = 0;
								continue;
							}
						case '\'':
							{
								if (builder == null)
								{
									builder = new StringBuilder(value.Length + 5);
								}
								if (count > 0)
								{
									builder.Append(value, startIndex, count);
								}
								builder.Append(@"\'");
								startIndex = i + 1;
								count = 0;
								continue;
							}
						case '\\':
							{
								if (builder == null)
								{
									builder = new StringBuilder(value.Length + 5);
								}
								if (count > 0)
								{
									builder.Append(value, startIndex, count);
								}
								builder.Append(@"\\");
								startIndex = i + 1;
								count = 0;
								continue;
							}
						case '\t':
							{
								if (builder == null)
								{
									builder = new StringBuilder(value.Length + 5);
								}
								if (count > 0)
								{
									builder.Append(value, startIndex, count);
								}
								builder.Append(@"\t");
								startIndex = i + 1;
								count = 0;
								continue;
							}
						case '\n':
							{
								if (builder == null)
								{
									builder = new StringBuilder(value.Length + 5);
								}
								if (count > 0)
								{
									builder.Append(value, startIndex, count);
								}
								builder.Append(@"\n");
								startIndex = i + 1;
								count = 0;
								continue;
							}
						case '\r':
							{
								if (builder == null)
								{
									builder = new StringBuilder(value.Length + 5);
								}
								if (count > 0)
								{
									builder.Append(value, startIndex, count);
								}
								builder.Append(@"\r");
								startIndex = i + 1;
								count = 0;
								continue;
							}
						case '"':
							{
								if (builder == null)
								{
									builder = new StringBuilder(value.Length + 5);
								}
								if (count > 0)
								{
									builder.Append(value, startIndex, count);
								}
								builder.Append("\\\"");
								startIndex = i + 1;
								count = 0;
								continue;
							}
					}
					count++;
				}
				if (builder == null)
				{
					return value;
				}
				if (count > 0)
				{
					builder.Append(value, startIndex, count);
				}
				return builder.ToString();
			}
		}
		[AttributeUsage(AttributeTargets.Class, AllowMultiple = false), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
		public sealed class SupportsEventValidationAttribute : Attribute
		{
			// Fields
			private static Hashtable _typesSupportsEventValidation = Hashtable.Synchronized(new Hashtable());

			// Methods
			internal static bool SupportsEventValidation(Type type)
			{
				object obj2 = _typesSupportsEventValidation[type];
				if (obj2 != null)
				{
					return (bool)obj2;
				}
				object[] customAttributes = type.GetCustomAttributes(typeof(SupportsEventValidationAttribute), false);
				bool flag = (customAttributes != null) && (customAttributes.Length > 0);
				_typesSupportsEventValidation[type] = flag;
				return flag;
			}
		}
	}





}

