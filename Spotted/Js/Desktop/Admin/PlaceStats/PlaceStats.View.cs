//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.PlaceStats
{
	public partial class View
		 : Js.AdminUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element PlaceName {get {if (_PlaceName == null) {_PlaceName = (Element)Document.GetElementById(clientId + "_PlaceName");}; return _PlaceName;}} private Element _PlaceName;
		public jQueryObject PlaceNameJ {get {if (_PlaceNameJ == null) {_PlaceNameJ = jQuery.Select("#" + clientId + "_PlaceName");}; return _PlaceNameJ;}} private jQueryObject _PlaceNameJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element Tab {get {if (_Tab == null) {_Tab = (Element)Document.GetElementById(clientId + "_Tab");}; return _Tab;}} private Element _Tab;
		public jQueryObject TabJ {get {if (_TabJ == null) {_TabJ = jQuery.Select("#" + clientId + "_Tab");}; return _TabJ;}} private jQueryObject _TabJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
