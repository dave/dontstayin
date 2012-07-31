//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Venues.Delete
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
		public DivElement PanelDelete {get {if (_PanelDelete == null) {_PanelDelete = (DivElement)Document.GetElementById(clientId + "_PanelDelete");}; return _PanelDelete;}} private DivElement _PanelDelete;
		public jQueryObject PanelDeleteJ {get {if (_PanelDeleteJ == null) {_PanelDeleteJ = jQuery.Select("#" + clientId + "_PanelDelete");}; return _PanelDeleteJ;}} private jQueryObject _PanelDeleteJ;
		public Element H10 {get {if (_H10 == null) {_H10 = (Element)Document.GetElementById(clientId + "_H10");}; return _H10;}} private Element _H10;
		public jQueryObject H10J {get {if (_H10J == null) {_H10J = jQuery.Select("#" + clientId + "_H10");}; return _H10J;}} private jQueryObject _H10J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element VenueDescriptionP {get {if (_VenueDescriptionP == null) {_VenueDescriptionP = (Element)Document.GetElementById(clientId + "_VenueDescriptionP");}; return _VenueDescriptionP;}} private Element _VenueDescriptionP;
		public jQueryObject VenueDescriptionPJ {get {if (_VenueDescriptionPJ == null) {_VenueDescriptionPJ = jQuery.Select("#" + clientId + "_VenueDescriptionP");}; return _VenueDescriptionPJ;}} private jQueryObject _VenueDescriptionPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement Password {get {if (_Password == null) {_Password = (InputElement)Document.GetElementById(clientId + "_Password");}; return _Password;}} private InputElement _Password;
		public jQueryObject PasswordJ {get {if (_PasswordJ == null) {_PasswordJ = jQuery.Select("#" + clientId + "_Password");}; return _PasswordJ;}} private jQueryObject _PasswordJ;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement PanelError {get {if (_PanelError == null) {_PanelError = (DivElement)Document.GetElementById(clientId + "_PanelError");}; return _PanelError;}} private DivElement _PanelError;
		public jQueryObject PanelErrorJ {get {if (_PanelErrorJ == null) {_PanelErrorJ = jQuery.Select("#" + clientId + "_PanelError");}; return _PanelErrorJ;}} private jQueryObject _PanelErrorJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element DeleteFailedP {get {if (_DeleteFailedP == null) {_DeleteFailedP = (Element)Document.GetElementById(clientId + "_DeleteFailedP");}; return _DeleteFailedP;}} private Element _DeleteFailedP;
		public jQueryObject DeleteFailedPJ {get {if (_DeleteFailedPJ == null) {_DeleteFailedPJ = jQuery.Select("#" + clientId + "_DeleteFailedP");}; return _DeleteFailedPJ;}} private jQueryObject _DeleteFailedPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement ErrorBackAnchor {get {if (_ErrorBackAnchor == null) {_ErrorBackAnchor = (AnchorElement)Document.GetElementById(clientId + "_ErrorBackAnchor");}; return _ErrorBackAnchor;}} private AnchorElement _ErrorBackAnchor;
		public jQueryObject ErrorBackAnchorJ {get {if (_ErrorBackAnchorJ == null) {_ErrorBackAnchorJ = jQuery.Select("#" + clientId + "_ErrorBackAnchor");}; return _ErrorBackAnchorJ;}} private jQueryObject _ErrorBackAnchorJ;
		public DivElement PanelDone {get {if (_PanelDone == null) {_PanelDone = (DivElement)Document.GetElementById(clientId + "_PanelDone");}; return _PanelDone;}} private DivElement _PanelDone;
		public jQueryObject PanelDoneJ {get {if (_PanelDoneJ == null) {_PanelDoneJ = jQuery.Select("#" + clientId + "_PanelDone");}; return _PanelDoneJ;}} private jQueryObject _PanelDoneJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
