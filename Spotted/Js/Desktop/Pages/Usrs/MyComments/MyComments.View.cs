//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Cal", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.MyComments
{
	public partial class View
		 : Js.Pages.Usrs.UsrUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement MyChatLinksPanel {get {if (_MyChatLinksPanel == null) {_MyChatLinksPanel = (DivElement)Document.GetElementById(clientId + "_MyChatLinksPanel");}; return _MyChatLinksPanel;}} private DivElement _MyChatLinksPanel;
		public jQueryObject MyChatLinksPanelJ {get {if (_MyChatLinksPanelJ == null) {_MyChatLinksPanelJ = jQuery.Select("#" + clientId + "_MyChatLinksPanel");}; return _MyChatLinksPanelJ;}} private jQueryObject _MyChatLinksPanelJ;
		public Element UsrIntro {get {if (_UsrIntro == null) {_UsrIntro = (Element)Document.GetElementById(clientId + "_UsrIntro");}; return _UsrIntro;}} private Element _UsrIntro;
		public jQueryObject UsrIntroJ {get {if (_UsrIntroJ == null) {_UsrIntroJ = jQuery.Select("#" + clientId + "_UsrIntro");}; return _UsrIntroJ;}} private jQueryObject _UsrIntroJ;//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
		public Element Cal {get {if (_Cal == null) {_Cal = (Element)Document.GetElementById(clientId + "_Cal");}; return _Cal;}} private Element _Cal;
		public jQueryObject CalJ {get {if (_CalJ == null) {_CalJ = jQuery.Select("#" + clientId + "_Cal");}; return _CalJ;}} private jQueryObject _CalJ;//mappings.Add("Spotted.Controls.Cal", ElementGetter("Element"));
		public DivElement PanelMyComments {get {if (_PanelMyComments == null) {_PanelMyComments = (DivElement)Document.GetElementById(clientId + "_PanelMyComments");}; return _PanelMyComments;}} private DivElement _PanelMyComments;
		public jQueryObject PanelMyCommentsJ {get {if (_PanelMyCommentsJ == null) {_PanelMyCommentsJ = jQuery.Select("#" + clientId + "_PanelMyComments");}; return _PanelMyCommentsJ;}} private jQueryObject _PanelMyCommentsJ;
		public DivElement ThreadsPanel {get {if (_ThreadsPanel == null) {_ThreadsPanel = (DivElement)Document.GetElementById(clientId + "_ThreadsPanel");}; return _ThreadsPanel;}} private DivElement _ThreadsPanel;
		public jQueryObject ThreadsPanelJ {get {if (_ThreadsPanelJ == null) {_ThreadsPanelJ = jQuery.Select("#" + clientId + "_ThreadsPanel");}; return _ThreadsPanelJ;}} private jQueryObject _ThreadsPanelJ;
		public DivElement NoThreadsPanel {get {if (_NoThreadsPanel == null) {_NoThreadsPanel = (DivElement)Document.GetElementById(clientId + "_NoThreadsPanel");}; return _NoThreadsPanel;}} private DivElement _NoThreadsPanel;
		public jQueryObject NoThreadsPanelJ {get {if (_NoThreadsPanelJ == null) {_NoThreadsPanelJ = jQuery.Select("#" + clientId + "_NoThreadsPanel");}; return _NoThreadsPanelJ;}} private jQueryObject _NoThreadsPanelJ;
		public Element ThreadsPageP {get {if (_ThreadsPageP == null) {_ThreadsPageP = (Element)Document.GetElementById(clientId + "_ThreadsPageP");}; return _ThreadsPageP;}} private Element _ThreadsPageP;
		public jQueryObject ThreadsPagePJ {get {if (_ThreadsPagePJ == null) {_ThreadsPagePJ = jQuery.Select("#" + clientId + "_ThreadsPageP");}; return _ThreadsPagePJ;}} private jQueryObject _ThreadsPagePJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ThreadsPageP1 {get {if (_ThreadsPageP1 == null) {_ThreadsPageP1 = (Element)Document.GetElementById(clientId + "_ThreadsPageP1");}; return _ThreadsPageP1;}} private Element _ThreadsPageP1;
		public jQueryObject ThreadsPageP1J {get {if (_ThreadsPageP1J == null) {_ThreadsPageP1J = jQuery.Select("#" + clientId + "_ThreadsPageP1");}; return _ThreadsPageP1J;}} private jQueryObject _ThreadsPageP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement ThreadsNextPageLink {get {if (_ThreadsNextPageLink == null) {_ThreadsNextPageLink = (AnchorElement)Document.GetElementById(clientId + "_ThreadsNextPageLink");}; return _ThreadsNextPageLink;}} private AnchorElement _ThreadsNextPageLink;
		public jQueryObject ThreadsNextPageLinkJ {get {if (_ThreadsNextPageLinkJ == null) {_ThreadsNextPageLinkJ = jQuery.Select("#" + clientId + "_ThreadsNextPageLink");}; return _ThreadsNextPageLinkJ;}} private jQueryObject _ThreadsNextPageLinkJ;
		public AnchorElement ThreadsNextPageLink1 {get {if (_ThreadsNextPageLink1 == null) {_ThreadsNextPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_ThreadsNextPageLink1");}; return _ThreadsNextPageLink1;}} private AnchorElement _ThreadsNextPageLink1;
		public jQueryObject ThreadsNextPageLink1J {get {if (_ThreadsNextPageLink1J == null) {_ThreadsNextPageLink1J = jQuery.Select("#" + clientId + "_ThreadsNextPageLink1");}; return _ThreadsNextPageLink1J;}} private jQueryObject _ThreadsNextPageLink1J;
		public AnchorElement ThreadsPrevPageLink {get {if (_ThreadsPrevPageLink == null) {_ThreadsPrevPageLink = (AnchorElement)Document.GetElementById(clientId + "_ThreadsPrevPageLink");}; return _ThreadsPrevPageLink;}} private AnchorElement _ThreadsPrevPageLink;
		public jQueryObject ThreadsPrevPageLinkJ {get {if (_ThreadsPrevPageLinkJ == null) {_ThreadsPrevPageLinkJ = jQuery.Select("#" + clientId + "_ThreadsPrevPageLink");}; return _ThreadsPrevPageLinkJ;}} private jQueryObject _ThreadsPrevPageLinkJ;
		public AnchorElement ThreadsPrevPageLink1 {get {if (_ThreadsPrevPageLink1 == null) {_ThreadsPrevPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_ThreadsPrevPageLink1");}; return _ThreadsPrevPageLink1;}} private AnchorElement _ThreadsPrevPageLink1;
		public jQueryObject ThreadsPrevPageLink1J {get {if (_ThreadsPrevPageLink1J == null) {_ThreadsPrevPageLink1J = jQuery.Select("#" + clientId + "_ThreadsPrevPageLink1");}; return _ThreadsPrevPageLink1J;}} private jQueryObject _ThreadsPrevPageLink1J;
		public Element ThreadsDataGrid {get {if (_ThreadsDataGrid == null) {_ThreadsDataGrid = (Element)Document.GetElementById(clientId + "_ThreadsDataGrid");}; return _ThreadsDataGrid;}} private Element _ThreadsDataGrid;
		public jQueryObject ThreadsDataGridJ {get {if (_ThreadsDataGridJ == null) {_ThreadsDataGridJ = jQuery.Select("#" + clientId + "_ThreadsDataGrid");}; return _ThreadsDataGridJ;}} private jQueryObject _ThreadsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element InlineScript3 {get {if (_InlineScript3 == null) {_InlineScript3 = (Element)Document.GetElementById(clientId + "_InlineScript3");}; return _InlineScript3;}} private Element _InlineScript3;
		public jQueryObject InlineScript3J {get {if (_InlineScript3J == null) {_InlineScript3J = jQuery.Select("#" + clientId + "_InlineScript3");}; return _InlineScript3J;}} private jQueryObject _InlineScript3J;//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
