//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Photos.AddToGroup
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
		public ImageElement PhotoImg {get {if (_PhotoImg == null) {_PhotoImg = (ImageElement)Document.GetElementById(clientId + "_PhotoImg");}; return _PhotoImg;}} private ImageElement _PhotoImg;
		public jQueryObject PhotoImgJ {get {if (_PhotoImgJ == null) {_PhotoImgJ = jQuery.Select("#" + clientId + "_PhotoImg");}; return _PhotoImgJ;}} private jQueryObject _PhotoImgJ;
		public AnchorElement PhotoAnchor {get {if (_PhotoAnchor == null) {_PhotoAnchor = (AnchorElement)Document.GetElementById(clientId + "_PhotoAnchor");}; return _PhotoAnchor;}} private AnchorElement _PhotoAnchor;
		public jQueryObject PhotoAnchorJ {get {if (_PhotoAnchorJ == null) {_PhotoAnchorJ = jQuery.Select("#" + clientId + "_PhotoAnchor");}; return _PhotoAnchorJ;}} private jQueryObject _PhotoAnchorJ;
		public Element GroupRepeater {get {if (_GroupRepeater == null) {_GroupRepeater = (Element)Document.GetElementById(clientId + "_GroupRepeater");}; return _GroupRepeater;}} private Element _GroupRepeater;
		public jQueryObject GroupRepeaterJ {get {if (_GroupRepeaterJ == null) {_GroupRepeaterJ = jQuery.Select("#" + clientId + "_GroupRepeater");}; return _GroupRepeaterJ;}} private jQueryObject _GroupRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public DivElement GroupPanel {get {if (_GroupPanel == null) {_GroupPanel = (DivElement)Document.GetElementById(clientId + "_GroupPanel");}; return _GroupPanel;}} private DivElement _GroupPanel;
		public jQueryObject GroupPanelJ {get {if (_GroupPanelJ == null) {_GroupPanelJ = jQuery.Select("#" + clientId + "_GroupPanel");}; return _GroupPanelJ;}} private jQueryObject _GroupPanelJ;
		public DivElement RepeaterPanel {get {if (_RepeaterPanel == null) {_RepeaterPanel = (DivElement)Document.GetElementById(clientId + "_RepeaterPanel");}; return _RepeaterPanel;}} private DivElement _RepeaterPanel;
		public jQueryObject RepeaterPanelJ {get {if (_RepeaterPanelJ == null) {_RepeaterPanelJ = jQuery.Select("#" + clientId + "_RepeaterPanel");}; return _RepeaterPanelJ;}} private jQueryObject _RepeaterPanelJ;
		public Element GroupLabel {get {if (_GroupLabel == null) {_GroupLabel = (Element)Document.GetElementById(clientId + "_GroupLabel");}; return _GroupLabel;}} private Element _GroupLabel;
		public jQueryObject GroupLabelJ {get {if (_GroupLabelJ == null) {_GroupLabelJ = jQuery.Select("#" + clientId + "_GroupLabel");}; return _GroupLabelJ;}} private jQueryObject _GroupLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public CheckBoxElement ShowCheckBox {get {if (_ShowCheckBox == null) {_ShowCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_ShowCheckBox");}; return _ShowCheckBox;}} private CheckBoxElement _ShowCheckBox;
		public jQueryObject ShowCheckBoxJ {get {if (_ShowCheckBoxJ == null) {_ShowCheckBoxJ = jQuery.Select("#" + clientId + "_ShowCheckBox");}; return _ShowCheckBoxJ;}} private jQueryObject _ShowCheckBoxJ;
		public InputElement CaptionTextBox {get {if (_CaptionTextBox == null) {_CaptionTextBox = (InputElement)Document.GetElementById(clientId + "_CaptionTextBox");}; return _CaptionTextBox;}} private InputElement _CaptionTextBox;
		public jQueryObject CaptionTextBoxJ {get {if (_CaptionTextBoxJ == null) {_CaptionTextBoxJ = jQuery.Select("#" + clientId + "_CaptionTextBox");}; return _CaptionTextBoxJ;}} private jQueryObject _CaptionTextBoxJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public CheckBoxElement CaptionCompetitionCheckBox {get {if (_CaptionCompetitionCheckBox == null) {_CaptionCompetitionCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_CaptionCompetitionCheckBox");}; return _CaptionCompetitionCheckBox;}} private CheckBoxElement _CaptionCompetitionCheckBox;
		public jQueryObject CaptionCompetitionCheckBoxJ {get {if (_CaptionCompetitionCheckBoxJ == null) {_CaptionCompetitionCheckBoxJ = jQuery.Select("#" + clientId + "_CaptionCompetitionCheckBox");}; return _CaptionCompetitionCheckBoxJ;}} private jQueryObject _CaptionCompetitionCheckBoxJ;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SentEmailsLabel {get {if (_SentEmailsLabel == null) {_SentEmailsLabel = (Element)Document.GetElementById(clientId + "_SentEmailsLabel");}; return _SentEmailsLabel;}} private Element _SentEmailsLabel;
		public jQueryObject SentEmailsLabelJ {get {if (_SentEmailsLabelJ == null) {_SentEmailsLabelJ = jQuery.Select("#" + clientId + "_SentEmailsLabel");}; return _SentEmailsLabelJ;}} private jQueryObject _SentEmailsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
