//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.MapControl
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element map {get {if (_map == null) {_map = (Element)Document.GetElementById(clientId + "_map");}; return _map;}} private Element _map;
		public jQueryObject mapJ {get {if (_mapJ == null) {_mapJ = jQuery.Select("#" + clientId + "_map");}; return _mapJ;}} private jQueryObject _mapJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement uiNorth {get {if (_uiNorth == null) {_uiNorth = (InputElement)Document.GetElementById(clientId + "_uiNorth");}; return _uiNorth;}} private InputElement _uiNorth;
		public jQueryObject uiNorthJ {get {if (_uiNorthJ == null) {_uiNorthJ = jQuery.Select("#" + clientId + "_uiNorth");}; return _uiNorthJ;}} private jQueryObject _uiNorthJ;
		public InputElement uiSouth {get {if (_uiSouth == null) {_uiSouth = (InputElement)Document.GetElementById(clientId + "_uiSouth");}; return _uiSouth;}} private InputElement _uiSouth;
		public jQueryObject uiSouthJ {get {if (_uiSouthJ == null) {_uiSouthJ = jQuery.Select("#" + clientId + "_uiSouth");}; return _uiSouthJ;}} private jQueryObject _uiSouthJ;
		public InputElement uiEast {get {if (_uiEast == null) {_uiEast = (InputElement)Document.GetElementById(clientId + "_uiEast");}; return _uiEast;}} private InputElement _uiEast;
		public jQueryObject uiEastJ {get {if (_uiEastJ == null) {_uiEastJ = jQuery.Select("#" + clientId + "_uiEast");}; return _uiEastJ;}} private jQueryObject _uiEastJ;
		public InputElement uiWest {get {if (_uiWest == null) {_uiWest = (InputElement)Document.GetElementById(clientId + "_uiWest");}; return _uiWest;}} private InputElement _uiWest;
		public jQueryObject uiWestJ {get {if (_uiWestJ == null) {_uiWestJ = jQuery.Select("#" + clientId + "_uiWest");}; return _uiWestJ;}} private jQueryObject _uiWestJ;
	}
}
