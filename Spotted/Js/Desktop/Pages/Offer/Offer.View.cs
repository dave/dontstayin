//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Offer
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
		public DivElement PanelEnd {get {if (_PanelEnd == null) {_PanelEnd = (DivElement)Document.GetElementById(clientId + "_PanelEnd");}; return _PanelEnd;}} private DivElement _PanelEnd;
		public jQueryObject PanelEndJ {get {if (_PanelEndJ == null) {_PanelEndJ = jQuery.Select("#" + clientId + "_PanelEnd");}; return _PanelEndJ;}} private jQueryObject _PanelEndJ;
		public Element H5 {get {if (_H5 == null) {_H5 = (Element)Document.GetElementById(clientId + "_H5");}; return _H5;}} private Element _H5;
		public jQueryObject H5J {get {if (_H5J == null) {_H5J = jQuery.Select("#" + clientId + "_H5");}; return _H5J;}} private jQueryObject _H5J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
