//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.UsrUserControl
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
		public DivElement BannedUserPanel {get {if (_BannedUserPanel == null) {_BannedUserPanel = (DivElement)Document.GetElementById(clientId + "_BannedUserPanel");}; return _BannedUserPanel;}} private DivElement _BannedUserPanel;
		public jQueryObject BannedUserPanelJ {get {if (_BannedUserPanelJ == null) {_BannedUserPanelJ = jQuery.Select("#" + clientId + "_BannedUserPanel");}; return _BannedUserPanelJ;}} private jQueryObject _BannedUserPanelJ;
		public DivElement UnsubscribedUserPanel {get {if (_UnsubscribedUserPanel == null) {_UnsubscribedUserPanel = (DivElement)Document.GetElementById(clientId + "_UnsubscribedUserPanel");}; return _UnsubscribedUserPanel;}} private DivElement _UnsubscribedUserPanel;
		public jQueryObject UnsubscribedUserPanelJ {get {if (_UnsubscribedUserPanelJ == null) {_UnsubscribedUserPanelJ = jQuery.Select("#" + clientId + "_UnsubscribedUserPanel");}; return _UnsubscribedUserPanelJ;}} private jQueryObject _UnsubscribedUserPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
