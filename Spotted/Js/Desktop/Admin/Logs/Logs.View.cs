//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Calendar", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.Logs
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
		public Element Times {get {if (_Times == null) {_Times = (Element)Document.GetElementById(clientId + "_Times");}; return _Times;}} private Element _Times;
		public jQueryObject TimesJ {get {if (_TimesJ == null) {_TimesJ = jQuery.Select("#" + clientId + "_Times");}; return _TimesJ;}} private jQueryObject _TimesJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element Cal {get {if (_Cal == null) {_Cal = (Element)Document.GetElementById(clientId + "_Cal");}; return _Cal;}} private Element _Cal;
		public jQueryObject CalJ {get {if (_CalJ == null) {_CalJ = jQuery.Select("#" + clientId + "_Cal");}; return _CalJ;}} private jQueryObject _CalJ;//mappings.Add("System.Web.UI.WebControls.Calendar", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
