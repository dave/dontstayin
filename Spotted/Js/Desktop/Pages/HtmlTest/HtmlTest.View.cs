//mappings.Add("Spotted.Controls.EventGetter", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.VenueGetter", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.HtmlTest
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
		public Element uiEventGetter {get {if (_uiEventGetter == null) {_uiEventGetter = (Element)Document.GetElementById(clientId + "_uiEventGetter");}; return _uiEventGetter;}} private Element _uiEventGetter;
		public jQueryObject uiEventGetterJ {get {if (_uiEventGetterJ == null) {_uiEventGetterJ = jQuery.Select("#" + clientId + "_uiEventGetter");}; return _uiEventGetterJ;}} private jQueryObject _uiEventGetterJ;//mappings.Add("Spotted.Controls.EventGetter", ElementGetter("Element"));
		public Element uiVenueGetter {get {if (_uiVenueGetter == null) {_uiVenueGetter = (Element)Document.GetElementById(clientId + "_uiVenueGetter");}; return _uiVenueGetter;}} private Element _uiVenueGetter;
		public jQueryObject uiVenueGetterJ {get {if (_uiVenueGetterJ == null) {_uiVenueGetterJ = jQuery.Select("#" + clientId + "_uiVenueGetter");}; return _uiVenueGetterJ;}} private jQueryObject _uiVenueGetterJ;//mappings.Add("Spotted.Controls.VenueGetter", ElementGetter("Element"));
		public Js.Controls.MultiBuddyChooser.Controller MultiBuddyChooser1 {get {return (Js.Controls.MultiBuddyChooser.Controller) Script.Eval(clientId + "_MultiBuddyChooser1Controller");}}
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GridView1 {get {if (_GridView1 == null) {_GridView1 = (Element)Document.GetElementById(clientId + "_GridView1");}; return _GridView1;}} private Element _GridView1;
		public jQueryObject GridView1J {get {if (_GridView1J == null) {_GridView1J = jQuery.Select("#" + clientId + "_GridView1");}; return _GridView1J;}} private jQueryObject _GridView1J;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
