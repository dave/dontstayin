//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Styled.EventDetail
{
	public partial class View
		 : Js.StyledUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element EventHeader {get {if (_EventHeader == null) {_EventHeader = (Element)Document.GetElementById(clientId + "_EventHeader");}; return _EventHeader;}} private Element _EventHeader;
		public jQueryObject EventHeaderJ {get {if (_EventHeaderJ == null) {_EventHeaderJ = jQuery.Select("#" + clientId + "_EventHeader");}; return _EventHeaderJ;}} private jQueryObject _EventHeaderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public ImageElement EventPic {get {if (_EventPic == null) {_EventPic = (ImageElement)Document.GetElementById(clientId + "_EventPic");}; return _EventPic;}} private ImageElement _EventPic;
		public jQueryObject EventPicJ {get {if (_EventPicJ == null) {_EventPicJ = jQuery.Select("#" + clientId + "_EventPic");}; return _EventPicJ;}} private jQueryObject _EventPicJ;
		public Element RunningTicketRunsForPromoterRepeater {get {if (_RunningTicketRunsForPromoterRepeater == null) {_RunningTicketRunsForPromoterRepeater = (Element)Document.GetElementById(clientId + "_RunningTicketRunsForPromoterRepeater");}; return _RunningTicketRunsForPromoterRepeater;}} private Element _RunningTicketRunsForPromoterRepeater;
		public jQueryObject RunningTicketRunsForPromoterRepeaterJ {get {if (_RunningTicketRunsForPromoterRepeaterJ == null) {_RunningTicketRunsForPromoterRepeaterJ = jQuery.Select("#" + clientId + "_RunningTicketRunsForPromoterRepeater");}; return _RunningTicketRunsForPromoterRepeaterJ;}} private jQueryObject _RunningTicketRunsForPromoterRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element NoTicketsAvailableDiv {get {if (_NoTicketsAvailableDiv == null) {_NoTicketsAvailableDiv = (Element)Document.GetElementById(clientId + "_NoTicketsAvailableDiv");}; return _NoTicketsAvailableDiv;}} private Element _NoTicketsAvailableDiv;
		public jQueryObject NoTicketsAvailableDivJ {get {if (_NoTicketsAvailableDivJ == null) {_NoTicketsAvailableDivJ = jQuery.Select("#" + clientId + "_NoTicketsAvailableDiv");}; return _NoTicketsAvailableDivJ;}} private jQueryObject _NoTicketsAvailableDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element TicketNoteP {get {if (_TicketNoteP == null) {_TicketNoteP = (Element)Document.GetElementById(clientId + "_TicketNoteP");}; return _TicketNoteP;}} private Element _TicketNoteP;
		public jQueryObject TicketNotePJ {get {if (_TicketNotePJ == null) {_TicketNotePJ = jQuery.Select("#" + clientId + "_TicketNoteP");}; return _TicketNotePJ;}} private jQueryObject _TicketNotePJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element TicketNoteLabel {get {if (_TicketNoteLabel == null) {_TicketNoteLabel = (Element)Document.GetElementById(clientId + "_TicketNoteLabel");}; return _TicketNoteLabel;}} private Element _TicketNoteLabel;
		public jQueryObject TicketNoteLabelJ {get {if (_TicketNoteLabelJ == null) {_TicketNoteLabelJ = jQuery.Select("#" + clientId + "_TicketNoteLabel");}; return _TicketNoteLabelJ;}} private jQueryObject _TicketNoteLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ProcessingVal {get {if (_ProcessingVal == null) {_ProcessingVal = (Element)Document.GetElementById(clientId + "_ProcessingVal");}; return _ProcessingVal;}} private Element _ProcessingVal;
		public jQueryObject ProcessingValJ {get {if (_ProcessingValJ == null) {_ProcessingValJ = jQuery.Select("#" + clientId + "_ProcessingVal");}; return _ProcessingValJ;}} private jQueryObject _ProcessingValJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element TicketValidationSummary {get {if (_TicketValidationSummary == null) {_TicketValidationSummary = (Element)Document.GetElementById(clientId + "_TicketValidationSummary");}; return _TicketValidationSummary;}} private Element _TicketValidationSummary;
		public jQueryObject TicketValidationSummaryJ {get {if (_TicketValidationSummaryJ == null) {_TicketValidationSummaryJ = jQuery.Select("#" + clientId + "_TicketValidationSummary");}; return _TicketValidationSummaryJ;}} private jQueryObject _TicketValidationSummaryJ;//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
		public Element EventShortDescription {get {if (_EventShortDescription == null) {_EventShortDescription = (Element)Document.GetElementById(clientId + "_EventShortDescription");}; return _EventShortDescription;}} private Element _EventShortDescription;
		public jQueryObject EventShortDescriptionJ {get {if (_EventShortDescriptionJ == null) {_EventShortDescriptionJ = jQuery.Select("#" + clientId + "_EventShortDescription");}; return _EventShortDescriptionJ;}} private jQueryObject _EventShortDescriptionJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
