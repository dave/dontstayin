//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.MusicTypes", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.MyMusic
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
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element MusicTypes {get {if (_MusicTypes == null) {_MusicTypes = (Element)Document.GetElementById(clientId + "_MusicTypes");}; return _MusicTypes;}} private Element _MusicTypes;
		public jQueryObject MusicTypesJ {get {if (_MusicTypesJ == null) {_MusicTypesJ = jQuery.Select("#" + clientId + "_MusicTypes");}; return _MusicTypesJ;}} private jQueryObject _MusicTypesJ;//mappings.Add("Spotted.Controls.MusicTypes", ElementGetter("Element"));
		public Element UpdatedLabel {get {if (_UpdatedLabel == null) {_UpdatedLabel = (Element)Document.GetElementById(clientId + "_UpdatedLabel");}; return _UpdatedLabel;}} private Element _UpdatedLabel;
		public jQueryObject UpdatedLabelJ {get {if (_UpdatedLabelJ == null) {_UpdatedLabelJ = jQuery.Select("#" + clientId + "_UpdatedLabel");}; return _UpdatedLabelJ;}} private jQueryObject _UpdatedLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
