//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Plus
{
	public partial class View
		 : Js.Pages.Promoters.PromoterUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element PromoterIntro1 {get {if (_PromoterIntro1 == null) {_PromoterIntro1 = (Element)Document.GetElementById(clientId + "_PromoterIntro1");}; return _PromoterIntro1;}} private Element _PromoterIntro1;
		public jQueryObject PromoterIntro1J {get {if (_PromoterIntro1J == null) {_PromoterIntro1J = jQuery.Select("#" + clientId + "_PromoterIntro1");}; return _PromoterIntro1J;}} private jQueryObject _PromoterIntro1J;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public Element H1fd4 {get {if (_H1fd4 == null) {_H1fd4 = (Element)Document.GetElementById(clientId + "_H1fd4");}; return _H1fd4;}} private Element _H1fd4;
		public jQueryObject H1fd4J {get {if (_H1fd4J == null) {_H1fd4J = jQuery.Select("#" + clientId + "_H1fd4");}; return _H1fd4J;}} private jQueryObject _H1fd4J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
