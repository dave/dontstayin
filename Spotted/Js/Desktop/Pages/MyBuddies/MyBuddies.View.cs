//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.MyBuddies
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
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelNoBuddies {get {if (_PanelNoBuddies == null) {_PanelNoBuddies = (DivElement)Document.GetElementById(clientId + "_PanelNoBuddies");}; return _PanelNoBuddies;}} private DivElement _PanelNoBuddies;
		public jQueryObject PanelNoBuddiesJ {get {if (_PanelNoBuddiesJ == null) {_PanelNoBuddiesJ = jQuery.Select("#" + clientId + "_PanelNoBuddies");}; return _PanelNoBuddiesJ;}} private jQueryObject _PanelNoBuddiesJ;
		public DivElement PanelBuddies {get {if (_PanelBuddies == null) {_PanelBuddies = (DivElement)Document.GetElementById(clientId + "_PanelBuddies");}; return _PanelBuddies;}} private DivElement _PanelBuddies;
		public jQueryObject PanelBuddiesJ {get {if (_PanelBuddiesJ == null) {_PanelBuddiesJ = jQuery.Select("#" + clientId + "_PanelBuddies");}; return _PanelBuddiesJ;}} private jQueryObject _PanelBuddiesJ;
		public DivElement FullBuddyListPanel {get {if (_FullBuddyListPanel == null) {_FullBuddyListPanel = (DivElement)Document.GetElementById(clientId + "_FullBuddyListPanel");}; return _FullBuddyListPanel;}} private DivElement _FullBuddyListPanel;
		public jQueryObject FullBuddyListPanelJ {get {if (_FullBuddyListPanelJ == null) {_FullBuddyListPanelJ = jQuery.Select("#" + clientId + "_FullBuddyListPanel");}; return _FullBuddyListPanelJ;}} private jQueryObject _FullBuddyListPanelJ;
		public DivElement HalfBuddyListPanel {get {if (_HalfBuddyListPanel == null) {_HalfBuddyListPanel = (DivElement)Document.GetElementById(clientId + "_HalfBuddyListPanel");}; return _HalfBuddyListPanel;}} private DivElement _HalfBuddyListPanel;
		public jQueryObject HalfBuddyListPanelJ {get {if (_HalfBuddyListPanelJ == null) {_HalfBuddyListPanelJ = jQuery.Select("#" + clientId + "_HalfBuddyListPanel");}; return _HalfBuddyListPanelJ;}} private jQueryObject _HalfBuddyListPanelJ;
		public DivElement ReverseHalfBuddyListPanel {get {if (_ReverseHalfBuddyListPanel == null) {_ReverseHalfBuddyListPanel = (DivElement)Document.GetElementById(clientId + "_ReverseHalfBuddyListPanel");}; return _ReverseHalfBuddyListPanel;}} private DivElement _ReverseHalfBuddyListPanel;
		public jQueryObject ReverseHalfBuddyListPanelJ {get {if (_ReverseHalfBuddyListPanelJ == null) {_ReverseHalfBuddyListPanelJ = jQuery.Select("#" + clientId + "_ReverseHalfBuddyListPanel");}; return _ReverseHalfBuddyListPanelJ;}} private jQueryObject _ReverseHalfBuddyListPanelJ;
		public Element FullBuddyList {get {if (_FullBuddyList == null) {_FullBuddyList = (Element)Document.GetElementById(clientId + "_FullBuddyList");}; return _FullBuddyList;}} private Element _FullBuddyList;
		public jQueryObject FullBuddyListJ {get {if (_FullBuddyListJ == null) {_FullBuddyListJ = jQuery.Select("#" + clientId + "_FullBuddyList");}; return _FullBuddyListJ;}} private jQueryObject _FullBuddyListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element HalfBuddyList {get {if (_HalfBuddyList == null) {_HalfBuddyList = (Element)Document.GetElementById(clientId + "_HalfBuddyList");}; return _HalfBuddyList;}} private Element _HalfBuddyList;
		public jQueryObject HalfBuddyListJ {get {if (_HalfBuddyListJ == null) {_HalfBuddyListJ = jQuery.Select("#" + clientId + "_HalfBuddyList");}; return _HalfBuddyListJ;}} private jQueryObject _HalfBuddyListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element ReverseHalfBuddyList {get {if (_ReverseHalfBuddyList == null) {_ReverseHalfBuddyList = (Element)Document.GetElementById(clientId + "_ReverseHalfBuddyList");}; return _ReverseHalfBuddyList;}} private Element _ReverseHalfBuddyList;
		public jQueryObject ReverseHalfBuddyListJ {get {if (_ReverseHalfBuddyListJ == null) {_ReverseHalfBuddyListJ = jQuery.Select("#" + clientId + "_ReverseHalfBuddyList");}; return _ReverseHalfBuddyListJ;}} private jQueryObject _ReverseHalfBuddyListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
