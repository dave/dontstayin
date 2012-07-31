//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.FindYourPhoto
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
		public ImageElement TopIcon {get {if (_TopIcon == null) {_TopIcon = (ImageElement)Document.GetElementById(clientId + "_TopIcon");}; return _TopIcon;}} private ImageElement _TopIcon;
		public jQueryObject TopIconJ {get {if (_TopIconJ == null) {_TopIconJ = jQuery.Select("#" + clientId + "_TopIcon");}; return _TopIconJ;}} private jQueryObject _TopIconJ;
		public Js.Controls.Picker.Controller Picker {get {return (Js.Controls.Picker.Controller) Script.Eval(clientId + "_PickerController");}}
		public Element ResultOuter {get {if (_ResultOuter == null) {_ResultOuter = (Element)Document.GetElementById(clientId + "_ResultOuter");}; return _ResultOuter;}} private Element _ResultOuter;
		public jQueryObject ResultOuterJ {get {if (_ResultOuterJ == null) {_ResultOuterJ = jQuery.Select("#" + clientId + "_ResultOuter");}; return _ResultOuterJ;}} private jQueryObject _ResultOuterJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element LoadingOverlay {get {if (_LoadingOverlay == null) {_LoadingOverlay = (Element)Document.GetElementById(clientId + "_LoadingOverlay");}; return _LoadingOverlay;}} private Element _LoadingOverlay;
		public jQueryObject LoadingOverlayJ {get {if (_LoadingOverlayJ == null) {_LoadingOverlayJ = jQuery.Select("#" + clientId + "_LoadingOverlay");}; return _LoadingOverlayJ;}} private jQueryObject _LoadingOverlayJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element Result {get {if (_Result == null) {_Result = (Element)Document.GetElementById(clientId + "_Result");}; return _Result;}} private Element _Result;
		public jQueryObject ResultJ {get {if (_ResultJ == null) {_ResultJ = jQuery.Select("#" + clientId + "_Result");}; return _ResultJ;}} private jQueryObject _ResultJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
