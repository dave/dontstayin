//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Cal", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.MyGalleries
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
		public Element UsrIntro {get {if (_UsrIntro == null) {_UsrIntro = (Element)Document.GetElementById(clientId + "_UsrIntro");}; return _UsrIntro;}} private Element _UsrIntro;
		public jQueryObject UsrIntroJ {get {if (_UsrIntroJ == null) {_UsrIntroJ = jQuery.Select("#" + clientId + "_UsrIntro");}; return _UsrIntroJ;}} private jQueryObject _UsrIntroJ;//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
		public Element Cal {get {if (_Cal == null) {_Cal = (Element)Document.GetElementById(clientId + "_Cal");}; return _Cal;}} private Element _Cal;
		public jQueryObject CalJ {get {if (_CalJ == null) {_CalJ = jQuery.Select("#" + clientId + "_Cal");}; return _CalJ;}} private jQueryObject _CalJ;//mappings.Add("Spotted.Controls.Cal", ElementGetter("Element"));
		public DivElement PanelItems {get {if (_PanelItems == null) {_PanelItems = (DivElement)Document.GetElementById(clientId + "_PanelItems");}; return _PanelItems;}} private DivElement _PanelItems;
		public jQueryObject PanelItemsJ {get {if (_PanelItemsJ == null) {_PanelItemsJ = jQuery.Select("#" + clientId + "_PanelItems");}; return _PanelItemsJ;}} private jQueryObject _PanelItemsJ;
		public DivElement NoItemsPanel {get {if (_NoItemsPanel == null) {_NoItemsPanel = (DivElement)Document.GetElementById(clientId + "_NoItemsPanel");}; return _NoItemsPanel;}} private DivElement _NoItemsPanel;
		public jQueryObject NoItemsPanelJ {get {if (_NoItemsPanelJ == null) {_NoItemsPanelJ = jQuery.Select("#" + clientId + "_NoItemsPanel");}; return _NoItemsPanelJ;}} private jQueryObject _NoItemsPanelJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement ItemsPanel {get {if (_ItemsPanel == null) {_ItemsPanel = (DivElement)Document.GetElementById(clientId + "_ItemsPanel");}; return _ItemsPanel;}} private DivElement _ItemsPanel;
		public jQueryObject ItemsPanelJ {get {if (_ItemsPanelJ == null) {_ItemsPanelJ = jQuery.Select("#" + clientId + "_ItemsPanel");}; return _ItemsPanelJ;}} private jQueryObject _ItemsPanelJ;
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element PageP {get {if (_PageP == null) {_PageP = (Element)Document.GetElementById(clientId + "_PageP");}; return _PageP;}} private Element _PageP;
		public jQueryObject PagePJ {get {if (_PagePJ == null) {_PagePJ = jQuery.Select("#" + clientId + "_PageP");}; return _PagePJ;}} private jQueryObject _PagePJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement PrevPageLink {get {if (_PrevPageLink == null) {_PrevPageLink = (AnchorElement)Document.GetElementById(clientId + "_PrevPageLink");}; return _PrevPageLink;}} private AnchorElement _PrevPageLink;
		public jQueryObject PrevPageLinkJ {get {if (_PrevPageLinkJ == null) {_PrevPageLinkJ = jQuery.Select("#" + clientId + "_PrevPageLink");}; return _PrevPageLinkJ;}} private jQueryObject _PrevPageLinkJ;
		public AnchorElement NextPageLink {get {if (_NextPageLink == null) {_NextPageLink = (AnchorElement)Document.GetElementById(clientId + "_NextPageLink");}; return _NextPageLink;}} private AnchorElement _NextPageLink;
		public jQueryObject NextPageLinkJ {get {if (_NextPageLinkJ == null) {_NextPageLinkJ = jQuery.Select("#" + clientId + "_NextPageLink");}; return _NextPageLinkJ;}} private jQueryObject _NextPageLinkJ;
		public Element DataList {get {if (_DataList == null) {_DataList = (Element)Document.GetElementById(clientId + "_DataList");}; return _DataList;}} private Element _DataList;
		public jQueryObject DataListJ {get {if (_DataListJ == null) {_DataListJ = jQuery.Select("#" + clientId + "_DataList");}; return _DataListJ;}} private jQueryObject _DataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element PageP1 {get {if (_PageP1 == null) {_PageP1 = (Element)Document.GetElementById(clientId + "_PageP1");}; return _PageP1;}} private Element _PageP1;
		public jQueryObject PageP1J {get {if (_PageP1J == null) {_PageP1J = jQuery.Select("#" + clientId + "_PageP1");}; return _PageP1J;}} private jQueryObject _PageP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement PrevPageLink1 {get {if (_PrevPageLink1 == null) {_PrevPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_PrevPageLink1");}; return _PrevPageLink1;}} private AnchorElement _PrevPageLink1;
		public jQueryObject PrevPageLink1J {get {if (_PrevPageLink1J == null) {_PrevPageLink1J = jQuery.Select("#" + clientId + "_PrevPageLink1");}; return _PrevPageLink1J;}} private jQueryObject _PrevPageLink1J;
		public AnchorElement NextPageLink1 {get {if (_NextPageLink1 == null) {_NextPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_NextPageLink1");}; return _NextPageLink1;}} private AnchorElement _NextPageLink1;
		public jQueryObject NextPageLink1J {get {if (_NextPageLink1J == null) {_NextPageLink1J = jQuery.Select("#" + clientId + "_NextPageLink1");}; return _NextPageLink1J;}} private jQueryObject _NextPageLink1J;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
