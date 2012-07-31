//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.SubmitSuccessfulTransfers
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
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement SuccessfulTransferPanel {get {if (_SuccessfulTransferPanel == null) {_SuccessfulTransferPanel = (DivElement)Document.GetElementById(clientId + "_SuccessfulTransferPanel");}; return _SuccessfulTransferPanel;}} private DivElement _SuccessfulTransferPanel;
		public jQueryObject SuccessfulTransferPanelJ {get {if (_SuccessfulTransferPanelJ == null) {_SuccessfulTransferPanelJ = jQuery.Select("#" + clientId + "_SuccessfulTransferPanel");}; return _SuccessfulTransferPanelJ;}} private jQueryObject _SuccessfulTransferPanelJ;
		public Element ErrorLabel {get {if (_ErrorLabel == null) {_ErrorLabel = (Element)Document.GetElementById(clientId + "_ErrorLabel");}; return _ErrorLabel;}} private Element _ErrorLabel;
		public jQueryObject ErrorLabelJ {get {if (_ErrorLabelJ == null) {_ErrorLabelJ = jQuery.Select("#" + clientId + "_ErrorLabel");}; return _ErrorLabelJ;}} private jQueryObject _ErrorLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SuccessfulTransferGridView {get {if (_SuccessfulTransferGridView == null) {_SuccessfulTransferGridView = (Element)Document.GetElementById(clientId + "_SuccessfulTransferGridView");}; return _SuccessfulTransferGridView;}} private Element _SuccessfulTransferGridView;
		public jQueryObject SuccessfulTransferGridViewJ {get {if (_SuccessfulTransferGridViewJ == null) {_SuccessfulTransferGridViewJ = jQuery.Select("#" + clientId + "_SuccessfulTransferGridView");}; return _SuccessfulTransferGridViewJ;}} private jQueryObject _SuccessfulTransferGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element SaveButton {get {if (_SaveButton == null) {_SaveButton = (Element)Document.GetElementById(clientId + "_SaveButton");}; return _SaveButton;}} private Element _SaveButton;
		public jQueryObject SaveButtonJ {get {if (_SaveButtonJ == null) {_SaveButtonJ = jQuery.Select("#" + clientId + "_SaveButton");}; return _SaveButtonJ;}} private jQueryObject _SaveButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element CancelButton {get {if (_CancelButton == null) {_CancelButton = (Element)Document.GetElementById(clientId + "_CancelButton");}; return _CancelButton;}} private Element _CancelButton;
		public jQueryObject CancelButtonJ {get {if (_CancelButtonJ == null) {_CancelButtonJ = jQuery.Select("#" + clientId + "_CancelButton");}; return _CancelButtonJ;}} private jQueryObject _CancelButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
