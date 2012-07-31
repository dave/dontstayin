//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.Tickets
{
	public partial class View
		 : Js.BlankUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element Tab {get {if (_Tab == null) {_Tab = (Element)Document.GetElementById(clientId + "_Tab");}; return _Tab;}} private Element _Tab;
		public jQueryObject TabJ {get {if (_TabJ == null) {_TabJ = jQuery.Select("#" + clientId + "_Tab");}; return _TabJ;}} private jQueryObject _TabJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
