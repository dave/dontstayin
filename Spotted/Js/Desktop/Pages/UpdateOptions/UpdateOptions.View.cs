//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.UpdateOptions
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
		public Element H19 {get {if (_H19 == null) {_H19 = (Element)Document.GetElementById(clientId + "_H19");}; return _H19;}} private Element _H19;
		public jQueryObject H19J {get {if (_H19J == null) {_H19J = jQuery.Select("#" + clientId + "_H19");}; return _H19J;}} private jQueryObject _H19J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public CheckBoxElement EmailCheck {get {if (_EmailCheck == null) {_EmailCheck = (CheckBoxElement)Document.GetElementById(clientId + "_EmailCheck");}; return _EmailCheck;}} private CheckBoxElement _EmailCheck;
		public jQueryObject EmailCheckJ {get {if (_EmailCheckJ == null) {_EmailCheckJ = jQuery.Select("#" + clientId + "_EmailCheck");}; return _EmailCheckJ;}} private jQueryObject _EmailCheckJ;
		public CheckBoxElement MusicGeneric {get {if (_MusicGeneric == null) {_MusicGeneric = (CheckBoxElement)Document.GetElementById(clientId + "_MusicGeneric");}; return _MusicGeneric;}} private CheckBoxElement _MusicGeneric;
		public jQueryObject MusicGenericJ {get {if (_MusicGenericJ == null) {_MusicGenericJ = jQuery.Select("#" + clientId + "_MusicGeneric");}; return _MusicGenericJ;}} private jQueryObject _MusicGenericJ;
		public Element MusicLabel {get {if (_MusicLabel == null) {_MusicLabel = (Element)Document.GetElementById(clientId + "_MusicLabel");}; return _MusicLabel;}} private Element _MusicLabel;
		public jQueryObject MusicLabelJ {get {if (_MusicLabelJ == null) {_MusicLabelJ = jQuery.Select("#" + clientId + "_MusicLabel");}; return _MusicLabelJ;}} private jQueryObject _MusicLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericMusicLabel {get {if (_GenericMusicLabel == null) {_GenericMusicLabel = (Element)Document.GetElementById(clientId + "_GenericMusicLabel");}; return _GenericMusicLabel;}} private Element _GenericMusicLabel;
		public jQueryObject GenericMusicLabelJ {get {if (_GenericMusicLabelJ == null) {_GenericMusicLabelJ = jQuery.Select("#" + clientId + "_GenericMusicLabel");}; return _GenericMusicLabelJ;}} private jQueryObject _GenericMusicLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PlacesLabel {get {if (_PlacesLabel == null) {_PlacesLabel = (Element)Document.GetElementById(clientId + "_PlacesLabel");}; return _PlacesLabel;}} private Element _PlacesLabel;
		public jQueryObject PlacesLabelJ {get {if (_PlacesLabelJ == null) {_PlacesLabelJ = jQuery.Select("#" + clientId + "_PlacesLabel");}; return _PlacesLabelJ;}} private jQueryObject _PlacesLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericMusicP {get {if (_GenericMusicP == null) {_GenericMusicP = (Element)Document.GetElementById(clientId + "_GenericMusicP");}; return _GenericMusicP;}} private Element _GenericMusicP;
		public jQueryObject GenericMusicPJ {get {if (_GenericMusicPJ == null) {_GenericMusicPJ = jQuery.Select("#" + clientId + "_GenericMusicP");}; return _GenericMusicPJ;}} private jQueryObject _GenericMusicPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement OptionsPanel {get {if (_OptionsPanel == null) {_OptionsPanel = (DivElement)Document.GetElementById(clientId + "_OptionsPanel");}; return _OptionsPanel;}} private DivElement _OptionsPanel;
		public jQueryObject OptionsPanelJ {get {if (_OptionsPanelJ == null) {_OptionsPanelJ = jQuery.Select("#" + clientId + "_OptionsPanel");}; return _OptionsPanelJ;}} private jQueryObject _OptionsPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
