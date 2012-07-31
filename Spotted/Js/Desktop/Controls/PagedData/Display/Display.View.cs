//mappings.Add("Spotted.EnhancedUserControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.PagedData.Display
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiHeaderControl {get {if (_uiHeaderControl == null) {_uiHeaderControl = (Element)Document.GetElementById(clientId + "_uiHeaderControl");}; return _uiHeaderControl;}} private Element _uiHeaderControl;
		public jQueryObject uiHeaderControlJ {get {if (_uiHeaderControlJ == null) {_uiHeaderControlJ = jQuery.Select("#" + clientId + "_uiHeaderControl");}; return _uiHeaderControlJ;}} private jQueryObject _uiHeaderControlJ;//mappings.Add("Spotted.EnhancedUserControl", ElementGetter("Element"));
		public DivElement uiPanel {get {if (_uiPanel == null) {_uiPanel = (DivElement)Document.GetElementById(clientId + "_uiPanel");}; return _uiPanel;}} private DivElement _uiPanel;
		public jQueryObject uiPanelJ {get {if (_uiPanelJ == null) {_uiPanelJ = jQuery.Select("#" + clientId + "_uiPanel");}; return _uiPanelJ;}} private jQueryObject _uiPanelJ;
		public Js.Controls.PaginationControl2.Controller uiPager {get {return (Js.Controls.PaginationControl2.Controller) Script.Eval(clientId + "_uiPagerController");}}
		public Js.Controls.ClientSideRepeater.Repeater.Controller uiRepeater {get {return (Js.Controls.ClientSideRepeater.Repeater.Controller) Script.Eval(clientId + "_uiRepeaterController");}}
		public InputElement uiDefaultTop {get {if (_uiDefaultTop == null) {_uiDefaultTop = (InputElement)Document.GetElementById(clientId + "_uiDefaultTop");}; return _uiDefaultTop;}} private InputElement _uiDefaultTop;
		public jQueryObject uiDefaultTopJ {get {if (_uiDefaultTopJ == null) {_uiDefaultTopJ = jQuery.Select("#" + clientId + "_uiDefaultTop");}; return _uiDefaultTopJ;}} private jQueryObject _uiDefaultTopJ;
		public InputElement uiPageSize {get {if (_uiPageSize == null) {_uiPageSize = (InputElement)Document.GetElementById(clientId + "_uiPageSize");}; return _uiPageSize;}} private InputElement _uiPageSize;
		public jQueryObject uiPageSizeJ {get {if (_uiPageSizeJ == null) {_uiPageSizeJ = jQuery.Select("#" + clientId + "_uiPageSize");}; return _uiPageSizeJ;}} private jQueryObject _uiPageSizeJ;
		public InputElement uiServicePath {get {if (_uiServicePath == null) {_uiServicePath = (InputElement)Document.GetElementById(clientId + "_uiServicePath");}; return _uiServicePath;}} private InputElement _uiServicePath;
		public jQueryObject uiServicePathJ {get {if (_uiServicePathJ == null) {_uiServicePathJ = jQuery.Select("#" + clientId + "_uiServicePath");}; return _uiServicePathJ;}} private jQueryObject _uiServicePathJ;
		public InputElement uiServiceMethod {get {if (_uiServiceMethod == null) {_uiServiceMethod = (InputElement)Document.GetElementById(clientId + "_uiServiceMethod");}; return _uiServiceMethod;}} private InputElement _uiServiceMethod;
		public jQueryObject uiServiceMethodJ {get {if (_uiServiceMethodJ == null) {_uiServiceMethodJ = jQuery.Select("#" + clientId + "_uiServiceMethod");}; return _uiServiceMethodJ;}} private jQueryObject _uiServiceMethodJ;
		public InputElement uiTimeout {get {if (_uiTimeout == null) {_uiTimeout = (InputElement)Document.GetElementById(clientId + "_uiTimeout");}; return _uiTimeout;}} private InputElement _uiTimeout;
		public jQueryObject uiTimeoutJ {get {if (_uiTimeoutJ == null) {_uiTimeoutJ = jQuery.Select("#" + clientId + "_uiTimeout");}; return _uiTimeoutJ;}} private jQueryObject _uiTimeoutJ;
		public InputElement uiTabName {get {if (_uiTabName == null) {_uiTabName = (InputElement)Document.GetElementById(clientId + "_uiTabName");}; return _uiTabName;}} private InputElement _uiTabName;
		public jQueryObject uiTabNameJ {get {if (_uiTabNameJ == null) {_uiTabNameJ = jQuery.Select("#" + clientId + "_uiTabName");}; return _uiTabNameJ;}} private jQueryObject _uiTabNameJ;
	}
}
