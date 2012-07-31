//mappings.Add("Spotted.Controls.UsrBrowser", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.Buddies
{
	public partial class View
		 : Js.Pages.Usrs.UsrUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element usrBrowser {get {if (_usrBrowser == null) {_usrBrowser = (Element)Document.GetElementById(clientId + "_usrBrowser");}; return _usrBrowser;}} private Element _usrBrowser;
		public jQueryObject usrBrowserJ {get {if (_usrBrowserJ == null) {_usrBrowserJ = jQuery.Select("#" + clientId + "_usrBrowser");}; return _usrBrowserJ;}} private jQueryObject _usrBrowserJ;//mappings.Add("Spotted.Controls.UsrBrowser", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
