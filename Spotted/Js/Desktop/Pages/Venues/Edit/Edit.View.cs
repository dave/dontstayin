//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlInputButton", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Pic", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Venues.Edit
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
		public DivElement PanelDetails {get {if (_PanelDetails == null) {_PanelDetails = (DivElement)Document.GetElementById(clientId + "_PanelDetails");}; return _PanelDetails;}} private DivElement _PanelDetails;
		public jQueryObject PanelDetailsJ {get {if (_PanelDetailsJ == null) {_PanelDetailsJ = jQuery.Select("#" + clientId + "_PanelDetails");}; return _PanelDetailsJ;}} private jQueryObject _PanelDetailsJ;
		public Element PanelDetailsPlaceDiv {get {if (_PanelDetailsPlaceDiv == null) {_PanelDetailsPlaceDiv = (Element)Document.GetElementById(clientId + "_PanelDetailsPlaceDiv");}; return _PanelDetailsPlaceDiv;}} private Element _PanelDetailsPlaceDiv;
		public jQueryObject PanelDetailsPlaceDivJ {get {if (_PanelDetailsPlaceDivJ == null) {_PanelDetailsPlaceDivJ = jQuery.Select("#" + clientId + "_PanelDetailsPlaceDiv");}; return _PanelDetailsPlaceDivJ;}} private jQueryObject _PanelDetailsPlaceDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element RequiredFieldValidator4 {get {if (_RequiredFieldValidator4 == null) {_RequiredFieldValidator4 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator4");}; return _RequiredFieldValidator4;}} private Element _RequiredFieldValidator4;
		public jQueryObject RequiredFieldValidator4J {get {if (_RequiredFieldValidator4J == null) {_RequiredFieldValidator4J = jQuery.Select("#" + clientId + "_RequiredFieldValidator4");}; return _RequiredFieldValidator4J;}} private jQueryObject _RequiredFieldValidator4J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element PanelDetailsPlaceLockedDiv {get {if (_PanelDetailsPlaceLockedDiv == null) {_PanelDetailsPlaceLockedDiv = (Element)Document.GetElementById(clientId + "_PanelDetailsPlaceLockedDiv");}; return _PanelDetailsPlaceLockedDiv;}} private Element _PanelDetailsPlaceLockedDiv;
		public jQueryObject PanelDetailsPlaceLockedDivJ {get {if (_PanelDetailsPlaceLockedDivJ == null) {_PanelDetailsPlaceLockedDivJ = jQuery.Select("#" + clientId + "_PanelDetailsPlaceLockedDiv");}; return _PanelDetailsPlaceLockedDivJ;}} private jQueryObject _PanelDetailsPlaceLockedDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element PanelDetailsPlaceLockedLabel {get {if (_PanelDetailsPlaceLockedLabel == null) {_PanelDetailsPlaceLockedLabel = (Element)Document.GetElementById(clientId + "_PanelDetailsPlaceLockedLabel");}; return _PanelDetailsPlaceLockedLabel;}} private Element _PanelDetailsPlaceLockedLabel;
		public jQueryObject PanelDetailsPlaceLockedLabelJ {get {if (_PanelDetailsPlaceLockedLabelJ == null) {_PanelDetailsPlaceLockedLabelJ = jQuery.Select("#" + clientId + "_PanelDetailsPlaceLockedLabel");}; return _PanelDetailsPlaceLockedLabelJ;}} private jQueryObject _PanelDetailsPlaceLockedLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PanelDetailsPostcodeDiv {get {if (_PanelDetailsPostcodeDiv == null) {_PanelDetailsPostcodeDiv = (Element)Document.GetElementById(clientId + "_PanelDetailsPostcodeDiv");}; return _PanelDetailsPostcodeDiv;}} private Element _PanelDetailsPostcodeDiv;
		public jQueryObject PanelDetailsPostcodeDivJ {get {if (_PanelDetailsPostcodeDivJ == null) {_PanelDetailsPostcodeDivJ = jQuery.Select("#" + clientId + "_PanelDetailsPostcodeDiv");}; return _PanelDetailsPostcodeDivJ;}} private jQueryObject _PanelDetailsPostcodeDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement PanelDetailsPostcodeTextBox {get {if (_PanelDetailsPostcodeTextBox == null) {_PanelDetailsPostcodeTextBox = (InputElement)Document.GetElementById(clientId + "_PanelDetailsPostcodeTextBox");}; return _PanelDetailsPostcodeTextBox;}} private InputElement _PanelDetailsPostcodeTextBox;
		public jQueryObject PanelDetailsPostcodeTextBoxJ {get {if (_PanelDetailsPostcodeTextBoxJ == null) {_PanelDetailsPostcodeTextBoxJ = jQuery.Select("#" + clientId + "_PanelDetailsPostcodeTextBox");}; return _PanelDetailsPostcodeTextBoxJ;}} private jQueryObject _PanelDetailsPostcodeTextBoxJ;
		public Element CustomValidator2 {get {if (_CustomValidator2 == null) {_CustomValidator2 = (Element)Document.GetElementById(clientId + "_CustomValidator2");}; return _CustomValidator2;}} private Element _CustomValidator2;
		public jQueryObject CustomValidator2J {get {if (_CustomValidator2J == null) {_CustomValidator2J = jQuery.Select("#" + clientId + "_CustomValidator2");}; return _CustomValidator2J;}} private jQueryObject _CustomValidator2J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element PanelDetailsNameDiv {get {if (_PanelDetailsNameDiv == null) {_PanelDetailsNameDiv = (Element)Document.GetElementById(clientId + "_PanelDetailsNameDiv");}; return _PanelDetailsNameDiv;}} private Element _PanelDetailsNameDiv;
		public jQueryObject PanelDetailsNameDivJ {get {if (_PanelDetailsNameDivJ == null) {_PanelDetailsNameDivJ = jQuery.Select("#" + clientId + "_PanelDetailsNameDiv");}; return _PanelDetailsNameDivJ;}} private jQueryObject _PanelDetailsNameDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement PanelDetailsVenueName {get {if (_PanelDetailsVenueName == null) {_PanelDetailsVenueName = (InputElement)Document.GetElementById(clientId + "_PanelDetailsVenueName");}; return _PanelDetailsVenueName;}} private InputElement _PanelDetailsVenueName;
		public jQueryObject PanelDetailsVenueNameJ {get {if (_PanelDetailsVenueNameJ == null) {_PanelDetailsVenueNameJ = jQuery.Select("#" + clientId + "_PanelDetailsVenueName");}; return _PanelDetailsVenueNameJ;}} private jQueryObject _PanelDetailsVenueNameJ;
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public CheckBoxElement PanelDetailsVenueRegularEventsYes {get {if (_PanelDetailsVenueRegularEventsYes == null) {_PanelDetailsVenueRegularEventsYes = (CheckBoxElement)Document.GetElementById(clientId + "_PanelDetailsVenueRegularEventsYes");}; return _PanelDetailsVenueRegularEventsYes;}} private CheckBoxElement _PanelDetailsVenueRegularEventsYes;
		public jQueryObject PanelDetailsVenueRegularEventsYesJ {get {if (_PanelDetailsVenueRegularEventsYesJ == null) {_PanelDetailsVenueRegularEventsYesJ = jQuery.Select("#" + clientId + "_PanelDetailsVenueRegularEventsYes");}; return _PanelDetailsVenueRegularEventsYesJ;}} private jQueryObject _PanelDetailsVenueRegularEventsYesJ;
		public CheckBoxElement PanelDetailsVenueRegularEventsNo {get {if (_PanelDetailsVenueRegularEventsNo == null) {_PanelDetailsVenueRegularEventsNo = (CheckBoxElement)Document.GetElementById(clientId + "_PanelDetailsVenueRegularEventsNo");}; return _PanelDetailsVenueRegularEventsNo;}} private CheckBoxElement _PanelDetailsVenueRegularEventsNo;
		public jQueryObject PanelDetailsVenueRegularEventsNoJ {get {if (_PanelDetailsVenueRegularEventsNoJ == null) {_PanelDetailsVenueRegularEventsNoJ = jQuery.Select("#" + clientId + "_PanelDetailsVenueRegularEventsNo");}; return _PanelDetailsVenueRegularEventsNoJ;}} private jQueryObject _PanelDetailsVenueRegularEventsNoJ;
		public Element CustomValidator1 {get {if (_CustomValidator1 == null) {_CustomValidator1 = (Element)Document.GetElementById(clientId + "_CustomValidator1");}; return _CustomValidator1;}} private Element _CustomValidator1;
		public jQueryObject CustomValidator1J {get {if (_CustomValidator1J == null) {_CustomValidator1J = jQuery.Select("#" + clientId + "_CustomValidator1");}; return _CustomValidator1J;}} private jQueryObject _CustomValidator1J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public InputElement PanelDetailsVenueCapacity {get {if (_PanelDetailsVenueCapacity == null) {_PanelDetailsVenueCapacity = (InputElement)Document.GetElementById(clientId + "_PanelDetailsVenueCapacity");}; return _PanelDetailsVenueCapacity;}} private InputElement _PanelDetailsVenueCapacity;
		public jQueryObject PanelDetailsVenueCapacityJ {get {if (_PanelDetailsVenueCapacityJ == null) {_PanelDetailsVenueCapacityJ = jQuery.Select("#" + clientId + "_PanelDetailsVenueCapacity");}; return _PanelDetailsVenueCapacityJ;}} private jQueryObject _PanelDetailsVenueCapacityJ;
		public Element RequiredFieldValidator2 {get {if (_RequiredFieldValidator2 == null) {_RequiredFieldValidator2 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator2");}; return _RequiredFieldValidator2;}} private Element _RequiredFieldValidator2;
		public jQueryObject RequiredFieldValidator2J {get {if (_RequiredFieldValidator2J == null) {_RequiredFieldValidator2J = jQuery.Select("#" + clientId + "_RequiredFieldValidator2");}; return _RequiredFieldValidator2J;}} private jQueryObject _RequiredFieldValidator2J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element CompareValidator1 {get {if (_CompareValidator1 == null) {_CompareValidator1 = (Element)Document.GetElementById(clientId + "_CompareValidator1");}; return _CompareValidator1;}} private Element _CompareValidator1;
		public jQueryObject CompareValidator1J {get {if (_CompareValidator1J == null) {_CompareValidator1J = jQuery.Select("#" + clientId + "_CompareValidator1");}; return _CompareValidator1J;}} private jQueryObject _CompareValidator1J;//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
		public Js.Controls.Html.Controller PanelDetailsVenueDetailsHtml {get {return (Js.Controls.Html.Controller) Script.Eval(clientId + "_PanelDetailsVenueDetailsHtmlController");}}
		public Element RequiredFieldValidator3 {get {if (_RequiredFieldValidator3 == null) {_RequiredFieldValidator3 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator3");}; return _RequiredFieldValidator3;}} private Element _RequiredFieldValidator3;
		public jQueryObject RequiredFieldValidator3J {get {if (_RequiredFieldValidator3J == null) {_RequiredFieldValidator3J = jQuery.Select("#" + clientId + "_RequiredFieldValidator3");}; return _RequiredFieldValidator3J;}} private jQueryObject _RequiredFieldValidator3J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.HtmlControls.HtmlInputButton", ElementGetter("Element"));
		public Element Button3 {get {if (_Button3 == null) {_Button3 = (Element)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private Element _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Customvalidator3 {get {if (_Customvalidator3 == null) {_Customvalidator3 = (Element)Document.GetElementById(clientId + "_Customvalidator3");}; return _Customvalidator3;}} private Element _Customvalidator3;
		public jQueryObject Customvalidator3J {get {if (_Customvalidator3J == null) {_Customvalidator3J = jQuery.Select("#" + clientId + "_Customvalidator3");}; return _Customvalidator3J;}} private jQueryObject _Customvalidator3J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public DivElement PanelPic {get {if (_PanelPic == null) {_PanelPic = (DivElement)Document.GetElementById(clientId + "_PanelPic");}; return _PanelPic;}} private DivElement _PanelPic;
		public jQueryObject PanelPicJ {get {if (_PanelPicJ == null) {_PanelPicJ = jQuery.Select("#" + clientId + "_PanelPic");}; return _PanelPicJ;}} private jQueryObject _PanelPicJ;
		public Element PicUc {get {if (_PicUc == null) {_PicUc = (Element)Document.GetElementById(clientId + "_PicUc");}; return _PicUc;}} private Element _PicUc;
		public jQueryObject PicUcJ {get {if (_PicUcJ == null) {_PicUcJ = jQuery.Select("#" + clientId + "_PicUc");}; return _PicUcJ;}} private jQueryObject _PicUcJ;//mappings.Add("Spotted.Controls.Pic", ElementGetter("Element"));
		public DivElement PanelSaved {get {if (_PanelSaved == null) {_PanelSaved = (DivElement)Document.GetElementById(clientId + "_PanelSaved");}; return _PanelSaved;}} private DivElement _PanelSaved;
		public jQueryObject PanelSavedJ {get {if (_PanelSavedJ == null) {_PanelSavedJ = jQuery.Select("#" + clientId + "_PanelSaved");}; return _PanelSavedJ;}} private jQueryObject _PanelSavedJ;
		public AnchorElement PanelSavedAddEventLink {get {if (_PanelSavedAddEventLink == null) {_PanelSavedAddEventLink = (AnchorElement)Document.GetElementById(clientId + "_PanelSavedAddEventLink");}; return _PanelSavedAddEventLink;}} private AnchorElement _PanelSavedAddEventLink;
		public jQueryObject PanelSavedAddEventLinkJ {get {if (_PanelSavedAddEventLinkJ == null) {_PanelSavedAddEventLinkJ = jQuery.Select("#" + clientId + "_PanelSavedAddEventLink");}; return _PanelSavedAddEventLinkJ;}} private jQueryObject _PanelSavedAddEventLinkJ;
		public AnchorElement PanelSavedVenueLink {get {if (_PanelSavedVenueLink == null) {_PanelSavedVenueLink = (AnchorElement)Document.GetElementById(clientId + "_PanelSavedVenueLink");}; return _PanelSavedVenueLink;}} private AnchorElement _PanelSavedVenueLink;
		public jQueryObject PanelSavedVenueLinkJ {get {if (_PanelSavedVenueLinkJ == null) {_PanelSavedVenueLinkJ = jQuery.Select("#" + clientId + "_PanelSavedVenueLink");}; return _PanelSavedVenueLinkJ;}} private jQueryObject _PanelSavedVenueLinkJ;
		public Element HeaderH1 {get {if (_HeaderH1 == null) {_HeaderH1 = (Element)Document.GetElementById(clientId + "_HeaderH1");}; return _HeaderH1;}} private Element _HeaderH1;
		public jQueryObject HeaderH1J {get {if (_HeaderH1J == null) {_HeaderH1J = jQuery.Select("#" + clientId + "_HeaderH1");}; return _HeaderH1J;}} private jQueryObject _HeaderH1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelEditError {get {if (_PanelEditError == null) {_PanelEditError = (DivElement)Document.GetElementById(clientId + "_PanelEditError");}; return _PanelEditError;}} private DivElement _PanelEditError;
		public jQueryObject PanelEditErrorJ {get {if (_PanelEditErrorJ == null) {_PanelEditErrorJ = jQuery.Select("#" + clientId + "_PanelEditError");}; return _PanelEditErrorJ;}} private jQueryObject _PanelEditErrorJ;
		public Js.Controls.Picker.Controller PanelDetailsPlacePicker {get {return (Js.Controls.Picker.Controller) Script.Eval(clientId + "_PanelDetailsPlacePickerController");}}
		public DivElement PanelPostcodeCheck {get {if (_PanelPostcodeCheck == null) {_PanelPostcodeCheck = (DivElement)Document.GetElementById(clientId + "_PanelPostcodeCheck");}; return _PanelPostcodeCheck;}} private DivElement _PanelPostcodeCheck;
		public jQueryObject PanelPostcodeCheckJ {get {if (_PanelPostcodeCheckJ == null) {_PanelPostcodeCheckJ = jQuery.Select("#" + clientId + "_PanelPostcodeCheck");}; return _PanelPostcodeCheckJ;}} private jQueryObject _PanelPostcodeCheckJ;
		public Element PanelPostcodeCheckDataGrid {get {if (_PanelPostcodeCheckDataGrid == null) {_PanelPostcodeCheckDataGrid = (Element)Document.GetElementById(clientId + "_PanelPostcodeCheckDataGrid");}; return _PanelPostcodeCheckDataGrid;}} private Element _PanelPostcodeCheckDataGrid;
		public jQueryObject PanelPostcodeCheckDataGridJ {get {if (_PanelPostcodeCheckDataGridJ == null) {_PanelPostcodeCheckDataGridJ = jQuery.Select("#" + clientId + "_PanelPostcodeCheckDataGrid");}; return _PanelPostcodeCheckDataGridJ;}} private jQueryObject _PanelPostcodeCheckDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public CheckBoxElement PanelPostcodeCheckNewCheckBox {get {if (_PanelPostcodeCheckNewCheckBox == null) {_PanelPostcodeCheckNewCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_PanelPostcodeCheckNewCheckBox");}; return _PanelPostcodeCheckNewCheckBox;}} private CheckBoxElement _PanelPostcodeCheckNewCheckBox;
		public jQueryObject PanelPostcodeCheckNewCheckBoxJ {get {if (_PanelPostcodeCheckNewCheckBoxJ == null) {_PanelPostcodeCheckNewCheckBoxJ = jQuery.Select("#" + clientId + "_PanelPostcodeCheckNewCheckBox");}; return _PanelPostcodeCheckNewCheckBoxJ;}} private jQueryObject _PanelPostcodeCheckNewCheckBoxJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
