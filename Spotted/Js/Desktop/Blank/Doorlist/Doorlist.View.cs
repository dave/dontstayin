//mappings.Add("Spotted.Controls.Doorlist", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.Doorlist
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
		public Element uiDoorlist {get {if (_uiDoorlist == null) {_uiDoorlist = (Element)Document.GetElementById(clientId + "_uiDoorlist");}; return _uiDoorlist;}} private Element _uiDoorlist;
		public jQueryObject uiDoorlistJ {get {if (_uiDoorlistJ == null) {_uiDoorlistJ = jQuery.Select("#" + clientId + "_uiDoorlist");}; return _uiDoorlistJ;}} private jQueryObject _uiDoorlistJ;//mappings.Add("Spotted.Controls.Doorlist", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
