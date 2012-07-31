//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.UpdatePanel", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Galleries.Edit
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
		public DivElement InfoPanel {get {if (_InfoPanel == null) {_InfoPanel = (DivElement)Document.GetElementById(clientId + "_InfoPanel");}; return _InfoPanel;}} private DivElement _InfoPanel;
		public jQueryObject InfoPanelJ {get {if (_InfoPanelJ == null) {_InfoPanelJ = jQuery.Select("#" + clientId + "_InfoPanel");}; return _InfoPanelJ;}} private jQueryObject _InfoPanelJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element TitleImgCell {get {if (_TitleImgCell == null) {_TitleImgCell = (Element)Document.GetElementById(clientId + "_TitleImgCell");}; return _TitleImgCell;}} private Element _TitleImgCell;
		public jQueryObject TitleImgCellJ {get {if (_TitleImgCellJ == null) {_TitleImgCellJ = jQuery.Select("#" + clientId + "_TitleImgCell");}; return _TitleImgCellJ;}} private jQueryObject _TitleImgCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public ImageElement TitlePicImg {get {if (_TitlePicImg == null) {_TitlePicImg = (ImageElement)Document.GetElementById(clientId + "_TitlePicImg");}; return _TitlePicImg;}} private ImageElement _TitlePicImg;
		public jQueryObject TitlePicImgJ {get {if (_TitlePicImgJ == null) {_TitlePicImgJ = jQuery.Select("#" + clientId + "_TitlePicImg");}; return _TitlePicImgJ;}} private jQueryObject _TitlePicImgJ;
		public InputElement GalleryName {get {if (_GalleryName == null) {_GalleryName = (InputElement)Document.GetElementById(clientId + "_GalleryName");}; return _GalleryName;}} private InputElement _GalleryName;
		public jQueryObject GalleryNameJ {get {if (_GalleryNameJ == null) {_GalleryNameJ = jQuery.Select("#" + clientId + "_GalleryName");}; return _GalleryNameJ;}} private jQueryObject _GalleryNameJ;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Requiredfieldvalidator1 {get {if (_Requiredfieldvalidator1 == null) {_Requiredfieldvalidator1 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator1");}; return _Requiredfieldvalidator1;}} private Element _Requiredfieldvalidator1;
		public jQueryObject Requiredfieldvalidator1J {get {if (_Requiredfieldvalidator1J == null) {_Requiredfieldvalidator1J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator1");}; return _Requiredfieldvalidator1J;}} private jQueryObject _Requiredfieldvalidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public CheckBoxElement WatchCheckBox {get {if (_WatchCheckBox == null) {_WatchCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_WatchCheckBox");}; return _WatchCheckBox;}} private CheckBoxElement _WatchCheckBox;
		public jQueryObject WatchCheckBoxJ {get {if (_WatchCheckBoxJ == null) {_WatchCheckBoxJ = jQuery.Select("#" + clientId + "_WatchCheckBox");}; return _WatchCheckBoxJ;}} private jQueryObject _WatchCheckBoxJ;
		public DivElement BackToEditArticlePanel {get {if (_BackToEditArticlePanel == null) {_BackToEditArticlePanel = (DivElement)Document.GetElementById(clientId + "_BackToEditArticlePanel");}; return _BackToEditArticlePanel;}} private DivElement _BackToEditArticlePanel;
		public jQueryObject BackToEditArticlePanelJ {get {if (_BackToEditArticlePanelJ == null) {_BackToEditArticlePanelJ = jQuery.Select("#" + clientId + "_BackToEditArticlePanel");}; return _BackToEditArticlePanelJ;}} private jQueryObject _BackToEditArticlePanelJ;
		public Element sdH11 {get {if (_sdH11 == null) {_sdH11 = (Element)Document.GetElementById(clientId + "_sdH11");}; return _sdH11;}} private Element _sdH11;
		public jQueryObject sdH11J {get {if (_sdH11J == null) {_sdH11J = jQuery.Select("#" + clientId + "_sdH11");}; return _sdH11J;}} private jQueryObject _sdH11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PhotoUploadPanel {get {if (_PhotoUploadPanel == null) {_PhotoUploadPanel = (DivElement)Document.GetElementById(clientId + "_PhotoUploadPanel");}; return _PhotoUploadPanel;}} private DivElement _PhotoUploadPanel;
		public jQueryObject PhotoUploadPanelJ {get {if (_PhotoUploadPanelJ == null) {_PhotoUploadPanelJ = jQuery.Select("#" + clientId + "_PhotoUploadPanel");}; return _PhotoUploadPanelJ;}} private jQueryObject _PhotoUploadPanelJ;
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelUpload {get {if (_PanelUpload == null) {_PanelUpload = (DivElement)Document.GetElementById(clientId + "_PanelUpload");}; return _PanelUpload;}} private DivElement _PanelUpload;
		public jQueryObject PanelUploadJ {get {if (_PanelUploadJ == null) {_PanelUploadJ = jQuery.Select("#" + clientId + "_PanelUpload");}; return _PanelUploadJ;}} private jQueryObject _PanelUploadJ;
		public Element UpdatePanel1 {get {if (_UpdatePanel1 == null) {_UpdatePanel1 = (Element)Document.GetElementById(clientId + "_UpdatePanel1");}; return _UpdatePanel1;}} private Element _UpdatePanel1;
		public jQueryObject UpdatePanel1J {get {if (_UpdatePanel1J == null) {_UpdatePanel1J = jQuery.Select("#" + clientId + "_UpdatePanel1");}; return _UpdatePanel1J;}} private jQueryObject _UpdatePanel1J;//mappings.Add("System.Web.UI.UpdatePanel", ElementGetter("Element"));
		public DivElement uiYourPhotos {get {if (_uiYourPhotos == null) {_uiYourPhotos = (DivElement)Document.GetElementById(clientId + "_uiYourPhotos");}; return _uiYourPhotos;}} private DivElement _uiYourPhotos;
		public jQueryObject uiYourPhotosJ {get {if (_uiYourPhotosJ == null) {_uiYourPhotosJ = jQuery.Select("#" + clientId + "_uiYourPhotos");}; return _uiYourPhotosJ;}} private jQueryObject _uiYourPhotosJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement GalleryEmptyPanel {get {if (_GalleryEmptyPanel == null) {_GalleryEmptyPanel = (DivElement)Document.GetElementById(clientId + "_GalleryEmptyPanel");}; return _GalleryEmptyPanel;}} private DivElement _GalleryEmptyPanel;
		public jQueryObject GalleryEmptyPanelJ {get {if (_GalleryEmptyPanelJ == null) {_GalleryEmptyPanelJ = jQuery.Select("#" + clientId + "_GalleryEmptyPanel");}; return _GalleryEmptyPanelJ;}} private jQueryObject _GalleryEmptyPanelJ;
		public Element Button4 {get {if (_Button4 == null) {_Button4 = (Element)Document.GetElementById(clientId + "_Button4");}; return _Button4;}} private Element _Button4;
		public jQueryObject Button4J {get {if (_Button4J == null) {_Button4J = jQuery.Select("#" + clientId + "_Button4");}; return _Button4J;}} private jQueryObject _Button4J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement PhotoProcessingPanel {get {if (_PhotoProcessingPanel == null) {_PhotoProcessingPanel = (DivElement)Document.GetElementById(clientId + "_PhotoProcessingPanel");}; return _PhotoProcessingPanel;}} private DivElement _PhotoProcessingPanel;
		public jQueryObject PhotoProcessingPanelJ {get {if (_PhotoProcessingPanelJ == null) {_PhotoProcessingPanelJ = jQuery.Select("#" + clientId + "_PhotoProcessingPanel");}; return _PhotoProcessingPanelJ;}} private jQueryObject _PhotoProcessingPanelJ;
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element PhotoProcessingDataList {get {if (_PhotoProcessingDataList == null) {_PhotoProcessingDataList = (Element)Document.GetElementById(clientId + "_PhotoProcessingDataList");}; return _PhotoProcessingDataList;}} private Element _PhotoProcessingDataList;
		public jQueryObject PhotoProcessingDataListJ {get {if (_PhotoProcessingDataListJ == null) {_PhotoProcessingDataListJ = jQuery.Select("#" + clientId + "_PhotoProcessingDataList");}; return _PhotoProcessingDataListJ;}} private jQueryObject _PhotoProcessingDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public DivElement PhotoModeratePanel {get {if (_PhotoModeratePanel == null) {_PhotoModeratePanel = (DivElement)Document.GetElementById(clientId + "_PhotoModeratePanel");}; return _PhotoModeratePanel;}} private DivElement _PhotoModeratePanel;
		public jQueryObject PhotoModeratePanelJ {get {if (_PhotoModeratePanelJ == null) {_PhotoModeratePanelJ = jQuery.Select("#" + clientId + "_PhotoModeratePanel");}; return _PhotoModeratePanelJ;}} private jQueryObject _PhotoModeratePanelJ;
		public Element PhotoModerateDataList {get {if (_PhotoModerateDataList == null) {_PhotoModerateDataList = (Element)Document.GetElementById(clientId + "_PhotoModerateDataList");}; return _PhotoModerateDataList;}} private Element _PhotoModerateDataList;
		public jQueryObject PhotoModerateDataListJ {get {if (_PhotoModerateDataListJ == null) {_PhotoModerateDataListJ = jQuery.Select("#" + clientId + "_PhotoModerateDataList");}; return _PhotoModerateDataListJ;}} private jQueryObject _PhotoModerateDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public DivElement PhotoEnabledPanel {get {if (_PhotoEnabledPanel == null) {_PhotoEnabledPanel = (DivElement)Document.GetElementById(clientId + "_PhotoEnabledPanel");}; return _PhotoEnabledPanel;}} private DivElement _PhotoEnabledPanel;
		public jQueryObject PhotoEnabledPanelJ {get {if (_PhotoEnabledPanelJ == null) {_PhotoEnabledPanelJ = jQuery.Select("#" + clientId + "_PhotoEnabledPanel");}; return _PhotoEnabledPanelJ;}} private jQueryObject _PhotoEnabledPanelJ;
		public Element PhotoEnabledDataList {get {if (_PhotoEnabledDataList == null) {_PhotoEnabledDataList = (Element)Document.GetElementById(clientId + "_PhotoEnabledDataList");}; return _PhotoEnabledDataList;}} private Element _PhotoEnabledDataList;
		public jQueryObject PhotoEnabledDataListJ {get {if (_PhotoEnabledDataListJ == null) {_PhotoEnabledDataListJ = jQuery.Select("#" + clientId + "_PhotoEnabledDataList");}; return _PhotoEnabledDataListJ;}} private jQueryObject _PhotoEnabledDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public DivElement PhotoActionsPanel {get {if (_PhotoActionsPanel == null) {_PhotoActionsPanel = (DivElement)Document.GetElementById(clientId + "_PhotoActionsPanel");}; return _PhotoActionsPanel;}} private DivElement _PhotoActionsPanel;
		public jQueryObject PhotoActionsPanelJ {get {if (_PhotoActionsPanelJ == null) {_PhotoActionsPanelJ = jQuery.Select("#" + clientId + "_PhotoActionsPanel");}; return _PhotoActionsPanelJ;}} private jQueryObject _PhotoActionsPanelJ;
		public Element DeleteSelectedButton {get {if (_DeleteSelectedButton == null) {_DeleteSelectedButton = (Element)Document.GetElementById(clientId + "_DeleteSelectedButton");}; return _DeleteSelectedButton;}} private Element _DeleteSelectedButton;
		public jQueryObject DeleteSelectedButtonJ {get {if (_DeleteSelectedButtonJ == null) {_DeleteSelectedButtonJ = jQuery.Select("#" + clientId + "_DeleteSelectedButton");}; return _DeleteSelectedButtonJ;}} private jQueryObject _DeleteSelectedButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element DeleteSelectedOutput {get {if (_DeleteSelectedOutput == null) {_DeleteSelectedOutput = (Element)Document.GetElementById(clientId + "_DeleteSelectedOutput");}; return _DeleteSelectedOutput;}} private Element _DeleteSelectedOutput;
		public jQueryObject DeleteSelectedOutputJ {get {if (_DeleteSelectedOutputJ == null) {_DeleteSelectedOutputJ = jQuery.Select("#" + clientId + "_DeleteSelectedOutput");}; return _DeleteSelectedOutputJ;}} private jQueryObject _DeleteSelectedOutputJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
