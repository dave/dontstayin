//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RegularExpressionValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.ChangePassword
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
		public DivElement PanelChange {get {if (_PanelChange == null) {_PanelChange = (DivElement)Document.GetElementById(clientId + "_PanelChange");}; return _PanelChange;}} private DivElement _PanelChange;
		public jQueryObject PanelChangeJ {get {if (_PanelChangeJ == null) {_PanelChangeJ = jQuery.Select("#" + clientId + "_PanelChange");}; return _PanelChangeJ;}} private jQueryObject _PanelChangeJ;
		public InputElement CurrentPassword {get {if (_CurrentPassword == null) {_CurrentPassword = (InputElement)Document.GetElementById(clientId + "_CurrentPassword");}; return _CurrentPassword;}} private InputElement _CurrentPassword;
		public jQueryObject CurrentPasswordJ {get {if (_CurrentPasswordJ == null) {_CurrentPasswordJ = jQuery.Select("#" + clientId + "_CurrentPassword");}; return _CurrentPasswordJ;}} private jQueryObject _CurrentPasswordJ;
		public InputElement Password1 {get {if (_Password1 == null) {_Password1 = (InputElement)Document.GetElementById(clientId + "_Password1");}; return _Password1;}} private InputElement _Password1;
		public jQueryObject Password1J {get {if (_Password1J == null) {_Password1J = jQuery.Select("#" + clientId + "_Password1");}; return _Password1J;}} private jQueryObject _Password1J;
		public InputElement Password2 {get {if (_Password2 == null) {_Password2 = (InputElement)Document.GetElementById(clientId + "_Password2");}; return _Password2;}} private InputElement _Password2;
		public jQueryObject Password2J {get {if (_Password2J == null) {_Password2J = jQuery.Select("#" + clientId + "_Password2");}; return _Password2J;}} private jQueryObject _Password2J;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element CustomValidator1 {get {if (_CustomValidator1 == null) {_CustomValidator1 = (Element)Document.GetElementById(clientId + "_CustomValidator1");}; return _CustomValidator1;}} private Element _CustomValidator1;
		public jQueryObject CustomValidator1J {get {if (_CustomValidator1J == null) {_CustomValidator1J = jQuery.Select("#" + clientId + "_CustomValidator1");}; return _CustomValidator1J;}} private jQueryObject _CustomValidator1J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element RequiredFieldValidator2 {get {if (_RequiredFieldValidator2 == null) {_RequiredFieldValidator2 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator2");}; return _RequiredFieldValidator2;}} private Element _RequiredFieldValidator2;
		public jQueryObject RequiredFieldValidator2J {get {if (_RequiredFieldValidator2J == null) {_RequiredFieldValidator2J = jQuery.Select("#" + clientId + "_RequiredFieldValidator2");}; return _RequiredFieldValidator2J;}} private jQueryObject _RequiredFieldValidator2J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element RegularExpressionValidator1 {get {if (_RegularExpressionValidator1 == null) {_RegularExpressionValidator1 = (Element)Document.GetElementById(clientId + "_RegularExpressionValidator1");}; return _RegularExpressionValidator1;}} private Element _RegularExpressionValidator1;
		public jQueryObject RegularExpressionValidator1J {get {if (_RegularExpressionValidator1J == null) {_RegularExpressionValidator1J = jQuery.Select("#" + clientId + "_RegularExpressionValidator1");}; return _RegularExpressionValidator1J;}} private jQueryObject _RegularExpressionValidator1J;//mappings.Add("System.Web.UI.WebControls.RegularExpressionValidator", ElementGetter("Element"));
		public Element CompareValidator1 {get {if (_CompareValidator1 == null) {_CompareValidator1 = (Element)Document.GetElementById(clientId + "_CompareValidator1");}; return _CompareValidator1;}} private Element _CompareValidator1;
		public jQueryObject CompareValidator1J {get {if (_CompareValidator1J == null) {_CompareValidator1J = jQuery.Select("#" + clientId + "_CompareValidator1");}; return _CompareValidator1J;}} private jQueryObject _CompareValidator1J;//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
		public DivElement PanelDone {get {if (_PanelDone == null) {_PanelDone = (DivElement)Document.GetElementById(clientId + "_PanelDone");}; return _PanelDone;}} private DivElement _PanelDone;
		public jQueryObject PanelDoneJ {get {if (_PanelDoneJ == null) {_PanelDoneJ = jQuery.Select("#" + clientId + "_PanelDone");}; return _PanelDoneJ;}} private jQueryObject _PanelDoneJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
