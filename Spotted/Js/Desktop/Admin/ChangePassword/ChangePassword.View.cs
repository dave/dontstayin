//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.ChangePassword
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
		public InputElement UsrK {get {if (_UsrK == null) {_UsrK = (InputElement)Document.GetElementById(clientId + "_UsrK");}; return _UsrK;}} private InputElement _UsrK;
		public jQueryObject UsrKJ {get {if (_UsrKJ == null) {_UsrKJ = jQuery.Select("#" + clientId + "_UsrK");}; return _UsrKJ;}} private jQueryObject _UsrKJ;
		public InputElement UsrPassword {get {if (_UsrPassword == null) {_UsrPassword = (InputElement)Document.GetElementById(clientId + "_UsrPassword");}; return _UsrPassword;}} private InputElement _UsrPassword;
		public jQueryObject UsrPasswordJ {get {if (_UsrPasswordJ == null) {_UsrPasswordJ = jQuery.Select("#" + clientId + "_UsrPassword");}; return _UsrPasswordJ;}} private jQueryObject _UsrPasswordJ;
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element OutLabel {get {if (_OutLabel == null) {_OutLabel = (Element)Document.GetElementById(clientId + "_OutLabel");}; return _OutLabel;}} private Element _OutLabel;
		public jQueryObject OutLabelJ {get {if (_OutLabelJ == null) {_OutLabelJ = jQuery.Select("#" + clientId + "_OutLabel");}; return _OutLabelJ;}} private jQueryObject _OutLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
