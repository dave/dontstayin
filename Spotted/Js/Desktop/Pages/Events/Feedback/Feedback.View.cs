//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Events.Feedback
{
	public partial class View
		 : Js.Pages.Events.EventUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement EventTicketFeedbackPanel {get {if (_EventTicketFeedbackPanel == null) {_EventTicketFeedbackPanel = (DivElement)Document.GetElementById(clientId + "_EventTicketFeedbackPanel");}; return _EventTicketFeedbackPanel;}} private DivElement _EventTicketFeedbackPanel;
		public jQueryObject EventTicketFeedbackPanelJ {get {if (_EventTicketFeedbackPanelJ == null) {_EventTicketFeedbackPanelJ = jQuery.Select("#" + clientId + "_EventTicketFeedbackPanel");}; return _EventTicketFeedbackPanelJ;}} private jQueryObject _EventTicketFeedbackPanelJ;
		public Element TicketsHeading {get {if (_TicketsHeading == null) {_TicketsHeading = (Element)Document.GetElementById(clientId + "_TicketsHeading");}; return _TicketsHeading;}} private Element _TicketsHeading;
		public jQueryObject TicketsHeadingJ {get {if (_TicketsHeadingJ == null) {_TicketsHeadingJ = jQuery.Select("#" + clientId + "_TicketsHeading");}; return _TicketsHeadingJ;}} private jQueryObject _TicketsHeadingJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element TicketEventDataList {get {if (_TicketEventDataList == null) {_TicketEventDataList = (Element)Document.GetElementById(clientId + "_TicketEventDataList");}; return _TicketEventDataList;}} private Element _TicketEventDataList;
		public jQueryObject TicketEventDataListJ {get {if (_TicketEventDataListJ == null) {_TicketEventDataListJ = jQuery.Select("#" + clientId + "_TicketEventDataList");}; return _TicketEventDataListJ;}} private jQueryObject _TicketEventDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element TicketFeedbackP {get {if (_TicketFeedbackP == null) {_TicketFeedbackP = (Element)Document.GetElementById(clientId + "_TicketFeedbackP");}; return _TicketFeedbackP;}} private Element _TicketFeedbackP;
		public jQueryObject TicketFeedbackPJ {get {if (_TicketFeedbackPJ == null) {_TicketFeedbackPJ = jQuery.Select("#" + clientId + "_TicketFeedbackP");}; return _TicketFeedbackPJ;}} private jQueryObject _TicketFeedbackPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element UsrTicketFeedbackHeaderLabel {get {if (_UsrTicketFeedbackHeaderLabel == null) {_UsrTicketFeedbackHeaderLabel = (Element)Document.GetElementById(clientId + "_UsrTicketFeedbackHeaderLabel");}; return _UsrTicketFeedbackHeaderLabel;}} private Element _UsrTicketFeedbackHeaderLabel;
		public jQueryObject UsrTicketFeedbackHeaderLabelJ {get {if (_UsrTicketFeedbackHeaderLabelJ == null) {_UsrTicketFeedbackHeaderLabelJ = jQuery.Select("#" + clientId + "_UsrTicketFeedbackHeaderLabel");}; return _UsrTicketFeedbackHeaderLabelJ;}} private jQueryObject _UsrTicketFeedbackHeaderLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element UsrTicketResponseGoodLinkButtonDiv {get {if (_UsrTicketResponseGoodLinkButtonDiv == null) {_UsrTicketResponseGoodLinkButtonDiv = (Element)Document.GetElementById(clientId + "_UsrTicketResponseGoodLinkButtonDiv");}; return _UsrTicketResponseGoodLinkButtonDiv;}} private Element _UsrTicketResponseGoodLinkButtonDiv;
		public jQueryObject UsrTicketResponseGoodLinkButtonDivJ {get {if (_UsrTicketResponseGoodLinkButtonDivJ == null) {_UsrTicketResponseGoodLinkButtonDivJ = jQuery.Select("#" + clientId + "_UsrTicketResponseGoodLinkButtonDiv");}; return _UsrTicketResponseGoodLinkButtonDivJ;}} private jQueryObject _UsrTicketResponseGoodLinkButtonDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element UsrTicketResponseGoodLinkButton {get {if (_UsrTicketResponseGoodLinkButton == null) {_UsrTicketResponseGoodLinkButton = (Element)Document.GetElementById(clientId + "_UsrTicketResponseGoodLinkButton");}; return _UsrTicketResponseGoodLinkButton;}} private Element _UsrTicketResponseGoodLinkButton;
		public jQueryObject UsrTicketResponseGoodLinkButtonJ {get {if (_UsrTicketResponseGoodLinkButtonJ == null) {_UsrTicketResponseGoodLinkButtonJ = jQuery.Select("#" + clientId + "_UsrTicketResponseGoodLinkButton");}; return _UsrTicketResponseGoodLinkButtonJ;}} private jQueryObject _UsrTicketResponseGoodLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element UsrTicketResponseGoodDiv {get {if (_UsrTicketResponseGoodDiv == null) {_UsrTicketResponseGoodDiv = (Element)Document.GetElementById(clientId + "_UsrTicketResponseGoodDiv");}; return _UsrTicketResponseGoodDiv;}} private Element _UsrTicketResponseGoodDiv;
		public jQueryObject UsrTicketResponseGoodDivJ {get {if (_UsrTicketResponseGoodDivJ == null) {_UsrTicketResponseGoodDivJ = jQuery.Select("#" + clientId + "_UsrTicketResponseGoodDiv");}; return _UsrTicketResponseGoodDivJ;}} private jQueryObject _UsrTicketResponseGoodDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element UsrTicketResponseBadLinkButtonDiv {get {if (_UsrTicketResponseBadLinkButtonDiv == null) {_UsrTicketResponseBadLinkButtonDiv = (Element)Document.GetElementById(clientId + "_UsrTicketResponseBadLinkButtonDiv");}; return _UsrTicketResponseBadLinkButtonDiv;}} private Element _UsrTicketResponseBadLinkButtonDiv;
		public jQueryObject UsrTicketResponseBadLinkButtonDivJ {get {if (_UsrTicketResponseBadLinkButtonDivJ == null) {_UsrTicketResponseBadLinkButtonDivJ = jQuery.Select("#" + clientId + "_UsrTicketResponseBadLinkButtonDiv");}; return _UsrTicketResponseBadLinkButtonDivJ;}} private jQueryObject _UsrTicketResponseBadLinkButtonDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element UsrTicketResponseBadLinkButton {get {if (_UsrTicketResponseBadLinkButton == null) {_UsrTicketResponseBadLinkButton = (Element)Document.GetElementById(clientId + "_UsrTicketResponseBadLinkButton");}; return _UsrTicketResponseBadLinkButton;}} private Element _UsrTicketResponseBadLinkButton;
		public jQueryObject UsrTicketResponseBadLinkButtonJ {get {if (_UsrTicketResponseBadLinkButtonJ == null) {_UsrTicketResponseBadLinkButtonJ = jQuery.Select("#" + clientId + "_UsrTicketResponseBadLinkButton");}; return _UsrTicketResponseBadLinkButtonJ;}} private jQueryObject _UsrTicketResponseBadLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element UsrTicketResponseBadDiv {get {if (_UsrTicketResponseBadDiv == null) {_UsrTicketResponseBadDiv = (Element)Document.GetElementById(clientId + "_UsrTicketResponseBadDiv");}; return _UsrTicketResponseBadDiv;}} private Element _UsrTicketResponseBadDiv;
		public jQueryObject UsrTicketResponseBadDivJ {get {if (_UsrTicketResponseBadDivJ == null) {_UsrTicketResponseBadDivJ = jQuery.Select("#" + clientId + "_UsrTicketResponseBadDiv");}; return _UsrTicketResponseBadDivJ;}} private jQueryObject _UsrTicketResponseBadDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element UsrTicketFeedbackTextDiv {get {if (_UsrTicketFeedbackTextDiv == null) {_UsrTicketFeedbackTextDiv = (Element)Document.GetElementById(clientId + "_UsrTicketFeedbackTextDiv");}; return _UsrTicketFeedbackTextDiv;}} private Element _UsrTicketFeedbackTextDiv;
		public jQueryObject UsrTicketFeedbackTextDivJ {get {if (_UsrTicketFeedbackTextDivJ == null) {_UsrTicketFeedbackTextDivJ = jQuery.Select("#" + clientId + "_UsrTicketFeedbackTextDiv");}; return _UsrTicketFeedbackTextDivJ;}} private jQueryObject _UsrTicketFeedbackTextDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement UsrTicketFeedbackTextBox {get {if (_UsrTicketFeedbackTextBox == null) {_UsrTicketFeedbackTextBox = (InputElement)Document.GetElementById(clientId + "_UsrTicketFeedbackTextBox");}; return _UsrTicketFeedbackTextBox;}} private InputElement _UsrTicketFeedbackTextBox;
		public jQueryObject UsrTicketFeedbackTextBoxJ {get {if (_UsrTicketFeedbackTextBoxJ == null) {_UsrTicketFeedbackTextBoxJ = jQuery.Select("#" + clientId + "_UsrTicketFeedbackTextBox");}; return _UsrTicketFeedbackTextBoxJ;}} private jQueryObject _UsrTicketFeedbackTextBoxJ;
		public Element UsrTicketFeedbackLabel {get {if (_UsrTicketFeedbackLabel == null) {_UsrTicketFeedbackLabel = (Element)Document.GetElementById(clientId + "_UsrTicketFeedbackLabel");}; return _UsrTicketFeedbackLabel;}} private Element _UsrTicketFeedbackLabel;
		public jQueryObject UsrTicketFeedbackLabelJ {get {if (_UsrTicketFeedbackLabelJ == null) {_UsrTicketFeedbackLabelJ = jQuery.Select("#" + clientId + "_UsrTicketFeedbackLabel");}; return _UsrTicketFeedbackLabelJ;}} private jQueryObject _UsrTicketFeedbackLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public InputElement UsrTicketFeedbackTextSubmitButton {get {if (_UsrTicketFeedbackTextSubmitButton == null) {_UsrTicketFeedbackTextSubmitButton = (InputElement)Document.GetElementById(clientId + "_UsrTicketFeedbackTextSubmitButton");}; return _UsrTicketFeedbackTextSubmitButton;}} private InputElement _UsrTicketFeedbackTextSubmitButton;
		public jQueryObject UsrTicketFeedbackTextSubmitButtonJ {get {if (_UsrTicketFeedbackTextSubmitButtonJ == null) {_UsrTicketFeedbackTextSubmitButtonJ = jQuery.Select("#" + clientId + "_UsrTicketFeedbackTextSubmitButton");}; return _UsrTicketFeedbackTextSubmitButtonJ;}} private jQueryObject _UsrTicketFeedbackTextSubmitButtonJ;
		public DivElement SuccessfulTicketEventPanel {get {if (_SuccessfulTicketEventPanel == null) {_SuccessfulTicketEventPanel = (DivElement)Document.GetElementById(clientId + "_SuccessfulTicketEventPanel");}; return _SuccessfulTicketEventPanel;}} private DivElement _SuccessfulTicketEventPanel;
		public jQueryObject SuccessfulTicketEventPanelJ {get {if (_SuccessfulTicketEventPanelJ == null) {_SuccessfulTicketEventPanelJ = jQuery.Select("#" + clientId + "_SuccessfulTicketEventPanel");}; return _SuccessfulTicketEventPanelJ;}} private jQueryObject _SuccessfulTicketEventPanelJ;
		public Element JoinBrandRegularsGroup {get {if (_JoinBrandRegularsGroup == null) {_JoinBrandRegularsGroup = (Element)Document.GetElementById(clientId + "_JoinBrandRegularsGroup");}; return _JoinBrandRegularsGroup;}} private Element _JoinBrandRegularsGroup;
		public jQueryObject JoinBrandRegularsGroupJ {get {if (_JoinBrandRegularsGroupJ == null) {_JoinBrandRegularsGroupJ = jQuery.Select("#" + clientId + "_JoinBrandRegularsGroup");}; return _JoinBrandRegularsGroupJ;}} private jQueryObject _JoinBrandRegularsGroupJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element BrandGroupRepeater {get {if (_BrandGroupRepeater == null) {_BrandGroupRepeater = (Element)Document.GetElementById(clientId + "_BrandGroupRepeater");}; return _BrandGroupRepeater;}} private Element _BrandGroupRepeater;
		public jQueryObject BrandGroupRepeaterJ {get {if (_BrandGroupRepeaterJ == null) {_BrandGroupRepeaterJ = jQuery.Select("#" + clientId + "_BrandGroupRepeater");}; return _BrandGroupRepeaterJ;}} private jQueryObject _BrandGroupRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
