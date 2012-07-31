//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Intro
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
		public DivElement PanelNoAccount {get {if (_PanelNoAccount == null) {_PanelNoAccount = (DivElement)Document.GetElementById(clientId + "_PanelNoAccount");}; return _PanelNoAccount;}} private DivElement _PanelNoAccount;
		public jQueryObject PanelNoAccountJ {get {if (_PanelNoAccountJ == null) {_PanelNoAccountJ = jQuery.Select("#" + clientId + "_PanelNoAccount");}; return _PanelNoAccountJ;}} private jQueryObject _PanelNoAccountJ;
		public DivElement PanelPromoterList {get {if (_PanelPromoterList == null) {_PanelPromoterList = (DivElement)Document.GetElementById(clientId + "_PanelPromoterList");}; return _PanelPromoterList;}} private DivElement _PanelPromoterList;
		public jQueryObject PanelPromoterListJ {get {if (_PanelPromoterListJ == null) {_PanelPromoterListJ = jQuery.Select("#" + clientId + "_PanelPromoterList");}; return _PanelPromoterListJ;}} private jQueryObject _PanelPromoterListJ;
		public Element PromoterRepeater {get {if (_PromoterRepeater == null) {_PromoterRepeater = (Element)Document.GetElementById(clientId + "_PromoterRepeater");}; return _PromoterRepeater;}} private Element _PromoterRepeater;
		public jQueryObject PromoterRepeaterJ {get {if (_PromoterRepeaterJ == null) {_PromoterRepeaterJ = jQuery.Select("#" + clientId + "_PromoterRepeater");}; return _PromoterRepeaterJ;}} private jQueryObject _PromoterRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
