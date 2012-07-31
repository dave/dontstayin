//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.Guestlist
{
	public partial class View
		 : Js.BlankUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element GuestlistDataList {get {if (_GuestlistDataList == null) {_GuestlistDataList = (Element)Document.GetElementById(clientId + "_GuestlistDataList");}; return _GuestlistDataList;}} private Element _GuestlistDataList;
		public jQueryObject GuestlistDataListJ {get {if (_GuestlistDataListJ == null) {_GuestlistDataListJ = jQuery.Select("#" + clientId + "_GuestlistDataList");}; return _GuestlistDataListJ;}} private jQueryObject _GuestlistDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element EventLabel {get {if (_EventLabel == null) {_EventLabel = (Element)Document.GetElementById(clientId + "_EventLabel");}; return _EventLabel;}} private Element _EventLabel;
		public jQueryObject EventLabelJ {get {if (_EventLabelJ == null) {_EventLabelJ = jQuery.Select("#" + clientId + "_EventLabel");}; return _EventLabelJ;}} private jQueryObject _EventLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PriceLabel {get {if (_PriceLabel == null) {_PriceLabel = (Element)Document.GetElementById(clientId + "_PriceLabel");}; return _PriceLabel;}} private Element _PriceLabel;
		public jQueryObject PriceLabelJ {get {if (_PriceLabelJ == null) {_PriceLabelJ = jQuery.Select("#" + clientId + "_PriceLabel");}; return _PriceLabelJ;}} private jQueryObject _PriceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
