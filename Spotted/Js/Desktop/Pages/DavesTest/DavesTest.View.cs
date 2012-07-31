//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.DavesTest
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
		public Element MyAspButton {get {if (_MyAspButton == null) {_MyAspButton = (Element)Document.GetElementById(clientId + "_MyAspButton");}; return _MyAspButton;}} private Element _MyAspButton;
		public jQueryObject MyAspButtonJ {get {if (_MyAspButtonJ == null) {_MyAspButtonJ = jQuery.Select("#" + clientId + "_MyAspButton");}; return _MyAspButtonJ;}} private jQueryObject _MyAspButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public InputElement MyButton {get {if (_MyButton == null) {_MyButton = (InputElement)Document.GetElementById(clientId + "_MyButton");}; return _MyButton;}} private InputElement _MyButton;
		public jQueryObject MyButtonJ {get {if (_MyButtonJ == null) {_MyButtonJ = jQuery.Select("#" + clientId + "_MyButton");}; return _MyButtonJ;}} private jQueryObject _MyButtonJ;
		public Element ServerP {get {if (_ServerP == null) {_ServerP = (Element)Document.GetElementById(clientId + "_ServerP");}; return _ServerP;}} private Element _ServerP;
		public jQueryObject ServerPJ {get {if (_ServerPJ == null) {_ServerPJ = jQuery.Select("#" + clientId + "_ServerP");}; return _ServerPJ;}} private jQueryObject _ServerPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ClientP {get {if (_ClientP == null) {_ClientP = (Element)Document.GetElementById(clientId + "_ClientP");}; return _ClientP;}} private Element _ClientP;
		public jQueryObject ClientPJ {get {if (_ClientPJ == null) {_ClientPJ = jQuery.Select("#" + clientId + "_ClientP");}; return _ClientPJ;}} private jQueryObject _ClientPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
