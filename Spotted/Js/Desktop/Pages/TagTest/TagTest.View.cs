//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.TagTest
{
	public partial class View
		 : Js.DsiUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element TagOut {get {if (_TagOut == null) {_TagOut = (Element)Document.GetElementById(clientId + "_TagOut");}; return _TagOut;}} private Element _TagOut;
		public jQueryObject TagOutJ {get {if (_TagOutJ == null) {_TagOutJ = jQuery.Select("#" + clientId + "_TagOut");}; return _TagOutJ;}} private jQueryObject _TagOutJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement TagIn {get {if (_TagIn == null) {_TagIn = (InputElement)Document.GetElementById(clientId + "_TagIn");}; return _TagIn;}} private InputElement _TagIn;
		public jQueryObject TagInJ {get {if (_TagInJ == null) {_TagInJ = jQuery.Select("#" + clientId + "_TagIn");}; return _TagInJ;}} private jQueryObject _TagInJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
