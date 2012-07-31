//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlInputButton", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Banners.Preview", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Events.Copy
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
		public DivElement PanelStart {get {if (_PanelStart == null) {_PanelStart = (DivElement)Document.GetElementById(clientId + "_PanelStart");}; return _PanelStart;}} private DivElement _PanelStart;
		public jQueryObject PanelStartJ {get {if (_PanelStartJ == null) {_PanelStartJ = jQuery.Select("#" + clientId + "_PanelStart");}; return _PanelStartJ;}} private jQueryObject _PanelStartJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Js.Controls.Picker.Controller uiEventPicker {get {return (Js.Controls.Picker.Controller) Script.Eval(clientId + "_uiEventPickerController");}}
		public Js.CustomControls.Cal.Controller NewDateCalendar {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_NewDateCalendarController");}}
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element CustomValidator1 {get {if (_CustomValidator1 == null) {_CustomValidator1 = (Element)Document.GetElementById(clientId + "_CustomValidator1");}; return _CustomValidator1;}} private Element _CustomValidator1;
		public jQueryObject CustomValidator1J {get {if (_CustomValidator1J == null) {_CustomValidator1J = jQuery.Select("#" + clientId + "_CustomValidator1");}; return _CustomValidator1J;}} private jQueryObject _CustomValidator1J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element Customvalidator2 {get {if (_Customvalidator2 == null) {_Customvalidator2 = (Element)Document.GetElementById(clientId + "_Customvalidator2");}; return _Customvalidator2;}} private Element _Customvalidator2;
		public jQueryObject Customvalidator2J {get {if (_Customvalidator2J == null) {_Customvalidator2J = jQuery.Select("#" + clientId + "_Customvalidator2");}; return _Customvalidator2J;}} private jQueryObject _Customvalidator2J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public DivElement PanelConflict {get {if (_PanelConflict == null) {_PanelConflict = (DivElement)Document.GetElementById(clientId + "_PanelConflict");}; return _PanelConflict;}} private DivElement _PanelConflict;
		public jQueryObject PanelConflictJ {get {if (_PanelConflictJ == null) {_PanelConflictJ = jQuery.Select("#" + clientId + "_PanelConflict");}; return _PanelConflictJ;}} private jQueryObject _PanelConflictJ;
		public Element PanelConflictDataGrid {get {if (_PanelConflictDataGrid == null) {_PanelConflictDataGrid = (Element)Document.GetElementById(clientId + "_PanelConflictDataGrid");}; return _PanelConflictDataGrid;}} private Element _PanelConflictDataGrid;
		public jQueryObject PanelConflictDataGridJ {get {if (_PanelConflictDataGridJ == null) {_PanelConflictDataGridJ = jQuery.Select("#" + clientId + "_PanelConflictDataGrid");}; return _PanelConflictDataGridJ;}} private jQueryObject _PanelConflictDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public CheckBoxElement PanelConflictCheckbox {get {if (_PanelConflictCheckbox == null) {_PanelConflictCheckbox = (CheckBoxElement)Document.GetElementById(clientId + "_PanelConflictCheckbox");}; return _PanelConflictCheckbox;}} private CheckBoxElement _PanelConflictCheckbox;
		public jQueryObject PanelConflictCheckboxJ {get {if (_PanelConflictCheckboxJ == null) {_PanelConflictCheckboxJ = jQuery.Select("#" + clientId + "_PanelConflictCheckbox");}; return _PanelConflictCheckboxJ;}} private jQueryObject _PanelConflictCheckboxJ;
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.HtmlControls.HtmlInputButton", ElementGetter("Element"));
		public Element Button3 {get {if (_Button3 == null) {_Button3 = (Element)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private Element _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Customvalidator3 {get {if (_Customvalidator3 == null) {_Customvalidator3 = (Element)Document.GetElementById(clientId + "_Customvalidator3");}; return _Customvalidator3;}} private Element _Customvalidator3;
		public jQueryObject Customvalidator3J {get {if (_Customvalidator3J == null) {_Customvalidator3J = jQuery.Select("#" + clientId + "_Customvalidator3");}; return _Customvalidator3J;}} private jQueryObject _Customvalidator3J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public DivElement PanelReview {get {if (_PanelReview == null) {_PanelReview = (DivElement)Document.GetElementById(clientId + "_PanelReview");}; return _PanelReview;}} private DivElement _PanelReview;
		public jQueryObject PanelReviewJ {get {if (_PanelReviewJ == null) {_PanelReviewJ = jQuery.Select("#" + clientId + "_PanelReview");}; return _PanelReviewJ;}} private jQueryObject _PanelReviewJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element ReviewVenue {get {if (_ReviewVenue == null) {_ReviewVenue = (Element)Document.GetElementById(clientId + "_ReviewVenue");}; return _ReviewVenue;}} private Element _ReviewVenue;
		public jQueryObject ReviewVenueJ {get {if (_ReviewVenueJ == null) {_ReviewVenueJ = jQuery.Select("#" + clientId + "_ReviewVenue");}; return _ReviewVenueJ;}} private jQueryObject _ReviewVenueJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewDate {get {if (_ReviewDate == null) {_ReviewDate = (Element)Document.GetElementById(clientId + "_ReviewDate");}; return _ReviewDate;}} private Element _ReviewDate;
		public jQueryObject ReviewDateJ {get {if (_ReviewDateJ == null) {_ReviewDateJ = jQuery.Select("#" + clientId + "_ReviewDate");}; return _ReviewDateJ;}} private jQueryObject _ReviewDateJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewName {get {if (_ReviewName == null) {_ReviewName = (Element)Document.GetElementById(clientId + "_ReviewName");}; return _ReviewName;}} private Element _ReviewName;
		public jQueryObject ReviewNameJ {get {if (_ReviewNameJ == null) {_ReviewNameJ = jQuery.Select("#" + clientId + "_ReviewName");}; return _ReviewNameJ;}} private jQueryObject _ReviewNameJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewStartTime {get {if (_ReviewStartTime == null) {_ReviewStartTime = (Element)Document.GetElementById(clientId + "_ReviewStartTime");}; return _ReviewStartTime;}} private Element _ReviewStartTime;
		public jQueryObject ReviewStartTimeJ {get {if (_ReviewStartTimeJ == null) {_ReviewStartTimeJ = jQuery.Select("#" + clientId + "_ReviewStartTime");}; return _ReviewStartTimeJ;}} private jQueryObject _ReviewStartTimeJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewShortDetailsHtml {get {if (_ReviewShortDetailsHtml == null) {_ReviewShortDetailsHtml = (Element)Document.GetElementById(clientId + "_ReviewShortDetailsHtml");}; return _ReviewShortDetailsHtml;}} private Element _ReviewShortDetailsHtml;
		public jQueryObject ReviewShortDetailsHtmlJ {get {if (_ReviewShortDetailsHtmlJ == null) {_ReviewShortDetailsHtmlJ = jQuery.Select("#" + clientId + "_ReviewShortDetailsHtml");}; return _ReviewShortDetailsHtmlJ;}} private jQueryObject _ReviewShortDetailsHtmlJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewLongDetailsHtml {get {if (_ReviewLongDetailsHtml == null) {_ReviewLongDetailsHtml = (Element)Document.GetElementById(clientId + "_ReviewLongDetailsHtml");}; return _ReviewLongDetailsHtml;}} private Element _ReviewLongDetailsHtml;
		public jQueryObject ReviewLongDetailsHtmlJ {get {if (_ReviewLongDetailsHtmlJ == null) {_ReviewLongDetailsHtmlJ = jQuery.Select("#" + clientId + "_ReviewLongDetailsHtml");}; return _ReviewLongDetailsHtmlJ;}} private jQueryObject _ReviewLongDetailsHtmlJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewCapacity {get {if (_ReviewCapacity == null) {_ReviewCapacity = (Element)Document.GetElementById(clientId + "_ReviewCapacity");}; return _ReviewCapacity;}} private Element _ReviewCapacity;
		public jQueryObject ReviewCapacityJ {get {if (_ReviewCapacityJ == null) {_ReviewCapacityJ = jQuery.Select("#" + clientId + "_ReviewCapacity");}; return _ReviewCapacityJ;}} private jQueryObject _ReviewCapacityJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewBrandTr {get {if (_ReviewBrandTr == null) {_ReviewBrandTr = (Element)Document.GetElementById(clientId + "_ReviewBrandTr");}; return _ReviewBrandTr;}} private Element _ReviewBrandTr;
		public jQueryObject ReviewBrandTrJ {get {if (_ReviewBrandTrJ == null) {_ReviewBrandTrJ = jQuery.Select("#" + clientId + "_ReviewBrandTr");}; return _ReviewBrandTrJ;}} private jQueryObject _ReviewBrandTrJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element ReviewBrand {get {if (_ReviewBrand == null) {_ReviewBrand = (Element)Document.GetElementById(clientId + "_ReviewBrand");}; return _ReviewBrand;}} private Element _ReviewBrand;
		public jQueryObject ReviewBrandJ {get {if (_ReviewBrandJ == null) {_ReviewBrandJ = jQuery.Select("#" + clientId + "_ReviewBrand");}; return _ReviewBrandJ;}} private jQueryObject _ReviewBrandJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewMusicTypes {get {if (_ReviewMusicTypes == null) {_ReviewMusicTypes = (Element)Document.GetElementById(clientId + "_ReviewMusicTypes");}; return _ReviewMusicTypes;}} private Element _ReviewMusicTypes;
		public jQueryObject ReviewMusicTypesJ {get {if (_ReviewMusicTypesJ == null) {_ReviewMusicTypesJ = jQuery.Select("#" + clientId + "_ReviewMusicTypes");}; return _ReviewMusicTypesJ;}} private jQueryObject _ReviewMusicTypesJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ReviewNoPicTd {get {if (_ReviewNoPicTd == null) {_ReviewNoPicTd = (Element)Document.GetElementById(clientId + "_ReviewNoPicTd");}; return _ReviewNoPicTd;}} private Element _ReviewNoPicTd;
		public jQueryObject ReviewNoPicTdJ {get {if (_ReviewNoPicTdJ == null) {_ReviewNoPicTdJ = jQuery.Select("#" + clientId + "_ReviewNoPicTd");}; return _ReviewNoPicTdJ;}} private jQueryObject _ReviewNoPicTdJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element ReviewPicTd {get {if (_ReviewPicTd == null) {_ReviewPicTd = (Element)Document.GetElementById(clientId + "_ReviewPicTd");}; return _ReviewPicTd;}} private Element _ReviewPicTd;
		public jQueryObject ReviewPicTdJ {get {if (_ReviewPicTdJ == null) {_ReviewPicTdJ = jQuery.Select("#" + clientId + "_ReviewPicTd");}; return _ReviewPicTdJ;}} private jQueryObject _ReviewPicTdJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public ImageElement ReviewPicImg {get {if (_ReviewPicImg == null) {_ReviewPicImg = (ImageElement)Document.GetElementById(clientId + "_ReviewPicImg");}; return _ReviewPicImg;}} private ImageElement _ReviewPicImg;
		public jQueryObject ReviewPicImgJ {get {if (_ReviewPicImgJ == null) {_ReviewPicImgJ = jQuery.Select("#" + clientId + "_ReviewPicImg");}; return _ReviewPicImgJ;}} private jQueryObject _ReviewPicImgJ;
		public AnchorElement ReviewEditAnchor {get {if (_ReviewEditAnchor == null) {_ReviewEditAnchor = (AnchorElement)Document.GetElementById(clientId + "_ReviewEditAnchor");}; return _ReviewEditAnchor;}} private AnchorElement _ReviewEditAnchor;
		public jQueryObject ReviewEditAnchorJ {get {if (_ReviewEditAnchorJ == null) {_ReviewEditAnchorJ = jQuery.Select("#" + clientId + "_ReviewEditAnchor");}; return _ReviewEditAnchorJ;}} private jQueryObject _ReviewEditAnchorJ;
		public Element LinkButton1 {get {if (_LinkButton1 == null) {_LinkButton1 = (Element)Document.GetElementById(clientId + "_LinkButton1");}; return _LinkButton1;}} private Element _LinkButton1;
		public jQueryObject LinkButton1J {get {if (_LinkButton1J == null) {_LinkButton1J = jQuery.Select("#" + clientId + "_LinkButton1");}; return _LinkButton1J;}} private jQueryObject _LinkButton1J;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public DivElement PanelSaved {get {if (_PanelSaved == null) {_PanelSaved = (DivElement)Document.GetElementById(clientId + "_PanelSaved");}; return _PanelSaved;}} private DivElement _PanelSaved;
		public jQueryObject PanelSavedJ {get {if (_PanelSavedJ == null) {_PanelSavedJ = jQuery.Select("#" + clientId + "_PanelSaved");}; return _PanelSavedJ;}} private jQueryObject _PanelSavedJ;
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element ucBannerPreview {get {if (_ucBannerPreview == null) {_ucBannerPreview = (Element)Document.GetElementById(clientId + "_ucBannerPreview");}; return _ucBannerPreview;}} private Element _ucBannerPreview;
		public jQueryObject ucBannerPreviewJ {get {if (_ucBannerPreviewJ == null) {_ucBannerPreviewJ = jQuery.Select("#" + clientId + "_ucBannerPreview");}; return _ucBannerPreviewJ;}} private jQueryObject _ucBannerPreviewJ;//mappings.Add("Spotted.Controls.Banners.Preview", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement PanelSavedEventLink {get {if (_PanelSavedEventLink == null) {_PanelSavedEventLink = (AnchorElement)Document.GetElementById(clientId + "_PanelSavedEventLink");}; return _PanelSavedEventLink;}} private AnchorElement _PanelSavedEventLink;
		public jQueryObject PanelSavedEventLinkJ {get {if (_PanelSavedEventLinkJ == null) {_PanelSavedEventLinkJ = jQuery.Select("#" + clientId + "_PanelSavedEventLink");}; return _PanelSavedEventLinkJ;}} private jQueryObject _PanelSavedEventLinkJ;
		public AnchorElement PanelSavedSignUpLink {get {if (_PanelSavedSignUpLink == null) {_PanelSavedSignUpLink = (AnchorElement)Document.GetElementById(clientId + "_PanelSavedSignUpLink");}; return _PanelSavedSignUpLink;}} private AnchorElement _PanelSavedSignUpLink;
		public jQueryObject PanelSavedSignUpLinkJ {get {if (_PanelSavedSignUpLinkJ == null) {_PanelSavedSignUpLinkJ = jQuery.Select("#" + clientId + "_PanelSavedSignUpLink");}; return _PanelSavedSignUpLinkJ;}} private jQueryObject _PanelSavedSignUpLinkJ;
		public Element LinkButton2 {get {if (_LinkButton2 == null) {_LinkButton2 = (Element)Document.GetElementById(clientId + "_LinkButton2");}; return _LinkButton2;}} private Element _LinkButton2;
		public jQueryObject LinkButton2J {get {if (_LinkButton2J == null) {_LinkButton2J = jQuery.Select("#" + clientId + "_LinkButton2");}; return _LinkButton2J;}} private jQueryObject _LinkButton2J;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
