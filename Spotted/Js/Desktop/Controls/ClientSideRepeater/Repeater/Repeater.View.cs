//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.ClientSideRepeater.Repeater
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiHeaderTemplateHolder {get {if (_uiHeaderTemplateHolder == null) {_uiHeaderTemplateHolder = (Element)Document.GetElementById(clientId + "_uiHeaderTemplateHolder");}; return _uiHeaderTemplateHolder;}} private Element _uiHeaderTemplateHolder;
		public jQueryObject uiHeaderTemplateHolderJ {get {if (_uiHeaderTemplateHolderJ == null) {_uiHeaderTemplateHolderJ = jQuery.Select("#" + clientId + "_uiHeaderTemplateHolder");}; return _uiHeaderTemplateHolderJ;}} private jQueryObject _uiHeaderTemplateHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiBetweenTemplateHolder {get {if (_uiBetweenTemplateHolder == null) {_uiBetweenTemplateHolder = (Element)Document.GetElementById(clientId + "_uiBetweenTemplateHolder");}; return _uiBetweenTemplateHolder;}} private Element _uiBetweenTemplateHolder;
		public jQueryObject uiBetweenTemplateHolderJ {get {if (_uiBetweenTemplateHolderJ == null) {_uiBetweenTemplateHolderJ = jQuery.Select("#" + clientId + "_uiBetweenTemplateHolder");}; return _uiBetweenTemplateHolderJ;}} private jQueryObject _uiBetweenTemplateHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiFooterTemplateHolder {get {if (_uiFooterTemplateHolder == null) {_uiFooterTemplateHolder = (Element)Document.GetElementById(clientId + "_uiFooterTemplateHolder");}; return _uiFooterTemplateHolder;}} private Element _uiFooterTemplateHolder;
		public jQueryObject uiFooterTemplateHolderJ {get {if (_uiFooterTemplateHolderJ == null) {_uiFooterTemplateHolderJ = jQuery.Select("#" + clientId + "_uiFooterTemplateHolder");}; return _uiFooterTemplateHolderJ;}} private jQueryObject _uiFooterTemplateHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Js.Controls.ClientSideRepeater.Template.Controller uiItemTemplate {get {return (Js.Controls.ClientSideRepeater.Template.Controller) Script.Eval(clientId + "_uiItemTemplateController");}}
		public DivElement uiContent {get {if (_uiContent == null) {_uiContent = (DivElement)Document.GetElementById(clientId + "_uiContent");}; return _uiContent;}} private DivElement _uiContent;
		public jQueryObject uiContentJ {get {if (_uiContentJ == null) {_uiContentJ = jQuery.Select("#" + clientId + "_uiContent");}; return _uiContentJ;}} private jQueryObject _uiContentJ;
	}
}
