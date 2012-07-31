//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.ParaPhotoList
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
		public Element PhotosDataList {get {if (_PhotosDataList == null) {_PhotosDataList = (Element)Document.GetElementById(clientId + "_PhotosDataList");}; return _PhotosDataList;}} private Element _PhotosDataList;
		public jQueryObject PhotosDataListJ {get {if (_PhotosDataListJ == null) {_PhotosDataListJ = jQuery.Select("#" + clientId + "_PhotosDataList");}; return _PhotosDataListJ;}} private jQueryObject _PhotosDataListJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element NoPhotosDiv {get {if (_NoPhotosDiv == null) {_NoPhotosDiv = (Element)Document.GetElementById(clientId + "_NoPhotosDiv");}; return _NoPhotosDiv;}} private Element _NoPhotosDiv;
		public jQueryObject NoPhotosDivJ {get {if (_NoPhotosDivJ == null) {_NoPhotosDivJ = jQuery.Select("#" + clientId + "_NoPhotosDiv");}; return _NoPhotosDivJ;}} private jQueryObject _NoPhotosDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
