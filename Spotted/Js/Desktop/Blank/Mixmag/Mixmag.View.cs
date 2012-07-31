//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.Mixmag
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
		public InputElement Button3 {get {if (_Button3 == null) {_Button3 = (InputElement)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private InputElement _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
