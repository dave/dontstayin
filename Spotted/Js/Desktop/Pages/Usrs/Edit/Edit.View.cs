//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RegularExpressionValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.Edit
{
	public partial class View
		 : Js.Pages.Usrs.UsrUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PrefsUpdatePanel {get {if (_PrefsUpdatePanel == null) {_PrefsUpdatePanel = (DivElement)Document.GetElementById(clientId + "_PrefsUpdatePanel");}; return _PrefsUpdatePanel;}} private DivElement _PrefsUpdatePanel;
		public jQueryObject PrefsUpdatePanelJ {get {if (_PrefsUpdatePanelJ == null) {_PrefsUpdatePanelJ = jQuery.Select("#" + clientId + "_PrefsUpdatePanel");}; return _PrefsUpdatePanelJ;}} private jQueryObject _PrefsUpdatePanelJ;
		public Element ValidationSummary {get {if (_ValidationSummary == null) {_ValidationSummary = (Element)Document.GetElementById(clientId + "_ValidationSummary");}; return _ValidationSummary;}} private Element _ValidationSummary;
		public jQueryObject ValidationSummaryJ {get {if (_ValidationSummaryJ == null) {_ValidationSummaryJ = jQuery.Select("#" + clientId + "_ValidationSummary");}; return _ValidationSummaryJ;}} private jQueryObject _ValidationSummaryJ;//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
		public Element SuccessDiv {get {if (_SuccessDiv == null) {_SuccessDiv = (Element)Document.GetElementById(clientId + "_SuccessDiv");}; return _SuccessDiv;}} private Element _SuccessDiv;
		public jQueryObject SuccessDivJ {get {if (_SuccessDivJ == null) {_SuccessDivJ = jQuery.Select("#" + clientId + "_SuccessDiv");}; return _SuccessDivJ;}} private jQueryObject _SuccessDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement FirstName {get {if (_FirstName == null) {_FirstName = (InputElement)Document.GetElementById(clientId + "_FirstName");}; return _FirstName;}} private InputElement _FirstName;
		public jQueryObject FirstNameJ {get {if (_FirstNameJ == null) {_FirstNameJ = jQuery.Select("#" + clientId + "_FirstName");}; return _FirstNameJ;}} private jQueryObject _FirstNameJ;
		public InputElement LastName {get {if (_LastName == null) {_LastName = (InputElement)Document.GetElementById(clientId + "_LastName");}; return _LastName;}} private InputElement _LastName;
		public jQueryObject LastNameJ {get {if (_LastNameJ == null) {_LastNameJ = jQuery.Select("#" + clientId + "_LastName");}; return _LastNameJ;}} private jQueryObject _LastNameJ;
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element Requiredfieldvalidator3 {get {if (_Requiredfieldvalidator3 == null) {_Requiredfieldvalidator3 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator3");}; return _Requiredfieldvalidator3;}} private Element _Requiredfieldvalidator3;
		public jQueryObject Requiredfieldvalidator3J {get {if (_Requiredfieldvalidator3J == null) {_Requiredfieldvalidator3J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator3");}; return _Requiredfieldvalidator3J;}} private jQueryObject _Requiredfieldvalidator3J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public InputElement NickName {get {if (_NickName == null) {_NickName = (InputElement)Document.GetElementById(clientId + "_NickName");}; return _NickName;}} private InputElement _NickName;
		public jQueryObject NickNameJ {get {if (_NickNameJ == null) {_NickNameJ = jQuery.Select("#" + clientId + "_NickName");}; return _NickNameJ;}} private jQueryObject _NickNameJ;
		public Element Requiredfieldvalidator49999 {get {if (_Requiredfieldvalidator49999 == null) {_Requiredfieldvalidator49999 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator49999");}; return _Requiredfieldvalidator49999;}} private Element _Requiredfieldvalidator49999;
		public jQueryObject Requiredfieldvalidator49999J {get {if (_Requiredfieldvalidator49999J == null) {_Requiredfieldvalidator49999J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator49999");}; return _Requiredfieldvalidator49999J;}} private jQueryObject _Requiredfieldvalidator49999J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element Regularexpressionvalidator99 {get {if (_Regularexpressionvalidator99 == null) {_Regularexpressionvalidator99 = (Element)Document.GetElementById(clientId + "_Regularexpressionvalidator99");}; return _Regularexpressionvalidator99;}} private Element _Regularexpressionvalidator99;
		public jQueryObject Regularexpressionvalidator99J {get {if (_Regularexpressionvalidator99J == null) {_Regularexpressionvalidator99J = jQuery.Select("#" + clientId + "_Regularexpressionvalidator99");}; return _Regularexpressionvalidator99J;}} private jQueryObject _Regularexpressionvalidator99J;//mappings.Add("System.Web.UI.WebControls.RegularExpressionValidator", ElementGetter("Element"));
		public Element Customvalidator5 {get {if (_Customvalidator5 == null) {_Customvalidator5 = (Element)Document.GetElementById(clientId + "_Customvalidator5");}; return _Customvalidator5;}} private Element _Customvalidator5;
		public jQueryObject Customvalidator5J {get {if (_Customvalidator5J == null) {_Customvalidator5J = jQuery.Select("#" + clientId + "_Customvalidator5");}; return _Customvalidator5J;}} private jQueryObject _Customvalidator5J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public InputElement Email {get {if (_Email == null) {_Email = (InputElement)Document.GetElementById(clientId + "_Email");}; return _Email;}} private InputElement _Email;
		public jQueryObject EmailJ {get {if (_EmailJ == null) {_EmailJ = jQuery.Select("#" + clientId + "_Email");}; return _EmailJ;}} private jQueryObject _EmailJ;
		public Element Requiredfieldvalidator2 {get {if (_Requiredfieldvalidator2 == null) {_Requiredfieldvalidator2 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator2");}; return _Requiredfieldvalidator2;}} private Element _Requiredfieldvalidator2;
		public jQueryObject Requiredfieldvalidator2J {get {if (_Requiredfieldvalidator2J == null) {_Requiredfieldvalidator2J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator2");}; return _Requiredfieldvalidator2J;}} private jQueryObject _Requiredfieldvalidator2J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element EmailRegex {get {if (_EmailRegex == null) {_EmailRegex = (Element)Document.GetElementById(clientId + "_EmailRegex");}; return _EmailRegex;}} private Element _EmailRegex;
		public jQueryObject EmailRegexJ {get {if (_EmailRegexJ == null) {_EmailRegexJ = jQuery.Select("#" + clientId + "_EmailRegex");}; return _EmailRegexJ;}} private jQueryObject _EmailRegexJ;//mappings.Add("System.Web.UI.WebControls.RegularExpressionValidator", ElementGetter("Element"));
		public Element emailDuplicateValidator {get {if (_emailDuplicateValidator == null) {_emailDuplicateValidator = (Element)Document.GetElementById(clientId + "_emailDuplicateValidator");}; return _emailDuplicateValidator;}} private Element _emailDuplicateValidator;
		public jQueryObject emailDuplicateValidatorJ {get {if (_emailDuplicateValidatorJ == null) {_emailDuplicateValidatorJ = jQuery.Select("#" + clientId + "_emailDuplicateValidator");}; return _emailDuplicateValidatorJ;}} private jQueryObject _emailDuplicateValidatorJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public SelectElement DialingCodeDropDown {get {if (_DialingCodeDropDown == null) {_DialingCodeDropDown = (SelectElement)Document.GetElementById(clientId + "_DialingCodeDropDown");}; return _DialingCodeDropDown;}} private SelectElement _DialingCodeDropDown;
		public jQueryObject DialingCodeDropDownJ {get {if (_DialingCodeDropDownJ == null) {_DialingCodeDropDownJ = jQuery.Select("#" + clientId + "_DialingCodeDropDown");}; return _DialingCodeDropDownJ;}} private jQueryObject _DialingCodeDropDownJ;
		public InputElement MobileNumber {get {if (_MobileNumber == null) {_MobileNumber = (InputElement)Document.GetElementById(clientId + "_MobileNumber");}; return _MobileNumber;}} private InputElement _MobileNumber;
		public jQueryObject MobileNumberJ {get {if (_MobileNumberJ == null) {_MobileNumberJ = jQuery.Select("#" + clientId + "_MobileNumber");}; return _MobileNumberJ;}} private jQueryObject _MobileNumberJ;
		public Element DialingCodeOtherSpan {get {if (_DialingCodeOtherSpan == null) {_DialingCodeOtherSpan = (Element)Document.GetElementById(clientId + "_DialingCodeOtherSpan");}; return _DialingCodeOtherSpan;}} private Element _DialingCodeOtherSpan;
		public jQueryObject DialingCodeOtherSpanJ {get {if (_DialingCodeOtherSpanJ == null) {_DialingCodeOtherSpanJ = jQuery.Select("#" + clientId + "_DialingCodeOtherSpan");}; return _DialingCodeOtherSpanJ;}} private jQueryObject _DialingCodeOtherSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement DialingCodeOther {get {if (_DialingCodeOther == null) {_DialingCodeOther = (InputElement)Document.GetElementById(clientId + "_DialingCodeOther");}; return _DialingCodeOther;}} private InputElement _DialingCodeOther;
		public jQueryObject DialingCodeOtherJ {get {if (_DialingCodeOtherJ == null) {_DialingCodeOtherJ = jQuery.Select("#" + clientId + "_DialingCodeOther");}; return _DialingCodeOtherJ;}} private jQueryObject _DialingCodeOtherJ;
		public CheckBoxElement SexMale {get {if (_SexMale == null) {_SexMale = (CheckBoxElement)Document.GetElementById(clientId + "_SexMale");}; return _SexMale;}} private CheckBoxElement _SexMale;
		public jQueryObject SexMaleJ {get {if (_SexMaleJ == null) {_SexMaleJ = jQuery.Select("#" + clientId + "_SexMale");}; return _SexMaleJ;}} private jQueryObject _SexMaleJ;
		public CheckBoxElement SexFemale {get {if (_SexFemale == null) {_SexFemale = (CheckBoxElement)Document.GetElementById(clientId + "_SexFemale");}; return _SexFemale;}} private CheckBoxElement _SexFemale;
		public jQueryObject SexFemaleJ {get {if (_SexFemaleJ == null) {_SexFemaleJ = jQuery.Select("#" + clientId + "_SexFemale");}; return _SexFemaleJ;}} private jQueryObject _SexFemaleJ;
		public Element CustomValidator1 {get {if (_CustomValidator1 == null) {_CustomValidator1 = (Element)Document.GetElementById(clientId + "_CustomValidator1");}; return _CustomValidator1;}} private Element _CustomValidator1;
		public jQueryObject CustomValidator1J {get {if (_CustomValidator1J == null) {_CustomValidator1J = jQuery.Select("#" + clientId + "_CustomValidator1");}; return _CustomValidator1J;}} private jQueryObject _CustomValidator1J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public InputElement DateOfBirthYear {get {if (_DateOfBirthYear == null) {_DateOfBirthYear = (InputElement)Document.GetElementById(clientId + "_DateOfBirthYear");}; return _DateOfBirthYear;}} private InputElement _DateOfBirthYear;
		public jQueryObject DateOfBirthYearJ {get {if (_DateOfBirthYearJ == null) {_DateOfBirthYearJ = jQuery.Select("#" + clientId + "_DateOfBirthYear");}; return _DateOfBirthYearJ;}} private jQueryObject _DateOfBirthYearJ;
		public InputElement DateOfBirthMonth {get {if (_DateOfBirthMonth == null) {_DateOfBirthMonth = (InputElement)Document.GetElementById(clientId + "_DateOfBirthMonth");}; return _DateOfBirthMonth;}} private InputElement _DateOfBirthMonth;
		public jQueryObject DateOfBirthMonthJ {get {if (_DateOfBirthMonthJ == null) {_DateOfBirthMonthJ = jQuery.Select("#" + clientId + "_DateOfBirthMonth");}; return _DateOfBirthMonthJ;}} private jQueryObject _DateOfBirthMonthJ;
		public InputElement DateOfBirthDay {get {if (_DateOfBirthDay == null) {_DateOfBirthDay = (InputElement)Document.GetElementById(clientId + "_DateOfBirthDay");}; return _DateOfBirthDay;}} private InputElement _DateOfBirthDay;
		public jQueryObject DateOfBirthDayJ {get {if (_DateOfBirthDayJ == null) {_DateOfBirthDayJ = jQuery.Select("#" + clientId + "_DateOfBirthDay");}; return _DateOfBirthDayJ;}} private jQueryObject _DateOfBirthDayJ;
		public Element Customvalidator8 {get {if (_Customvalidator8 == null) {_Customvalidator8 = (Element)Document.GetElementById(clientId + "_Customvalidator8");}; return _Customvalidator8;}} private Element _Customvalidator8;
		public jQueryObject Customvalidator8J {get {if (_Customvalidator8J == null) {_Customvalidator8J = jQuery.Select("#" + clientId + "_Customvalidator8");}; return _Customvalidator8J;}} private jQueryObject _Customvalidator8J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element Customvalidator9 {get {if (_Customvalidator9 == null) {_Customvalidator9 = (Element)Document.GetElementById(clientId + "_Customvalidator9");}; return _Customvalidator9;}} private Element _Customvalidator9;
		public jQueryObject Customvalidator9J {get {if (_Customvalidator9J == null) {_Customvalidator9J = jQuery.Select("#" + clientId + "_Customvalidator9");}; return _Customvalidator9J;}} private jQueryObject _Customvalidator9J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public SelectElement FavouriteMusicDropDownList {get {if (_FavouriteMusicDropDownList == null) {_FavouriteMusicDropDownList = (SelectElement)Document.GetElementById(clientId + "_FavouriteMusicDropDownList");}; return _FavouriteMusicDropDownList;}} private SelectElement _FavouriteMusicDropDownList;
		public jQueryObject FavouriteMusicDropDownListJ {get {if (_FavouriteMusicDropDownListJ == null) {_FavouriteMusicDropDownListJ = jQuery.Select("#" + clientId + "_FavouriteMusicDropDownList");}; return _FavouriteMusicDropDownListJ;}} private jQueryObject _FavouriteMusicDropDownListJ;
		public Element Customvalidator4 {get {if (_Customvalidator4 == null) {_Customvalidator4 = (Element)Document.GetElementById(clientId + "_Customvalidator4");}; return _Customvalidator4;}} private Element _Customvalidator4;
		public jQueryObject Customvalidator4J {get {if (_Customvalidator4J == null) {_Customvalidator4J = jQuery.Select("#" + clientId + "_Customvalidator4");}; return _Customvalidator4J;}} private jQueryObject _Customvalidator4J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Js.Controls.Picker.Controller HomeTownPlacePicker {get {return (Js.Controls.Picker.Controller) Script.Eval(clientId + "_HomeTownPlacePickerController");}}
		public InputElement AddressStreetTextBox {get {if (_AddressStreetTextBox == null) {_AddressStreetTextBox = (InputElement)Document.GetElementById(clientId + "_AddressStreetTextBox");}; return _AddressStreetTextBox;}} private InputElement _AddressStreetTextBox;
		public jQueryObject AddressStreetTextBoxJ {get {if (_AddressStreetTextBoxJ == null) {_AddressStreetTextBoxJ = jQuery.Select("#" + clientId + "_AddressStreetTextBox");}; return _AddressStreetTextBoxJ;}} private jQueryObject _AddressStreetTextBoxJ;
		public InputElement AddressAreaTextBox {get {if (_AddressAreaTextBox == null) {_AddressAreaTextBox = (InputElement)Document.GetElementById(clientId + "_AddressAreaTextBox");}; return _AddressAreaTextBox;}} private InputElement _AddressAreaTextBox;
		public jQueryObject AddressAreaTextBoxJ {get {if (_AddressAreaTextBoxJ == null) {_AddressAreaTextBoxJ = jQuery.Select("#" + clientId + "_AddressAreaTextBox");}; return _AddressAreaTextBoxJ;}} private jQueryObject _AddressAreaTextBoxJ;
		public InputElement AddressTownTextBox {get {if (_AddressTownTextBox == null) {_AddressTownTextBox = (InputElement)Document.GetElementById(clientId + "_AddressTownTextBox");}; return _AddressTownTextBox;}} private InputElement _AddressTownTextBox;
		public jQueryObject AddressTownTextBoxJ {get {if (_AddressTownTextBoxJ == null) {_AddressTownTextBoxJ = jQuery.Select("#" + clientId + "_AddressTownTextBox");}; return _AddressTownTextBoxJ;}} private jQueryObject _AddressTownTextBoxJ;
		public InputElement AddressCountyTextBox {get {if (_AddressCountyTextBox == null) {_AddressCountyTextBox = (InputElement)Document.GetElementById(clientId + "_AddressCountyTextBox");}; return _AddressCountyTextBox;}} private InputElement _AddressCountyTextBox;
		public jQueryObject AddressCountyTextBoxJ {get {if (_AddressCountyTextBoxJ == null) {_AddressCountyTextBoxJ = jQuery.Select("#" + clientId + "_AddressCountyTextBox");}; return _AddressCountyTextBoxJ;}} private jQueryObject _AddressCountyTextBoxJ;
		public InputElement AddressPostcodeTextBox {get {if (_AddressPostcodeTextBox == null) {_AddressPostcodeTextBox = (InputElement)Document.GetElementById(clientId + "_AddressPostcodeTextBox");}; return _AddressPostcodeTextBox;}} private InputElement _AddressPostcodeTextBox;
		public jQueryObject AddressPostcodeTextBoxJ {get {if (_AddressPostcodeTextBoxJ == null) {_AddressPostcodeTextBoxJ = jQuery.Select("#" + clientId + "_AddressPostcodeTextBox");}; return _AddressPostcodeTextBoxJ;}} private jQueryObject _AddressPostcodeTextBoxJ;
		public SelectElement AddressCountryDropDownList {get {if (_AddressCountryDropDownList == null) {_AddressCountryDropDownList = (SelectElement)Document.GetElementById(clientId + "_AddressCountryDropDownList");}; return _AddressCountryDropDownList;}} private SelectElement _AddressCountryDropDownList;
		public jQueryObject AddressCountryDropDownListJ {get {if (_AddressCountryDropDownListJ == null) {_AddressCountryDropDownListJ = jQuery.Select("#" + clientId + "_AddressCountryDropDownList");}; return _AddressCountryDropDownListJ;}} private jQueryObject _AddressCountryDropDownListJ;
		public CheckBoxElement IsDjYes {get {if (_IsDjYes == null) {_IsDjYes = (CheckBoxElement)Document.GetElementById(clientId + "_IsDjYes");}; return _IsDjYes;}} private CheckBoxElement _IsDjYes;
		public jQueryObject IsDjYesJ {get {if (_IsDjYesJ == null) {_IsDjYesJ = jQuery.Select("#" + clientId + "_IsDjYes");}; return _IsDjYesJ;}} private jQueryObject _IsDjYesJ;
		public CheckBoxElement IsDjNo {get {if (_IsDjNo == null) {_IsDjNo = (CheckBoxElement)Document.GetElementById(clientId + "_IsDjNo");}; return _IsDjNo;}} private CheckBoxElement _IsDjNo;
		public jQueryObject IsDjNoJ {get {if (_IsDjNoJ == null) {_IsDjNoJ = jQuery.Select("#" + clientId + "_IsDjNo");}; return _IsDjNoJ;}} private jQueryObject _IsDjNoJ;
		public Element CustomValidatorIsDj {get {if (_CustomValidatorIsDj == null) {_CustomValidatorIsDj = (Element)Document.GetElementById(clientId + "_CustomValidatorIsDj");}; return _CustomValidatorIsDj;}} private Element _CustomValidatorIsDj;
		public jQueryObject CustomValidatorIsDjJ {get {if (_CustomValidatorIsDjJ == null) {_CustomValidatorIsDjJ = jQuery.Select("#" + clientId + "_CustomValidatorIsDj");}; return _CustomValidatorIsDjJ;}} private jQueryObject _CustomValidatorIsDjJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element PrefsUpdateButton {get {if (_PrefsUpdateButton == null) {_PrefsUpdateButton = (Element)Document.GetElementById(clientId + "_PrefsUpdateButton");}; return _PrefsUpdateButton;}} private Element _PrefsUpdateButton;
		public jQueryObject PrefsUpdateButtonJ {get {if (_PrefsUpdateButtonJ == null) {_PrefsUpdateButtonJ = jQuery.Select("#" + clientId + "_PrefsUpdateButton");}; return _PrefsUpdateButtonJ;}} private jQueryObject _PrefsUpdateButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
