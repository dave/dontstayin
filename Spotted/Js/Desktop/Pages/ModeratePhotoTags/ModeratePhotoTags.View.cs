//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.ModeratePhotoTags
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
		public Element H16 {get {if (_H16 == null) {_H16 = (Element)Document.GetElementById(clientId + "_H16");}; return _H16;}} private Element _H16;
		public jQueryObject H16J {get {if (_H16J == null) {_H16J = jQuery.Select("#" + clientId + "_H16");}; return _H16J;}} private jQueryObject _H16J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public ImageElement uiPhotoImg {get {if (_uiPhotoImg == null) {_uiPhotoImg = (ImageElement)Document.GetElementById(clientId + "_uiPhotoImg");}; return _uiPhotoImg;}} private ImageElement _uiPhotoImg;
		public jQueryObject uiPhotoImgJ {get {if (_uiPhotoImgJ == null) {_uiPhotoImgJ = jQuery.Select("#" + clientId + "_uiPhotoImg");}; return _uiPhotoImgJ;}} private jQueryObject _uiPhotoImgJ;
		public Element uiNoTags {get {if (_uiNoTags == null) {_uiNoTags = (Element)Document.GetElementById(clientId + "_uiNoTags");}; return _uiNoTags;}} private Element _uiNoTags;
		public jQueryObject uiNoTagsJ {get {if (_uiNoTagsJ == null) {_uiNoTagsJ = jQuery.Select("#" + clientId + "_uiNoTags");}; return _uiNoTagsJ;}} private jQueryObject _uiNoTagsJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiPhotoTags {get {if (_uiPhotoTags == null) {_uiPhotoTags = (Element)Document.GetElementById(clientId + "_uiPhotoTags");}; return _uiPhotoTags;}} private Element _uiPhotoTags;
		public jQueryObject uiPhotoTagsJ {get {if (_uiPhotoTagsJ == null) {_uiPhotoTagsJ = jQuery.Select("#" + clientId + "_uiPhotoTags");}; return _uiPhotoTagsJ;}} private jQueryObject _uiPhotoTagsJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
