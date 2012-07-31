//mappings.Add("Spotted.Controls.NewUserWizardOptions", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.FindEvents
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
		public Element NewUserWizardOptions {get {if (_NewUserWizardOptions == null) {_NewUserWizardOptions = (Element)Document.GetElementById(clientId + "_NewUserWizardOptions");}; return _NewUserWizardOptions;}} private Element _NewUserWizardOptions;
		public jQueryObject NewUserWizardOptionsJ {get {if (_NewUserWizardOptionsJ == null) {_NewUserWizardOptionsJ = jQuery.Select("#" + clientId + "_NewUserWizardOptions");}; return _NewUserWizardOptionsJ;}} private jQueryObject _NewUserWizardOptionsJ;//mappings.Add("Spotted.Controls.NewUserWizardOptions", ElementGetter("Element"));
		public Element FindEventsHeader {get {if (_FindEventsHeader == null) {_FindEventsHeader = (Element)Document.GetElementById(clientId + "_FindEventsHeader");}; return _FindEventsHeader;}} private Element _FindEventsHeader;
		public jQueryObject FindEventsHeaderJ {get {if (_FindEventsHeaderJ == null) {_FindEventsHeaderJ = jQuery.Select("#" + clientId + "_FindEventsHeader");}; return _FindEventsHeaderJ;}} private jQueryObject _FindEventsHeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element CalendarTabHolder {get {if (_CalendarTabHolder == null) {_CalendarTabHolder = (Element)Document.GetElementById(clientId + "_CalendarTabHolder");}; return _CalendarTabHolder;}} private Element _CalendarTabHolder;
		public jQueryObject CalendarTabHolderJ {get {if (_CalendarTabHolderJ == null) {_CalendarTabHolderJ = jQuery.Select("#" + clientId + "_CalendarTabHolder");}; return _CalendarTabHolderJ;}} private jQueryObject _CalendarTabHolderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement EventFinderTab {get {if (_EventFinderTab == null) {_EventFinderTab = (AnchorElement)Document.GetElementById(clientId + "_EventFinderTab");}; return _EventFinderTab;}} private AnchorElement _EventFinderTab;
		public jQueryObject EventFinderTabJ {get {if (_EventFinderTabJ == null) {_EventFinderTabJ = jQuery.Select("#" + clientId + "_EventFinderTab");}; return _EventFinderTabJ;}} private jQueryObject _EventFinderTabJ;
		public AnchorElement MyCalendarTab {get {if (_MyCalendarTab == null) {_MyCalendarTab = (AnchorElement)Document.GetElementById(clientId + "_MyCalendarTab");}; return _MyCalendarTab;}} private AnchorElement _MyCalendarTab;
		public jQueryObject MyCalendarTabJ {get {if (_MyCalendarTabJ == null) {_MyCalendarTabJ = jQuery.Select("#" + clientId + "_MyCalendarTab");}; return _MyCalendarTabJ;}} private jQueryObject _MyCalendarTabJ;
		public AnchorElement BuddyCalendarTab {get {if (_BuddyCalendarTab == null) {_BuddyCalendarTab = (AnchorElement)Document.GetElementById(clientId + "_BuddyCalendarTab");}; return _BuddyCalendarTab;}} private AnchorElement _BuddyCalendarTab;
		public jQueryObject BuddyCalendarTabJ {get {if (_BuddyCalendarTabJ == null) {_BuddyCalendarTabJ = jQuery.Select("#" + clientId + "_BuddyCalendarTab");}; return _BuddyCalendarTabJ;}} private jQueryObject _BuddyCalendarTabJ;
		public ImageElement TopIcon {get {if (_TopIcon == null) {_TopIcon = (ImageElement)Document.GetElementById(clientId + "_TopIcon");}; return _TopIcon;}} private ImageElement _TopIcon;
		public jQueryObject TopIconJ {get {if (_TopIconJ == null) {_TopIconJ = jQuery.Select("#" + clientId + "_TopIcon");}; return _TopIconJ;}} private jQueryObject _TopIconJ;
		public InputElement Debug {get {if (_Debug == null) {_Debug = (InputElement)Document.GetElementById(clientId + "_Debug");}; return _Debug;}} private InputElement _Debug;
		public jQueryObject DebugJ {get {if (_DebugJ == null) {_DebugJ = jQuery.Select("#" + clientId + "_Debug");}; return _DebugJ;}} private jQueryObject _DebugJ;
		public Js.Controls.Picker.Controller Picker {get {return (Js.Controls.Picker.Controller) Script.Eval(clientId + "_PickerController");}}
		public Element CalendarHolderOuter {get {if (_CalendarHolderOuter == null) {_CalendarHolderOuter = (Element)Document.GetElementById(clientId + "_CalendarHolderOuter");}; return _CalendarHolderOuter;}} private Element _CalendarHolderOuter;
		public jQueryObject CalendarHolderOuterJ {get {if (_CalendarHolderOuterJ == null) {_CalendarHolderOuterJ = jQuery.Select("#" + clientId + "_CalendarHolderOuter");}; return _CalendarHolderOuterJ;}} private jQueryObject _CalendarHolderOuterJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement BackLink {get {if (_BackLink == null) {_BackLink = (AnchorElement)Document.GetElementById(clientId + "_BackLink");}; return _BackLink;}} private AnchorElement _BackLink;
		public jQueryObject BackLinkJ {get {if (_BackLinkJ == null) {_BackLinkJ = jQuery.Select("#" + clientId + "_BackLink");}; return _BackLinkJ;}} private jQueryObject _BackLinkJ;
		public Element MonthLabel {get {if (_MonthLabel == null) {_MonthLabel = (Element)Document.GetElementById(clientId + "_MonthLabel");}; return _MonthLabel;}} private Element _MonthLabel;
		public jQueryObject MonthLabelJ {get {if (_MonthLabelJ == null) {_MonthLabelJ = jQuery.Select("#" + clientId + "_MonthLabel");}; return _MonthLabelJ;}} private jQueryObject _MonthLabelJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element LoadingLabel {get {if (_LoadingLabel == null) {_LoadingLabel = (Element)Document.GetElementById(clientId + "_LoadingLabel");}; return _LoadingLabel;}} private Element _LoadingLabel;
		public jQueryObject LoadingLabelJ {get {if (_LoadingLabelJ == null) {_LoadingLabelJ = jQuery.Select("#" + clientId + "_LoadingLabel");}; return _LoadingLabelJ;}} private jQueryObject _LoadingLabelJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement ForwardLink {get {if (_ForwardLink == null) {_ForwardLink = (AnchorElement)Document.GetElementById(clientId + "_ForwardLink");}; return _ForwardLink;}} private AnchorElement _ForwardLink;
		public jQueryObject ForwardLinkJ {get {if (_ForwardLinkJ == null) {_ForwardLinkJ = jQuery.Select("#" + clientId + "_ForwardLink");}; return _ForwardLinkJ;}} private jQueryObject _ForwardLinkJ;
		public Element CalendarLoadingOverlay {get {if (_CalendarLoadingOverlay == null) {_CalendarLoadingOverlay = (Element)Document.GetElementById(clientId + "_CalendarLoadingOverlay");}; return _CalendarLoadingOverlay;}} private Element _CalendarLoadingOverlay;
		public jQueryObject CalendarLoadingOverlayJ {get {if (_CalendarLoadingOverlayJ == null) {_CalendarLoadingOverlayJ = jQuery.Select("#" + clientId + "_CalendarLoadingOverlay");}; return _CalendarLoadingOverlayJ;}} private jQueryObject _CalendarLoadingOverlayJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element CalendarHolder {get {if (_CalendarHolder == null) {_CalendarHolder = (Element)Document.GetElementById(clientId + "_CalendarHolder");}; return _CalendarHolder;}} private Element _CalendarHolder;
		public jQueryObject CalendarHolderJ {get {if (_CalendarHolderJ == null) {_CalendarHolderJ = jQuery.Select("#" + clientId + "_CalendarHolder");}; return _CalendarHolderJ;}} private jQueryObject _CalendarHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
