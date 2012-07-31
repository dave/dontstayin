//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.AdminStats
{
	public partial class View
		 : Js.AdminUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element uiCurrentJobProcessorQueueSize {get {if (_uiCurrentJobProcessorQueueSize == null) {_uiCurrentJobProcessorQueueSize = (Element)Document.GetElementById(clientId + "_uiCurrentJobProcessorQueueSize");}; return _uiCurrentJobProcessorQueueSize;}} private Element _uiCurrentJobProcessorQueueSize;
		public jQueryObject uiCurrentJobProcessorQueueSizeJ {get {if (_uiCurrentJobProcessorQueueSizeJ == null) {_uiCurrentJobProcessorQueueSizeJ = jQuery.Select("#" + clientId + "_uiCurrentJobProcessorQueueSize");}; return _uiCurrentJobProcessorQueueSizeJ;}} private jQueryObject _uiCurrentJobProcessorQueueSizeJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element JobProcessorDataItemsGridView {get {if (_JobProcessorDataItemsGridView == null) {_JobProcessorDataItemsGridView = (Element)Document.GetElementById(clientId + "_JobProcessorDataItemsGridView");}; return _JobProcessorDataItemsGridView;}} private Element _JobProcessorDataItemsGridView;
		public jQueryObject JobProcessorDataItemsGridViewJ {get {if (_JobProcessorDataItemsGridViewJ == null) {_JobProcessorDataItemsGridViewJ = jQuery.Select("#" + clientId + "_JobProcessorDataItemsGridView");}; return _JobProcessorDataItemsGridViewJ;}} private jQueryObject _JobProcessorDataItemsGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element H2 {get {if (_H2 == null) {_H2 = (Element)Document.GetElementById(clientId + "_H2");}; return _H2;}} private Element _H2;
		public jQueryObject H2J {get {if (_H2J == null) {_H2J = jQuery.Select("#" + clientId + "_H2");}; return _H2J;}} private jQueryObject _H2J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element uiPhotoUploaderTriesDataItemsGridView {get {if (_uiPhotoUploaderTriesDataItemsGridView == null) {_uiPhotoUploaderTriesDataItemsGridView = (Element)Document.GetElementById(clientId + "_uiPhotoUploaderTriesDataItemsGridView");}; return _uiPhotoUploaderTriesDataItemsGridView;}} private Element _uiPhotoUploaderTriesDataItemsGridView;
		public jQueryObject uiPhotoUploaderTriesDataItemsGridViewJ {get {if (_uiPhotoUploaderTriesDataItemsGridViewJ == null) {_uiPhotoUploaderTriesDataItemsGridViewJ = jQuery.Select("#" + clientId + "_uiPhotoUploaderTriesDataItemsGridView");}; return _uiPhotoUploaderTriesDataItemsGridViewJ;}} private jQueryObject _uiPhotoUploaderTriesDataItemsGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element uiPhotoUploaderSuccessFailureDataItemsGridView {get {if (_uiPhotoUploaderSuccessFailureDataItemsGridView == null) {_uiPhotoUploaderSuccessFailureDataItemsGridView = (Element)Document.GetElementById(clientId + "_uiPhotoUploaderSuccessFailureDataItemsGridView");}; return _uiPhotoUploaderSuccessFailureDataItemsGridView;}} private Element _uiPhotoUploaderSuccessFailureDataItemsGridView;
		public jQueryObject uiPhotoUploaderSuccessFailureDataItemsGridViewJ {get {if (_uiPhotoUploaderSuccessFailureDataItemsGridViewJ == null) {_uiPhotoUploaderSuccessFailureDataItemsGridViewJ = jQuery.Select("#" + clientId + "_uiPhotoUploaderSuccessFailureDataItemsGridView");}; return _uiPhotoUploaderSuccessFailureDataItemsGridViewJ;}} private jQueryObject _uiPhotoUploaderSuccessFailureDataItemsGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
