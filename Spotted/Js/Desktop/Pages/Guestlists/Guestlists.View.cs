//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Guestlists
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
		public Element H17 {get {if (_H17 == null) {_H17 = (Element)Document.GetElementById(clientId + "_H17");}; return _H17;}} private Element _H17;
		public jQueryObject H17J {get {if (_H17J == null) {_H17J = jQuery.Select("#" + clientId + "_H17");}; return _H17J;}} private jQueryObject _H17J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element OtherEventDataList {get {if (_OtherEventDataList == null) {_OtherEventDataList = (Element)Document.GetElementById(clientId + "_OtherEventDataList");}; return _OtherEventDataList;}} private Element _OtherEventDataList;
		public jQueryObject OtherEventDataListJ {get {if (_OtherEventDataListJ == null) {_OtherEventDataListJ = jQuery.Select("#" + clientId + "_OtherEventDataList");}; return _OtherEventDataListJ;}} private jQueryObject _OtherEventDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element CurrentEventDataList {get {if (_CurrentEventDataList == null) {_CurrentEventDataList = (Element)Document.GetElementById(clientId + "_CurrentEventDataList");}; return _CurrentEventDataList;}} private Element _CurrentEventDataList;
		public jQueryObject CurrentEventDataListJ {get {if (_CurrentEventDataListJ == null) {_CurrentEventDataListJ = jQuery.Select("#" + clientId + "_CurrentEventDataList");}; return _CurrentEventDataListJ;}} private jQueryObject _CurrentEventDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
