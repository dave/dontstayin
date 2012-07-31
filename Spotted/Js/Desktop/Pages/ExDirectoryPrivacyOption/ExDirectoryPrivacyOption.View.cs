//mappings.Add("Spotted.Controls.ExDirectoryPrivacyOption", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.ExDirectoryPrivacyOption
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
		public Element uiExDirectory {get {if (_uiExDirectory == null) {_uiExDirectory = (Element)Document.GetElementById(clientId + "_uiExDirectory");}; return _uiExDirectory;}} private Element _uiExDirectory;
		public jQueryObject uiExDirectoryJ {get {if (_uiExDirectoryJ == null) {_uiExDirectoryJ = jQuery.Select("#" + clientId + "_uiExDirectory");}; return _uiExDirectoryJ;}} private jQueryObject _uiExDirectoryJ;//mappings.Add("Spotted.Controls.ExDirectoryPrivacyOption", ElementGetter("Element"));
		public Element uiSuccess {get {if (_uiSuccess == null) {_uiSuccess = (Element)Document.GetElementById(clientId + "_uiSuccess");}; return _uiSuccess;}} private Element _uiSuccess;
		public jQueryObject uiSuccessJ {get {if (_uiSuccessJ == null) {_uiSuccessJ = jQuery.Select("#" + clientId + "_uiSuccess");}; return _uiSuccessJ;}} private jQueryObject _uiSuccessJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
