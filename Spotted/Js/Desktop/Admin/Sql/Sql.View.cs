//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.Sql
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
		public InputElement Password {get {if (_Password == null) {_Password = (InputElement)Document.GetElementById(clientId + "_Password");}; return _Password;}} private InputElement _Password;
		public jQueryObject PasswordJ {get {if (_PasswordJ == null) {_PasswordJ = jQuery.Select("#" + clientId + "_Password");}; return _PasswordJ;}} private jQueryObject _PasswordJ;
		public InputElement Query {get {if (_Query == null) {_Query = (InputElement)Document.GetElementById(clientId + "_Query");}; return _Query;}} private InputElement _Query;
		public jQueryObject QueryJ {get {if (_QueryJ == null) {_QueryJ = jQuery.Select("#" + clientId + "_Query");}; return _QueryJ;}} private jQueryObject _QueryJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
