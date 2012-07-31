//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Logout
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
		public InputElement AutoLogout_Value {get {if (_AutoLogout_Value == null) {_AutoLogout_Value = (InputElement)Document.GetElementById(clientId + "_AutoLogout_Value");}; return _AutoLogout_Value;}} private InputElement _AutoLogout_Value;
		public jQueryObject AutoLogout_ValueJ {get {if (_AutoLogout_ValueJ == null) {_AutoLogout_ValueJ = jQuery.Select("#" + clientId + "_AutoLogout_Value");}; return _AutoLogout_ValueJ;}} private jQueryObject _AutoLogout_ValueJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
