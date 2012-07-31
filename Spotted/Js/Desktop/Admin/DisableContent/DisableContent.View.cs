//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.DisableContent
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
		public InputElement PhotoK {get {if (_PhotoK == null) {_PhotoK = (InputElement)Document.GetElementById(clientId + "_PhotoK");}; return _PhotoK;}} private InputElement _PhotoK;
		public jQueryObject PhotoKJ {get {if (_PhotoKJ == null) {_PhotoKJ = jQuery.Select("#" + clientId + "_PhotoK");}; return _PhotoKJ;}} private jQueryObject _PhotoKJ;
		public Element OutLabel {get {if (_OutLabel == null) {_OutLabel = (Element)Document.GetElementById(clientId + "_OutLabel");}; return _OutLabel;}} private Element _OutLabel;
		public jQueryObject OutLabelJ {get {if (_OutLabelJ == null) {_OutLabelJ = jQuery.Select("#" + clientId + "_OutLabel");}; return _OutLabelJ;}} private jQueryObject _OutLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
