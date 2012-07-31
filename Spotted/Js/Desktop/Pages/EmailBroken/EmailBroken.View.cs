//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.EmailBroken
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
		public DivElement EmailBrokenPanel {get {if (_EmailBrokenPanel == null) {_EmailBrokenPanel = (DivElement)Document.GetElementById(clientId + "_EmailBrokenPanel");}; return _EmailBrokenPanel;}} private DivElement _EmailBrokenPanel;
		public jQueryObject EmailBrokenPanelJ {get {if (_EmailBrokenPanelJ == null) {_EmailBrokenPanelJ = jQuery.Select("#" + clientId + "_EmailBrokenPanel");}; return _EmailBrokenPanelJ;}} private jQueryObject _EmailBrokenPanelJ;
		public Element LinkButton1 {get {if (_LinkButton1 == null) {_LinkButton1 = (Element)Document.GetElementById(clientId + "_LinkButton1");}; return _LinkButton1;}} private Element _LinkButton1;
		public jQueryObject LinkButton1J {get {if (_LinkButton1J == null) {_LinkButton1J = jQuery.Select("#" + clientId + "_LinkButton1");}; return _LinkButton1J;}} private jQueryObject _LinkButton1J;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element DoneP {get {if (_DoneP == null) {_DoneP = (Element)Document.GetElementById(clientId + "_DoneP");}; return _DoneP;}} private Element _DoneP;
		public jQueryObject DonePJ {get {if (_DonePJ == null) {_DonePJ = jQuery.Select("#" + clientId + "_DoneP");}; return _DonePJ;}} private jQueryObject _DonePJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement EmailNotBrokenPanel {get {if (_EmailNotBrokenPanel == null) {_EmailNotBrokenPanel = (DivElement)Document.GetElementById(clientId + "_EmailNotBrokenPanel");}; return _EmailNotBrokenPanel;}} private DivElement _EmailNotBrokenPanel;
		public jQueryObject EmailNotBrokenPanelJ {get {if (_EmailNotBrokenPanelJ == null) {_EmailNotBrokenPanelJ = jQuery.Select("#" + clientId + "_EmailNotBrokenPanel");}; return _EmailNotBrokenPanelJ;}} private jQueryObject _EmailNotBrokenPanelJ;
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
