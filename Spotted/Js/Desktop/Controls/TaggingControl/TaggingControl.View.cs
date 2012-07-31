//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.TaggingControl
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public DivElement uiTagPanel {get {if (_uiTagPanel == null) {_uiTagPanel = (DivElement)Document.GetElementById(clientId + "_uiTagPanel");}; return _uiTagPanel;}} private DivElement _uiTagPanel;
		public jQueryObject uiTagPanelJ {get {if (_uiTagPanelJ == null) {_uiTagPanelJ = jQuery.Select("#" + clientId + "_uiTagPanel");}; return _uiTagPanelJ;}} private jQueryObject _uiTagPanelJ;
		public Element uiTagsDiv {get {if (_uiTagsDiv == null) {_uiTagsDiv = (Element)Document.GetElementById(clientId + "_uiTagsDiv");}; return _uiTagsDiv;}} private Element _uiTagsDiv;
		public jQueryObject uiTagsDivJ {get {if (_uiTagsDivJ == null) {_uiTagsDivJ = jQuery.Select("#" + clientId + "_uiTagsDiv");}; return _uiTagsDivJ;}} private jQueryObject _uiTagsDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiTagsDivServerSide {get {if (_uiTagsDivServerSide == null) {_uiTagsDivServerSide = (Element)Document.GetElementById(clientId + "_uiTagsDivServerSide");}; return _uiTagsDivServerSide;}} private Element _uiTagsDivServerSide;
		public jQueryObject uiTagsDivServerSideJ {get {if (_uiTagsDivServerSideJ == null) {_uiTagsDivServerSideJ = jQuery.Select("#" + clientId + "_uiTagsDivServerSide");}; return _uiTagsDivServerSideJ;}} private jQueryObject _uiTagsDivServerSideJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiTagRepeater {get {if (_uiTagRepeater == null) {_uiTagRepeater = (Element)Document.GetElementById(clientId + "_uiTagRepeater");}; return _uiTagRepeater;}} private Element _uiTagRepeater;
		public jQueryObject uiTagRepeaterJ {get {if (_uiTagRepeaterJ == null) {_uiTagRepeaterJ = jQuery.Select("#" + clientId + "_uiTagRepeater");}; return _uiTagRepeaterJ;}} private jQueryObject _uiTagRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public DivElement uiAddPanel {get {if (_uiAddPanel == null) {_uiAddPanel = (DivElement)Document.GetElementById(clientId + "_uiAddPanel");}; return _uiAddPanel;}} private DivElement _uiAddPanel;
		public jQueryObject uiAddPanelJ {get {if (_uiAddPanelJ == null) {_uiAddPanelJ = jQuery.Select("#" + clientId + "_uiAddPanel");}; return _uiAddPanelJ;}} private jQueryObject _uiAddPanelJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiTagAutoSuggest {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiTagAutoSuggestBehaviour");}}
		public ImageElement uiAddTagButton {get {if (_uiAddTagButton == null) {_uiAddTagButton = (ImageElement)Document.GetElementById(clientId + "_uiAddTagButton");}; return _uiAddTagButton;}} private ImageElement _uiAddTagButton;
		public jQueryObject uiAddTagButtonJ {get {if (_uiAddTagButtonJ == null) {_uiAddTagButtonJ = jQuery.Select("#" + clientId + "_uiAddTagButton");}; return _uiAddTagButtonJ;}} private jQueryObject _uiAddTagButtonJ;
	}
}
