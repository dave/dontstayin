//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.ProSpotters
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
		public DivElement PanelProSpotters {get {if (_PanelProSpotters == null) {_PanelProSpotters = (DivElement)Document.GetElementById(clientId + "_PanelProSpotters");}; return _PanelProSpotters;}} private DivElement _PanelProSpotters;
		public jQueryObject PanelProSpottersJ {get {if (_PanelProSpottersJ == null) {_PanelProSpottersJ = jQuery.Select("#" + clientId + "_PanelProSpotters");}; return _PanelProSpottersJ;}} private jQueryObject _PanelProSpottersJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element ProSpottersDataList {get {if (_ProSpottersDataList == null) {_ProSpottersDataList = (Element)Document.GetElementById(clientId + "_ProSpottersDataList");}; return _ProSpottersDataList;}} private Element _ProSpottersDataList;
		public jQueryObject ProSpottersDataListJ {get {if (_ProSpottersDataListJ == null) {_ProSpottersDataListJ = jQuery.Select("#" + clientId + "_ProSpottersDataList");}; return _ProSpottersDataListJ;}} private jQueryObject _ProSpottersDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
