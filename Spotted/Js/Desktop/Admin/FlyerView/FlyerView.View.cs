//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.FlyerView
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
		public DivElement uiBasicInfo {get {if (_uiBasicInfo == null) {_uiBasicInfo = (DivElement)Document.GetElementById(clientId + "_uiBasicInfo");}; return _uiBasicInfo;}} private DivElement _uiBasicInfo;
		public jQueryObject uiBasicInfoJ {get {if (_uiBasicInfoJ == null) {_uiBasicInfoJ = jQuery.Select("#" + clientId + "_uiBasicInfo");}; return _uiBasicInfoJ;}} private jQueryObject _uiBasicInfoJ;
		public Element uiValidationErrors {get {if (_uiValidationErrors == null) {_uiValidationErrors = (Element)Document.GetElementById(clientId + "_uiValidationErrors");}; return _uiValidationErrors;}} private Element _uiValidationErrors;
		public jQueryObject uiValidationErrorsJ {get {if (_uiValidationErrorsJ == null) {_uiValidationErrorsJ = jQuery.Select("#" + clientId + "_uiValidationErrors");}; return _uiValidationErrorsJ;}} private jQueryObject _uiValidationErrorsJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiSendButtons {get {if (_uiSendButtons == null) {_uiSendButtons = (Element)Document.GetElementById(clientId + "_uiSendButtons");}; return _uiSendButtons;}} private Element _uiSendButtons;
		public jQueryObject uiSendButtonsJ {get {if (_uiSendButtonsJ == null) {_uiSendButtonsJ = jQuery.Select("#" + clientId + "_uiSendButtons");}; return _uiSendButtonsJ;}} private jQueryObject _uiSendButtonsJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiSend {get {if (_uiSend == null) {_uiSend = (Element)Document.GetElementById(clientId + "_uiSend");}; return _uiSend;}} private Element _uiSend;
		public jQueryObject uiSendJ {get {if (_uiSendJ == null) {_uiSendJ = jQuery.Select("#" + clientId + "_uiSend");}; return _uiSendJ;}} private jQueryObject _uiSendJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiStop {get {if (_uiStop == null) {_uiStop = (Element)Document.GetElementById(clientId + "_uiStop");}; return _uiStop;}} private Element _uiStop;
		public jQueryObject uiStopJ {get {if (_uiStopJ == null) {_uiStopJ = jQuery.Select("#" + clientId + "_uiStop");}; return _uiStopJ;}} private jQueryObject _uiStopJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiRestart {get {if (_uiRestart == null) {_uiRestart = (Element)Document.GetElementById(clientId + "_uiRestart");}; return _uiRestart;}} private Element _uiRestart;
		public jQueryObject uiRestartJ {get {if (_uiRestartJ == null) {_uiRestartJ = jQuery.Select("#" + clientId + "_uiRestart");}; return _uiRestartJ;}} private jQueryObject _uiRestartJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiSentSuccessfully {get {if (_uiSentSuccessfully == null) {_uiSentSuccessfully = (Element)Document.GetElementById(clientId + "_uiSentSuccessfully");}; return _uiSentSuccessfully;}} private Element _uiSentSuccessfully;
		public jQueryObject uiSentSuccessfullyJ {get {if (_uiSentSuccessfullyJ == null) {_uiSentSuccessfullyJ = jQuery.Select("#" + clientId + "_uiSentSuccessfully");}; return _uiSentSuccessfullyJ;}} private jQueryObject _uiSentSuccessfullyJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
