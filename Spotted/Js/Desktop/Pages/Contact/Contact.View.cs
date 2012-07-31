//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Contact
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
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H1ghd4 {get {if (_H1ghd4 == null) {_H1ghd4 = (Element)Document.GetElementById(clientId + "_H1ghd4");}; return _H1ghd4;}} private Element _H1ghd4;
		public jQueryObject H1ghd4J {get {if (_H1ghd4J == null) {_H1ghd4J = jQuery.Select("#" + clientId + "_H1ghd4");}; return _H1ghd4J;}} private jQueryObject _H1ghd4J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public ImageElement JohnPic {get {if (_JohnPic == null) {_JohnPic = (ImageElement)Document.GetElementById(clientId + "_JohnPic");}; return _JohnPic;}} private ImageElement _JohnPic;
		public jQueryObject JohnPicJ {get {if (_JohnPicJ == null) {_JohnPicJ = jQuery.Select("#" + clientId + "_JohnPic");}; return _JohnPicJ;}} private jQueryObject _JohnPicJ;
		public ImageElement TimPic {get {if (_TimPic == null) {_TimPic = (ImageElement)Document.GetElementById(clientId + "_TimPic");}; return _TimPic;}} private ImageElement _TimPic;
		public jQueryObject TimPicJ {get {if (_TimPicJ == null) {_TimPicJ = jQuery.Select("#" + clientId + "_TimPic");}; return _TimPicJ;}} private jQueryObject _TimPicJ;
		public ImageElement DavePic {get {if (_DavePic == null) {_DavePic = (ImageElement)Document.GetElementById(clientId + "_DavePic");}; return _DavePic;}} private ImageElement _DavePic;
		public jQueryObject DavePicJ {get {if (_DavePicJ == null) {_DavePicJ = jQuery.Select("#" + clientId + "_DavePic");}; return _DavePicJ;}} private jQueryObject _DavePicJ;
		public AnchorElement JohnLink {get {if (_JohnLink == null) {_JohnLink = (AnchorElement)Document.GetElementById(clientId + "_JohnLink");}; return _JohnLink;}} private AnchorElement _JohnLink;
		public jQueryObject JohnLinkJ {get {if (_JohnLinkJ == null) {_JohnLinkJ = jQuery.Select("#" + clientId + "_JohnLink");}; return _JohnLinkJ;}} private jQueryObject _JohnLinkJ;
		public AnchorElement TimLink {get {if (_TimLink == null) {_TimLink = (AnchorElement)Document.GetElementById(clientId + "_TimLink");}; return _TimLink;}} private AnchorElement _TimLink;
		public jQueryObject TimLinkJ {get {if (_TimLinkJ == null) {_TimLinkJ = jQuery.Select("#" + clientId + "_TimLink");}; return _TimLinkJ;}} private jQueryObject _TimLinkJ;
		public AnchorElement DaveLink {get {if (_DaveLink == null) {_DaveLink = (AnchorElement)Document.GetElementById(clientId + "_DaveLink");}; return _DaveLink;}} private AnchorElement _DaveLink;
		public jQueryObject DaveLinkJ {get {if (_DaveLinkJ == null) {_DaveLinkJ = jQuery.Select("#" + clientId + "_DaveLink");}; return _DaveLinkJ;}} private jQueryObject _DaveLinkJ;
		public Element SuperAdminDataList {get {if (_SuperAdminDataList == null) {_SuperAdminDataList = (Element)Document.GetElementById(clientId + "_SuperAdminDataList");}; return _SuperAdminDataList;}} private Element _SuperAdminDataList;
		public jQueryObject SuperAdminDataListJ {get {if (_SuperAdminDataListJ == null) {_SuperAdminDataListJ = jQuery.Select("#" + clientId + "_SuperAdminDataList");}; return _SuperAdminDataListJ;}} private jQueryObject _SuperAdminDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element SeniorAdminDataList {get {if (_SeniorAdminDataList == null) {_SeniorAdminDataList = (Element)Document.GetElementById(clientId + "_SeniorAdminDataList");}; return _SeniorAdminDataList;}} private Element _SeniorAdminDataList;
		public jQueryObject SeniorAdminDataListJ {get {if (_SeniorAdminDataListJ == null) {_SeniorAdminDataListJ = jQuery.Select("#" + clientId + "_SeniorAdminDataList");}; return _SeniorAdminDataListJ;}} private jQueryObject _SeniorAdminDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element JuniorAdminDataList {get {if (_JuniorAdminDataList == null) {_JuniorAdminDataList = (Element)Document.GetElementById(clientId + "_JuniorAdminDataList");}; return _JuniorAdminDataList;}} private Element _JuniorAdminDataList;
		public jQueryObject JuniorAdminDataListJ {get {if (_JuniorAdminDataListJ == null) {_JuniorAdminDataListJ = jQuery.Select("#" + clientId + "_JuniorAdminDataList");}; return _JuniorAdminDataListJ;}} private jQueryObject _JuniorAdminDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
