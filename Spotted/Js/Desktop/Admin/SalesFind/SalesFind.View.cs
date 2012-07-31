//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.SalesFind
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
		public Element FindByUser {get {if (_FindByUser == null) {_FindByUser = (Element)Document.GetElementById(clientId + "_FindByUser");}; return _FindByUser;}} private Element _FindByUser;
		public jQueryObject FindByUserJ {get {if (_FindByUserJ == null) {_FindByUserJ = jQuery.Select("#" + clientId + "_FindByUser");}; return _FindByUserJ;}} private jQueryObject _FindByUserJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement uiFindByUserPanel {get {if (_uiFindByUserPanel == null) {_uiFindByUserPanel = (DivElement)Document.GetElementById(clientId + "_uiFindByUserPanel");}; return _uiFindByUserPanel;}} private DivElement _uiFindByUserPanel;
		public jQueryObject uiFindByUserPanelJ {get {if (_uiFindByUserPanelJ == null) {_uiFindByUserPanelJ = jQuery.Select("#" + clientId + "_uiFindByUserPanel");}; return _uiFindByUserPanelJ;}} private jQueryObject _uiFindByUserPanelJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiUserAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiUserAutoCompleteBehaviour");}}
		public Element uiLookupUserButton {get {if (_uiLookupUserButton == null) {_uiLookupUserButton = (Element)Document.GetElementById(clientId + "_uiLookupUserButton");}; return _uiLookupUserButton;}} private Element _uiLookupUserButton;
		public jQueryObject uiLookupUserButtonJ {get {if (_uiLookupUserButtonJ == null) {_uiLookupUserButtonJ = jQuery.Select("#" + clientId + "_uiLookupUserButton");}; return _uiLookupUserButtonJ;}} private jQueryObject _uiLookupUserButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiUserIsNotPromoterPanel {get {if (_uiUserIsNotPromoterPanel == null) {_uiUserIsNotPromoterPanel = (Element)Document.GetElementById(clientId + "_uiUserIsNotPromoterPanel");}; return _uiUserIsNotPromoterPanel;}} private Element _uiUserIsNotPromoterPanel;
		public jQueryObject uiUserIsNotPromoterPanelJ {get {if (_uiUserIsNotPromoterPanelJ == null) {_uiUserIsNotPromoterPanelJ = jQuery.Select("#" + clientId + "_uiUserIsNotPromoterPanel");}; return _uiUserIsNotPromoterPanelJ;}} private jQueryObject _uiUserIsNotPromoterPanelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FindByBrand {get {if (_FindByBrand == null) {_FindByBrand = (Element)Document.GetElementById(clientId + "_FindByBrand");}; return _FindByBrand;}} private Element _FindByBrand;
		public jQueryObject FindByBrandJ {get {if (_FindByBrandJ == null) {_FindByBrandJ = jQuery.Select("#" + clientId + "_FindByBrand");}; return _FindByBrandJ;}} private jQueryObject _FindByBrandJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement uiFindByBrandPanel {get {if (_uiFindByBrandPanel == null) {_uiFindByBrandPanel = (DivElement)Document.GetElementById(clientId + "_uiFindByBrandPanel");}; return _uiFindByBrandPanel;}} private DivElement _uiFindByBrandPanel;
		public jQueryObject uiFindByBrandPanelJ {get {if (_uiFindByBrandPanelJ == null) {_uiFindByBrandPanelJ = jQuery.Select("#" + clientId + "_uiFindByBrandPanel");}; return _uiFindByBrandPanelJ;}} private jQueryObject _uiFindByBrandPanelJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiBrandsAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiBrandsAutoCompleteBehaviour");}}
		public Element uiGoToPromoterPageByBrandButton {get {if (_uiGoToPromoterPageByBrandButton == null) {_uiGoToPromoterPageByBrandButton = (Element)Document.GetElementById(clientId + "_uiGoToPromoterPageByBrandButton");}; return _uiGoToPromoterPageByBrandButton;}} private Element _uiGoToPromoterPageByBrandButton;
		public jQueryObject uiGoToPromoterPageByBrandButtonJ {get {if (_uiGoToPromoterPageByBrandButtonJ == null) {_uiGoToPromoterPageByBrandButtonJ = jQuery.Select("#" + clientId + "_uiGoToPromoterPageByBrandButton");}; return _uiGoToPromoterPageByBrandButtonJ;}} private jQueryObject _uiGoToPromoterPageByBrandButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element FindByPromoter {get {if (_FindByPromoter == null) {_FindByPromoter = (Element)Document.GetElementById(clientId + "_FindByPromoter");}; return _FindByPromoter;}} private Element _FindByPromoter;
		public jQueryObject FindByPromoterJ {get {if (_FindByPromoterJ == null) {_FindByPromoterJ = jQuery.Select("#" + clientId + "_FindByPromoter");}; return _FindByPromoterJ;}} private jQueryObject _FindByPromoterJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement uiFindByPromoterPanel {get {if (_uiFindByPromoterPanel == null) {_uiFindByPromoterPanel = (DivElement)Document.GetElementById(clientId + "_uiFindByPromoterPanel");}; return _uiFindByPromoterPanel;}} private DivElement _uiFindByPromoterPanel;
		public jQueryObject uiFindByPromoterPanelJ {get {if (_uiFindByPromoterPanelJ == null) {_uiFindByPromoterPanelJ = jQuery.Select("#" + clientId + "_uiFindByPromoterPanel");}; return _uiFindByPromoterPanelJ;}} private jQueryObject _uiFindByPromoterPanelJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPromoterAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPromoterAutoCompleteBehaviour");}}
		public Element uiGoToPromoterPageByPromoterButton {get {if (_uiGoToPromoterPageByPromoterButton == null) {_uiGoToPromoterPageByPromoterButton = (Element)Document.GetElementById(clientId + "_uiGoToPromoterPageByPromoterButton");}; return _uiGoToPromoterPageByPromoterButton;}} private Element _uiGoToPromoterPageByPromoterButton;
		public jQueryObject uiGoToPromoterPageByPromoterButtonJ {get {if (_uiGoToPromoterPageByPromoterButtonJ == null) {_uiGoToPromoterPageByPromoterButtonJ = jQuery.Select("#" + clientId + "_uiGoToPromoterPageByPromoterButton");}; return _uiGoToPromoterPageByPromoterButtonJ;}} private jQueryObject _uiGoToPromoterPageByPromoterButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiBrandPromoterIsNullPanel {get {if (_uiBrandPromoterIsNullPanel == null) {_uiBrandPromoterIsNullPanel = (Element)Document.GetElementById(clientId + "_uiBrandPromoterIsNullPanel");}; return _uiBrandPromoterIsNullPanel;}} private Element _uiBrandPromoterIsNullPanel;
		public jQueryObject uiBrandPromoterIsNullPanelJ {get {if (_uiBrandPromoterIsNullPanelJ == null) {_uiBrandPromoterIsNullPanelJ = jQuery.Select("#" + clientId + "_uiBrandPromoterIsNullPanel");}; return _uiBrandPromoterIsNullPanelJ;}} private jQueryObject _uiBrandPromoterIsNullPanelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
