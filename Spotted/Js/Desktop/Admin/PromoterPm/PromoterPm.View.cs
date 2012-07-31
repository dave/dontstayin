//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.PromoterPm
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
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Button3 {get {if (_Button3 == null) {_Button3 = (Element)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private Element _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SendCommentButton {get {if (_SendCommentButton == null) {_SendCommentButton = (Element)Document.GetElementById(clientId + "_SendCommentButton");}; return _SendCommentButton;}} private Element _SendCommentButton;
		public jQueryObject SendCommentButtonJ {get {if (_SendCommentButtonJ == null) {_SendCommentButtonJ = jQuery.Select("#" + clientId + "_SendCommentButton");}; return _SendCommentButtonJ;}} private jQueryObject _SendCommentButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public InputElement MessageId {get {if (_MessageId == null) {_MessageId = (InputElement)Document.GetElementById(clientId + "_MessageId");}; return _MessageId;}} private InputElement _MessageId;
		public jQueryObject MessageIdJ {get {if (_MessageIdJ == null) {_MessageIdJ = jQuery.Select("#" + clientId + "_MessageId");}; return _MessageIdJ;}} private jQueryObject _MessageIdJ;
		public InputElement CommentTextBox {get {if (_CommentTextBox == null) {_CommentTextBox = (InputElement)Document.GetElementById(clientId + "_CommentTextBox");}; return _CommentTextBox;}} private InputElement _CommentTextBox;
		public jQueryObject CommentTextBoxJ {get {if (_CommentTextBoxJ == null) {_CommentTextBoxJ = jQuery.Select("#" + clientId + "_CommentTextBox");}; return _CommentTextBoxJ;}} private jQueryObject _CommentTextBoxJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
