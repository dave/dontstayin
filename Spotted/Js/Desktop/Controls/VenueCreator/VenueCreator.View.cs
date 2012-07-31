//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.VenueCreator
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiContainer {get {if (_uiContainer == null) {_uiContainer = (Element)Document.GetElementById(clientId + "_uiContainer");}; return _uiContainer;}} private Element _uiContainer;
		public jQueryObject uiContainerJ {get {if (_uiContainerJ == null) {_uiContainerJ = jQuery.Select("#" + clientId + "_uiContainer");}; return _uiContainerJ;}} private jQueryObject _uiContainerJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPlace {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPlaceBehaviour");}}
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiNameSuggest {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiNameSuggestBehaviour");}}
		public InputElement uiPostCode {get {if (_uiPostCode == null) {_uiPostCode = (InputElement)Document.GetElementById(clientId + "_uiPostCode");}; return _uiPostCode;}} private InputElement _uiPostCode;
		public jQueryObject uiPostCodeJ {get {if (_uiPostCodeJ == null) {_uiPostCodeJ = jQuery.Select("#" + clientId + "_uiPostCode");}; return _uiPostCodeJ;}} private jQueryObject _uiPostCodeJ;
	}
}
