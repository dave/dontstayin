//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Online
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
		public Element OnlineLabel {get {if (_OnlineLabel == null) {_OnlineLabel = (Element)Document.GetElementById(clientId + "_OnlineLabel");}; return _OnlineLabel;}} private Element _OnlineLabel;
		public jQueryObject OnlineLabelJ {get {if (_OnlineLabelJ == null) {_OnlineLabelJ = jQuery.Select("#" + clientId + "_OnlineLabel");}; return _OnlineLabelJ;}} private jQueryObject _OnlineLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element OnlineP {get {if (_OnlineP == null) {_OnlineP = (Element)Document.GetElementById(clientId + "_OnlineP");}; return _OnlineP;}} private Element _OnlineP;
		public jQueryObject OnlinePJ {get {if (_OnlinePJ == null) {_OnlinePJ = jQuery.Select("#" + clientId + "_OnlineP");}; return _OnlinePJ;}} private jQueryObject _OnlinePJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element OnlineDataList {get {if (_OnlineDataList == null) {_OnlineDataList = (Element)Document.GetElementById(clientId + "_OnlineDataList");}; return _OnlineDataList;}} private Element _OnlineDataList;
		public jQueryObject OnlineDataListJ {get {if (_OnlineDataListJ == null) {_OnlineDataListJ = jQuery.Select("#" + clientId + "_OnlineDataList");}; return _OnlineDataListJ;}} private jQueryObject _OnlineDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
