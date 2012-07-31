//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.AbuseReport
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
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement PanelNone {get {if (_PanelNone == null) {_PanelNone = (DivElement)Document.GetElementById(clientId + "_PanelNone");}; return _PanelNone;}} private DivElement _PanelNone;
		public jQueryObject PanelNoneJ {get {if (_PanelNoneJ == null) {_PanelNoneJ = jQuery.Select("#" + clientId + "_PanelNone");}; return _PanelNoneJ;}} private jQueryObject _PanelNoneJ;
		public DivElement PanelAbuse {get {if (_PanelAbuse == null) {_PanelAbuse = (DivElement)Document.GetElementById(clientId + "_PanelAbuse");}; return _PanelAbuse;}} private DivElement _PanelAbuse;
		public jQueryObject PanelAbuseJ {get {if (_PanelAbuseJ == null) {_PanelAbuseJ = jQuery.Select("#" + clientId + "_PanelAbuse");}; return _PanelAbuseJ;}} private jQueryObject _PanelAbuseJ;
		public Element PhotoKLabel {get {if (_PhotoKLabel == null) {_PhotoKLabel = (Element)Document.GetElementById(clientId + "_PhotoKLabel");}; return _PhotoKLabel;}} private Element _PhotoKLabel;
		public jQueryObject PhotoKLabelJ {get {if (_PhotoKLabelJ == null) {_PhotoKLabelJ = jQuery.Select("#" + clientId + "_PhotoKLabel");}; return _PhotoKLabelJ;}} private jQueryObject _PhotoKLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PhotoStringLabel {get {if (_PhotoStringLabel == null) {_PhotoStringLabel = (Element)Document.GetElementById(clientId + "_PhotoStringLabel");}; return _PhotoStringLabel;}} private Element _PhotoStringLabel;
		public jQueryObject PhotoStringLabelJ {get {if (_PhotoStringLabelJ == null) {_PhotoStringLabelJ = jQuery.Select("#" + clientId + "_PhotoStringLabel");}; return _PhotoStringLabelJ;}} private jQueryObject _PhotoStringLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public AnchorElement PhotoAnchor {get {if (_PhotoAnchor == null) {_PhotoAnchor = (AnchorElement)Document.GetElementById(clientId + "_PhotoAnchor");}; return _PhotoAnchor;}} private AnchorElement _PhotoAnchor;
		public jQueryObject PhotoAnchorJ {get {if (_PhotoAnchorJ == null) {_PhotoAnchorJ = jQuery.Select("#" + clientId + "_PhotoAnchor");}; return _PhotoAnchorJ;}} private jQueryObject _PhotoAnchorJ;
		public ImageElement PhotoImg {get {if (_PhotoImg == null) {_PhotoImg = (ImageElement)Document.GetElementById(clientId + "_PhotoImg");}; return _PhotoImg;}} private ImageElement _PhotoImg;
		public jQueryObject PhotoImgJ {get {if (_PhotoImgJ == null) {_PhotoImgJ = jQuery.Select("#" + clientId + "_PhotoImg");}; return _PhotoImgJ;}} private jQueryObject _PhotoImgJ;
		public DivElement PhotoPanel {get {if (_PhotoPanel == null) {_PhotoPanel = (DivElement)Document.GetElementById(clientId + "_PhotoPanel");}; return _PhotoPanel;}} private DivElement _PhotoPanel;
		public jQueryObject PhotoPanelJ {get {if (_PhotoPanelJ == null) {_PhotoPanelJ = jQuery.Select("#" + clientId + "_PhotoPanel");}; return _PhotoPanelJ;}} private jQueryObject _PhotoPanelJ;
		public DivElement NoPhotoPanel {get {if (_NoPhotoPanel == null) {_NoPhotoPanel = (DivElement)Document.GetElementById(clientId + "_NoPhotoPanel");}; return _NoPhotoPanel;}} private DivElement _NoPhotoPanel;
		public jQueryObject NoPhotoPanelJ {get {if (_NoPhotoPanelJ == null) {_NoPhotoPanelJ = jQuery.Select("#" + clientId + "_NoPhotoPanel");}; return _NoPhotoPanelJ;}} private jQueryObject _NoPhotoPanelJ;
		public Element AbuseByP {get {if (_AbuseByP == null) {_AbuseByP = (Element)Document.GetElementById(clientId + "_AbuseByP");}; return _AbuseByP;}} private Element _AbuseByP;
		public jQueryObject AbuseByPJ {get {if (_AbuseByPJ == null) {_AbuseByPJ = jQuery.Select("#" + clientId + "_AbuseByP");}; return _AbuseByPJ;}} private jQueryObject _AbuseByPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ReportByP {get {if (_ReportByP == null) {_ReportByP = (Element)Document.GetElementById(clientId + "_ReportByP");}; return _ReportByP;}} private Element _ReportByP;
		public jQueryObject ReportByPJ {get {if (_ReportByPJ == null) {_ReportByPJ = jQuery.Select("#" + clientId + "_ReportByP");}; return _ReportByPJ;}} private jQueryObject _ReportByPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GalleriesP {get {if (_GalleriesP == null) {_GalleriesP = (Element)Document.GetElementById(clientId + "_GalleriesP");}; return _GalleriesP;}} private Element _GalleriesP;
		public jQueryObject GalleriesPJ {get {if (_GalleriesPJ == null) {_GalleriesPJ = jQuery.Select("#" + clientId + "_GalleriesP");}; return _GalleriesPJ;}} private jQueryObject _GalleriesPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ThisGalleryP {get {if (_ThisGalleryP == null) {_ThisGalleryP = (Element)Document.GetElementById(clientId + "_ThisGalleryP");}; return _ThisGalleryP;}} private Element _ThisGalleryP;
		public jQueryObject ThisGalleryPJ {get {if (_ThisGalleryPJ == null) {_ThisGalleryPJ = jQuery.Select("#" + clientId + "_ThisGalleryP");}; return _ThisGalleryPJ;}} private jQueryObject _ThisGalleryPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ReportDescriptionP {get {if (_ReportDescriptionP == null) {_ReportDescriptionP = (Element)Document.GetElementById(clientId + "_ReportDescriptionP");}; return _ReportDescriptionP;}} private Element _ReportDescriptionP;
		public jQueryObject ReportDescriptionPJ {get {if (_ReportDescriptionPJ == null) {_ReportDescriptionPJ = jQuery.Select("#" + clientId + "_ReportDescriptionP");}; return _ReportDescriptionPJ;}} private jQueryObject _ReportDescriptionPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ResolveDescriptionP {get {if (_ResolveDescriptionP == null) {_ResolveDescriptionP = (Element)Document.GetElementById(clientId + "_ResolveDescriptionP");}; return _ResolveDescriptionP;}} private Element _ResolveDescriptionP;
		public jQueryObject ResolveDescriptionPJ {get {if (_ResolveDescriptionPJ == null) {_ResolveDescriptionPJ = jQuery.Select("#" + clientId + "_ResolveDescriptionP");}; return _ResolveDescriptionPJ;}} private jQueryObject _ResolveDescriptionPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement ActionsPanel {get {if (_ActionsPanel == null) {_ActionsPanel = (DivElement)Document.GetElementById(clientId + "_ActionsPanel");}; return _ActionsPanel;}} private DivElement _ActionsPanel;
		public jQueryObject ActionsPanelJ {get {if (_ActionsPanelJ == null) {_ActionsPanelJ = jQuery.Select("#" + clientId + "_ActionsPanel");}; return _ActionsPanelJ;}} private jQueryObject _ActionsPanelJ;
		public DivElement ResolvedPanel {get {if (_ResolvedPanel == null) {_ResolvedPanel = (DivElement)Document.GetElementById(clientId + "_ResolvedPanel");}; return _ResolvedPanel;}} private DivElement _ResolvedPanel;
		public jQueryObject ResolvedPanelJ {get {if (_ResolvedPanelJ == null) {_ResolvedPanelJ = jQuery.Select("#" + clientId + "_ResolvedPanel");}; return _ResolvedPanelJ;}} private jQueryObject _ResolvedPanelJ;
		public DivElement ThisGalleryPanel {get {if (_ThisGalleryPanel == null) {_ThisGalleryPanel = (DivElement)Document.GetElementById(clientId + "_ThisGalleryPanel");}; return _ThisGalleryPanel;}} private DivElement _ThisGalleryPanel;
		public jQueryObject ThisGalleryPanelJ {get {if (_ThisGalleryPanelJ == null) {_ThisGalleryPanelJ = jQuery.Select("#" + clientId + "_ThisGalleryPanel");}; return _ThisGalleryPanelJ;}} private jQueryObject _ThisGalleryPanelJ;
		public Element ResolvedLabel {get {if (_ResolvedLabel == null) {_ResolvedLabel = (Element)Document.GetElementById(clientId + "_ResolvedLabel");}; return _ResolvedLabel;}} private Element _ResolvedLabel;
		public jQueryObject ResolvedLabelJ {get {if (_ResolvedLabelJ == null) {_ResolvedLabelJ = jQuery.Select("#" + clientId + "_ResolvedLabel");}; return _ResolvedLabelJ;}} private jQueryObject _ResolvedLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public CheckBoxElement OverturnRadio {get {if (_OverturnRadio == null) {_OverturnRadio = (CheckBoxElement)Document.GetElementById(clientId + "_OverturnRadio");}; return _OverturnRadio;}} private CheckBoxElement _OverturnRadio;
		public jQueryObject OverturnRadioJ {get {if (_OverturnRadioJ == null) {_OverturnRadioJ = jQuery.Select("#" + clientId + "_OverturnRadio");}; return _OverturnRadioJ;}} private jQueryObject _OverturnRadioJ;
		public CheckBoxElement NoAbuseRadio {get {if (_NoAbuseRadio == null) {_NoAbuseRadio = (CheckBoxElement)Document.GetElementById(clientId + "_NoAbuseRadio");}; return _NoAbuseRadio;}} private CheckBoxElement _NoAbuseRadio;
		public jQueryObject NoAbuseRadioJ {get {if (_NoAbuseRadioJ == null) {_NoAbuseRadioJ = jQuery.Select("#" + clientId + "_NoAbuseRadio");}; return _NoAbuseRadioJ;}} private jQueryObject _NoAbuseRadioJ;
		public CheckBoxElement NoAbuseDeleteRadio {get {if (_NoAbuseDeleteRadio == null) {_NoAbuseDeleteRadio = (CheckBoxElement)Document.GetElementById(clientId + "_NoAbuseDeleteRadio");}; return _NoAbuseDeleteRadio;}} private CheckBoxElement _NoAbuseDeleteRadio;
		public jQueryObject NoAbuseDeleteRadioJ {get {if (_NoAbuseDeleteRadioJ == null) {_NoAbuseDeleteRadioJ = jQuery.Select("#" + clientId + "_NoAbuseDeleteRadio");}; return _NoAbuseDeleteRadioJ;}} private jQueryObject _NoAbuseDeleteRadioJ;
		public CheckBoxElement AbuseDeleteRadio {get {if (_AbuseDeleteRadio == null) {_AbuseDeleteRadio = (CheckBoxElement)Document.GetElementById(clientId + "_AbuseDeleteRadio");}; return _AbuseDeleteRadio;}} private CheckBoxElement _AbuseDeleteRadio;
		public jQueryObject AbuseDeleteRadioJ {get {if (_AbuseDeleteRadioJ == null) {_AbuseDeleteRadioJ = jQuery.Select("#" + clientId + "_AbuseDeleteRadio");}; return _AbuseDeleteRadioJ;}} private jQueryObject _AbuseDeleteRadioJ;
		public CheckBoxElement AbuseDeleteWatchRadio {get {if (_AbuseDeleteWatchRadio == null) {_AbuseDeleteWatchRadio = (CheckBoxElement)Document.GetElementById(clientId + "_AbuseDeleteWatchRadio");}; return _AbuseDeleteWatchRadio;}} private CheckBoxElement _AbuseDeleteWatchRadio;
		public jQueryObject AbuseDeleteWatchRadioJ {get {if (_AbuseDeleteWatchRadioJ == null) {_AbuseDeleteWatchRadioJ = jQuery.Select("#" + clientId + "_AbuseDeleteWatchRadio");}; return _AbuseDeleteWatchRadioJ;}} private jQueryObject _AbuseDeleteWatchRadioJ;
		public CheckBoxElement AbuseDeleteBanRadio {get {if (_AbuseDeleteBanRadio == null) {_AbuseDeleteBanRadio = (CheckBoxElement)Document.GetElementById(clientId + "_AbuseDeleteBanRadio");}; return _AbuseDeleteBanRadio;}} private CheckBoxElement _AbuseDeleteBanRadio;
		public jQueryObject AbuseDeleteBanRadioJ {get {if (_AbuseDeleteBanRadioJ == null) {_AbuseDeleteBanRadioJ = jQuery.Select("#" + clientId + "_AbuseDeleteBanRadio");}; return _AbuseDeleteBanRadioJ;}} private jQueryObject _AbuseDeleteBanRadioJ;
		public CheckBoxElement AbuseDeleteBanModerateRadio {get {if (_AbuseDeleteBanModerateRadio == null) {_AbuseDeleteBanModerateRadio = (CheckBoxElement)Document.GetElementById(clientId + "_AbuseDeleteBanModerateRadio");}; return _AbuseDeleteBanModerateRadio;}} private CheckBoxElement _AbuseDeleteBanModerateRadio;
		public jQueryObject AbuseDeleteBanModerateRadioJ {get {if (_AbuseDeleteBanModerateRadioJ == null) {_AbuseDeleteBanModerateRadioJ = jQuery.Select("#" + clientId + "_AbuseDeleteBanModerateRadio");}; return _AbuseDeleteBanModerateRadioJ;}} private jQueryObject _AbuseDeleteBanModerateRadioJ;
		public InputElement ResolveDescriptionTextBox {get {if (_ResolveDescriptionTextBox == null) {_ResolveDescriptionTextBox = (InputElement)Document.GetElementById(clientId + "_ResolveDescriptionTextBox");}; return _ResolveDescriptionTextBox;}} private InputElement _ResolveDescriptionTextBox;
		public jQueryObject ResolveDescriptionTextBoxJ {get {if (_ResolveDescriptionTextBoxJ == null) {_ResolveDescriptionTextBoxJ = jQuery.Select("#" + clientId + "_ResolveDescriptionTextBox");}; return _ResolveDescriptionTextBoxJ;}} private jQueryObject _ResolveDescriptionTextBoxJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
