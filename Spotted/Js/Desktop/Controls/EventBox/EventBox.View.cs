//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.EventBox
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public AnchorElement FutureEventsTab {get {if (_FutureEventsTab == null) {_FutureEventsTab = (AnchorElement)Document.GetElementById(clientId + "_FutureEventsTab");}; return _FutureEventsTab;}} private AnchorElement _FutureEventsTab;
		public jQueryObject FutureEventsTabJ {get {if (_FutureEventsTabJ == null) {_FutureEventsTabJ = jQuery.Select("#" + clientId + "_FutureEventsTab");}; return _FutureEventsTabJ;}} private jQueryObject _FutureEventsTabJ;
		public AnchorElement PastEventsTab {get {if (_PastEventsTab == null) {_PastEventsTab = (AnchorElement)Document.GetElementById(clientId + "_PastEventsTab");}; return _PastEventsTab;}} private AnchorElement _PastEventsTab;
		public jQueryObject PastEventsTabJ {get {if (_PastEventsTabJ == null) {_PastEventsTabJ = jQuery.Select("#" + clientId + "_PastEventsTab");}; return _PastEventsTabJ;}} private jQueryObject _PastEventsTabJ;
		public AnchorElement TicketsTab {get {if (_TicketsTab == null) {_TicketsTab = (AnchorElement)Document.GetElementById(clientId + "_TicketsTab");}; return _TicketsTab;}} private AnchorElement _TicketsTab;
		public jQueryObject TicketsTabJ {get {if (_TicketsTabJ == null) {_TicketsTabJ = jQuery.Select("#" + clientId + "_TicketsTab");}; return _TicketsTabJ;}} private jQueryObject _TicketsTabJ;
		public InputElement InitEnableEffects {get {if (_InitEnableEffects == null) {_InitEnableEffects = (InputElement)Document.GetElementById(clientId + "_InitEnableEffects");}; return _InitEnableEffects;}} private InputElement _InitEnableEffects;
		public jQueryObject InitEnableEffectsJ {get {if (_InitEnableEffectsJ == null) {_InitEnableEffectsJ = jQuery.Select("#" + clientId + "_InitEnableEffects");}; return _InitEnableEffectsJ;}} private jQueryObject _InitEnableEffectsJ;
		public InputElement InitClientID {get {if (_InitClientID == null) {_InitClientID = (InputElement)Document.GetElementById(clientId + "_InitClientID");}; return _InitClientID;}} private InputElement _InitClientID;
		public jQueryObject InitClientIDJ {get {if (_InitClientIDJ == null) {_InitClientIDJ = jQuery.Select("#" + clientId + "_InitClientID");}; return _InitClientIDJ;}} private jQueryObject _InitClientIDJ;
		public InputElement InitFirstPage {get {if (_InitFirstPage == null) {_InitFirstPage = (InputElement)Document.GetElementById(clientId + "_InitFirstPage");}; return _InitFirstPage;}} private InputElement _InitFirstPage;
		public jQueryObject InitFirstPageJ {get {if (_InitFirstPageJ == null) {_InitFirstPageJ = jQuery.Select("#" + clientId + "_InitFirstPage");}; return _InitFirstPageJ;}} private jQueryObject _InitFirstPageJ;
		public Element TitleHolder {get {if (_TitleHolder == null) {_TitleHolder = (Element)Document.GetElementById(clientId + "_TitleHolder");}; return _TitleHolder;}} private Element _TitleHolder;
		public jQueryObject TitleHolderJ {get {if (_TitleHolderJ == null) {_TitleHolderJ = jQuery.Select("#" + clientId + "_TitleHolder");}; return _TitleHolderJ;}} private jQueryObject _TitleHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element MusicDropDownHolder {get {if (_MusicDropDownHolder == null) {_MusicDropDownHolder = (Element)Document.GetElementById(clientId + "_MusicDropDownHolder");}; return _MusicDropDownHolder;}} private Element _MusicDropDownHolder;
		public jQueryObject MusicDropDownHolderJ {get {if (_MusicDropDownHolderJ == null) {_MusicDropDownHolderJ = jQuery.Select("#" + clientId + "_MusicDropDownHolder");}; return _MusicDropDownHolderJ;}} private jQueryObject _MusicDropDownHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Js.Controls.MusicDropDown.Controller MusicDropDownControl {get {return (Js.Controls.MusicDropDown.Controller) Script.Eval(clientId + "_MusicDropDownControlController");}}
		public Element EventIconsHolder {get {if (_EventIconsHolder == null) {_EventIconsHolder = (Element)Document.GetElementById(clientId + "_EventIconsHolder");}; return _EventIconsHolder;}} private Element _EventIconsHolder;
		public jQueryObject EventIconsHolderJ {get {if (_EventIconsHolderJ == null) {_EventIconsHolderJ = jQuery.Select("#" + clientId + "_EventIconsHolder");}; return _EventIconsHolderJ;}} private jQueryObject _EventIconsHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element EventIconsNavigationBackHolder {get {if (_EventIconsNavigationBackHolder == null) {_EventIconsNavigationBackHolder = (Element)Document.GetElementById(clientId + "_EventIconsNavigationBackHolder");}; return _EventIconsNavigationBackHolder;}} private Element _EventIconsNavigationBackHolder;
		public jQueryObject EventIconsNavigationBackHolderJ {get {if (_EventIconsNavigationBackHolderJ == null) {_EventIconsNavigationBackHolderJ = jQuery.Select("#" + clientId + "_EventIconsNavigationBackHolder");}; return _EventIconsNavigationBackHolderJ;}} private jQueryObject _EventIconsNavigationBackHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element EventIconsNavigationForwardHolder {get {if (_EventIconsNavigationForwardHolder == null) {_EventIconsNavigationForwardHolder = (Element)Document.GetElementById(clientId + "_EventIconsNavigationForwardHolder");}; return _EventIconsNavigationForwardHolder;}} private Element _EventIconsNavigationForwardHolder;
		public jQueryObject EventIconsNavigationForwardHolderJ {get {if (_EventIconsNavigationForwardHolderJ == null) {_EventIconsNavigationForwardHolderJ = jQuery.Select("#" + clientId + "_EventIconsNavigationForwardHolder");}; return _EventIconsNavigationForwardHolderJ;}} private jQueryObject _EventIconsNavigationForwardHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element EventInfoHolderOuter {get {if (_EventInfoHolderOuter == null) {_EventInfoHolderOuter = (Element)Document.GetElementById(clientId + "_EventInfoHolderOuter");}; return _EventInfoHolderOuter;}} private Element _EventInfoHolderOuter;
		public jQueryObject EventInfoHolderOuterJ {get {if (_EventInfoHolderOuterJ == null) {_EventInfoHolderOuterJ = jQuery.Select("#" + clientId + "_EventInfoHolderOuter");}; return _EventInfoHolderOuterJ;}} private jQueryObject _EventInfoHolderOuterJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element BottomNavigationTitle {get {if (_BottomNavigationTitle == null) {_BottomNavigationTitle = (Element)Document.GetElementById(clientId + "_BottomNavigationTitle");}; return _BottomNavigationTitle;}} private Element _BottomNavigationTitle;
		public jQueryObject BottomNavigationTitleJ {get {if (_BottomNavigationTitleJ == null) {_BottomNavigationTitleJ = jQuery.Select("#" + clientId + "_BottomNavigationTitle");}; return _BottomNavigationTitleJ;}} private jQueryObject _BottomNavigationTitleJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element BottomNavigationHolder {get {if (_BottomNavigationHolder == null) {_BottomNavigationHolder = (Element)Document.GetElementById(clientId + "_BottomNavigationHolder");}; return _BottomNavigationHolder;}} private Element _BottomNavigationHolder;
		public jQueryObject BottomNavigationHolderJ {get {if (_BottomNavigationHolderJ == null) {_BottomNavigationHolderJ = jQuery.Select("#" + clientId + "_BottomNavigationHolder");}; return _BottomNavigationHolderJ;}} private jQueryObject _BottomNavigationHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
	}
}
