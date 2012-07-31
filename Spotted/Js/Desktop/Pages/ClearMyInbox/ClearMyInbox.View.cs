//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.ClearMyInbox
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
		public Element ConfirmDiv {get {if (_ConfirmDiv == null) {_ConfirmDiv = (Element)Document.GetElementById(clientId + "_ConfirmDiv");}; return _ConfirmDiv;}} private Element _ConfirmDiv;
		public jQueryObject ConfirmDivJ {get {if (_ConfirmDivJ == null) {_ConfirmDivJ = jQuery.Select("#" + clientId + "_ConfirmDiv");}; return _ConfirmDivJ;}} private jQueryObject _ConfirmDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement Password {get {if (_Password == null) {_Password = (InputElement)Document.GetElementById(clientId + "_Password");}; return _Password;}} private InputElement _Password;
		public jQueryObject PasswordJ {get {if (_PasswordJ == null) {_PasswordJ = jQuery.Select("#" + clientId + "_Password");}; return _PasswordJ;}} private jQueryObject _PasswordJ;
		public Element Error {get {if (_Error == null) {_Error = (Element)Document.GetElementById(clientId + "_Error");}; return _Error;}} private Element _Error;
		public jQueryObject ErrorJ {get {if (_ErrorJ == null) {_ErrorJ = jQuery.Select("#" + clientId + "_Error");}; return _ErrorJ;}} private jQueryObject _ErrorJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element Done {get {if (_Done == null) {_Done = (Element)Document.GetElementById(clientId + "_Done");}; return _Done;}} private Element _Done;
		public jQueryObject DoneJ {get {if (_DoneJ == null) {_DoneJ = jQuery.Select("#" + clientId + "_Done");}; return _DoneJ;}} private jQueryObject _DoneJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
