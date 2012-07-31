//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.SiteSearchBox
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiAuto {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiAutoBehaviour");}}
		public Element GoogleSearchCode {get {if (_GoogleSearchCode == null) {_GoogleSearchCode = (Element)Document.GetElementById(clientId + "_GoogleSearchCode");}; return _GoogleSearchCode;}} private Element _GoogleSearchCode;
		public jQueryObject GoogleSearchCodeJ {get {if (_GoogleSearchCodeJ == null) {_GoogleSearchCodeJ = jQuery.Select("#" + clientId + "_GoogleSearchCode");}; return _GoogleSearchCodeJ;}} private jQueryObject _GoogleSearchCodeJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
	}
}
