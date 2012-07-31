//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Password
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
		public DivElement PanelPassword {get {if (_PanelPassword == null) {_PanelPassword = (DivElement)Document.GetElementById(clientId + "_PanelPassword");}; return _PanelPassword;}} private DivElement _PanelPassword;
		public jQueryObject PanelPasswordJ {get {if (_PanelPasswordJ == null) {_PanelPasswordJ = jQuery.Select("#" + clientId + "_PanelPassword");}; return _PanelPasswordJ;}} private jQueryObject _PanelPasswordJ;
		public Element Header99 {get {if (_Header99 == null) {_Header99 = (Element)Document.GetElementById(clientId + "_Header99");}; return _Header99;}} private Element _Header99;
		public jQueryObject Header99J {get {if (_Header99J == null) {_Header99J = jQuery.Select("#" + clientId + "_Header99");}; return _Header99J;}} private jQueryObject _Header99J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement EmailTextBox {get {if (_EmailTextBox == null) {_EmailTextBox = (InputElement)Document.GetElementById(clientId + "_EmailTextBox");}; return _EmailTextBox;}} private InputElement _EmailTextBox;
		public jQueryObject EmailTextBoxJ {get {if (_EmailTextBoxJ == null) {_EmailTextBoxJ = jQuery.Select("#" + clientId + "_EmailTextBox");}; return _EmailTextBoxJ;}} private jQueryObject _EmailTextBoxJ;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement ErrorPanel {get {if (_ErrorPanel == null) {_ErrorPanel = (DivElement)Document.GetElementById(clientId + "_ErrorPanel");}; return _ErrorPanel;}} private DivElement _ErrorPanel;
		public jQueryObject ErrorPanelJ {get {if (_ErrorPanelJ == null) {_ErrorPanelJ = jQuery.Select("#" + clientId + "_ErrorPanel");}; return _ErrorPanelJ;}} private jQueryObject _ErrorPanelJ;
		public DivElement DonePanel {get {if (_DonePanel == null) {_DonePanel = (DivElement)Document.GetElementById(clientId + "_DonePanel");}; return _DonePanel;}} private DivElement _DonePanel;
		public jQueryObject DonePanelJ {get {if (_DonePanelJ == null) {_DonePanelJ = jQuery.Select("#" + clientId + "_DonePanel");}; return _DonePanelJ;}} private jQueryObject _DonePanelJ;
		public DivElement PanelReset {get {if (_PanelReset == null) {_PanelReset = (DivElement)Document.GetElementById(clientId + "_PanelReset");}; return _PanelReset;}} private DivElement _PanelReset;
		public jQueryObject PanelResetJ {get {if (_PanelResetJ == null) {_PanelResetJ = jQuery.Select("#" + clientId + "_PanelReset");}; return _PanelResetJ;}} private jQueryObject _PanelResetJ;
		public Element Header1 {get {if (_Header1 == null) {_Header1 = (Element)Document.GetElementById(clientId + "_Header1");}; return _Header1;}} private Element _Header1;
		public jQueryObject Header1J {get {if (_Header1J == null) {_Header1J = jQuery.Select("#" + clientId + "_Header1");}; return _Header1J;}} private jQueryObject _Header1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement Password1 {get {if (_Password1 == null) {_Password1 = (InputElement)Document.GetElementById(clientId + "_Password1");}; return _Password1;}} private InputElement _Password1;
		public jQueryObject Password1J {get {if (_Password1J == null) {_Password1J = jQuery.Select("#" + clientId + "_Password1");}; return _Password1J;}} private jQueryObject _Password1J;
		public InputElement Password2 {get {if (_Password2 == null) {_Password2 = (InputElement)Document.GetElementById(clientId + "_Password2");}; return _Password2;}} private InputElement _Password2;
		public jQueryObject Password2J {get {if (_Password2J == null) {_Password2J = jQuery.Select("#" + clientId + "_Password2");}; return _Password2J;}} private jQueryObject _Password2J;
		public DivElement PanelResetCancelled {get {if (_PanelResetCancelled == null) {_PanelResetCancelled = (DivElement)Document.GetElementById(clientId + "_PanelResetCancelled");}; return _PanelResetCancelled;}} private DivElement _PanelResetCancelled;
		public jQueryObject PanelResetCancelledJ {get {if (_PanelResetCancelledJ == null) {_PanelResetCancelledJ = jQuery.Select("#" + clientId + "_PanelResetCancelled");}; return _PanelResetCancelledJ;}} private jQueryObject _PanelResetCancelledJ;
		public Element Header2 {get {if (_Header2 == null) {_Header2 = (Element)Document.GetElementById(clientId + "_Header2");}; return _Header2;}} private Element _Header2;
		public jQueryObject Header2J {get {if (_Header2J == null) {_Header2J = jQuery.Select("#" + clientId + "_Header2");}; return _Header2J;}} private jQueryObject _Header2J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelResetDone {get {if (_PanelResetDone == null) {_PanelResetDone = (DivElement)Document.GetElementById(clientId + "_PanelResetDone");}; return _PanelResetDone;}} private DivElement _PanelResetDone;
		public jQueryObject PanelResetDoneJ {get {if (_PanelResetDoneJ == null) {_PanelResetDoneJ = jQuery.Select("#" + clientId + "_PanelResetDone");}; return _PanelResetDoneJ;}} private jQueryObject _PanelResetDoneJ;
		public Element Header3 {get {if (_Header3 == null) {_Header3 = (Element)Document.GetElementById(clientId + "_Header3");}; return _Header3;}} private Element _Header3;
		public jQueryObject Header3J {get {if (_Header3J == null) {_Header3J = jQuery.Select("#" + clientId + "_Header3");}; return _Header3J;}} private jQueryObject _Header3J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelResetError {get {if (_PanelResetError == null) {_PanelResetError = (DivElement)Document.GetElementById(clientId + "_PanelResetError");}; return _PanelResetError;}} private DivElement _PanelResetError;
		public jQueryObject PanelResetErrorJ {get {if (_PanelResetErrorJ == null) {_PanelResetErrorJ = jQuery.Select("#" + clientId + "_PanelResetError");}; return _PanelResetErrorJ;}} private jQueryObject _PanelResetErrorJ;
		public Element Header4 {get {if (_Header4 == null) {_Header4 = (Element)Document.GetElementById(clientId + "_Header4");}; return _Header4;}} private Element _Header4;
		public jQueryObject Header4J {get {if (_Header4J == null) {_Header4J = jQuery.Select("#" + clientId + "_Header4");}; return _Header4J;}} private jQueryObject _Header4J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
