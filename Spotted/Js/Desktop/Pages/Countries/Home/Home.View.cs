//mappings.Add("Spotted.Pages.Countries.HomeContentTop", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Latest", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Countries.Home
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
		public Element HomeContentTopUc {get {if (_HomeContentTopUc == null) {_HomeContentTopUc = (Element)Document.GetElementById(clientId + "_HomeContentTopUc");}; return _HomeContentTopUc;}} private Element _HomeContentTopUc;
		public jQueryObject HomeContentTopUcJ {get {if (_HomeContentTopUcJ == null) {_HomeContentTopUcJ = jQuery.Select("#" + clientId + "_HomeContentTopUc");}; return _HomeContentTopUcJ;}} private jQueryObject _HomeContentTopUcJ;//mappings.Add("Spotted.Pages.Countries.HomeContentTop", ElementGetter("Element"));
		public Element Latest {get {if (_Latest == null) {_Latest = (Element)Document.GetElementById(clientId + "_Latest");}; return _Latest;}} private Element _Latest;
		public jQueryObject LatestJ {get {if (_LatestJ == null) {_LatestJ = jQuery.Select("#" + clientId + "_Latest");}; return _LatestJ;}} private jQueryObject _LatestJ;//mappings.Add("Spotted.Controls.Latest", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
