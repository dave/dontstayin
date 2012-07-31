//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.PhotoBrowser
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiPhotoRepeaterContainer {get {if (_uiPhotoRepeaterContainer == null) {_uiPhotoRepeaterContainer = (Element)Document.GetElementById(clientId + "_uiPhotoRepeaterContainer");}; return _uiPhotoRepeaterContainer;}} private Element _uiPhotoRepeaterContainer;
		public jQueryObject uiPhotoRepeaterContainerJ {get {if (_uiPhotoRepeaterContainerJ == null) {_uiPhotoRepeaterContainerJ = jQuery.Select("#" + clientId + "_uiPhotoRepeaterContainer");}; return _uiPhotoRepeaterContainerJ;}} private jQueryObject _uiPhotoRepeaterContainerJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Js.Controls.PaginationControl2.Controller uiPaginationControl {get {return (Js.Controls.PaginationControl2.Controller) Script.Eval(clientId + "_uiPaginationControlController");}}
		public Element uiPhotoRepeater {get {if (_uiPhotoRepeater == null) {_uiPhotoRepeater = (Element)Document.GetElementById(clientId + "_uiPhotoRepeater");}; return _uiPhotoRepeater;}} private Element _uiPhotoRepeater;
		public jQueryObject uiPhotoRepeaterJ {get {if (_uiPhotoRepeaterJ == null) {_uiPhotoRepeaterJ = jQuery.Select("#" + clientId + "_uiPhotoRepeater");}; return _uiPhotoRepeaterJ;}} private jQueryObject _uiPhotoRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element uiBlowUpIconSpan {get {if (_uiBlowUpIconSpan == null) {_uiBlowUpIconSpan = (Element)Document.GetElementById(clientId + "_uiBlowUpIconSpan");}; return _uiBlowUpIconSpan;}} private Element _uiBlowUpIconSpan;
		public jQueryObject uiBlowUpIconSpanJ {get {if (_uiBlowUpIconSpanJ == null) {_uiBlowUpIconSpanJ = jQuery.Select("#" + clientId + "_uiBlowUpIconSpan");}; return _uiBlowUpIconSpanJ;}} private jQueryObject _uiBlowUpIconSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement uiIconsPerPage {get {if (_uiIconsPerPage == null) {_uiIconsPerPage = (InputElement)Document.GetElementById(clientId + "_uiIconsPerPage");}; return _uiIconsPerPage;}} private InputElement _uiIconsPerPage;
		public jQueryObject uiIconsPerPageJ {get {if (_uiIconsPerPageJ == null) {_uiIconsPerPageJ = jQuery.Select("#" + clientId + "_uiIconsPerPage");}; return _uiIconsPerPageJ;}} private jQueryObject _uiIconsPerPageJ;
		public InputElement uiIconsPerRow {get {if (_uiIconsPerRow == null) {_uiIconsPerRow = (InputElement)Document.GetElementById(clientId + "_uiIconsPerRow");}; return _uiIconsPerRow;}} private InputElement _uiIconsPerRow;
		public jQueryObject uiIconsPerRowJ {get {if (_uiIconsPerRowJ == null) {_uiIconsPerRowJ = jQuery.Select("#" + clientId + "_uiIconsPerRow");}; return _uiIconsPerRowJ;}} private jQueryObject _uiIconsPerRowJ;
		public InputElement uiIconSize {get {if (_uiIconSize == null) {_uiIconSize = (InputElement)Document.GetElementById(clientId + "_uiIconSize");}; return _uiIconSize;}} private InputElement _uiIconSize;
		public jQueryObject uiIconSizeJ {get {if (_uiIconSizeJ == null) {_uiIconSizeJ = jQuery.Select("#" + clientId + "_uiIconSize");}; return _uiIconSizeJ;}} private jQueryObject _uiIconSizeJ;
		public InputElement uiTableCellsPrefix {get {if (_uiTableCellsPrefix == null) {_uiTableCellsPrefix = (InputElement)Document.GetElementById(clientId + "_uiTableCellsPrefix");}; return _uiTableCellsPrefix;}} private InputElement _uiTableCellsPrefix;
		public jQueryObject uiTableCellsPrefixJ {get {if (_uiTableCellsPrefixJ == null) {_uiTableCellsPrefixJ = jQuery.Select("#" + clientId + "_uiTableCellsPrefix");}; return _uiTableCellsPrefixJ;}} private jQueryObject _uiTableCellsPrefixJ;
	}
}
