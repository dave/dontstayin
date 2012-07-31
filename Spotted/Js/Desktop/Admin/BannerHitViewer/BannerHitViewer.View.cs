//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.BannerHitViewer
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
		public SelectElement TimeslotStart {get {if (_TimeslotStart == null) {_TimeslotStart = (SelectElement)Document.GetElementById(clientId + "_TimeslotStart");}; return _TimeslotStart;}} private SelectElement _TimeslotStart;
		public jQueryObject TimeslotStartJ {get {if (_TimeslotStartJ == null) {_TimeslotStartJ = jQuery.Select("#" + clientId + "_TimeslotStart");}; return _TimeslotStartJ;}} private jQueryObject _TimeslotStartJ;
		public SelectElement TimeslotEnd {get {if (_TimeslotEnd == null) {_TimeslotEnd = (SelectElement)Document.GetElementById(clientId + "_TimeslotEnd");}; return _TimeslotEnd;}} private SelectElement _TimeslotEnd;
		public jQueryObject TimeslotEndJ {get {if (_TimeslotEndJ == null) {_TimeslotEndJ = jQuery.Select("#" + clientId + "_TimeslotEnd");}; return _TimeslotEndJ;}} private jQueryObject _TimeslotEndJ;
		public Element TimeslotInfoRepeater {get {if (_TimeslotInfoRepeater == null) {_TimeslotInfoRepeater = (Element)Document.GetElementById(clientId + "_TimeslotInfoRepeater");}; return _TimeslotInfoRepeater;}} private Element _TimeslotInfoRepeater;
		public jQueryObject TimeslotInfoRepeaterJ {get {if (_TimeslotInfoRepeaterJ == null) {_TimeslotInfoRepeaterJ = jQuery.Select("#" + clientId + "_TimeslotInfoRepeater");}; return _TimeslotInfoRepeaterJ;}} private jQueryObject _TimeslotInfoRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element BannerInfoRepeater {get {if (_BannerInfoRepeater == null) {_BannerInfoRepeater = (Element)Document.GetElementById(clientId + "_BannerInfoRepeater");}; return _BannerInfoRepeater;}} private Element _BannerInfoRepeater;
		public jQueryObject BannerInfoRepeaterJ {get {if (_BannerInfoRepeaterJ == null) {_BannerInfoRepeaterJ = jQuery.Select("#" + clientId + "_BannerInfoRepeater");}; return _BannerInfoRepeaterJ;}} private jQueryObject _BannerInfoRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
