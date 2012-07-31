//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Competitions
{
	public partial class View
		 : Js.DsiUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement TopPhotosNewsPanel {get {if (_TopPhotosNewsPanel == null) {_TopPhotosNewsPanel = (DivElement)Document.GetElementById(clientId + "_TopPhotosNewsPanel");}; return _TopPhotosNewsPanel;}} private DivElement _TopPhotosNewsPanel;
		public jQueryObject TopPhotosNewsPanelJ {get {if (_TopPhotosNewsPanelJ == null) {_TopPhotosNewsPanelJ = jQuery.Select("#" + clientId + "_TopPhotosNewsPanel");}; return _TopPhotosNewsPanelJ;}} private jQueryObject _TopPhotosNewsPanelJ;
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element PlaceImgCell {get {if (_PlaceImgCell == null) {_PlaceImgCell = (Element)Document.GetElementById(clientId + "_PlaceImgCell");}; return _PlaceImgCell;}} private Element _PlaceImgCell;
		public jQueryObject PlaceImgCellJ {get {if (_PlaceImgCellJ == null) {_PlaceImgCellJ = jQuery.Select("#" + clientId + "_PlaceImgCell");}; return _PlaceImgCellJ;}} private jQueryObject _PlaceImgCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element PlaceInfoTopPhotoCell {get {if (_PlaceInfoTopPhotoCell == null) {_PlaceInfoTopPhotoCell = (Element)Document.GetElementById(clientId + "_PlaceInfoTopPhotoCell");}; return _PlaceInfoTopPhotoCell;}} private Element _PlaceInfoTopPhotoCell;
		public jQueryObject PlaceInfoTopPhotoCellJ {get {if (_PlaceInfoTopPhotoCellJ == null) {_PlaceInfoTopPhotoCellJ = jQuery.Select("#" + clientId + "_PlaceInfoTopPhotoCell");}; return _PlaceInfoTopPhotoCellJ;}} private jQueryObject _PlaceInfoTopPhotoCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public DivElement TopPhotoPanel {get {if (_TopPhotoPanel == null) {_TopPhotoPanel = (DivElement)Document.GetElementById(clientId + "_TopPhotoPanel");}; return _TopPhotoPanel;}} private DivElement _TopPhotoPanel;
		public jQueryObject TopPhotoPanelJ {get {if (_TopPhotoPanelJ == null) {_TopPhotoPanelJ = jQuery.Select("#" + clientId + "_TopPhotoPanel");}; return _TopPhotoPanelJ;}} private jQueryObject _TopPhotoPanelJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element LatestCell {get {if (_LatestCell == null) {_LatestCell = (Element)Document.GetElementById(clientId + "_LatestCell");}; return _LatestCell;}} private Element _LatestCell;
		public jQueryObject LatestCellJ {get {if (_LatestCellJ == null) {_LatestCellJ = jQuery.Select("#" + clientId + "_LatestCell");}; return _LatestCellJ;}} private jQueryObject _LatestCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public DivElement SpottedPanel {get {if (_SpottedPanel == null) {_SpottedPanel = (DivElement)Document.GetElementById(clientId + "_SpottedPanel");}; return _SpottedPanel;}} private DivElement _SpottedPanel;
		public jQueryObject SpottedPanelJ {get {if (_SpottedPanelJ == null) {_SpottedPanelJ = jQuery.Select("#" + clientId + "_SpottedPanel");}; return _SpottedPanelJ;}} private jQueryObject _SpottedPanelJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement MoreInfoPanel {get {if (_MoreInfoPanel == null) {_MoreInfoPanel = (DivElement)Document.GetElementById(clientId + "_MoreInfoPanel");}; return _MoreInfoPanel;}} private DivElement _MoreInfoPanel;
		public jQueryObject MoreInfoPanelJ {get {if (_MoreInfoPanelJ == null) {_MoreInfoPanelJ = jQuery.Select("#" + clientId + "_MoreInfoPanel");}; return _MoreInfoPanelJ;}} private jQueryObject _MoreInfoPanelJ;
		public DivElement MoreInfoPanel1 {get {if (_MoreInfoPanel1 == null) {_MoreInfoPanel1 = (DivElement)Document.GetElementById(clientId + "_MoreInfoPanel1");}; return _MoreInfoPanel1;}} private DivElement _MoreInfoPanel1;
		public jQueryObject MoreInfoPanel1J {get {if (_MoreInfoPanel1J == null) {_MoreInfoPanel1J = jQuery.Select("#" + clientId + "_MoreInfoPanel1");}; return _MoreInfoPanel1J;}} private jQueryObject _MoreInfoPanel1J;
		public Element MoreInfoLabel {get {if (_MoreInfoLabel == null) {_MoreInfoLabel = (Element)Document.GetElementById(clientId + "_MoreInfoLabel");}; return _MoreInfoLabel;}} private Element _MoreInfoLabel;
		public jQueryObject MoreInfoLabelJ {get {if (_MoreInfoLabelJ == null) {_MoreInfoLabelJ = jQuery.Select("#" + clientId + "_MoreInfoLabel");}; return _MoreInfoLabelJ;}} private jQueryObject _MoreInfoLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element MoreInfoLabel1 {get {if (_MoreInfoLabel1 == null) {_MoreInfoLabel1 = (Element)Document.GetElementById(clientId + "_MoreInfoLabel1");}; return _MoreInfoLabel1;}} private Element _MoreInfoLabel1;
		public jQueryObject MoreInfoLabel1J {get {if (_MoreInfoLabel1J == null) {_MoreInfoLabel1J = jQuery.Select("#" + clientId + "_MoreInfoLabel1");}; return _MoreInfoLabel1J;}} private jQueryObject _MoreInfoLabel1J;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement CurrentCompPanel {get {if (_CurrentCompPanel == null) {_CurrentCompPanel = (DivElement)Document.GetElementById(clientId + "_CurrentCompPanel");}; return _CurrentCompPanel;}} private DivElement _CurrentCompPanel;
		public jQueryObject CurrentCompPanelJ {get {if (_CurrentCompPanelJ == null) {_CurrentCompPanelJ = jQuery.Select("#" + clientId + "_CurrentCompPanel");}; return _CurrentCompPanelJ;}} private jQueryObject _CurrentCompPanelJ;
		public DivElement EnteredPanel {get {if (_EnteredPanel == null) {_EnteredPanel = (DivElement)Document.GetElementById(clientId + "_EnteredPanel");}; return _EnteredPanel;}} private DivElement _EnteredPanel;
		public jQueryObject EnteredPanelJ {get {if (_EnteredPanelJ == null) {_EnteredPanelJ = jQuery.Select("#" + clientId + "_EnteredPanel");}; return _EnteredPanelJ;}} private jQueryObject _EnteredPanelJ;
		public DivElement EntryPanel {get {if (_EntryPanel == null) {_EntryPanel = (DivElement)Document.GetElementById(clientId + "_EntryPanel");}; return _EntryPanel;}} private DivElement _EntryPanel;
		public jQueryObject EntryPanelJ {get {if (_EntryPanelJ == null) {_EntryPanelJ = jQuery.Select("#" + clientId + "_EntryPanel");}; return _EntryPanelJ;}} private jQueryObject _EntryPanelJ;
		public DivElement FinishedPanel {get {if (_FinishedPanel == null) {_FinishedPanel = (DivElement)Document.GetElementById(clientId + "_FinishedPanel");}; return _FinishedPanel;}} private DivElement _FinishedPanel;
		public jQueryObject FinishedPanelJ {get {if (_FinishedPanelJ == null) {_FinishedPanelJ = jQuery.Select("#" + clientId + "_FinishedPanel");}; return _FinishedPanelJ;}} private jQueryObject _FinishedPanelJ;
		public DivElement WinnersPanel {get {if (_WinnersPanel == null) {_WinnersPanel = (DivElement)Document.GetElementById(clientId + "_WinnersPanel");}; return _WinnersPanel;}} private DivElement _WinnersPanel;
		public jQueryObject WinnersPanelJ {get {if (_WinnersPanelJ == null) {_WinnersPanelJ = jQuery.Select("#" + clientId + "_WinnersPanel");}; return _WinnersPanelJ;}} private jQueryObject _WinnersPanelJ;
		public DivElement NoWinnersPanel {get {if (_NoWinnersPanel == null) {_NoWinnersPanel = (DivElement)Document.GetElementById(clientId + "_NoWinnersPanel");}; return _NoWinnersPanel;}} private DivElement _NoWinnersPanel;
		public jQueryObject NoWinnersPanelJ {get {if (_NoWinnersPanelJ == null) {_NoWinnersPanelJ = jQuery.Select("#" + clientId + "_NoWinnersPanel");}; return _NoWinnersPanelJ;}} private jQueryObject _NoWinnersPanelJ;
		public DivElement YouAreAWinnerPanel {get {if (_YouAreAWinnerPanel == null) {_YouAreAWinnerPanel = (DivElement)Document.GetElementById(clientId + "_YouAreAWinnerPanel");}; return _YouAreAWinnerPanel;}} private DivElement _YouAreAWinnerPanel;
		public jQueryObject YouAreAWinnerPanelJ {get {if (_YouAreAWinnerPanelJ == null) {_YouAreAWinnerPanelJ = jQuery.Select("#" + clientId + "_YouAreAWinnerPanel");}; return _YouAreAWinnerPanelJ;}} private jQueryObject _YouAreAWinnerPanelJ;
		public Element QuestionLabel {get {if (_QuestionLabel == null) {_QuestionLabel = (Element)Document.GetElementById(clientId + "_QuestionLabel");}; return _QuestionLabel;}} private Element _QuestionLabel;
		public jQueryObject QuestionLabelJ {get {if (_QuestionLabelJ == null) {_QuestionLabelJ = jQuery.Select("#" + clientId + "_QuestionLabel");}; return _QuestionLabelJ;}} private jQueryObject _QuestionLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SelectedAnswerLabel {get {if (_SelectedAnswerLabel == null) {_SelectedAnswerLabel = (Element)Document.GetElementById(clientId + "_SelectedAnswerLabel");}; return _SelectedAnswerLabel;}} private Element _SelectedAnswerLabel;
		public jQueryObject SelectedAnswerLabelJ {get {if (_SelectedAnswerLabelJ == null) {_SelectedAnswerLabelJ = jQuery.Select("#" + clientId + "_SelectedAnswerLabel");}; return _SelectedAnswerLabelJ;}} private jQueryObject _SelectedAnswerLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element DateTimeCloseLabel {get {if (_DateTimeCloseLabel == null) {_DateTimeCloseLabel = (Element)Document.GetElementById(clientId + "_DateTimeCloseLabel");}; return _DateTimeCloseLabel;}} private Element _DateTimeCloseLabel;
		public jQueryObject DateTimeCloseLabelJ {get {if (_DateTimeCloseLabelJ == null) {_DateTimeCloseLabelJ = jQuery.Select("#" + clientId + "_DateTimeCloseLabel");}; return _DateTimeCloseLabelJ;}} private jQueryObject _DateTimeCloseLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element DateTimeCloseLabel1 {get {if (_DateTimeCloseLabel1 == null) {_DateTimeCloseLabel1 = (Element)Document.GetElementById(clientId + "_DateTimeCloseLabel1");}; return _DateTimeCloseLabel1;}} private Element _DateTimeCloseLabel1;
		public jQueryObject DateTimeCloseLabel1J {get {if (_DateTimeCloseLabel1J == null) {_DateTimeCloseLabel1J = jQuery.Select("#" + clientId + "_DateTimeCloseLabel1");}; return _DateTimeCloseLabel1J;}} private jQueryObject _DateTimeCloseLabel1J;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element CorrentAnswerLabel {get {if (_CorrentAnswerLabel == null) {_CorrentAnswerLabel = (Element)Document.GetElementById(clientId + "_CorrentAnswerLabel");}; return _CorrentAnswerLabel;}} private Element _CorrentAnswerLabel;
		public jQueryObject CorrentAnswerLabelJ {get {if (_CorrentAnswerLabelJ == null) {_CorrentAnswerLabelJ = jQuery.Select("#" + clientId + "_CorrentAnswerLabel");}; return _CorrentAnswerLabelJ;}} private jQueryObject _CorrentAnswerLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element EnterLinkButton1 {get {if (_EnterLinkButton1 == null) {_EnterLinkButton1 = (Element)Document.GetElementById(clientId + "_EnterLinkButton1");}; return _EnterLinkButton1;}} private Element _EnterLinkButton1;
		public jQueryObject EnterLinkButton1J {get {if (_EnterLinkButton1J == null) {_EnterLinkButton1J = jQuery.Select("#" + clientId + "_EnterLinkButton1");}; return _EnterLinkButton1J;}} private jQueryObject _EnterLinkButton1J;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element EnterLinkButton2 {get {if (_EnterLinkButton2 == null) {_EnterLinkButton2 = (Element)Document.GetElementById(clientId + "_EnterLinkButton2");}; return _EnterLinkButton2;}} private Element _EnterLinkButton2;
		public jQueryObject EnterLinkButton2J {get {if (_EnterLinkButton2J == null) {_EnterLinkButton2J = jQuery.Select("#" + clientId + "_EnterLinkButton2");}; return _EnterLinkButton2J;}} private jQueryObject _EnterLinkButton2J;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element EnterLinkButton3 {get {if (_EnterLinkButton3 == null) {_EnterLinkButton3 = (Element)Document.GetElementById(clientId + "_EnterLinkButton3");}; return _EnterLinkButton3;}} private Element _EnterLinkButton3;
		public jQueryObject EnterLinkButton3J {get {if (_EnterLinkButton3J == null) {_EnterLinkButton3J = jQuery.Select("#" + clientId + "_EnterLinkButton3");}; return _EnterLinkButton3J;}} private jQueryObject _EnterLinkButton3J;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public AnchorElement OwnerAnchor {get {if (_OwnerAnchor == null) {_OwnerAnchor = (AnchorElement)Document.GetElementById(clientId + "_OwnerAnchor");}; return _OwnerAnchor;}} private AnchorElement _OwnerAnchor;
		public jQueryObject OwnerAnchorJ {get {if (_OwnerAnchorJ == null) {_OwnerAnchorJ = jQuery.Select("#" + clientId + "_OwnerAnchor");}; return _OwnerAnchorJ;}} private jQueryObject _OwnerAnchorJ;
		public DivElement NoCompPanel {get {if (_NoCompPanel == null) {_NoCompPanel = (DivElement)Document.GetElementById(clientId + "_NoCompPanel");}; return _NoCompPanel;}} private DivElement _NoCompPanel;
		public jQueryObject NoCompPanelJ {get {if (_NoCompPanelJ == null) {_NoCompPanelJ = jQuery.Select("#" + clientId + "_NoCompPanel");}; return _NoCompPanelJ;}} private jQueryObject _NoCompPanelJ;
		public DivElement CompPanel {get {if (_CompPanel == null) {_CompPanel = (DivElement)Document.GetElementById(clientId + "_CompPanel");}; return _CompPanel;}} private DivElement _CompPanel;
		public jQueryObject CompPanelJ {get {if (_CompPanelJ == null) {_CompPanelJ = jQuery.Select("#" + clientId + "_CompPanel");}; return _CompPanelJ;}} private jQueryObject _CompPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
