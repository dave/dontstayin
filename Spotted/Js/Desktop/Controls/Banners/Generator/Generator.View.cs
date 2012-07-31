using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.Banners.Generator
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public DivElement Holder {get {if (_Holder == null) {_Holder = (DivElement)Document.GetElementById(clientId + "_Holder");}; return _Holder;}} private DivElement _Holder;
		public jQueryObject HolderJ {get {if (_HolderJ == null) {_HolderJ = jQuery.Select("#" + clientId + "_Holder");}; return _HolderJ;}} private jQueryObject _HolderJ;
		public DivElement uiBanner {get {if (_uiBanner == null) {_uiBanner = (DivElement)Document.GetElementById(clientId + "_uiBanner");}; return _uiBanner;}} private DivElement _uiBanner;
		public jQueryObject uiBannerJ {get {if (_uiBannerJ == null) {_uiBannerJ = jQuery.Select("#" + clientId + "_uiBanner");}; return _uiBannerJ;}} private jQueryObject _uiBannerJ;
		public InputElement uiPosition {get {if (_uiPosition == null) {_uiPosition = (InputElement)Document.GetElementById(clientId + "_uiPosition");}; return _uiPosition;}} private InputElement _uiPosition;
		public jQueryObject uiPositionJ {get {if (_uiPositionJ == null) {_uiPositionJ = jQuery.Select("#" + clientId + "_uiPosition");}; return _uiPositionJ;}} private jQueryObject _uiPositionJ;
		public InputElement uiK {get {if (_uiK == null) {_uiK = (InputElement)Document.GetElementById(clientId + "_uiK");}; return _uiK;}} private InputElement _uiK;
		public jQueryObject uiKJ {get {if (_uiKJ == null) {_uiKJ = jQuery.Select("#" + clientId + "_uiK");}; return _uiKJ;}} private jQueryObject _uiKJ;
		public InputElement uiMusicTypes {get {if (_uiMusicTypes == null) {_uiMusicTypes = (InputElement)Document.GetElementById(clientId + "_uiMusicTypes");}; return _uiMusicTypes;}} private InputElement _uiMusicTypes;
		public jQueryObject uiMusicTypesJ {get {if (_uiMusicTypesJ == null) {_uiMusicTypesJ = jQuery.Select("#" + clientId + "_uiMusicTypes");}; return _uiMusicTypesJ;}} private jQueryObject _uiMusicTypesJ;
		public InputElement uiPlaceKs {get {if (_uiPlaceKs == null) {_uiPlaceKs = (InputElement)Document.GetElementById(clientId + "_uiPlaceKs");}; return _uiPlaceKs;}} private InputElement _uiPlaceKs;
		public jQueryObject uiPlaceKsJ {get {if (_uiPlaceKsJ == null) {_uiPlaceKsJ = jQuery.Select("#" + clientId + "_uiPlaceKs");}; return _uiPlaceKsJ;}} private jQueryObject _uiPlaceKsJ;
		public InputElement uiDuration {get {if (_uiDuration == null) {_uiDuration = (InputElement)Document.GetElementById(clientId + "_uiDuration");}; return _uiDuration;}} private InputElement _uiDuration;
		public jQueryObject uiDurationJ {get {if (_uiDurationJ == null) {_uiDurationJ = jQuery.Select("#" + clientId + "_uiDuration");}; return _uiDurationJ;}} private jQueryObject _uiDurationJ;
		public InputElement uiInactivityTimeoutDuration {get {if (_uiInactivityTimeoutDuration == null) {_uiInactivityTimeoutDuration = (InputElement)Document.GetElementById(clientId + "_uiInactivityTimeoutDuration");}; return _uiInactivityTimeoutDuration;}} private InputElement _uiInactivityTimeoutDuration;
		public jQueryObject uiInactivityTimeoutDurationJ {get {if (_uiInactivityTimeoutDurationJ == null) {_uiInactivityTimeoutDurationJ = jQuery.Select("#" + clientId + "_uiInactivityTimeoutDuration");}; return _uiInactivityTimeoutDurationJ;}} private jQueryObject _uiInactivityTimeoutDurationJ;
		public InputElement uiClickHelperLeft {get {if (_uiClickHelperLeft == null) {_uiClickHelperLeft = (InputElement)Document.GetElementById(clientId + "_uiClickHelperLeft");}; return _uiClickHelperLeft;}} private InputElement _uiClickHelperLeft;
		public jQueryObject uiClickHelperLeftJ {get {if (_uiClickHelperLeftJ == null) {_uiClickHelperLeftJ = jQuery.Select("#" + clientId + "_uiClickHelperLeft");}; return _uiClickHelperLeftJ;}} private jQueryObject _uiClickHelperLeftJ;
		public InputElement uiClickHelperTop {get {if (_uiClickHelperTop == null) {_uiClickHelperTop = (InputElement)Document.GetElementById(clientId + "_uiClickHelperTop");}; return _uiClickHelperTop;}} private InputElement _uiClickHelperTop;
		public jQueryObject uiClickHelperTopJ {get {if (_uiClickHelperTopJ == null) {_uiClickHelperTopJ = jQuery.Select("#" + clientId + "_uiClickHelperTop");}; return _uiClickHelperTopJ;}} private jQueryObject _uiClickHelperTopJ;
	}
}
