//mappings.Add("Spotted.Blank.QuickBrowserPhotoListContent", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.QuickBrowserPhotoList
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
		public Element PhotoListContent {get {if (_PhotoListContent == null) {_PhotoListContent = (Element)Document.GetElementById(clientId + "_PhotoListContent");}; return _PhotoListContent;}} private Element _PhotoListContent;
		public jQueryObject PhotoListContentJ {get {if (_PhotoListContentJ == null) {_PhotoListContentJ = jQuery.Select("#" + clientId + "_PhotoListContent");}; return _PhotoListContentJ;}} private jQueryObject _PhotoListContentJ;//mappings.Add("Spotted.Blank.QuickBrowserPhotoListContent", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
