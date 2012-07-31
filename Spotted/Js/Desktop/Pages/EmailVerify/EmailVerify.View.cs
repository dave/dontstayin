//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.EmailVerify
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
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement enableCommsPanel {get {if (_enableCommsPanel == null) {_enableCommsPanel = (DivElement)Document.GetElementById(clientId + "_enableCommsPanel");}; return _enableCommsPanel;}} private DivElement _enableCommsPanel;
		public jQueryObject enableCommsPanelJ {get {if (_enableCommsPanelJ == null) {_enableCommsPanelJ = jQuery.Select("#" + clientId + "_enableCommsPanel");}; return _enableCommsPanelJ;}} private jQueryObject _enableCommsPanelJ;
		public DivElement disableCommsPanel {get {if (_disableCommsPanel == null) {_disableCommsPanel = (DivElement)Document.GetElementById(clientId + "_disableCommsPanel");}; return _disableCommsPanel;}} private DivElement _disableCommsPanel;
		public jQueryObject disableCommsPanelJ {get {if (_disableCommsPanelJ == null) {_disableCommsPanelJ = jQuery.Select("#" + clientId + "_disableCommsPanel");}; return _disableCommsPanelJ;}} private jQueryObject _disableCommsPanelJ;
		public Element emailSentP {get {if (_emailSentP == null) {_emailSentP = (Element)Document.GetElementById(clientId + "_emailSentP");}; return _emailSentP;}} private Element _emailSentP;
		public jQueryObject emailSentPJ {get {if (_emailSentPJ == null) {_emailSentPJ = jQuery.Select("#" + clientId + "_emailSentP");}; return _emailSentPJ;}} private jQueryObject _emailSentPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element LinkButton1 {get {if (_LinkButton1 == null) {_LinkButton1 = (Element)Document.GetElementById(clientId + "_LinkButton1");}; return _LinkButton1;}} private Element _LinkButton1;
		public jQueryObject LinkButton1J {get {if (_LinkButton1J == null) {_LinkButton1J = jQuery.Select("#" + clientId + "_LinkButton1");}; return _LinkButton1J;}} private jQueryObject _LinkButton1J;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public DivElement PanelError {get {if (_PanelError == null) {_PanelError = (DivElement)Document.GetElementById(clientId + "_PanelError");}; return _PanelError;}} private DivElement _PanelError;
		public jQueryObject PanelErrorJ {get {if (_PanelErrorJ == null) {_PanelErrorJ = jQuery.Select("#" + clientId + "_PanelError");}; return _PanelErrorJ;}} private jQueryObject _PanelErrorJ;
		public Element enableCommsH1 {get {if (_enableCommsH1 == null) {_enableCommsH1 = (Element)Document.GetElementById(clientId + "_enableCommsH1");}; return _enableCommsH1;}} private Element _enableCommsH1;
		public jQueryObject enableCommsH1J {get {if (_enableCommsH1J == null) {_enableCommsH1J = jQuery.Select("#" + clientId + "_enableCommsH1");}; return _enableCommsH1J;}} private jQueryObject _enableCommsH1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
