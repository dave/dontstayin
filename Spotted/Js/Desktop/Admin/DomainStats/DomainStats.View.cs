//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.DomainStats
{
	public partial class View
		 : Js.AdminUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPromoterHtmlAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPromoterHtmlAutoCompleteBehaviour");}}
		public SelectElement uiDomainsList {get {if (_uiDomainsList == null) {_uiDomainsList = (SelectElement)Document.GetElementById(clientId + "_uiDomainsList");}; return _uiDomainsList;}} private SelectElement _uiDomainsList;
		public jQueryObject uiDomainsListJ {get {if (_uiDomainsListJ == null) {_uiDomainsListJ = jQuery.Select("#" + clientId + "_uiDomainsList");}; return _uiDomainsListJ;}} private jQueryObject _uiDomainsListJ;
		public InputElement uiSelectDomainButton {get {if (_uiSelectDomainButton == null) {_uiSelectDomainButton = (InputElement)Document.GetElementById(clientId + "_uiSelectDomainButton");}; return _uiSelectDomainButton;}} private InputElement _uiSelectDomainButton;
		public jQueryObject uiSelectDomainButtonJ {get {if (_uiSelectDomainButtonJ == null) {_uiSelectDomainButtonJ = jQuery.Select("#" + clientId + "_uiSelectDomainButton");}; return _uiSelectDomainButtonJ;}} private jQueryObject _uiSelectDomainButtonJ;
		public Element uiGridView {get {if (_uiGridView == null) {_uiGridView = (Element)Document.GetElementById(clientId + "_uiGridView");}; return _uiGridView;}} private Element _uiGridView;
		public jQueryObject uiGridViewJ {get {if (_uiGridViewJ == null) {_uiGridViewJ = jQuery.Select("#" + clientId + "_uiGridView");}; return _uiGridViewJ;}} private jQueryObject _uiGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
