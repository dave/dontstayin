//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.Stats
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
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element UsersOnline5MinLabel {get {if (_UsersOnline5MinLabel == null) {_UsersOnline5MinLabel = (Element)Document.GetElementById(clientId + "_UsersOnline5MinLabel");}; return _UsersOnline5MinLabel;}} private Element _UsersOnline5MinLabel;
		public jQueryObject UsersOnline5MinLabelJ {get {if (_UsersOnline5MinLabelJ == null) {_UsersOnline5MinLabelJ = jQuery.Select("#" + clientId + "_UsersOnline5MinLabel");}; return _UsersOnline5MinLabelJ;}} private jQueryObject _UsersOnline5MinLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element MaxUsersOnline5MinLabel {get {if (_MaxUsersOnline5MinLabel == null) {_MaxUsersOnline5MinLabel = (Element)Document.GetElementById(clientId + "_MaxUsersOnline5MinLabel");}; return _MaxUsersOnline5MinLabel;}} private Element _MaxUsersOnline5MinLabel;
		public jQueryObject MaxUsersOnline5MinLabelJ {get {if (_MaxUsersOnline5MinLabelJ == null) {_MaxUsersOnline5MinLabelJ = jQuery.Select("#" + clientId + "_MaxUsersOnline5MinLabel");}; return _MaxUsersOnline5MinLabelJ;}} private jQueryObject _MaxUsersOnline5MinLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element MaxUsersOnline5MinDateLabel {get {if (_MaxUsersOnline5MinDateLabel == null) {_MaxUsersOnline5MinDateLabel = (Element)Document.GetElementById(clientId + "_MaxUsersOnline5MinDateLabel");}; return _MaxUsersOnline5MinDateLabel;}} private Element _MaxUsersOnline5MinDateLabel;
		public jQueryObject MaxUsersOnline5MinDateLabelJ {get {if (_MaxUsersOnline5MinDateLabelJ == null) {_MaxUsersOnline5MinDateLabelJ = jQuery.Select("#" + clientId + "_MaxUsersOnline5MinDateLabel");}; return _MaxUsersOnline5MinDateLabelJ;}} private jQueryObject _MaxUsersOnline5MinDateLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element UsersOnline30MinLabel {get {if (_UsersOnline30MinLabel == null) {_UsersOnline30MinLabel = (Element)Document.GetElementById(clientId + "_UsersOnline30MinLabel");}; return _UsersOnline30MinLabel;}} private Element _UsersOnline30MinLabel;
		public jQueryObject UsersOnline30MinLabelJ {get {if (_UsersOnline30MinLabelJ == null) {_UsersOnline30MinLabelJ = jQuery.Select("#" + clientId + "_UsersOnline30MinLabel");}; return _UsersOnline30MinLabelJ;}} private jQueryObject _UsersOnline30MinLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element MaxUsersOnline30MinLabel {get {if (_MaxUsersOnline30MinLabel == null) {_MaxUsersOnline30MinLabel = (Element)Document.GetElementById(clientId + "_MaxUsersOnline30MinLabel");}; return _MaxUsersOnline30MinLabel;}} private Element _MaxUsersOnline30MinLabel;
		public jQueryObject MaxUsersOnline30MinLabelJ {get {if (_MaxUsersOnline30MinLabelJ == null) {_MaxUsersOnline30MinLabelJ = jQuery.Select("#" + clientId + "_MaxUsersOnline30MinLabel");}; return _MaxUsersOnline30MinLabelJ;}} private jQueryObject _MaxUsersOnline30MinLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
