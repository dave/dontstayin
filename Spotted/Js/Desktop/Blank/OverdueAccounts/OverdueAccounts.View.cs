//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Promoters.AccountsWarning", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.OverdueAccounts
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
		public DivElement LoggedInPanel {get {if (_LoggedInPanel == null) {_LoggedInPanel = (DivElement)Document.GetElementById(clientId + "_LoggedInPanel");}; return _LoggedInPanel;}} private DivElement _LoggedInPanel;
		public jQueryObject LoggedInPanelJ {get {if (_LoggedInPanelJ == null) {_LoggedInPanelJ = jQuery.Select("#" + clientId + "_LoggedInPanel");}; return _LoggedInPanelJ;}} private jQueryObject _LoggedInPanelJ;
		public AnchorElement LoggedInLink {get {if (_LoggedInLink == null) {_LoggedInLink = (AnchorElement)Document.GetElementById(clientId + "_LoggedInLink");}; return _LoggedInLink;}} private AnchorElement _LoggedInLink;
		public jQueryObject LoggedInLinkJ {get {if (_LoggedInLinkJ == null) {_LoggedInLinkJ = jQuery.Select("#" + clientId + "_LoggedInLink");}; return _LoggedInLinkJ;}} private jQueryObject _LoggedInLinkJ;
		public Element LogOutLink {get {if (_LogOutLink == null) {_LogOutLink = (Element)Document.GetElementById(clientId + "_LogOutLink");}; return _LogOutLink;}} private Element _LogOutLink;
		public jQueryObject LogOutLinkJ {get {if (_LogOutLinkJ == null) {_LogOutLinkJ = jQuery.Select("#" + clientId + "_LogOutLink");}; return _LogOutLinkJ;}} private jQueryObject _LogOutLinkJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element LoggedInAs {get {if (_LoggedInAs == null) {_LoggedInAs = (Element)Document.GetElementById(clientId + "_LoggedInAs");}; return _LoggedInAs;}} private Element _LoggedInAs;
		public jQueryObject LoggedInAsJ {get {if (_LoggedInAsJ == null) {_LoggedInAsJ = jQuery.Select("#" + clientId + "_LoggedInAs");}; return _LoggedInAsJ;}} private jQueryObject _LoggedInAsJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PromoterAccountsWarningControl {get {if (_PromoterAccountsWarningControl == null) {_PromoterAccountsWarningControl = (Element)Document.GetElementById(clientId + "_PromoterAccountsWarningControl");}; return _PromoterAccountsWarningControl;}} private Element _PromoterAccountsWarningControl;
		public jQueryObject PromoterAccountsWarningControlJ {get {if (_PromoterAccountsWarningControlJ == null) {_PromoterAccountsWarningControlJ = jQuery.Select("#" + clientId + "_PromoterAccountsWarningControl");}; return _PromoterAccountsWarningControlJ;}} private jQueryObject _PromoterAccountsWarningControlJ;//mappings.Add("Spotted.Controls.Promoters.AccountsWarning", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
