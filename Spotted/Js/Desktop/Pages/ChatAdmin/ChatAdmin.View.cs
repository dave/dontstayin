//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.ChatAdmin
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
		public DivElement AdminPanel {get {if (_AdminPanel == null) {_AdminPanel = (DivElement)Document.GetElementById(clientId + "_AdminPanel");}; return _AdminPanel;}} private DivElement _AdminPanel;
		public jQueryObject AdminPanelJ {get {if (_AdminPanelJ == null) {_AdminPanelJ = jQuery.Select("#" + clientId + "_AdminPanel");}; return _AdminPanelJ;}} private jQueryObject _AdminPanelJ;
		public DivElement PanelOptions {get {if (_PanelOptions == null) {_PanelOptions = (DivElement)Document.GetElementById(clientId + "_PanelOptions");}; return _PanelOptions;}} private DivElement _PanelOptions;
		public jQueryObject PanelOptionsJ {get {if (_PanelOptionsJ == null) {_PanelOptionsJ = jQuery.Select("#" + clientId + "_PanelOptions");}; return _PanelOptionsJ;}} private jQueryObject _PanelOptionsJ;
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement ThreadSubjectAnchor {get {if (_ThreadSubjectAnchor == null) {_ThreadSubjectAnchor = (AnchorElement)Document.GetElementById(clientId + "_ThreadSubjectAnchor");}; return _ThreadSubjectAnchor;}} private AnchorElement _ThreadSubjectAnchor;
		public jQueryObject ThreadSubjectAnchorJ {get {if (_ThreadSubjectAnchorJ == null) {_ThreadSubjectAnchorJ = jQuery.Select("#" + clientId + "_ThreadSubjectAnchor");}; return _ThreadSubjectAnchorJ;}} private jQueryObject _ThreadSubjectAnchorJ;
		public AnchorElement ThreadForumAnchor {get {if (_ThreadForumAnchor == null) {_ThreadForumAnchor = (AnchorElement)Document.GetElementById(clientId + "_ThreadForumAnchor");}; return _ThreadForumAnchor;}} private AnchorElement _ThreadForumAnchor;
		public jQueryObject ThreadForumAnchorJ {get {if (_ThreadForumAnchorJ == null) {_ThreadForumAnchorJ = jQuery.Select("#" + clientId + "_ThreadForumAnchor");}; return _ThreadForumAnchorJ;}} private jQueryObject _ThreadForumAnchorJ;
		public AnchorElement ThreadGroupAnchor {get {if (_ThreadGroupAnchor == null) {_ThreadGroupAnchor = (AnchorElement)Document.GetElementById(clientId + "_ThreadGroupAnchor");}; return _ThreadGroupAnchor;}} private AnchorElement _ThreadGroupAnchor;
		public jQueryObject ThreadGroupAnchorJ {get {if (_ThreadGroupAnchorJ == null) {_ThreadGroupAnchorJ = jQuery.Select("#" + clientId + "_ThreadGroupAnchor");}; return _ThreadGroupAnchorJ;}} private jQueryObject _ThreadGroupAnchorJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element ClosedSpan {get {if (_ClosedSpan == null) {_ClosedSpan = (Element)Document.GetElementById(clientId + "_ClosedSpan");}; return _ClosedSpan;}} private Element _ClosedSpan;
		public jQueryObject ClosedSpanJ {get {if (_ClosedSpanJ == null) {_ClosedSpanJ = jQuery.Select("#" + clientId + "_ClosedSpan");}; return _ClosedSpanJ;}} private jQueryObject _ClosedSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement ClosedCheckBox {get {if (_ClosedCheckBox == null) {_ClosedCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_ClosedCheckBox");}; return _ClosedCheckBox;}} private CheckBoxElement _ClosedCheckBox;
		public jQueryObject ClosedCheckBoxJ {get {if (_ClosedCheckBoxJ == null) {_ClosedCheckBoxJ = jQuery.Select("#" + clientId + "_ClosedCheckBox");}; return _ClosedCheckBoxJ;}} private jQueryObject _ClosedCheckBoxJ;
		public Element NewsSpan {get {if (_NewsSpan == null) {_NewsSpan = (Element)Document.GetElementById(clientId + "_NewsSpan");}; return _NewsSpan;}} private Element _NewsSpan;
		public jQueryObject NewsSpanJ {get {if (_NewsSpanJ == null) {_NewsSpanJ = jQuery.Select("#" + clientId + "_NewsSpan");}; return _NewsSpanJ;}} private jQueryObject _NewsSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement NewsCheckBox {get {if (_NewsCheckBox == null) {_NewsCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_NewsCheckBox");}; return _NewsCheckBox;}} private CheckBoxElement _NewsCheckBox;
		public jQueryObject NewsCheckBoxJ {get {if (_NewsCheckBoxJ == null) {_NewsCheckBoxJ = jQuery.Select("#" + clientId + "_NewsCheckBox");}; return _NewsCheckBoxJ;}} private jQueryObject _NewsCheckBoxJ;
		public Element PrivateSpan {get {if (_PrivateSpan == null) {_PrivateSpan = (Element)Document.GetElementById(clientId + "_PrivateSpan");}; return _PrivateSpan;}} private Element _PrivateSpan;
		public jQueryObject PrivateSpanJ {get {if (_PrivateSpanJ == null) {_PrivateSpanJ = jQuery.Select("#" + clientId + "_PrivateSpan");}; return _PrivateSpanJ;}} private jQueryObject _PrivateSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement PrivateCheckBox {get {if (_PrivateCheckBox == null) {_PrivateCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_PrivateCheckBox");}; return _PrivateCheckBox;}} private CheckBoxElement _PrivateCheckBox;
		public jQueryObject PrivateCheckBoxJ {get {if (_PrivateCheckBoxJ == null) {_PrivateCheckBoxJ = jQuery.Select("#" + clientId + "_PrivateCheckBox");}; return _PrivateCheckBoxJ;}} private jQueryObject _PrivateCheckBoxJ;
		public Element SealedSpan {get {if (_SealedSpan == null) {_SealedSpan = (Element)Document.GetElementById(clientId + "_SealedSpan");}; return _SealedSpan;}} private Element _SealedSpan;
		public jQueryObject SealedSpanJ {get {if (_SealedSpanJ == null) {_SealedSpanJ = jQuery.Select("#" + clientId + "_SealedSpan");}; return _SealedSpanJ;}} private jQueryObject _SealedSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement SealedCheckBox {get {if (_SealedCheckBox == null) {_SealedCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_SealedCheckBox");}; return _SealedCheckBox;}} private CheckBoxElement _SealedCheckBox;
		public jQueryObject SealedCheckBoxJ {get {if (_SealedCheckBoxJ == null) {_SealedCheckBoxJ = jQuery.Select("#" + clientId + "_SealedCheckBox");}; return _SealedCheckBoxJ;}} private jQueryObject _SealedCheckBoxJ;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement ChangeForumPanel {get {if (_ChangeForumPanel == null) {_ChangeForumPanel = (DivElement)Document.GetElementById(clientId + "_ChangeForumPanel");}; return _ChangeForumPanel;}} private DivElement _ChangeForumPanel;
		public jQueryObject ChangeForumPanelJ {get {if (_ChangeForumPanelJ == null) {_ChangeForumPanelJ = jQuery.Select("#" + clientId + "_ChangeForumPanel");}; return _ChangeForumPanelJ;}} private jQueryObject _ChangeForumPanelJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public CheckBoxElement ScopeEvent {get {if (_ScopeEvent == null) {_ScopeEvent = (CheckBoxElement)Document.GetElementById(clientId + "_ScopeEvent");}; return _ScopeEvent;}} private CheckBoxElement _ScopeEvent;
		public jQueryObject ScopeEventJ {get {if (_ScopeEventJ == null) {_ScopeEventJ = jQuery.Select("#" + clientId + "_ScopeEvent");}; return _ScopeEventJ;}} private jQueryObject _ScopeEventJ;
		public CheckBoxElement ScopeEventFuture {get {if (_ScopeEventFuture == null) {_ScopeEventFuture = (CheckBoxElement)Document.GetElementById(clientId + "_ScopeEventFuture");}; return _ScopeEventFuture;}} private CheckBoxElement _ScopeEventFuture;
		public jQueryObject ScopeEventFutureJ {get {if (_ScopeEventFutureJ == null) {_ScopeEventFutureJ = jQuery.Select("#" + clientId + "_ScopeEventFuture");}; return _ScopeEventFutureJ;}} private jQueryObject _ScopeEventFutureJ;
		public CheckBoxElement ScopeEventAttend {get {if (_ScopeEventAttend == null) {_ScopeEventAttend = (CheckBoxElement)Document.GetElementById(clientId + "_ScopeEventAttend");}; return _ScopeEventAttend;}} private CheckBoxElement _ScopeEventAttend;
		public jQueryObject ScopeEventAttendJ {get {if (_ScopeEventAttendJ == null) {_ScopeEventAttendJ = jQuery.Select("#" + clientId + "_ScopeEventAttend");}; return _ScopeEventAttendJ;}} private jQueryObject _ScopeEventAttendJ;
		public CheckBoxElement ScopeVenue {get {if (_ScopeVenue == null) {_ScopeVenue = (CheckBoxElement)Document.GetElementById(clientId + "_ScopeVenue");}; return _ScopeVenue;}} private CheckBoxElement _ScopeVenue;
		public jQueryObject ScopeVenueJ {get {if (_ScopeVenueJ == null) {_ScopeVenueJ = jQuery.Select("#" + clientId + "_ScopeVenue");}; return _ScopeVenueJ;}} private jQueryObject _ScopeVenueJ;
		public CheckBoxElement ScopePlace {get {if (_ScopePlace == null) {_ScopePlace = (CheckBoxElement)Document.GetElementById(clientId + "_ScopePlace");}; return _ScopePlace;}} private CheckBoxElement _ScopePlace;
		public jQueryObject ScopePlaceJ {get {if (_ScopePlaceJ == null) {_ScopePlaceJ = jQuery.Select("#" + clientId + "_ScopePlace");}; return _ScopePlaceJ;}} private jQueryObject _ScopePlaceJ;
		public CheckBoxElement ScopeCountry {get {if (_ScopeCountry == null) {_ScopeCountry = (CheckBoxElement)Document.GetElementById(clientId + "_ScopeCountry");}; return _ScopeCountry;}} private CheckBoxElement _ScopeCountry;
		public jQueryObject ScopeCountryJ {get {if (_ScopeCountryJ == null) {_ScopeCountryJ = jQuery.Select("#" + clientId + "_ScopeCountry");}; return _ScopeCountryJ;}} private jQueryObject _ScopeCountryJ;
		public CheckBoxElement ScopeGeneral {get {if (_ScopeGeneral == null) {_ScopeGeneral = (CheckBoxElement)Document.GetElementById(clientId + "_ScopeGeneral");}; return _ScopeGeneral;}} private CheckBoxElement _ScopeGeneral;
		public jQueryObject ScopeGeneralJ {get {if (_ScopeGeneralJ == null) {_ScopeGeneralJ = jQuery.Select("#" + clientId + "_ScopeGeneral");}; return _ScopeGeneralJ;}} private jQueryObject _ScopeGeneralJ;
		public Element Customvalidator3 {get {if (_Customvalidator3 == null) {_Customvalidator3 = (Element)Document.GetElementById(clientId + "_Customvalidator3");}; return _Customvalidator3;}} private Element _Customvalidator3;
		public jQueryObject Customvalidator3J {get {if (_Customvalidator3J == null) {_Customvalidator3J = jQuery.Select("#" + clientId + "_Customvalidator3");}; return _Customvalidator3J;}} private jQueryObject _Customvalidator3J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiObjectMultiComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiObjectMultiCompleteBehaviour");}}
		public Element Customvalidator4 {get {if (_Customvalidator4 == null) {_Customvalidator4 = (Element)Document.GetElementById(clientId + "_Customvalidator4");}; return _Customvalidator4;}} private Element _Customvalidator4;
		public jQueryObject Customvalidator4J {get {if (_Customvalidator4J == null) {_Customvalidator4J = jQuery.Select("#" + clientId + "_Customvalidator4");}; return _Customvalidator4J;}} private jQueryObject _Customvalidator4J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
