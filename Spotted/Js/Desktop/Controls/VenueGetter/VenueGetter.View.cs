using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.VenueGetter
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public DivElement uiOuterPanel {get {if (_uiOuterPanel == null) {_uiOuterPanel = (DivElement)Document.GetElementById(clientId + "_uiOuterPanel");}; return _uiOuterPanel;}} private DivElement _uiOuterPanel;
		public jQueryObject uiOuterPanelJ {get {if (_uiOuterPanelJ == null) {_uiOuterPanelJ = jQuery.Select("#" + clientId + "_uiOuterPanel");}; return _uiOuterPanelJ;}} private jQueryObject _uiOuterPanelJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiAuto {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiAutoBehaviour");}}
		public DivElement uiSelectedItemPanel {get {if (_uiSelectedItemPanel == null) {_uiSelectedItemPanel = (DivElement)Document.GetElementById(clientId + "_uiSelectedItemPanel");}; return _uiSelectedItemPanel;}} private DivElement _uiSelectedItemPanel;
		public jQueryObject uiSelectedItemPanelJ {get {if (_uiSelectedItemPanelJ == null) {_uiSelectedItemPanelJ = jQuery.Select("#" + clientId + "_uiSelectedItemPanel");}; return _uiSelectedItemPanelJ;}} private jQueryObject _uiSelectedItemPanelJ;
	}
}
