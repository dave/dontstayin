//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.StripUsr
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
		public InputElement ObjectKTextBox {get {if (_ObjectKTextBox == null) {_ObjectKTextBox = (InputElement)Document.GetElementById(clientId + "_ObjectKTextBox");}; return _ObjectKTextBox;}} private InputElement _ObjectKTextBox;
		public jQueryObject ObjectKTextBoxJ {get {if (_ObjectKTextBoxJ == null) {_ObjectKTextBoxJ = jQuery.Select("#" + clientId + "_ObjectKTextBox");}; return _ObjectKTextBoxJ;}} private jQueryObject _ObjectKTextBoxJ;
		public Element DeleteButton {get {if (_DeleteButton == null) {_DeleteButton = (Element)Document.GetElementById(clientId + "_DeleteButton");}; return _DeleteButton;}} private Element _DeleteButton;
		public jQueryObject DeleteButtonJ {get {if (_DeleteButtonJ == null) {_DeleteButtonJ = jQuery.Select("#" + clientId + "_DeleteButton");}; return _DeleteButtonJ;}} private jQueryObject _DeleteButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element DoneLabel {get {if (_DoneLabel == null) {_DoneLabel = (Element)Document.GetElementById(clientId + "_DoneLabel");}; return _DoneLabel;}} private Element _DoneLabel;
		public jQueryObject DoneLabelJ {get {if (_DoneLabelJ == null) {_DoneLabelJ = jQuery.Select("#" + clientId + "_DoneLabel");}; return _DoneLabelJ;}} private jQueryObject _DoneLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
