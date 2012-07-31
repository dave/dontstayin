//mappings.Add("Spotted.Controls.DonateText.Basic", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.DonateText.Default", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.DonateText.Monkey", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.DonateText.Default
{
	public partial class View
		 : Js.Controls.DonateText.DonateTextControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element uiBasic {get {if (_uiBasic == null) {_uiBasic = (Element)Document.GetElementById(clientId + "_uiBasic");}; return _uiBasic;}} private Element _uiBasic;
		public jQueryObject uiBasicJ {get {if (_uiBasicJ == null) {_uiBasicJ = jQuery.Select("#" + clientId + "_uiBasic");}; return _uiBasicJ;}} private jQueryObject _uiBasicJ;//mappings.Add("Spotted.Controls.DonateText.Basic", ElementGetter("Element"));
		public Element uiDefault {get {if (_uiDefault == null) {_uiDefault = (Element)Document.GetElementById(clientId + "_uiDefault");}; return _uiDefault;}} private Element _uiDefault;
		public jQueryObject uiDefaultJ {get {if (_uiDefaultJ == null) {_uiDefaultJ = jQuery.Select("#" + clientId + "_uiDefault");}; return _uiDefaultJ;}} private jQueryObject _uiDefaultJ;//mappings.Add("Spotted.Controls.DonateText.Default", ElementGetter("Element"));
		public Element uiMonkey {get {if (_uiMonkey == null) {_uiMonkey = (Element)Document.GetElementById(clientId + "_uiMonkey");}; return _uiMonkey;}} private Element _uiMonkey;
		public jQueryObject uiMonkeyJ {get {if (_uiMonkeyJ == null) {_uiMonkeyJ = jQuery.Select("#" + clientId + "_uiMonkey");}; return _uiMonkeyJ;}} private jQueryObject _uiMonkeyJ;//mappings.Add("Spotted.Controls.DonateText.Monkey", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
