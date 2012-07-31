//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.PhotoOfWeekUser
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
		public InputElement uiPhotoK {get {if (_uiPhotoK == null) {_uiPhotoK = (InputElement)Document.GetElementById(clientId + "_uiPhotoK");}; return _uiPhotoK;}} private InputElement _uiPhotoK;
		public jQueryObject uiPhotoKJ {get {if (_uiPhotoKJ == null) {_uiPhotoKJ = jQuery.Select("#" + clientId + "_uiPhotoK");}; return _uiPhotoKJ;}} private jQueryObject _uiPhotoKJ;
		public DivElement uiPhotoDetails {get {if (_uiPhotoDetails == null) {_uiPhotoDetails = (DivElement)Document.GetElementById(clientId + "_uiPhotoDetails");}; return _uiPhotoDetails;}} private DivElement _uiPhotoDetails;
		public jQueryObject uiPhotoDetailsJ {get {if (_uiPhotoDetailsJ == null) {_uiPhotoDetailsJ = jQuery.Select("#" + clientId + "_uiPhotoDetails");}; return _uiPhotoDetailsJ;}} private jQueryObject _uiPhotoDetailsJ;
		public Element uiPhotoKLabel {get {if (_uiPhotoKLabel == null) {_uiPhotoKLabel = (Element)Document.GetElementById(clientId + "_uiPhotoKLabel");}; return _uiPhotoKLabel;}} private Element _uiPhotoKLabel;
		public jQueryObject uiPhotoKLabelJ {get {if (_uiPhotoKLabelJ == null) {_uiPhotoKLabelJ = jQuery.Select("#" + clientId + "_uiPhotoKLabel");}; return _uiPhotoKLabelJ;}} private jQueryObject _uiPhotoKLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public ImageElement uiPhotoImg {get {if (_uiPhotoImg == null) {_uiPhotoImg = (ImageElement)Document.GetElementById(clientId + "_uiPhotoImg");}; return _uiPhotoImg;}} private ImageElement _uiPhotoImg;
		public jQueryObject uiPhotoImgJ {get {if (_uiPhotoImgJ == null) {_uiPhotoImgJ = jQuery.Select("#" + clientId + "_uiPhotoImg");}; return _uiPhotoImgJ;}} private jQueryObject _uiPhotoImgJ;
		public CheckBoxElement uiPhotoOfWeek {get {if (_uiPhotoOfWeek == null) {_uiPhotoOfWeek = (CheckBoxElement)Document.GetElementById(clientId + "_uiPhotoOfWeek");}; return _uiPhotoOfWeek;}} private CheckBoxElement _uiPhotoOfWeek;
		public jQueryObject uiPhotoOfWeekJ {get {if (_uiPhotoOfWeekJ == null) {_uiPhotoOfWeekJ = jQuery.Select("#" + clientId + "_uiPhotoOfWeek");}; return _uiPhotoOfWeekJ;}} private jQueryObject _uiPhotoOfWeekJ;
		public InputElement uiPhotoOfWeekUserCaption {get {if (_uiPhotoOfWeekUserCaption == null) {_uiPhotoOfWeekUserCaption = (InputElement)Document.GetElementById(clientId + "_uiPhotoOfWeekUserCaption");}; return _uiPhotoOfWeekUserCaption;}} private InputElement _uiPhotoOfWeekUserCaption;
		public jQueryObject uiPhotoOfWeekUserCaptionJ {get {if (_uiPhotoOfWeekUserCaptionJ == null) {_uiPhotoOfWeekUserCaptionJ = jQuery.Select("#" + clientId + "_uiPhotoOfWeekUserCaption");}; return _uiPhotoOfWeekUserCaptionJ;}} private jQueryObject _uiPhotoOfWeekUserCaptionJ;
		public CheckBoxElement uiResetDateTime {get {if (_uiResetDateTime == null) {_uiResetDateTime = (CheckBoxElement)Document.GetElementById(clientId + "_uiResetDateTime");}; return _uiResetDateTime;}} private CheckBoxElement _uiResetDateTime;
		public jQueryObject uiResetDateTimeJ {get {if (_uiResetDateTimeJ == null) {_uiResetDateTimeJ = jQuery.Select("#" + clientId + "_uiResetDateTime");}; return _uiResetDateTimeJ;}} private jQueryObject _uiResetDateTimeJ;
		public CheckBoxElement uiPhotoOfWeekUserBlocked {get {if (_uiPhotoOfWeekUserBlocked == null) {_uiPhotoOfWeekUserBlocked = (CheckBoxElement)Document.GetElementById(clientId + "_uiPhotoOfWeekUserBlocked");}; return _uiPhotoOfWeekUserBlocked;}} private CheckBoxElement _uiPhotoOfWeekUserBlocked;
		public jQueryObject uiPhotoOfWeekUserBlockedJ {get {if (_uiPhotoOfWeekUserBlockedJ == null) {_uiPhotoOfWeekUserBlockedJ = jQuery.Select("#" + clientId + "_uiPhotoOfWeekUserBlocked");}; return _uiPhotoOfWeekUserBlockedJ;}} private jQueryObject _uiPhotoOfWeekUserBlockedJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
