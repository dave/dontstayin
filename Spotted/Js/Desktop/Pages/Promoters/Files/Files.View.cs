//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlInputFile", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Files
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
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelDelete {get {if (_PanelDelete == null) {_PanelDelete = (DivElement)Document.GetElementById(clientId + "_PanelDelete");}; return _PanelDelete;}} private DivElement _PanelDelete;
		public jQueryObject PanelDeleteJ {get {if (_PanelDeleteJ == null) {_PanelDeleteJ = jQuery.Select("#" + clientId + "_PanelDelete");}; return _PanelDeleteJ;}} private jQueryObject _PanelDeleteJ;
		public DivElement PanelList {get {if (_PanelList == null) {_PanelList = (DivElement)Document.GetElementById(clientId + "_PanelList");}; return _PanelList;}} private DivElement _PanelList;
		public jQueryObject PanelListJ {get {if (_PanelListJ == null) {_PanelListJ = jQuery.Select("#" + clientId + "_PanelList");}; return _PanelListJ;}} private jQueryObject _PanelListJ;
		public Element MiscDataGrid {get {if (_MiscDataGrid == null) {_MiscDataGrid = (Element)Document.GetElementById(clientId + "_MiscDataGrid");}; return _MiscDataGrid;}} private Element _MiscDataGrid;
		public jQueryObject MiscDataGridJ {get {if (_MiscDataGridJ == null) {_MiscDataGridJ = jQuery.Select("#" + clientId + "_MiscDataGrid");}; return _MiscDataGridJ;}} private jQueryObject _MiscDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element MiscNoFilesP {get {if (_MiscNoFilesP == null) {_MiscNoFilesP = (Element)Document.GetElementById(clientId + "_MiscNoFilesP");}; return _MiscNoFilesP;}} private Element _MiscNoFilesP;
		public jQueryObject MiscNoFilesPJ {get {if (_MiscNoFilesPJ == null) {_MiscNoFilesPJ = jQuery.Select("#" + clientId + "_MiscNoFilesP");}; return _MiscNoFilesPJ;}} private jQueryObject _MiscNoFilesPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element MiscDataGridP {get {if (_MiscDataGridP == null) {_MiscDataGridP = (Element)Document.GetElementById(clientId + "_MiscDataGridP");}; return _MiscDataGridP;}} private Element _MiscDataGridP;
		public jQueryObject MiscDataGridPJ {get {if (_MiscDataGridPJ == null) {_MiscDataGridPJ = jQuery.Select("#" + clientId + "_MiscDataGridP");}; return _MiscDataGridPJ;}} private jQueryObject _MiscDataGridPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement PanelUpload {get {if (_PanelUpload == null) {_PanelUpload = (DivElement)Document.GetElementById(clientId + "_PanelUpload");}; return _PanelUpload;}} private DivElement _PanelUpload;
		public jQueryObject PanelUploadJ {get {if (_PanelUploadJ == null) {_PanelUploadJ = jQuery.Select("#" + clientId + "_PanelUpload");}; return _PanelUploadJ;}} private jQueryObject _PanelUploadJ;
		public DivElement PanelView {get {if (_PanelView == null) {_PanelView = (DivElement)Document.GetElementById(clientId + "_PanelView");}; return _PanelView;}} private DivElement _PanelView;
		public jQueryObject PanelViewJ {get {if (_PanelViewJ == null) {_PanelViewJ = jQuery.Select("#" + clientId + "_PanelView");}; return _PanelViewJ;}} private jQueryObject _PanelViewJ;
		public AnchorElement ViewBackAnchor {get {if (_ViewBackAnchor == null) {_ViewBackAnchor = (AnchorElement)Document.GetElementById(clientId + "_ViewBackAnchor");}; return _ViewBackAnchor;}} private AnchorElement _ViewBackAnchor;
		public jQueryObject ViewBackAnchorJ {get {if (_ViewBackAnchorJ == null) {_ViewBackAnchorJ = jQuery.Select("#" + clientId + "_ViewBackAnchor");}; return _ViewBackAnchorJ;}} private jQueryObject _ViewBackAnchorJ;
		public AnchorElement ViewNameAnchor {get {if (_ViewNameAnchor == null) {_ViewNameAnchor = (AnchorElement)Document.GetElementById(clientId + "_ViewNameAnchor");}; return _ViewNameAnchor;}} private AnchorElement _ViewNameAnchor;
		public jQueryObject ViewNameAnchorJ {get {if (_ViewNameAnchorJ == null) {_ViewNameAnchorJ = jQuery.Select("#" + clientId + "_ViewNameAnchor");}; return _ViewNameAnchorJ;}} private jQueryObject _ViewNameAnchorJ;
		public InputElement ViewUrlTextBox {get {if (_ViewUrlTextBox == null) {_ViewUrlTextBox = (InputElement)Document.GetElementById(clientId + "_ViewUrlTextBox");}; return _ViewUrlTextBox;}} private InputElement _ViewUrlTextBox;
		public jQueryObject ViewUrlTextBoxJ {get {if (_ViewUrlTextBoxJ == null) {_ViewUrlTextBoxJ = jQuery.Select("#" + clientId + "_ViewUrlTextBox");}; return _ViewUrlTextBoxJ;}} private jQueryObject _ViewUrlTextBoxJ;
		public InputElement ViewImageHtmlTextBox {get {if (_ViewImageHtmlTextBox == null) {_ViewImageHtmlTextBox = (InputElement)Document.GetElementById(clientId + "_ViewImageHtmlTextBox");}; return _ViewImageHtmlTextBox;}} private InputElement _ViewImageHtmlTextBox;
		public jQueryObject ViewImageHtmlTextBoxJ {get {if (_ViewImageHtmlTextBoxJ == null) {_ViewImageHtmlTextBoxJ = jQuery.Select("#" + clientId + "_ViewImageHtmlTextBox");}; return _ViewImageHtmlTextBoxJ;}} private jQueryObject _ViewImageHtmlTextBoxJ;
		public Element ViewNameCell {get {if (_ViewNameCell == null) {_ViewNameCell = (Element)Document.GetElementById(clientId + "_ViewNameCell");}; return _ViewNameCell;}} private Element _ViewNameCell;
		public jQueryObject ViewNameCellJ {get {if (_ViewNameCellJ == null) {_ViewNameCellJ = jQuery.Select("#" + clientId + "_ViewNameCell");}; return _ViewNameCellJ;}} private jQueryObject _ViewNameCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element ViewImageWidthCell {get {if (_ViewImageWidthCell == null) {_ViewImageWidthCell = (Element)Document.GetElementById(clientId + "_ViewImageWidthCell");}; return _ViewImageWidthCell;}} private Element _ViewImageWidthCell;
		public jQueryObject ViewImageWidthCellJ {get {if (_ViewImageWidthCellJ == null) {_ViewImageWidthCellJ = jQuery.Select("#" + clientId + "_ViewImageWidthCell");}; return _ViewImageWidthCellJ;}} private jQueryObject _ViewImageWidthCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element ViewImageHeightCell {get {if (_ViewImageHeightCell == null) {_ViewImageHeightCell = (Element)Document.GetElementById(clientId + "_ViewImageHeightCell");}; return _ViewImageHeightCell;}} private Element _ViewImageHeightCell;
		public jQueryObject ViewImageHeightCellJ {get {if (_ViewImageHeightCellJ == null) {_ViewImageHeightCellJ = jQuery.Select("#" + clientId + "_ViewImageHeightCell");}; return _ViewImageHeightCellJ;}} private jQueryObject _ViewImageHeightCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element ViewImageFileSizeCell {get {if (_ViewImageFileSizeCell == null) {_ViewImageFileSizeCell = (Element)Document.GetElementById(clientId + "_ViewImageFileSizeCell");}; return _ViewImageFileSizeCell;}} private Element _ViewImageFileSizeCell;
		public jQueryObject ViewImageFileSizeCellJ {get {if (_ViewImageFileSizeCellJ == null) {_ViewImageFileSizeCellJ = jQuery.Select("#" + clientId + "_ViewImageFileSizeCell");}; return _ViewImageFileSizeCellJ;}} private jQueryObject _ViewImageFileSizeCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public ImageElement ViewLeaderboardImg {get {if (_ViewLeaderboardImg == null) {_ViewLeaderboardImg = (ImageElement)Document.GetElementById(clientId + "_ViewLeaderboardImg");}; return _ViewLeaderboardImg;}} private ImageElement _ViewLeaderboardImg;
		public jQueryObject ViewLeaderboardImgJ {get {if (_ViewLeaderboardImgJ == null) {_ViewLeaderboardImgJ = jQuery.Select("#" + clientId + "_ViewLeaderboardImg");}; return _ViewLeaderboardImgJ;}} private jQueryObject _ViewLeaderboardImgJ;
		public ImageElement ViewHotboxImg {get {if (_ViewHotboxImg == null) {_ViewHotboxImg = (ImageElement)Document.GetElementById(clientId + "_ViewHotboxImg");}; return _ViewHotboxImg;}} private ImageElement _ViewHotboxImg;
		public jQueryObject ViewHotboxImgJ {get {if (_ViewHotboxImgJ == null) {_ViewHotboxImgJ = jQuery.Select("#" + clientId + "_ViewHotboxImg");}; return _ViewHotboxImgJ;}} private jQueryObject _ViewHotboxImgJ;
		public ImageElement ViewPhotoBannerImg {get {if (_ViewPhotoBannerImg == null) {_ViewPhotoBannerImg = (ImageElement)Document.GetElementById(clientId + "_ViewPhotoBannerImg");}; return _ViewPhotoBannerImg;}} private ImageElement _ViewPhotoBannerImg;
		public jQueryObject ViewPhotoBannerImgJ {get {if (_ViewPhotoBannerImgJ == null) {_ViewPhotoBannerImgJ = jQuery.Select("#" + clientId + "_ViewPhotoBannerImg");}; return _ViewPhotoBannerImgJ;}} private jQueryObject _ViewPhotoBannerImgJ;
		public ImageElement ViewEmailBannerImg {get {if (_ViewEmailBannerImg == null) {_ViewEmailBannerImg = (ImageElement)Document.GetElementById(clientId + "_ViewEmailBannerImg");}; return _ViewEmailBannerImg;}} private ImageElement _ViewEmailBannerImg;
		public jQueryObject ViewEmailBannerImgJ {get {if (_ViewEmailBannerImgJ == null) {_ViewEmailBannerImgJ = jQuery.Select("#" + clientId + "_ViewEmailBannerImg");}; return _ViewEmailBannerImgJ;}} private jQueryObject _ViewEmailBannerImgJ;
		public ImageElement ViewSkyscraperImg {get {if (_ViewSkyscraperImg == null) {_ViewSkyscraperImg = (ImageElement)Document.GetElementById(clientId + "_ViewSkyscraperImg");}; return _ViewSkyscraperImg;}} private ImageElement _ViewSkyscraperImg;
		public jQueryObject ViewSkyscraperImgJ {get {if (_ViewSkyscraperImgJ == null) {_ViewSkyscraperImgJ = jQuery.Select("#" + clientId + "_ViewSkyscraperImg");}; return _ViewSkyscraperImgJ;}} private jQueryObject _ViewSkyscraperImgJ;
		public Element ViewLeaderboardLabel {get {if (_ViewLeaderboardLabel == null) {_ViewLeaderboardLabel = (Element)Document.GetElementById(clientId + "_ViewLeaderboardLabel");}; return _ViewLeaderboardLabel;}} private Element _ViewLeaderboardLabel;
		public jQueryObject ViewLeaderboardLabelJ {get {if (_ViewLeaderboardLabelJ == null) {_ViewLeaderboardLabelJ = jQuery.Select("#" + clientId + "_ViewLeaderboardLabel");}; return _ViewLeaderboardLabelJ;}} private jQueryObject _ViewLeaderboardLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ViewHotboxLabel {get {if (_ViewHotboxLabel == null) {_ViewHotboxLabel = (Element)Document.GetElementById(clientId + "_ViewHotboxLabel");}; return _ViewHotboxLabel;}} private Element _ViewHotboxLabel;
		public jQueryObject ViewHotboxLabelJ {get {if (_ViewHotboxLabelJ == null) {_ViewHotboxLabelJ = jQuery.Select("#" + clientId + "_ViewHotboxLabel");}; return _ViewHotboxLabelJ;}} private jQueryObject _ViewHotboxLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ViewPhotoBannerLabel {get {if (_ViewPhotoBannerLabel == null) {_ViewPhotoBannerLabel = (Element)Document.GetElementById(clientId + "_ViewPhotoBannerLabel");}; return _ViewPhotoBannerLabel;}} private Element _ViewPhotoBannerLabel;
		public jQueryObject ViewPhotoBannerLabelJ {get {if (_ViewPhotoBannerLabelJ == null) {_ViewPhotoBannerLabelJ = jQuery.Select("#" + clientId + "_ViewPhotoBannerLabel");}; return _ViewPhotoBannerLabelJ;}} private jQueryObject _ViewPhotoBannerLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ViewEmailBannerLabel {get {if (_ViewEmailBannerLabel == null) {_ViewEmailBannerLabel = (Element)Document.GetElementById(clientId + "_ViewEmailBannerLabel");}; return _ViewEmailBannerLabel;}} private Element _ViewEmailBannerLabel;
		public jQueryObject ViewEmailBannerLabelJ {get {if (_ViewEmailBannerLabelJ == null) {_ViewEmailBannerLabelJ = jQuery.Select("#" + clientId + "_ViewEmailBannerLabel");}; return _ViewEmailBannerLabelJ;}} private jQueryObject _ViewEmailBannerLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ViewSkyscraperLabel {get {if (_ViewSkyscraperLabel == null) {_ViewSkyscraperLabel = (Element)Document.GetElementById(clientId + "_ViewSkyscraperLabel");}; return _ViewSkyscraperLabel;}} private Element _ViewSkyscraperLabel;
		public jQueryObject ViewSkyscraperLabelJ {get {if (_ViewSkyscraperLabelJ == null) {_ViewSkyscraperLabelJ = jQuery.Select("#" + clientId + "_ViewSkyscraperLabel");}; return _ViewSkyscraperLabelJ;}} private jQueryObject _ViewSkyscraperLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ViewImageHtmlTr {get {if (_ViewImageHtmlTr == null) {_ViewImageHtmlTr = (Element)Document.GetElementById(clientId + "_ViewImageHtmlTr");}; return _ViewImageHtmlTr;}} private Element _ViewImageHtmlTr;
		public jQueryObject ViewImageHtmlTrJ {get {if (_ViewImageHtmlTrJ == null) {_ViewImageHtmlTrJ = jQuery.Select("#" + clientId + "_ViewImageHtmlTr");}; return _ViewImageHtmlTrJ;}} private jQueryObject _ViewImageHtmlTrJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element PromoterIntro {get {if (_PromoterIntro == null) {_PromoterIntro = (Element)Document.GetElementById(clientId + "_PromoterIntro");}; return _PromoterIntro;}} private Element _PromoterIntro;
		public jQueryObject PromoterIntroJ {get {if (_PromoterIntroJ == null) {_PromoterIntroJ = jQuery.Select("#" + clientId + "_PromoterIntro");}; return _PromoterIntroJ;}} private jQueryObject _PromoterIntroJ;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element InputFile {get {if (_InputFile == null) {_InputFile = (Element)Document.GetElementById(clientId + "_InputFile");}; return _InputFile;}} private Element _InputFile;
		public jQueryObject InputFileJ {get {if (_InputFileJ == null) {_InputFileJ = jQuery.Select("#" + clientId + "_InputFile");}; return _InputFileJ;}} private jQueryObject _InputFileJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlInputFile", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element ViewBannerBody {get {if (_ViewBannerBody == null) {_ViewBannerBody = (Element)Document.GetElementById(clientId + "_ViewBannerBody");}; return _ViewBannerBody;}} private Element _ViewBannerBody;
		public jQueryObject ViewBannerBodyJ {get {if (_ViewBannerBodyJ == null) {_ViewBannerBodyJ = jQuery.Select("#" + clientId + "_ViewBannerBody");}; return _ViewBannerBodyJ;}} private jQueryObject _ViewBannerBodyJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ViewImageBody {get {if (_ViewImageBody == null) {_ViewImageBody = (Element)Document.GetElementById(clientId + "_ViewImageBody");}; return _ViewImageBody;}} private Element _ViewImageBody;
		public jQueryObject ViewImageBodyJ {get {if (_ViewImageBodyJ == null) {_ViewImageBodyJ = jQuery.Select("#" + clientId + "_ViewImageBody");}; return _ViewImageBodyJ;}} private jQueryObject _ViewImageBodyJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public ImageElement ViewBrokenImg {get {if (_ViewBrokenImg == null) {_ViewBrokenImg = (ImageElement)Document.GetElementById(clientId + "_ViewBrokenImg");}; return _ViewBrokenImg;}} private ImageElement _ViewBrokenImg;
		public jQueryObject ViewBrokenImgJ {get {if (_ViewBrokenImgJ == null) {_ViewBrokenImgJ = jQuery.Select("#" + clientId + "_ViewBrokenImg");}; return _ViewBrokenImgJ;}} private jQueryObject _ViewBrokenImgJ;
		public Element ViewBrokenLabel {get {if (_ViewBrokenLabel == null) {_ViewBrokenLabel = (Element)Document.GetElementById(clientId + "_ViewBrokenLabel");}; return _ViewBrokenLabel;}} private Element _ViewBrokenLabel;
		public jQueryObject ViewBrokenLabelJ {get {if (_ViewBrokenLabelJ == null) {_ViewBrokenLabelJ = jQuery.Select("#" + clientId + "_ViewBrokenLabel");}; return _ViewBrokenLabelJ;}} private jQueryObject _ViewBrokenLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element RequiredFlashVersionTr {get {if (_RequiredFlashVersionTr == null) {_RequiredFlashVersionTr = (Element)Document.GetElementById(clientId + "_RequiredFlashVersionTr");}; return _RequiredFlashVersionTr;}} private Element _RequiredFlashVersionTr;
		public jQueryObject RequiredFlashVersionTrJ {get {if (_RequiredFlashVersionTrJ == null) {_RequiredFlashVersionTrJ = jQuery.Select("#" + clientId + "_RequiredFlashVersionTr");}; return _RequiredFlashVersionTrJ;}} private jQueryObject _RequiredFlashVersionTrJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public InputElement RequiredFlashVersion {get {if (_RequiredFlashVersion == null) {_RequiredFlashVersion = (InputElement)Document.GetElementById(clientId + "_RequiredFlashVersion");}; return _RequiredFlashVersion;}} private InputElement _RequiredFlashVersion;
		public jQueryObject RequiredFlashVersionJ {get {if (_RequiredFlashVersionJ == null) {_RequiredFlashVersionJ = jQuery.Select("#" + clientId + "_RequiredFlashVersion");}; return _RequiredFlashVersionJ;}} private jQueryObject _RequiredFlashVersionJ;
		public Element UpdateFlashVersionDone {get {if (_UpdateFlashVersionDone == null) {_UpdateFlashVersionDone = (Element)Document.GetElementById(clientId + "_UpdateFlashVersionDone");}; return _UpdateFlashVersionDone;}} private Element _UpdateFlashVersionDone;
		public jQueryObject UpdateFlashVersionDoneJ {get {if (_UpdateFlashVersionDoneJ == null) {_UpdateFlashVersionDoneJ = jQuery.Select("#" + clientId + "_UpdateFlashVersionDone");}; return _UpdateFlashVersionDoneJ;}} private jQueryObject _UpdateFlashVersionDoneJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public InputElement SizeWidth {get {if (_SizeWidth == null) {_SizeWidth = (InputElement)Document.GetElementById(clientId + "_SizeWidth");}; return _SizeWidth;}} private InputElement _SizeWidth;
		public jQueryObject SizeWidthJ {get {if (_SizeWidthJ == null) {_SizeWidthJ = jQuery.Select("#" + clientId + "_SizeWidth");}; return _SizeWidthJ;}} private jQueryObject _SizeWidthJ;
		public InputElement SizeHeight {get {if (_SizeHeight == null) {_SizeHeight = (InputElement)Document.GetElementById(clientId + "_SizeHeight");}; return _SizeHeight;}} private InputElement _SizeHeight;
		public jQueryObject SizeHeightJ {get {if (_SizeHeightJ == null) {_SizeHeightJ = jQuery.Select("#" + clientId + "_SizeHeight");}; return _SizeHeightJ;}} private jQueryObject _SizeHeightJ;
		public InputElement Button1 {get {if (_Button1 == null) {_Button1 = (InputElement)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private InputElement _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;
		public Element UpdateSizeDone {get {if (_UpdateSizeDone == null) {_UpdateSizeDone = (Element)Document.GetElementById(clientId + "_UpdateSizeDone");}; return _UpdateSizeDone;}} private Element _UpdateSizeDone;
		public jQueryObject UpdateSizeDoneJ {get {if (_UpdateSizeDoneJ == null) {_UpdateSizeDoneJ = jQuery.Select("#" + clientId + "_UpdateSizeDone");}; return _UpdateSizeDoneJ;}} private jQueryObject _UpdateSizeDoneJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
