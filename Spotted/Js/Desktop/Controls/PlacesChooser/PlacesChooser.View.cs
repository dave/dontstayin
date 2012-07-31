//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.PlacesChooser
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Js.ClientControls.MultiSelectorBehaviour uiPlacesMultiSelector {get {return (Js.ClientControls.MultiSelectorBehaviour) Script.Eval(clientId + "_uiPlacesMultiSelectorBehaviour");}}
		public Js.Controls.MapControl.Controller uiMap {get {return (Js.Controls.MapControl.Controller) Script.Eval(clientId + "_uiMapController");}}
		public DivElement uiPlacesRadius {get {if (_uiPlacesRadius == null) {_uiPlacesRadius = (DivElement)Document.GetElementById(clientId + "_uiPlacesRadius");}; return _uiPlacesRadius;}} private DivElement _uiPlacesRadius;
		public jQueryObject uiPlacesRadiusJ {get {if (_uiPlacesRadiusJ == null) {_uiPlacesRadiusJ = jQuery.Select("#" + clientId + "_uiPlacesRadius");}; return _uiPlacesRadiusJ;}} private jQueryObject _uiPlacesRadiusJ;
		public SelectElement uiNumberOfSurroundingTownsDropDown {get {if (_uiNumberOfSurroundingTownsDropDown == null) {_uiNumberOfSurroundingTownsDropDown = (SelectElement)Document.GetElementById(clientId + "_uiNumberOfSurroundingTownsDropDown");}; return _uiNumberOfSurroundingTownsDropDown;}} private SelectElement _uiNumberOfSurroundingTownsDropDown;
		public jQueryObject uiNumberOfSurroundingTownsDropDownJ {get {if (_uiNumberOfSurroundingTownsDropDownJ == null) {_uiNumberOfSurroundingTownsDropDownJ = jQuery.Select("#" + clientId + "_uiNumberOfSurroundingTownsDropDown");}; return _uiNumberOfSurroundingTownsDropDownJ;}} private jQueryObject _uiNumberOfSurroundingTownsDropDownJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiRadiusPlaceAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiRadiusPlaceAutoCompleteBehaviour");}}
		public Element uiAddRadiusButton {get {if (_uiAddRadiusButton == null) {_uiAddRadiusButton = (Element)Document.GetElementById(clientId + "_uiAddRadiusButton");}; return _uiAddRadiusButton;}} private Element _uiAddRadiusButton;
		public jQueryObject uiAddRadiusButtonJ {get {if (_uiAddRadiusButtonJ == null) {_uiAddRadiusButtonJ = jQuery.Select("#" + clientId + "_uiAddRadiusButton");}; return _uiAddRadiusButtonJ;}} private jQueryObject _uiAddRadiusButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
	}
}
