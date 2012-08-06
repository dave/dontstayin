//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.GroupBrowser
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
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelGroups {get {if (_PanelGroups == null) {_PanelGroups = (DivElement)Document.GetElementById(clientId + "_PanelGroups");}; return _PanelGroups;}} private DivElement _PanelGroups;
		public jQueryObject PanelGroupsJ {get {if (_PanelGroupsJ == null) {_PanelGroupsJ = jQuery.Select("#" + clientId + "_PanelGroups");}; return _PanelGroupsJ;}} private jQueryObject _PanelGroupsJ;
		public Element GroupsDataList {get {if (_GroupsDataList == null) {_GroupsDataList = (Element)Document.GetElementById(clientId + "_GroupsDataList");}; return _GroupsDataList;}} private Element _GroupsDataList;
		public jQueryObject GroupsDataListJ {get {if (_GroupsDataListJ == null) {_GroupsDataListJ = jQuery.Select("#" + clientId + "_GroupsDataList");}; return _GroupsDataListJ;}} private jQueryObject _GroupsDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
