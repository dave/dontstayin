//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.BuddyControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.BuddyImporter", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.FindYourFriends
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
		public ImageElement TopIcon {get {if (_TopIcon == null) {_TopIcon = (ImageElement)Document.GetElementById(clientId + "_TopIcon");}; return _TopIcon;}} private ImageElement _TopIcon;
		public jQueryObject TopIconJ {get {if (_TopIconJ == null) {_TopIconJ = jQuery.Select("#" + clientId + "_TopIcon");}; return _TopIconJ;}} private jQueryObject _TopIconJ;
		public Element SearchTypeHolder {get {if (_SearchTypeHolder == null) {_SearchTypeHolder = (Element)Document.GetElementById(clientId + "_SearchTypeHolder");}; return _SearchTypeHolder;}} private Element _SearchTypeHolder;
		public jQueryObject SearchTypeHolderJ {get {if (_SearchTypeHolderJ == null) {_SearchTypeHolderJ = jQuery.Select("#" + clientId + "_SearchTypeHolder");}; return _SearchTypeHolderJ;}} private jQueryObject _SearchTypeHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement SearchType1 {get {if (_SearchType1 == null) {_SearchType1 = (CheckBoxElement)Document.GetElementById(clientId + "_SearchType1");}; return _SearchType1;}} private CheckBoxElement _SearchType1;
		public jQueryObject SearchType1J {get {if (_SearchType1J == null) {_SearchType1J = jQuery.Select("#" + clientId + "_SearchType1");}; return _SearchType1J;}} private jQueryObject _SearchType1J;
		public CheckBoxElement SearchType2 {get {if (_SearchType2 == null) {_SearchType2 = (CheckBoxElement)Document.GetElementById(clientId + "_SearchType2");}; return _SearchType2;}} private CheckBoxElement _SearchType2;
		public jQueryObject SearchType2J {get {if (_SearchType2J == null) {_SearchType2J = jQuery.Select("#" + clientId + "_SearchType2");}; return _SearchType2J;}} private jQueryObject _SearchType2J;
		public CheckBoxElement SearchType3 {get {if (_SearchType3 == null) {_SearchType3 = (CheckBoxElement)Document.GetElementById(clientId + "_SearchType3");}; return _SearchType3;}} private CheckBoxElement _SearchType3;
		public jQueryObject SearchType3J {get {if (_SearchType3J == null) {_SearchType3J = jQuery.Select("#" + clientId + "_SearchType3");}; return _SearchType3J;}} private jQueryObject _SearchType3J;
		public CheckBoxElement SearchType4 {get {if (_SearchType4 == null) {_SearchType4 = (CheckBoxElement)Document.GetElementById(clientId + "_SearchType4");}; return _SearchType4;}} private CheckBoxElement _SearchType4;
		public jQueryObject SearchType4J {get {if (_SearchType4J == null) {_SearchType4J = jQuery.Select("#" + clientId + "_SearchType4");}; return _SearchType4J;}} private jQueryObject _SearchType4J;
		public InputElement uiUserName {get {if (_uiUserName == null) {_uiUserName = (InputElement)Document.GetElementById(clientId + "_uiUserName");}; return _uiUserName;}} private InputElement _uiUserName;
		public jQueryObject uiUserNameJ {get {if (_uiUserNameJ == null) {_uiUserNameJ = jQuery.Select("#" + clientId + "_uiUserName");}; return _uiUserNameJ;}} private jQueryObject _uiUserNameJ;
		public InputElement uiUserNameButton {get {if (_uiUserNameButton == null) {_uiUserNameButton = (InputElement)Document.GetElementById(clientId + "_uiUserNameButton");}; return _uiUserNameButton;}} private InputElement _uiUserNameButton;
		public jQueryObject uiUserNameButtonJ {get {if (_uiUserNameButtonJ == null) {_uiUserNameButtonJ = jQuery.Select("#" + clientId + "_uiUserNameButton");}; return _uiUserNameButtonJ;}} private jQueryObject _uiUserNameButtonJ;
		public Element uiUserNameBuddyControl {get {if (_uiUserNameBuddyControl == null) {_uiUserNameBuddyControl = (Element)Document.GetElementById(clientId + "_uiUserNameBuddyControl");}; return _uiUserNameBuddyControl;}} private Element _uiUserNameBuddyControl;
		public jQueryObject uiUserNameBuddyControlJ {get {if (_uiUserNameBuddyControlJ == null) {_uiUserNameBuddyControlJ = jQuery.Select("#" + clientId + "_uiUserNameBuddyControl");}; return _uiUserNameBuddyControlJ;}} private jQueryObject _uiUserNameBuddyControlJ;//mappings.Add("Spotted.Controls.BuddyControl", ElementGetter("Element"));
		public InputElement uiSpotterCode {get {if (_uiSpotterCode == null) {_uiSpotterCode = (InputElement)Document.GetElementById(clientId + "_uiSpotterCode");}; return _uiSpotterCode;}} private InputElement _uiSpotterCode;
		public jQueryObject uiSpotterCodeJ {get {if (_uiSpotterCodeJ == null) {_uiSpotterCodeJ = jQuery.Select("#" + clientId + "_uiSpotterCode");}; return _uiSpotterCodeJ;}} private jQueryObject _uiSpotterCodeJ;
		public InputElement uiSpotterCodeButton {get {if (_uiSpotterCodeButton == null) {_uiSpotterCodeButton = (InputElement)Document.GetElementById(clientId + "_uiSpotterCodeButton");}; return _uiSpotterCodeButton;}} private InputElement _uiSpotterCodeButton;
		public jQueryObject uiSpotterCodeButtonJ {get {if (_uiSpotterCodeButtonJ == null) {_uiSpotterCodeButtonJ = jQuery.Select("#" + clientId + "_uiSpotterCodeButton");}; return _uiSpotterCodeButtonJ;}} private jQueryObject _uiSpotterCodeButtonJ;
		public Element uiInvalidSpottedCode {get {if (_uiInvalidSpottedCode == null) {_uiInvalidSpottedCode = (Element)Document.GetElementById(clientId + "_uiInvalidSpottedCode");}; return _uiInvalidSpottedCode;}} private Element _uiInvalidSpottedCode;
		public jQueryObject uiInvalidSpottedCodeJ {get {if (_uiInvalidSpottedCodeJ == null) {_uiInvalidSpottedCodeJ = jQuery.Select("#" + clientId + "_uiInvalidSpottedCode");}; return _uiInvalidSpottedCodeJ;}} private jQueryObject _uiInvalidSpottedCodeJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element uiSpotterBuddyControl {get {if (_uiSpotterBuddyControl == null) {_uiSpotterBuddyControl = (Element)Document.GetElementById(clientId + "_uiSpotterBuddyControl");}; return _uiSpotterBuddyControl;}} private Element _uiSpotterBuddyControl;
		public jQueryObject uiSpotterBuddyControlJ {get {if (_uiSpotterBuddyControlJ == null) {_uiSpotterBuddyControlJ = jQuery.Select("#" + clientId + "_uiSpotterBuddyControl");}; return _uiSpotterBuddyControlJ;}} private jQueryObject _uiSpotterBuddyControlJ;//mappings.Add("Spotted.Controls.BuddyControl", ElementGetter("Element"));
		public InputElement uiFirstName {get {if (_uiFirstName == null) {_uiFirstName = (InputElement)Document.GetElementById(clientId + "_uiFirstName");}; return _uiFirstName;}} private InputElement _uiFirstName;
		public jQueryObject uiFirstNameJ {get {if (_uiFirstNameJ == null) {_uiFirstNameJ = jQuery.Select("#" + clientId + "_uiFirstName");}; return _uiFirstNameJ;}} private jQueryObject _uiFirstNameJ;
		public InputElement uiLastName {get {if (_uiLastName == null) {_uiLastName = (InputElement)Document.GetElementById(clientId + "_uiLastName");}; return _uiLastName;}} private InputElement _uiLastName;
		public jQueryObject uiLastNameJ {get {if (_uiLastNameJ == null) {_uiLastNameJ = jQuery.Select("#" + clientId + "_uiLastName");}; return _uiLastNameJ;}} private jQueryObject _uiLastNameJ;
		public InputElement uiRealNameButton {get {if (_uiRealNameButton == null) {_uiRealNameButton = (InputElement)Document.GetElementById(clientId + "_uiRealNameButton");}; return _uiRealNameButton;}} private InputElement _uiRealNameButton;
		public jQueryObject uiRealNameButtonJ {get {if (_uiRealNameButtonJ == null) {_uiRealNameButtonJ = jQuery.Select("#" + clientId + "_uiRealNameButton");}; return _uiRealNameButtonJ;}} private jQueryObject _uiRealNameButtonJ;
		public Element uiRealNameBuddyControl {get {if (_uiRealNameBuddyControl == null) {_uiRealNameBuddyControl = (Element)Document.GetElementById(clientId + "_uiRealNameBuddyControl");}; return _uiRealNameBuddyControl;}} private Element _uiRealNameBuddyControl;
		public jQueryObject uiRealNameBuddyControlJ {get {if (_uiRealNameBuddyControlJ == null) {_uiRealNameBuddyControlJ = jQuery.Select("#" + clientId + "_uiRealNameBuddyControl");}; return _uiRealNameBuddyControlJ;}} private jQueryObject _uiRealNameBuddyControlJ;//mappings.Add("Spotted.Controls.BuddyControl", ElementGetter("Element"));
		public Element uiBuddyImporter {get {if (_uiBuddyImporter == null) {_uiBuddyImporter = (Element)Document.GetElementById(clientId + "_uiBuddyImporter");}; return _uiBuddyImporter;}} private Element _uiBuddyImporter;
		public jQueryObject uiBuddyImporterJ {get {if (_uiBuddyImporterJ == null) {_uiBuddyImporterJ = jQuery.Select("#" + clientId + "_uiBuddyImporter");}; return _uiBuddyImporterJ;}} private jQueryObject _uiBuddyImporterJ;//mappings.Add("Spotted.Controls.BuddyImporter", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
