//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.CommentEdit
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
		public DivElement SubjectPanel {get {if (_SubjectPanel == null) {_SubjectPanel = (DivElement)Document.GetElementById(clientId + "_SubjectPanel");}; return _SubjectPanel;}} private DivElement _SubjectPanel;
		public jQueryObject SubjectPanelJ {get {if (_SubjectPanelJ == null) {_SubjectPanelJ = jQuery.Select("#" + clientId + "_SubjectPanel");}; return _SubjectPanelJ;}} private jQueryObject _SubjectPanelJ;
		public InputElement ThreadSubjectEditBox {get {if (_ThreadSubjectEditBox == null) {_ThreadSubjectEditBox = (InputElement)Document.GetElementById(clientId + "_ThreadSubjectEditBox");}; return _ThreadSubjectEditBox;}} private InputElement _ThreadSubjectEditBox;
		public jQueryObject ThreadSubjectEditBoxJ {get {if (_ThreadSubjectEditBoxJ == null) {_ThreadSubjectEditBoxJ = jQuery.Select("#" + clientId + "_ThreadSubjectEditBox");}; return _ThreadSubjectEditBoxJ;}} private jQueryObject _ThreadSubjectEditBoxJ;
		public Js.Controls.Html.Controller CommentEditHtml {get {return (Js.Controls.Html.Controller) Script.Eval(clientId + "_CommentEditHtmlController");}}
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
