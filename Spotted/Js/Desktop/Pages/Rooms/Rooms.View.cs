//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Rooms
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
		public Element MessagesHeader {get {if (_MessagesHeader == null) {_MessagesHeader = (Element)Document.GetElementById(clientId + "_MessagesHeader");}; return _MessagesHeader;}} private Element _MessagesHeader;
		public jQueryObject MessagesHeaderJ {get {if (_MessagesHeaderJ == null) {_MessagesHeaderJ = jQuery.Select("#" + clientId + "_MessagesHeader");}; return _MessagesHeaderJ;}} private jQueryObject _MessagesHeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public CheckBoxElement PopupAminationsOn {get {if (_PopupAminationsOn == null) {_PopupAminationsOn = (CheckBoxElement)Document.GetElementById(clientId + "_PopupAminationsOn");}; return _PopupAminationsOn;}} private CheckBoxElement _PopupAminationsOn;
		public jQueryObject PopupAminationsOnJ {get {if (_PopupAminationsOnJ == null) {_PopupAminationsOnJ = jQuery.Select("#" + clientId + "_PopupAminationsOn");}; return _PopupAminationsOnJ;}} private jQueryObject _PopupAminationsOnJ;
		public CheckBoxElement PopupAminationsOff {get {if (_PopupAminationsOff == null) {_PopupAminationsOff = (CheckBoxElement)Document.GetElementById(clientId + "_PopupAminationsOff");}; return _PopupAminationsOff;}} private CheckBoxElement _PopupAminationsOff;
		public jQueryObject PopupAminationsOffJ {get {if (_PopupAminationsOffJ == null) {_PopupAminationsOffJ = jQuery.Select("#" + clientId + "_PopupAminationsOff");}; return _PopupAminationsOffJ;}} private jQueryObject _PopupAminationsOffJ;
		public DivElement Testing {get {if (_Testing == null) {_Testing = (DivElement)Document.GetElementById(clientId + "_Testing");}; return _Testing;}} private DivElement _Testing;
		public jQueryObject TestingJ {get {if (_TestingJ == null) {_TestingJ = jQuery.Select("#" + clientId + "_Testing");}; return _TestingJ;}} private jQueryObject _TestingJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
