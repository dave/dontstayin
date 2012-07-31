//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.Accounts
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
		public Element SpotterLetterRepeater {get {if (_SpotterLetterRepeater == null) {_SpotterLetterRepeater = (Element)Document.GetElementById(clientId + "_SpotterLetterRepeater");}; return _SpotterLetterRepeater;}} private Element _SpotterLetterRepeater;
		public jQueryObject SpotterLetterRepeaterJ {get {if (_SpotterLetterRepeaterJ == null) {_SpotterLetterRepeaterJ = jQuery.Select("#" + clientId + "_SpotterLetterRepeater");}; return _SpotterLetterRepeaterJ;}} private jQueryObject _SpotterLetterRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public CheckBoxElement MarkAllSent {get {if (_MarkAllSent == null) {_MarkAllSent = (CheckBoxElement)Document.GetElementById(clientId + "_MarkAllSent");}; return _MarkAllSent;}} private CheckBoxElement _MarkAllSent;
		public jQueryObject MarkAllSentJ {get {if (_MarkAllSentJ == null) {_MarkAllSentJ = jQuery.Select("#" + clientId + "_MarkAllSent");}; return _MarkAllSentJ;}} private jQueryObject _MarkAllSentJ;
		public Element SuccessLabel {get {if (_SuccessLabel == null) {_SuccessLabel = (Element)Document.GetElementById(clientId + "_SuccessLabel");}; return _SuccessLabel;}} private Element _SuccessLabel;
		public jQueryObject SuccessLabelJ {get {if (_SuccessLabelJ == null) {_SuccessLabelJ = jQuery.Select("#" + clientId + "_SuccessLabel");}; return _SuccessLabelJ;}} private jQueryObject _SuccessLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FailLabel {get {if (_FailLabel == null) {_FailLabel = (Element)Document.GetElementById(clientId + "_FailLabel");}; return _FailLabel;}} private Element _FailLabel;
		public jQueryObject FailLabelJ {get {if (_FailLabelJ == null) {_FailLabelJ = jQuery.Select("#" + clientId + "_FailLabel");}; return _FailLabelJ;}} private jQueryObject _FailLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
