//mappings.Add("Spotted.Blank.DatePhotoListContent", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.DatePhotoList
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
		public Element DatePhotoListContent {get {if (_DatePhotoListContent == null) {_DatePhotoListContent = (Element)Document.GetElementById(clientId + "_DatePhotoListContent");}; return _DatePhotoListContent;}} private Element _DatePhotoListContent;
		public jQueryObject DatePhotoListContentJ {get {if (_DatePhotoListContentJ == null) {_DatePhotoListContentJ = jQuery.Select("#" + clientId + "_DatePhotoListContent");}; return _DatePhotoListContentJ;}} private jQueryObject _DatePhotoListContentJ;//mappings.Add("Spotted.Blank.DatePhotoListContent", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
