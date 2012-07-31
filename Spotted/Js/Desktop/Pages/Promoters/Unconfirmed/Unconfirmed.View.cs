//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Unconfirmed
{
	public partial class View
		 : Js.Pages.Promoters.PromoterUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement PanelUnconfirmed {get {if (_PanelUnconfirmed == null) {_PanelUnconfirmed = (DivElement)Document.GetElementById(clientId + "_PanelUnconfirmed");}; return _PanelUnconfirmed;}} private DivElement _PanelUnconfirmed;
		public jQueryObject PanelUnconfirmedJ {get {if (_PanelUnconfirmedJ == null) {_PanelUnconfirmedJ = jQuery.Select("#" + clientId + "_PanelUnconfirmed");}; return _PanelUnconfirmedJ;}} private jQueryObject _PanelUnconfirmedJ;
		public Element PromoterIntro {get {if (_PromoterIntro == null) {_PromoterIntro = (Element)Document.GetElementById(clientId + "_PromoterIntro");}; return _PromoterIntro;}} private Element _PromoterIntro;
		public jQueryObject PromoterIntroJ {get {if (_PromoterIntroJ == null) {_PromoterIntroJ = jQuery.Select("#" + clientId + "_PromoterIntro");}; return _PromoterIntroJ;}} private jQueryObject _PromoterIntroJ;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
