//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.PaginationControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.CampaignCreditHistory
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
		public Element PromoterIntro {get {if (_PromoterIntro == null) {_PromoterIntro = (Element)Document.GetElementById(clientId + "_PromoterIntro");}; return _PromoterIntro;}} private Element _PromoterIntro;
		public jQueryObject PromoterIntroJ {get {if (_PromoterIntroJ == null) {_PromoterIntroJ = jQuery.Select("#" + clientId + "_PromoterIntro");}; return _PromoterIntroJ;}} private jQueryObject _PromoterIntroJ;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public Element H1Title {get {if (_H1Title == null) {_H1Title = (Element)Document.GetElementById(clientId + "_H1Title");}; return _H1Title;}} private Element _H1Title;
		public jQueryObject H1TitleJ {get {if (_H1TitleJ == null) {_H1TitleJ = jQuery.Select("#" + clientId + "_H1Title");}; return _H1TitleJ;}} private jQueryObject _H1TitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement CampaignCreditsHistoryPanel {get {if (_CampaignCreditsHistoryPanel == null) {_CampaignCreditsHistoryPanel = (DivElement)Document.GetElementById(clientId + "_CampaignCreditsHistoryPanel");}; return _CampaignCreditsHistoryPanel;}} private DivElement _CampaignCreditsHistoryPanel;
		public jQueryObject CampaignCreditsHistoryPanelJ {get {if (_CampaignCreditsHistoryPanelJ == null) {_CampaignCreditsHistoryPanelJ = jQuery.Select("#" + clientId + "_CampaignCreditsHistoryPanel");}; return _CampaignCreditsHistoryPanelJ;}} private jQueryObject _CampaignCreditsHistoryPanelJ;
		public Element CampaignCreditHistoryGridView {get {if (_CampaignCreditHistoryGridView == null) {_CampaignCreditHistoryGridView = (Element)Document.GetElementById(clientId + "_CampaignCreditHistoryGridView");}; return _CampaignCreditHistoryGridView;}} private Element _CampaignCreditHistoryGridView;
		public jQueryObject CampaignCreditHistoryGridViewJ {get {if (_CampaignCreditHistoryGridViewJ == null) {_CampaignCreditHistoryGridViewJ = jQuery.Select("#" + clientId + "_CampaignCreditHistoryGridView");}; return _CampaignCreditHistoryGridViewJ;}} private jQueryObject _CampaignCreditHistoryGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element uiPaginationControl {get {if (_uiPaginationControl == null) {_uiPaginationControl = (Element)Document.GetElementById(clientId + "_uiPaginationControl");}; return _uiPaginationControl;}} private Element _uiPaginationControl;
		public jQueryObject uiPaginationControlJ {get {if (_uiPaginationControlJ == null) {_uiPaginationControlJ = jQuery.Select("#" + clientId + "_uiPaginationControl");}; return _uiPaginationControlJ;}} private jQueryObject _uiPaginationControlJ;//mappings.Add("Spotted.Controls.PaginationControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
