//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.Spottings
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
		public DivElement PanelSpottings {get {if (_PanelSpottings == null) {_PanelSpottings = (DivElement)Document.GetElementById(clientId + "_PanelSpottings");}; return _PanelSpottings;}} private DivElement _PanelSpottings;
		public jQueryObject PanelSpottingsJ {get {if (_PanelSpottingsJ == null) {_PanelSpottingsJ = jQuery.Select("#" + clientId + "_PanelSpottings");}; return _PanelSpottingsJ;}} private jQueryObject _PanelSpottingsJ;
		public ImageElement SpotterIcon {get {if (_SpotterIcon == null) {_SpotterIcon = (ImageElement)Document.GetElementById(clientId + "_SpotterIcon");}; return _SpotterIcon;}} private ImageElement _SpotterIcon;
		public jQueryObject SpotterIconJ {get {if (_SpotterIconJ == null) {_SpotterIconJ = jQuery.Select("#" + clientId + "_SpotterIcon");}; return _SpotterIconJ;}} private jQueryObject _SpotterIconJ;
		public Element SpottingsDataList {get {if (_SpottingsDataList == null) {_SpottingsDataList = (Element)Document.GetElementById(clientId + "_SpottingsDataList");}; return _SpottingsDataList;}} private Element _SpottingsDataList;
		public jQueryObject SpottingsDataListJ {get {if (_SpottingsDataListJ == null) {_SpottingsDataListJ = jQuery.Select("#" + clientId + "_SpottingsDataList");}; return _SpottingsDataListJ;}} private jQueryObject _SpottingsDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public AnchorElement NoRecordsNewAnchor {get {if (_NoRecordsNewAnchor == null) {_NoRecordsNewAnchor = (AnchorElement)Document.GetElementById(clientId + "_NoRecordsNewAnchor");}; return _NoRecordsNewAnchor;}} private AnchorElement _NoRecordsNewAnchor;
		public jQueryObject NoRecordsNewAnchorJ {get {if (_NoRecordsNewAnchorJ == null) {_NoRecordsNewAnchorJ = jQuery.Select("#" + clientId + "_NoRecordsNewAnchor");}; return _NoRecordsNewAnchorJ;}} private jQueryObject _NoRecordsNewAnchorJ;
		public Element NoRecordsP {get {if (_NoRecordsP == null) {_NoRecordsP = (Element)Document.GetElementById(clientId + "_NoRecordsP");}; return _NoRecordsP;}} private Element _NoRecordsP;
		public jQueryObject NoRecordsPJ {get {if (_NoRecordsPJ == null) {_NoRecordsPJ = jQuery.Select("#" + clientId + "_NoRecordsP");}; return _NoRecordsPJ;}} private jQueryObject _NoRecordsPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element DataListP {get {if (_DataListP == null) {_DataListP = (Element)Document.GetElementById(clientId + "_DataListP");}; return _DataListP;}} private Element _DataListP;
		public jQueryObject DataListPJ {get {if (_DataListPJ == null) {_DataListPJ = jQuery.Select("#" + clientId + "_DataListP");}; return _DataListPJ;}} private jQueryObject _DataListPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ListPageLinksP {get {if (_ListPageLinksP == null) {_ListPageLinksP = (Element)Document.GetElementById(clientId + "_ListPageLinksP");}; return _ListPageLinksP;}} private Element _ListPageLinksP;
		public jQueryObject ListPageLinksPJ {get {if (_ListPageLinksPJ == null) {_ListPageLinksPJ = jQuery.Select("#" + clientId + "_ListPageLinksP");}; return _ListPageLinksPJ;}} private jQueryObject _ListPageLinksPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element UsrIntro {get {if (_UsrIntro == null) {_UsrIntro = (Element)Document.GetElementById(clientId + "_UsrIntro");}; return _UsrIntro;}} private Element _UsrIntro;
		public jQueryObject UsrIntroJ {get {if (_UsrIntroJ == null) {_UsrIntroJ = jQuery.Select("#" + clientId + "_UsrIntro");}; return _UsrIntroJ;}} private jQueryObject _UsrIntroJ;//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
