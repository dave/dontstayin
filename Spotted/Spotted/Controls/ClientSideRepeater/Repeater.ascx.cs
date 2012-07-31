using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.JScript;
using Spotted.Controls.PagedData;

namespace Spotted.Controls.ClientSideRepeater
{
	[ClientScript]
	public partial class Repeater : EnhancedUserControl
	{

		public Repeater()
		{
		}

		[Browsable(false), DefaultValue(""), TemplateContainer(typeof(Container)), PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate Header { get; set; }
		
		[Browsable(false), DefaultValue(""), TemplateContainer(typeof(Container)), PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate Footer { get; set; }
		[Browsable(false), DefaultValue(""), TemplateContainer(typeof(Container)), PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate Between { get; set; }
		public Template uiItemTemplate;
		public Func<Page, Template> ItemTemplateGetter;
		protected Panel uiContent; 
		public IEnumerable<IEnumerable<KeyValuePair<string, string>>> Data { get; set; }
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Instantiate(Between, uiBetweenTemplate);
			Instantiate(Footer, uiFooterTemplate);
		}
		protected override void OnPreRender(EventArgs e)
		{
			Instantiate(Header, uiHeaderTemplate);
			
			this.uiItemTemplate = ItemTemplateGetter(this.Page);
			this.uiItemTemplate.ID = "uiItemTemplate";
			this.Controls.Add(uiItemTemplate);
			base.OnPreRender(e);
		}
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);

			writer.Write("<span id='" + this.ClientID + "_uiContent'>");
			if (Data != null)
			{
				foreach (var dataItem in Data)
				{
					string text = uiItemTemplate.TemplateText;
					foreach (var dataItemField in dataItem)
					{
						text.Replace("{" + dataItemField.Key + "}", dataItemField.Value);
					}
					writer.Write(text);
				}
			}
			writer.Write("</span>");

		}

		static void Instantiate(ITemplate template, Control placeHolder)
		{
			if (template != null)
			{
				var container = new Container();
				template.InstantiateIn(container);
				placeHolder.Controls.Add(container);
			}
		}
		public class Container : Control, INamingContainer
		{
			internal Container()
			{
				 
			}
			protected override void Render(HtmlTextWriter writer)
			{
				using(StringWriter sw = new StringWriter())
				using(HtmlTextWriter tw = new HtmlTextWriter(sw))
				{
					base.Render(tw);
					writer.Write(GlobalObject.escape(sw.ToString()));
				}

			}

		}

	}
}
