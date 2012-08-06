//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Login
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
		public DivElement ErrorPanel1 {get {if (_ErrorPanel1 == null) {_ErrorPanel1 = (DivElement)Document.GetElementById(clientId + "_ErrorPanel1");}; return _ErrorPanel1;}} private DivElement _ErrorPanel1;
		public jQueryObject ErrorPanel1J {get {if (_ErrorPanel1J == null) {_ErrorPanel1J = jQuery.Select("#" + clientId + "_ErrorPanel1");}; return _ErrorPanel1J;}} private jQueryObject _ErrorPanel1J;
		public Element LogoutButton {get {if (_LogoutButton == null) {_LogoutButton = (Element)Document.GetElementById(clientId + "_LogoutButton");}; return _LogoutButton;}} private Element _LogoutButton;
		public jQueryObject LogoutButtonJ {get {if (_LogoutButtonJ == null) {_LogoutButtonJ = jQuery.Select("#" + clientId + "_LogoutButton");}; return _LogoutButtonJ;}} private jQueryObject _LogoutButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement LoginPanel {get {if (_LoginPanel == null) {_LoginPanel = (DivElement)Document.GetElementById(clientId + "_LoginPanel");}; return _LoginPanel;}} private DivElement _LoginPanel;
		public jQueryObject LoginPanelJ {get {if (_LoginPanelJ == null) {_LoginPanelJ = jQuery.Select("#" + clientId + "_LoginPanel");}; return _LoginPanelJ;}} private jQueryObject _LoginPanelJ;
		public DivElement LoggedInPanel {get {if (_LoggedInPanel == null) {_LoggedInPanel = (DivElement)Document.GetElementById(clientId + "_LoggedInPanel");}; return _LoggedInPanel;}} private DivElement _LoggedInPanel;
		public jQueryObject LoggedInPanelJ {get {if (_LoggedInPanelJ == null) {_LoggedInPanelJ = jQuery.Select("#" + clientId + "_LoggedInPanel");}; return _LoggedInPanelJ;}} private jQueryObject _LoggedInPanelJ;
		public Element loginCountLabel {get {if (_loginCountLabel == null) {_loginCountLabel = (Element)Document.GetElementById(clientId + "_loginCountLabel");}; return _loginCountLabel;}} private Element _loginCountLabel;
		public jQueryObject loginCountLabelJ {get {if (_loginCountLabelJ == null) {_loginCountLabelJ = jQuery.Select("#" + clientId + "_loginCountLabel");}; return _loginCountLabelJ;}} private jQueryObject _loginCountLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public AnchorElement PublicProfileLink {get {if (_PublicProfileLink == null) {_PublicProfileLink = (AnchorElement)Document.GetElementById(clientId + "_PublicProfileLink");}; return _PublicProfileLink;}} private AnchorElement _PublicProfileLink;
		public jQueryObject PublicProfileLinkJ {get {if (_PublicProfileLinkJ == null) {_PublicProfileLinkJ = jQuery.Select("#" + clientId + "_PublicProfileLink");}; return _PublicProfileLinkJ;}} private jQueryObject _PublicProfileLinkJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
