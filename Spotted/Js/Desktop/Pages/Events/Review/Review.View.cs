//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Events.Review
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
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element Requiredfieldvalidator2 {get {if (_Requiredfieldvalidator2 == null) {_Requiredfieldvalidator2 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator2");}; return _Requiredfieldvalidator2;}} private Element _Requiredfieldvalidator2;
		public jQueryObject Requiredfieldvalidator2J {get {if (_Requiredfieldvalidator2J == null) {_Requiredfieldvalidator2J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator2");}; return _Requiredfieldvalidator2J;}} private jQueryObject _Requiredfieldvalidator2J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Js.Controls.Html.Controller ReviewHtml {get {return (Js.Controls.Html.Controller) Script.Eval(clientId + "_ReviewHtmlController");}}
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement SummaryTextBox {get {if (_SummaryTextBox == null) {_SummaryTextBox = (InputElement)Document.GetElementById(clientId + "_SummaryTextBox");}; return _SummaryTextBox;}} private InputElement _SummaryTextBox;
		public jQueryObject SummaryTextBoxJ {get {if (_SummaryTextBoxJ == null) {_SummaryTextBoxJ = jQuery.Select("#" + clientId + "_SummaryTextBox");}; return _SummaryTextBoxJ;}} private jQueryObject _SummaryTextBoxJ;
		public InputElement ReviewBody {get {if (_ReviewBody == null) {_ReviewBody = (InputElement)Document.GetElementById(clientId + "_ReviewBody");}; return _ReviewBody;}} private InputElement _ReviewBody;
		public jQueryObject ReviewBodyJ {get {if (_ReviewBodyJ == null) {_ReviewBodyJ = jQuery.Select("#" + clientId + "_ReviewBody");}; return _ReviewBodyJ;}} private jQueryObject _ReviewBodyJ;
		public Element StatusLabel {get {if (_StatusLabel == null) {_StatusLabel = (Element)Document.GetElementById(clientId + "_StatusLabel");}; return _StatusLabel;}} private Element _StatusLabel;
		public jQueryObject StatusLabelJ {get {if (_StatusLabelJ == null) {_StatusLabelJ = jQuery.Select("#" + clientId + "_StatusLabel");}; return _StatusLabelJ;}} private jQueryObject _StatusLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement DeleteReviewPanel {get {if (_DeleteReviewPanel == null) {_DeleteReviewPanel = (DivElement)Document.GetElementById(clientId + "_DeleteReviewPanel");}; return _DeleteReviewPanel;}} private DivElement _DeleteReviewPanel;
		public jQueryObject DeleteReviewPanelJ {get {if (_DeleteReviewPanelJ == null) {_DeleteReviewPanelJ = jQuery.Select("#" + clientId + "_DeleteReviewPanel");}; return _DeleteReviewPanelJ;}} private jQueryObject _DeleteReviewPanelJ;
		public DivElement CantEditPanel {get {if (_CantEditPanel == null) {_CantEditPanel = (DivElement)Document.GetElementById(clientId + "_CantEditPanel");}; return _CantEditPanel;}} private DivElement _CantEditPanel;
		public jQueryObject CantEditPanelJ {get {if (_CantEditPanelJ == null) {_CantEditPanelJ = jQuery.Select("#" + clientId + "_CantEditPanel");}; return _CantEditPanelJ;}} private jQueryObject _CantEditPanelJ;
		public DivElement InfoPanel {get {if (_InfoPanel == null) {_InfoPanel = (DivElement)Document.GetElementById(clientId + "_InfoPanel");}; return _InfoPanel;}} private DivElement _InfoPanel;
		public jQueryObject InfoPanelJ {get {if (_InfoPanelJ == null) {_InfoPanelJ = jQuery.Select("#" + clientId + "_InfoPanel");}; return _InfoPanelJ;}} private jQueryObject _InfoPanelJ;
		public Element DeleteButton {get {if (_DeleteButton == null) {_DeleteButton = (Element)Document.GetElementById(clientId + "_DeleteButton");}; return _DeleteButton;}} private Element _DeleteButton;
		public jQueryObject DeleteButtonJ {get {if (_DeleteButtonJ == null) {_DeleteButtonJ = jQuery.Select("#" + clientId + "_DeleteButton");}; return _DeleteButtonJ;}} private jQueryObject _DeleteButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
