//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.ModerateUsrTags
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
		public Element H16 {get {if (_H16 == null) {_H16 = (Element)Document.GetElementById(clientId + "_H16");}; return _H16;}} private Element _H16;
		public jQueryObject H16J {get {if (_H16J == null) {_H16J = jQuery.Select("#" + clientId + "_H16");}; return _H16J;}} private jQueryObject _H16J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element uiTypeOfAction {get {if (_uiTypeOfAction == null) {_uiTypeOfAction = (Element)Document.GetElementById(clientId + "_uiTypeOfAction");}; return _uiTypeOfAction;}} private Element _uiTypeOfAction;
		public jQueryObject uiTypeOfActionJ {get {if (_uiTypeOfActionJ == null) {_uiTypeOfActionJ = jQuery.Select("#" + clientId + "_uiTypeOfAction");}; return _uiTypeOfActionJ;}} private jQueryObject _uiTypeOfActionJ;//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
		public Element uiNoTags {get {if (_uiNoTags == null) {_uiNoTags = (Element)Document.GetElementById(clientId + "_uiNoTags");}; return _uiNoTags;}} private Element _uiNoTags;
		public jQueryObject uiNoTagsJ {get {if (_uiNoTagsJ == null) {_uiNoTagsJ = jQuery.Select("#" + clientId + "_uiNoTags");}; return _uiNoTagsJ;}} private jQueryObject _uiNoTagsJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiInfo {get {if (_uiInfo == null) {_uiInfo = (Element)Document.GetElementById(clientId + "_uiInfo");}; return _uiInfo;}} private Element _uiInfo;
		public jQueryObject uiInfoJ {get {if (_uiInfoJ == null) {_uiInfoJ = jQuery.Select("#" + clientId + "_uiInfo");}; return _uiInfoJ;}} private jQueryObject _uiInfoJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
