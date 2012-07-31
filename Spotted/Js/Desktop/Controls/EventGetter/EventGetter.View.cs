//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.EventGetter
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiEventDisplayDiv {get {if (_uiEventDisplayDiv == null) {_uiEventDisplayDiv = (Element)Document.GetElementById(clientId + "_uiEventDisplayDiv");}; return _uiEventDisplayDiv;}} private Element _uiEventDisplayDiv;
		public jQueryObject uiEventDisplayDivJ {get {if (_uiEventDisplayDivJ == null) {_uiEventDisplayDivJ = jQuery.Select("#" + clientId + "_uiEventDisplayDiv");}; return _uiEventDisplayDivJ;}} private jQueryObject _uiEventDisplayDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
	}
}
