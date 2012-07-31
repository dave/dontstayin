//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.MixmagListings
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
		public SelectElement IssueDrop {get {if (_IssueDrop == null) {_IssueDrop = (SelectElement)Document.GetElementById(clientId + "_IssueDrop");}; return _IssueDrop;}} private SelectElement _IssueDrop;
		public jQueryObject IssueDropJ {get {if (_IssueDropJ == null) {_IssueDropJ = jQuery.Select("#" + clientId + "_IssueDrop");}; return _IssueDropJ;}} private jQueryObject _IssueDropJ;
		public SelectElement ZoneDrop {get {if (_ZoneDrop == null) {_ZoneDrop = (SelectElement)Document.GetElementById(clientId + "_ZoneDrop");}; return _ZoneDrop;}} private SelectElement _ZoneDrop;
		public jQueryObject ZoneDropJ {get {if (_ZoneDropJ == null) {_ZoneDropJ = jQuery.Select("#" + clientId + "_ZoneDrop");}; return _ZoneDropJ;}} private jQueryObject _ZoneDropJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
