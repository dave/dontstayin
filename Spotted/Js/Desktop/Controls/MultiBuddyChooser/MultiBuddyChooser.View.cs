//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.MultiBuddyChooser
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Js.ClientControls.MultiSelectorBehaviour uiBuddyMultiSelector {get {return (Js.ClientControls.MultiSelectorBehaviour) Script.Eval(clientId + "_uiBuddyMultiSelectorBehaviour");}}
		public CheckBoxElement uiJustBuddiesRadio {get {if (_uiJustBuddiesRadio == null) {_uiJustBuddiesRadio = (CheckBoxElement)Document.GetElementById(clientId + "_uiJustBuddiesRadio");}; return _uiJustBuddiesRadio;}} private CheckBoxElement _uiJustBuddiesRadio;
		public jQueryObject uiJustBuddiesRadioJ {get {if (_uiJustBuddiesRadioJ == null) {_uiJustBuddiesRadioJ = jQuery.Select("#" + clientId + "_uiJustBuddiesRadio");}; return _uiJustBuddiesRadioJ;}} private jQueryObject _uiJustBuddiesRadioJ;
		public CheckBoxElement uiAllMembersRadio {get {if (_uiAllMembersRadio == null) {_uiAllMembersRadio = (CheckBoxElement)Document.GetElementById(clientId + "_uiAllMembersRadio");}; return _uiAllMembersRadio;}} private CheckBoxElement _uiAllMembersRadio;
		public jQueryObject uiAllMembersRadioJ {get {if (_uiAllMembersRadioJ == null) {_uiAllMembersRadioJ = jQuery.Select("#" + clientId + "_uiAllMembersRadio");}; return _uiAllMembersRadioJ;}} private jQueryObject _uiAllMembersRadioJ;
		public CheckBoxElement uiShowBuddyList {get {if (_uiShowBuddyList == null) {_uiShowBuddyList = (CheckBoxElement)Document.GetElementById(clientId + "_uiShowBuddyList");}; return _uiShowBuddyList;}} private CheckBoxElement _uiShowBuddyList;
		public jQueryObject uiShowBuddyListJ {get {if (_uiShowBuddyListJ == null) {_uiShowBuddyListJ = jQuery.Select("#" + clientId + "_uiShowBuddyList");}; return _uiShowBuddyListJ;}} private jQueryObject _uiShowBuddyListJ;
		public DivElement uiBuddyListPanel {get {if (_uiBuddyListPanel == null) {_uiBuddyListPanel = (DivElement)Document.GetElementById(clientId + "_uiBuddyListPanel");}; return _uiBuddyListPanel;}} private DivElement _uiBuddyListPanel;
		public jQueryObject uiBuddyListPanelJ {get {if (_uiBuddyListPanelJ == null) {_uiBuddyListPanelJ = jQuery.Select("#" + clientId + "_uiBuddyListPanel");}; return _uiBuddyListPanelJ;}} private jQueryObject _uiBuddyListPanelJ;
		public SelectElement uiBuddyList {get {if (_uiBuddyList == null) {_uiBuddyList = (SelectElement)Document.GetElementById(clientId + "_uiBuddyList");}; return _uiBuddyList;}} private SelectElement _uiBuddyList;
		public jQueryObject uiBuddyListJ {get {if (_uiBuddyListJ == null) {_uiBuddyListJ = jQuery.Select("#" + clientId + "_uiBuddyList");}; return _uiBuddyListJ;}} private jQueryObject _uiBuddyListJ;
		public CheckBoxElement uiShowAddAll {get {if (_uiShowAddAll == null) {_uiShowAddAll = (CheckBoxElement)Document.GetElementById(clientId + "_uiShowAddAll");}; return _uiShowAddAll;}} private CheckBoxElement _uiShowAddAll;
		public jQueryObject uiShowAddAllJ {get {if (_uiShowAddAllJ == null) {_uiShowAddAllJ = jQuery.Select("#" + clientId + "_uiShowAddAll");}; return _uiShowAddAllJ;}} private jQueryObject _uiShowAddAllJ;
		public Element uiAddAll {get {if (_uiAddAll == null) {_uiAddAll = (Element)Document.GetElementById(clientId + "_uiAddAll");}; return _uiAddAll;}} private Element _uiAddAll;
		public jQueryObject uiAddAllJ {get {if (_uiAddAllJ == null) {_uiAddAllJ = jQuery.Select("#" + clientId + "_uiAddAll");}; return _uiAddAllJ;}} private jQueryObject _uiAddAllJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement uiAddAllButton {get {if (_uiAddAllButton == null) {_uiAddAllButton = (InputElement)Document.GetElementById(clientId + "_uiAddAllButton");}; return _uiAddAllButton;}} private InputElement _uiAddAllButton;
		public jQueryObject uiAddAllButtonJ {get {if (_uiAddAllButtonJ == null) {_uiAddAllButtonJ = jQuery.Select("#" + clientId + "_uiAddAllButton");}; return _uiAddAllButtonJ;}} private jQueryObject _uiAddAllButtonJ;
		public CheckBoxElement uiShowAddBy {get {if (_uiShowAddBy == null) {_uiShowAddBy = (CheckBoxElement)Document.GetElementById(clientId + "_uiShowAddBy");}; return _uiShowAddBy;}} private CheckBoxElement _uiShowAddBy;
		public jQueryObject uiShowAddByJ {get {if (_uiShowAddByJ == null) {_uiShowAddByJ = jQuery.Select("#" + clientId + "_uiShowAddBy");}; return _uiShowAddByJ;}} private jQueryObject _uiShowAddByJ;
		public Element uiAddBy {get {if (_uiAddBy == null) {_uiAddBy = (Element)Document.GetElementById(clientId + "_uiAddBy");}; return _uiAddBy;}} private Element _uiAddBy;
		public jQueryObject uiAddByJ {get {if (_uiAddByJ == null) {_uiAddByJ = jQuery.Select("#" + clientId + "_uiAddBy");}; return _uiAddByJ;}} private jQueryObject _uiAddByJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public SelectElement uiPlaces {get {if (_uiPlaces == null) {_uiPlaces = (SelectElement)Document.GetElementById(clientId + "_uiPlaces");}; return _uiPlaces;}} private SelectElement _uiPlaces;
		public jQueryObject uiPlacesJ {get {if (_uiPlacesJ == null) {_uiPlacesJ = jQuery.Select("#" + clientId + "_uiPlaces");}; return _uiPlacesJ;}} private jQueryObject _uiPlacesJ;
		public SelectElement uiMusicTypes {get {if (_uiMusicTypes == null) {_uiMusicTypes = (SelectElement)Document.GetElementById(clientId + "_uiMusicTypes");}; return _uiMusicTypes;}} private SelectElement _uiMusicTypes;
		public jQueryObject uiMusicTypesJ {get {if (_uiMusicTypesJ == null) {_uiMusicTypesJ = jQuery.Select("#" + clientId + "_uiMusicTypes");}; return _uiMusicTypesJ;}} private jQueryObject _uiMusicTypesJ;
		public InputElement uiAddByMusicAndPlace {get {if (_uiAddByMusicAndPlace == null) {_uiAddByMusicAndPlace = (InputElement)Document.GetElementById(clientId + "_uiAddByMusicAndPlace");}; return _uiAddByMusicAndPlace;}} private InputElement _uiAddByMusicAndPlace;
		public jQueryObject uiAddByMusicAndPlaceJ {get {if (_uiAddByMusicAndPlaceJ == null) {_uiAddByMusicAndPlaceJ = jQuery.Select("#" + clientId + "_uiAddByMusicAndPlace");}; return _uiAddByMusicAndPlaceJ;}} private jQueryObject _uiAddByMusicAndPlaceJ;
		public CheckBoxElement uiShowAllTownsAndMusic {get {if (_uiShowAllTownsAndMusic == null) {_uiShowAllTownsAndMusic = (CheckBoxElement)Document.GetElementById(clientId + "_uiShowAllTownsAndMusic");}; return _uiShowAllTownsAndMusic;}} private CheckBoxElement _uiShowAllTownsAndMusic;
		public jQueryObject uiShowAllTownsAndMusicJ {get {if (_uiShowAllTownsAndMusicJ == null) {_uiShowAllTownsAndMusicJ = jQuery.Select("#" + clientId + "_uiShowAllTownsAndMusic");}; return _uiShowAllTownsAndMusicJ;}} private jQueryObject _uiShowAllTownsAndMusicJ;
	}
}
