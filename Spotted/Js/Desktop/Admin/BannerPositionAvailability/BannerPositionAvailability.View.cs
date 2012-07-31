//mappings.Add("Spotted.Controls.Admin.BannerAvailability", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.BannerPositionAvailability
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
		public Element Slots0 {get {if (_Slots0 == null) {_Slots0 = (Element)Document.GetElementById(clientId + "_Slots0");}; return _Slots0;}} private Element _Slots0;
		public jQueryObject Slots0J {get {if (_Slots0J == null) {_Slots0J = jQuery.Select("#" + clientId + "_Slots0");}; return _Slots0J;}} private jQueryObject _Slots0J;//mappings.Add("Spotted.Controls.Admin.BannerAvailability", ElementGetter("Element"));
		public Element Slots1 {get {if (_Slots1 == null) {_Slots1 = (Element)Document.GetElementById(clientId + "_Slots1");}; return _Slots1;}} private Element _Slots1;
		public jQueryObject Slots1J {get {if (_Slots1J == null) {_Slots1J = jQuery.Select("#" + clientId + "_Slots1");}; return _Slots1J;}} private jQueryObject _Slots1J;//mappings.Add("Spotted.Controls.Admin.BannerAvailability", ElementGetter("Element"));
		public Element Slots4 {get {if (_Slots4 == null) {_Slots4 = (Element)Document.GetElementById(clientId + "_Slots4");}; return _Slots4;}} private Element _Slots4;
		public jQueryObject Slots4J {get {if (_Slots4J == null) {_Slots4J = jQuery.Select("#" + clientId + "_Slots4");}; return _Slots4J;}} private jQueryObject _Slots4J;//mappings.Add("Spotted.Controls.Admin.BannerAvailability", ElementGetter("Element"));
		public Element Slots3 {get {if (_Slots3 == null) {_Slots3 = (Element)Document.GetElementById(clientId + "_Slots3");}; return _Slots3;}} private Element _Slots3;
		public jQueryObject Slots3J {get {if (_Slots3J == null) {_Slots3J = jQuery.Select("#" + clientId + "_Slots3");}; return _Slots3J;}} private jQueryObject _Slots3J;//mappings.Add("Spotted.Controls.Admin.BannerAvailability", ElementGetter("Element"));
		public Element Slots2 {get {if (_Slots2 == null) {_Slots2 = (Element)Document.GetElementById(clientId + "_Slots2");}; return _Slots2;}} private Element _Slots2;
		public jQueryObject Slots2J {get {if (_Slots2J == null) {_Slots2J = jQuery.Select("#" + clientId + "_Slots2");}; return _Slots2J;}} private jQueryObject _Slots2J;//mappings.Add("Spotted.Controls.Admin.BannerAvailability", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
