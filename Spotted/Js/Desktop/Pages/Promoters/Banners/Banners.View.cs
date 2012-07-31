//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Banners
{
	public partial class View
		 : Js.Pages.Promoters.PromoterUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement PanelBannerList {get {if (_PanelBannerList == null) {_PanelBannerList = (DivElement)Document.GetElementById(clientId + "_PanelBannerList");}; return _PanelBannerList;}} private DivElement _PanelBannerList;
		public jQueryObject PanelBannerListJ {get {if (_PanelBannerListJ == null) {_PanelBannerListJ = jQuery.Select("#" + clientId + "_PanelBannerList");}; return _PanelBannerListJ;}} private jQueryObject _PanelBannerListJ;
		public Element BannerListHeader {get {if (_BannerListHeader == null) {_BannerListHeader = (Element)Document.GetElementById(clientId + "_BannerListHeader");}; return _BannerListHeader;}} private Element _BannerListHeader;
		public jQueryObject BannerListHeaderJ {get {if (_BannerListHeaderJ == null) {_BannerListHeaderJ = jQuery.Select("#" + clientId + "_BannerListHeader");}; return _BannerListHeaderJ;}} private jQueryObject _BannerListHeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement BannerListAddLink {get {if (_BannerListAddLink == null) {_BannerListAddLink = (AnchorElement)Document.GetElementById(clientId + "_BannerListAddLink");}; return _BannerListAddLink;}} private AnchorElement _BannerListAddLink;
		public jQueryObject BannerListAddLinkJ {get {if (_BannerListAddLinkJ == null) {_BannerListAddLinkJ = jQuery.Select("#" + clientId + "_BannerListAddLink");}; return _BannerListAddLinkJ;}} private jQueryObject _BannerListAddLinkJ;
		public Element BannerListDataGrid {get {if (_BannerListDataGrid == null) {_BannerListDataGrid = (Element)Document.GetElementById(clientId + "_BannerListDataGrid");}; return _BannerListDataGrid;}} private Element _BannerListDataGrid;
		public jQueryObject BannerListDataGridJ {get {if (_BannerListDataGridJ == null) {_BannerListDataGridJ = jQuery.Select("#" + clientId + "_BannerListDataGrid");}; return _BannerListDataGridJ;}} private jQueryObject _BannerListDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element Promoterintro1 {get {if (_Promoterintro1 == null) {_Promoterintro1 = (Element)Document.GetElementById(clientId + "_Promoterintro1");}; return _Promoterintro1;}} private Element _Promoterintro1;
		public jQueryObject Promoterintro1J {get {if (_Promoterintro1J == null) {_Promoterintro1J = jQuery.Select("#" + clientId + "_Promoterintro1");}; return _Promoterintro1J;}} private jQueryObject _Promoterintro1J;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public SelectElement FolderDropDown {get {if (_FolderDropDown == null) {_FolderDropDown = (SelectElement)Document.GetElementById(clientId + "_FolderDropDown");}; return _FolderDropDown;}} private SelectElement _FolderDropDown;
		public jQueryObject FolderDropDownJ {get {if (_FolderDropDownJ == null) {_FolderDropDownJ = jQuery.Select("#" + clientId + "_FolderDropDown");}; return _FolderDropDownJ;}} private jQueryObject _FolderDropDownJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
