//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.Unsubscribe
{
	public partial class View
		 : Js.BlankUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement SubscribedPanel {get {if (_SubscribedPanel == null) {_SubscribedPanel = (DivElement)Document.GetElementById(clientId + "_SubscribedPanel");}; return _SubscribedPanel;}} private DivElement _SubscribedPanel;
		public jQueryObject SubscribedPanelJ {get {if (_SubscribedPanelJ == null) {_SubscribedPanelJ = jQuery.Select("#" + clientId + "_SubscribedPanel");}; return _SubscribedPanelJ;}} private jQueryObject _SubscribedPanelJ;
		public DivElement UnsubscribedPanel {get {if (_UnsubscribedPanel == null) {_UnsubscribedPanel = (DivElement)Document.GetElementById(clientId + "_UnsubscribedPanel");}; return _UnsubscribedPanel;}} private DivElement _UnsubscribedPanel;
		public jQueryObject UnsubscribedPanelJ {get {if (_UnsubscribedPanelJ == null) {_UnsubscribedPanelJ = jQuery.Select("#" + clientId + "_UnsubscribedPanel");}; return _UnsubscribedPanelJ;}} private jQueryObject _UnsubscribedPanelJ;
		public DivElement CancelPanel {get {if (_CancelPanel == null) {_CancelPanel = (DivElement)Document.GetElementById(clientId + "_CancelPanel");}; return _CancelPanel;}} private DivElement _CancelPanel;
		public jQueryObject CancelPanelJ {get {if (_CancelPanelJ == null) {_CancelPanelJ = jQuery.Select("#" + clientId + "_CancelPanel");}; return _CancelPanelJ;}} private jQueryObject _CancelPanelJ;
		public Element AddedByUsrDiv {get {if (_AddedByUsrDiv == null) {_AddedByUsrDiv = (Element)Document.GetElementById(clientId + "_AddedByUsrDiv");}; return _AddedByUsrDiv;}} private Element _AddedByUsrDiv;
		public jQueryObject AddedByUsrDivJ {get {if (_AddedByUsrDivJ == null) {_AddedByUsrDivJ = jQuery.Select("#" + clientId + "_AddedByUsrDiv");}; return _AddedByUsrDivJ;}} private jQueryObject _AddedByUsrDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element UnsubscribeButton {get {if (_UnsubscribeButton == null) {_UnsubscribeButton = (Element)Document.GetElementById(clientId + "_UnsubscribeButton");}; return _UnsubscribeButton;}} private Element _UnsubscribeButton;
		public jQueryObject UnsubscribeButtonJ {get {if (_UnsubscribeButtonJ == null) {_UnsubscribeButtonJ = jQuery.Select("#" + clientId + "_UnsubscribeButton");}; return _UnsubscribeButtonJ;}} private jQueryObject _UnsubscribeButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SubscribeButton {get {if (_SubscribeButton == null) {_SubscribeButton = (Element)Document.GetElementById(clientId + "_SubscribeButton");}; return _SubscribeButton;}} private Element _SubscribeButton;
		public jQueryObject SubscribeButtonJ {get {if (_SubscribeButtonJ == null) {_SubscribeButtonJ = jQuery.Select("#" + clientId + "_SubscribeButton");}; return _SubscribeButtonJ;}} private jQueryObject _SubscribeButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Div1 {get {if (_Div1 == null) {_Div1 = (Element)Document.GetElementById(clientId + "_Div1");}; return _Div1;}} private Element _Div1;
		public jQueryObject Div1J {get {if (_Div1J == null) {_Div1J = jQuery.Select("#" + clientId + "_Div1");}; return _Div1J;}} private jQueryObject _Div1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element LogOffButton {get {if (_LogOffButton == null) {_LogOffButton = (Element)Document.GetElementById(clientId + "_LogOffButton");}; return _LogOffButton;}} private Element _LogOffButton;
		public jQueryObject LogOffButtonJ {get {if (_LogOffButtonJ == null) {_LogOffButtonJ = jQuery.Select("#" + clientId + "_LogOffButton");}; return _LogOffButtonJ;}} private jQueryObject _LogOffButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
