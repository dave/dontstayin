//mappings.Add("Spotted.Controls.GroupsListedByHeading", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Groups.Categories
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
		public Element uiList {get {if (_uiList == null) {_uiList = (Element)Document.GetElementById(clientId + "_uiList");}; return _uiList;}} private Element _uiList;
		public jQueryObject uiListJ {get {if (_uiListJ == null) {_uiListJ = jQuery.Select("#" + clientId + "_uiList");}; return _uiListJ;}} private jQueryObject _uiListJ;//mappings.Add("Spotted.Controls.GroupsListedByHeading", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
