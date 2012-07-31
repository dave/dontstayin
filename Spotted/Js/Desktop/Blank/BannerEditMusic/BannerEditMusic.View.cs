//mappings.Add("Spotted.Controls.MusicTypes", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.BannerEditMusic
{
	public partial class View
		 : Js.BlankUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element uiMusicTypesControl {get {if (_uiMusicTypesControl == null) {_uiMusicTypesControl = (Element)Document.GetElementById(clientId + "_uiMusicTypesControl");}; return _uiMusicTypesControl;}} private Element _uiMusicTypesControl;
		public jQueryObject uiMusicTypesControlJ {get {if (_uiMusicTypesControlJ == null) {_uiMusicTypesControlJ = jQuery.Select("#" + clientId + "_uiMusicTypesControl");}; return _uiMusicTypesControlJ;}} private jQueryObject _uiMusicTypesControlJ;//mappings.Add("Spotted.Controls.MusicTypes", ElementGetter("Element"));
		public Element uiSaveButton {get {if (_uiSaveButton == null) {_uiSaveButton = (Element)Document.GetElementById(clientId + "_uiSaveButton");}; return _uiSaveButton;}} private Element _uiSaveButton;
		public jQueryObject uiSaveButtonJ {get {if (_uiSaveButtonJ == null) {_uiSaveButtonJ = jQuery.Select("#" + clientId + "_uiSaveButton");}; return _uiSaveButtonJ;}} private jQueryObject _uiSaveButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
