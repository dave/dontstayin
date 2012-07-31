//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Out
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
		public Element TopPChoose {get {if (_TopPChoose == null) {_TopPChoose = (Element)Document.GetElementById(clientId + "_TopPChoose");}; return _TopPChoose;}} private Element _TopPChoose;
		public jQueryObject TopPChooseJ {get {if (_TopPChooseJ == null) {_TopPChooseJ = jQuery.Select("#" + clientId + "_TopPChoose");}; return _TopPChooseJ;}} private jQueryObject _TopPChooseJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element TopP {get {if (_TopP == null) {_TopP = (Element)Document.GetElementById(clientId + "_TopP");}; return _TopP;}} private Element _TopP;
		public jQueryObject TopPJ {get {if (_TopPJ == null) {_TopPJ = jQuery.Select("#" + clientId + "_TopP");}; return _TopPJ;}} private jQueryObject _TopPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public ImageElement Thumb1 {get {if (_Thumb1 == null) {_Thumb1 = (ImageElement)Document.GetElementById(clientId + "_Thumb1");}; return _Thumb1;}} private ImageElement _Thumb1;
		public jQueryObject Thumb1J {get {if (_Thumb1J == null) {_Thumb1J = jQuery.Select("#" + clientId + "_Thumb1");}; return _Thumb1J;}} private jQueryObject _Thumb1J;
		public ImageElement Thumb2 {get {if (_Thumb2 == null) {_Thumb2 = (ImageElement)Document.GetElementById(clientId + "_Thumb2");}; return _Thumb2;}} private ImageElement _Thumb2;
		public jQueryObject Thumb2J {get {if (_Thumb2J == null) {_Thumb2J = jQuery.Select("#" + clientId + "_Thumb2");}; return _Thumb2J;}} private jQueryObject _Thumb2J;
		public ImageElement Thumb3 {get {if (_Thumb3 == null) {_Thumb3 = (ImageElement)Document.GetElementById(clientId + "_Thumb3");}; return _Thumb3;}} private ImageElement _Thumb3;
		public jQueryObject Thumb3J {get {if (_Thumb3J == null) {_Thumb3J = jQuery.Select("#" + clientId + "_Thumb3");}; return _Thumb3J;}} private jQueryObject _Thumb3J;
		public ImageElement Thumb4 {get {if (_Thumb4 == null) {_Thumb4 = (ImageElement)Document.GetElementById(clientId + "_Thumb4");}; return _Thumb4;}} private ImageElement _Thumb4;
		public jQueryObject Thumb4J {get {if (_Thumb4J == null) {_Thumb4J = jQuery.Select("#" + clientId + "_Thumb4");}; return _Thumb4J;}} private jQueryObject _Thumb4J;
		public ImageElement Thumb5 {get {if (_Thumb5 == null) {_Thumb5 = (ImageElement)Document.GetElementById(clientId + "_Thumb5");}; return _Thumb5;}} private ImageElement _Thumb5;
		public jQueryObject Thumb5J {get {if (_Thumb5J == null) {_Thumb5J = jQuery.Select("#" + clientId + "_Thumb5");}; return _Thumb5J;}} private jQueryObject _Thumb5J;
		public AnchorElement Next {get {if (_Next == null) {_Next = (AnchorElement)Document.GetElementById(clientId + "_Next");}; return _Next;}} private AnchorElement _Next;
		public jQueryObject NextJ {get {if (_NextJ == null) {_NextJ = jQuery.Select("#" + clientId + "_Next");}; return _NextJ;}} private jQueryObject _NextJ;
		public Element WebHolder {get {if (_WebHolder == null) {_WebHolder = (Element)Document.GetElementById(clientId + "_WebHolder");}; return _WebHolder;}} private Element _WebHolder;
		public jQueryObject WebHolderJ {get {if (_WebHolderJ == null) {_WebHolderJ = jQuery.Select("#" + clientId + "_WebHolder");}; return _WebHolderJ;}} private jQueryObject _WebHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement Link {get {if (_Link == null) {_Link = (AnchorElement)Document.GetElementById(clientId + "_Link");}; return _Link;}} private AnchorElement _Link;
		public jQueryObject LinkJ {get {if (_LinkJ == null) {_LinkJ = jQuery.Select("#" + clientId + "_Link");}; return _LinkJ;}} private jQueryObject _LinkJ;
		public ImageElement Web {get {if (_Web == null) {_Web = (ImageElement)Document.GetElementById(clientId + "_Web");}; return _Web;}} private ImageElement _Web;
		public jQueryObject WebJ {get {if (_WebJ == null) {_WebJ = jQuery.Select("#" + clientId + "_Web");}; return _WebJ;}} private jQueryObject _WebJ;
		public Element BottomPara {get {if (_BottomPara == null) {_BottomPara = (Element)Document.GetElementById(clientId + "_BottomPara");}; return _BottomPara;}} private Element _BottomPara;
		public jQueryObject BottomParaJ {get {if (_BottomParaJ == null) {_BottomParaJ = jQuery.Select("#" + clientId + "_BottomPara");}; return _BottomParaJ;}} private jQueryObject _BottomParaJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
