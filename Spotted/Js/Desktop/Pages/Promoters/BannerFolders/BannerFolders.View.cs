//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.PaginationControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.BannerFolders
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
		public Element Promoterintro1 {get {if (_Promoterintro1 == null) {_Promoterintro1 = (Element)Document.GetElementById(clientId + "_Promoterintro1");}; return _Promoterintro1;}} private Element _Promoterintro1;
		public jQueryObject Promoterintro1J {get {if (_Promoterintro1J == null) {_Promoterintro1J = jQuery.Select("#" + clientId + "_Promoterintro1");}; return _Promoterintro1J;}} private jQueryObject _Promoterintro1J;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public Element BannerListHeader {get {if (_BannerListHeader == null) {_BannerListHeader = (Element)Document.GetElementById(clientId + "_BannerListHeader");}; return _BannerListHeader;}} private Element _BannerListHeader;
		public jQueryObject BannerListHeaderJ {get {if (_BannerListHeaderJ == null) {_BannerListHeaderJ = jQuery.Select("#" + clientId + "_BannerListHeader");}; return _BannerListHeaderJ;}} private jQueryObject _BannerListHeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement pnlContent {get {if (_pnlContent == null) {_pnlContent = (DivElement)Document.GetElementById(clientId + "_pnlContent");}; return _pnlContent;}} private DivElement _pnlContent;
		public jQueryObject pnlContentJ {get {if (_pnlContentJ == null) {_pnlContentJ = jQuery.Select("#" + clientId + "_pnlContent");}; return _pnlContentJ;}} private jQueryObject _pnlContentJ;
		public Element uiBannerFolderRepeater {get {if (_uiBannerFolderRepeater == null) {_uiBannerFolderRepeater = (Element)Document.GetElementById(clientId + "_uiBannerFolderRepeater");}; return _uiBannerFolderRepeater;}} private Element _uiBannerFolderRepeater;
		public jQueryObject uiBannerFolderRepeaterJ {get {if (_uiBannerFolderRepeaterJ == null) {_uiBannerFolderRepeaterJ = jQuery.Select("#" + clientId + "_uiBannerFolderRepeater");}; return _uiBannerFolderRepeaterJ;}} private jQueryObject _uiBannerFolderRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element uiPaginationControl {get {if (_uiPaginationControl == null) {_uiPaginationControl = (Element)Document.GetElementById(clientId + "_uiPaginationControl");}; return _uiPaginationControl;}} private Element _uiPaginationControl;
		public jQueryObject uiPaginationControlJ {get {if (_uiPaginationControlJ == null) {_uiPaginationControlJ = jQuery.Select("#" + clientId + "_uiPaginationControl");}; return _uiPaginationControlJ;}} private jQueryObject _uiPaginationControlJ;//mappings.Add("Spotted.Controls.PaginationControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
