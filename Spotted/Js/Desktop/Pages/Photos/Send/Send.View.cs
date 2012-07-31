//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Photos.Send
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
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement PhotoAnchor {get {if (_PhotoAnchor == null) {_PhotoAnchor = (AnchorElement)Document.GetElementById(clientId + "_PhotoAnchor");}; return _PhotoAnchor;}} private AnchorElement _PhotoAnchor;
		public jQueryObject PhotoAnchorJ {get {if (_PhotoAnchorJ == null) {_PhotoAnchorJ = jQuery.Select("#" + clientId + "_PhotoAnchor");}; return _PhotoAnchorJ;}} private jQueryObject _PhotoAnchorJ;
		public ImageElement PhotoImg {get {if (_PhotoImg == null) {_PhotoImg = (ImageElement)Document.GetElementById(clientId + "_PhotoImg");}; return _PhotoImg;}} private ImageElement _PhotoImg;
		public jQueryObject PhotoImgJ {get {if (_PhotoImgJ == null) {_PhotoImgJ = jQuery.Select("#" + clientId + "_PhotoImg");}; return _PhotoImgJ;}} private jQueryObject _PhotoImgJ;
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Js.Controls.Html.Controller MessageHtml {get {return (Js.Controls.Html.Controller) Script.Eval(clientId + "_MessageHtmlController");}}
		public DivElement BuddyPanel {get {if (_BuddyPanel == null) {_BuddyPanel = (DivElement)Document.GetElementById(clientId + "_BuddyPanel");}; return _BuddyPanel;}} private DivElement _BuddyPanel;
		public jQueryObject BuddyPanelJ {get {if (_BuddyPanelJ == null) {_BuddyPanelJ = jQuery.Select("#" + clientId + "_BuddyPanel");}; return _BuddyPanelJ;}} private jQueryObject _BuddyPanelJ;
		public Js.Controls.MultiBuddyChooser.Controller MultiBuddyChooser {get {return (Js.Controls.MultiBuddyChooser.Controller) Script.Eval(clientId + "_MultiBuddyChooserController");}}
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SentEmailsLabel {get {if (_SentEmailsLabel == null) {_SentEmailsLabel = (Element)Document.GetElementById(clientId + "_SentEmailsLabel");}; return _SentEmailsLabel;}} private Element _SentEmailsLabel;
		public jQueryObject SentEmailsLabelJ {get {if (_SentEmailsLabelJ == null) {_SentEmailsLabelJ = jQuery.Select("#" + clientId + "_SentEmailsLabel");}; return _SentEmailsLabelJ;}} private jQueryObject _SentEmailsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
