//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.LatestEventList", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.LatestChatHotTopics", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.LatestContent", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.Latest
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
		public AnchorElement ChatHeader {get {if (_ChatHeader == null) {_ChatHeader = (AnchorElement)Document.GetElementById(clientId + "_ChatHeader");}; return _ChatHeader;}} private AnchorElement _ChatHeader;
		public jQueryObject ChatHeaderJ {get {if (_ChatHeaderJ == null) {_ChatHeaderJ = jQuery.Select("#" + clientId + "_ChatHeader");}; return _ChatHeaderJ;}} private jQueryObject _ChatHeaderJ;
		public AnchorElement HotTopicsHeader {get {if (_HotTopicsHeader == null) {_HotTopicsHeader = (AnchorElement)Document.GetElementById(clientId + "_HotTopicsHeader");}; return _HotTopicsHeader;}} private AnchorElement _HotTopicsHeader;
		public jQueryObject HotTopicsHeaderJ {get {if (_HotTopicsHeaderJ == null) {_HotTopicsHeaderJ = jQuery.Select("#" + clientId + "_HotTopicsHeader");}; return _HotTopicsHeaderJ;}} private jQueryObject _HotTopicsHeaderJ;
		public Element ChatHeaderSpan {get {if (_ChatHeaderSpan == null) {_ChatHeaderSpan = (Element)Document.GetElementById(clientId + "_ChatHeaderSpan");}; return _ChatHeaderSpan;}} private Element _ChatHeaderSpan;
		public jQueryObject ChatHeaderSpanJ {get {if (_ChatHeaderSpanJ == null) {_ChatHeaderSpanJ = jQuery.Select("#" + clientId + "_ChatHeaderSpan");}; return _ChatHeaderSpanJ;}} private jQueryObject _ChatHeaderSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement ChatHolder {get {if (_ChatHolder == null) {_ChatHolder = (DivElement)Document.GetElementById(clientId + "_ChatHolder");}; return _ChatHolder;}} private DivElement _ChatHolder;
		public jQueryObject ChatHolderJ {get {if (_ChatHolderJ == null) {_ChatHolderJ = jQuery.Select("#" + clientId + "_ChatHolder");}; return _ChatHolderJ;}} private jQueryObject _ChatHolderJ;
		public DivElement AddThreadLinkPanel {get {if (_AddThreadLinkPanel == null) {_AddThreadLinkPanel = (DivElement)Document.GetElementById(clientId + "_AddThreadLinkPanel");}; return _AddThreadLinkPanel;}} private DivElement _AddThreadLinkPanel;
		public jQueryObject AddThreadLinkPanelJ {get {if (_AddThreadLinkPanelJ == null) {_AddThreadLinkPanelJ = jQuery.Select("#" + clientId + "_AddThreadLinkPanel");}; return _AddThreadLinkPanelJ;}} private jQueryObject _AddThreadLinkPanelJ;
		public Element InlineScript1 {get {if (_InlineScript1 == null) {_InlineScript1 = (Element)Document.GetElementById(clientId + "_InlineScript1");}; return _InlineScript1;}} private Element _InlineScript1;
		public jQueryObject InlineScript1J {get {if (_InlineScript1J == null) {_InlineScript1J = jQuery.Select("#" + clientId + "_InlineScript1");}; return _InlineScript1J;}} private jQueryObject _InlineScript1J;//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
		public InputElement AddThreadStatusHidden {get {if (_AddThreadStatusHidden == null) {_AddThreadStatusHidden = (InputElement)Document.GetElementById(clientId + "_AddThreadStatusHidden");}; return _AddThreadStatusHidden;}} private InputElement _AddThreadStatusHidden;
		public jQueryObject AddThreadStatusHiddenJ {get {if (_AddThreadStatusHiddenJ == null) {_AddThreadStatusHiddenJ = jQuery.Select("#" + clientId + "_AddThreadStatusHidden");}; return _AddThreadStatusHiddenJ;}} private jQueryObject _AddThreadStatusHiddenJ;
		public Element AddThreadLinkP {get {if (_AddThreadLinkP == null) {_AddThreadLinkP = (Element)Document.GetElementById(clientId + "_AddThreadLinkP");}; return _AddThreadLinkP;}} private Element _AddThreadLinkP;
		public jQueryObject AddThreadLinkPJ {get {if (_AddThreadLinkPJ == null) {_AddThreadLinkPJ = jQuery.Select("#" + clientId + "_AddThreadLinkP");}; return _AddThreadLinkPJ;}} private jQueryObject _AddThreadLinkPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement AddThreadPanel {get {if (_AddThreadPanel == null) {_AddThreadPanel = (DivElement)Document.GetElementById(clientId + "_AddThreadPanel");}; return _AddThreadPanel;}} private DivElement _AddThreadPanel;
		public jQueryObject AddThreadPanelJ {get {if (_AddThreadPanelJ == null) {_AddThreadPanelJ = jQuery.Select("#" + clientId + "_AddThreadPanel");}; return _AddThreadPanelJ;}} private jQueryObject _AddThreadPanelJ;
		public Js.Controls.AddThread.Controller AddThread {get {return (Js.Controls.AddThread.Controller) Script.Eval(clientId + "_AddThreadController");}}
		public Element LatestChatUcHolder {get {if (_LatestChatUcHolder == null) {_LatestChatUcHolder = (Element)Document.GetElementById(clientId + "_LatestChatUcHolder");}; return _LatestChatUcHolder;}} private Element _LatestChatUcHolder;
		public jQueryObject LatestChatUcHolderJ {get {if (_LatestChatUcHolderJ == null) {_LatestChatUcHolderJ = jQuery.Select("#" + clientId + "_LatestChatUcHolder");}; return _LatestChatUcHolderJ;}} private jQueryObject _LatestChatUcHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Js.Controls.LatestChat.Controller LatestChatUc {get {return (Js.Controls.LatestChat.Controller) Script.Eval(clientId + "_LatestChatUcController");}}
		public Element LatestHotTopicsUcHolder {get {if (_LatestHotTopicsUcHolder == null) {_LatestHotTopicsUcHolder = (Element)Document.GetElementById(clientId + "_LatestHotTopicsUcHolder");}; return _LatestHotTopicsUcHolder;}} private Element _LatestHotTopicsUcHolder;
		public jQueryObject LatestHotTopicsUcHolderJ {get {if (_LatestHotTopicsUcHolderJ == null) {_LatestHotTopicsUcHolderJ = jQuery.Select("#" + clientId + "_LatestHotTopicsUcHolder");}; return _LatestHotTopicsUcHolderJ;}} private jQueryObject _LatestHotTopicsUcHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element TabClientScript {get {if (_TabClientScript == null) {_TabClientScript = (Element)Document.GetElementById(clientId + "_TabClientScript");}; return _TabClientScript;}} private Element _TabClientScript;
		public jQueryObject TabClientScriptJ {get {if (_TabClientScriptJ == null) {_TabClientScriptJ = jQuery.Select("#" + clientId + "_TabClientScript");}; return _TabClientScriptJ;}} private jQueryObject _TabClientScriptJ;//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
		public Element LatestEventList1 {get {if (_LatestEventList1 == null) {_LatestEventList1 = (Element)Document.GetElementById(clientId + "_LatestEventList1");}; return _LatestEventList1;}} private Element _LatestEventList1;
		public jQueryObject LatestEventList1J {get {if (_LatestEventList1J == null) {_LatestEventList1J = jQuery.Select("#" + clientId + "_LatestEventList1");}; return _LatestEventList1J;}} private jQueryObject _LatestEventList1J;//mappings.Add("Spotted.Controls.LatestEventList", ElementGetter("Element"));
		public Element LatestHotTopicsUc {get {if (_LatestHotTopicsUc == null) {_LatestHotTopicsUc = (Element)Document.GetElementById(clientId + "_LatestHotTopicsUc");}; return _LatestHotTopicsUc;}} private Element _LatestHotTopicsUc;
		public jQueryObject LatestHotTopicsUcJ {get {if (_LatestHotTopicsUcJ == null) {_LatestHotTopicsUcJ = jQuery.Select("#" + clientId + "_LatestHotTopicsUc");}; return _LatestHotTopicsUcJ;}} private jQueryObject _LatestHotTopicsUcJ;//mappings.Add("Spotted.Controls.LatestChatHotTopics", ElementGetter("Element"));
		public Js.Controls.EventBox.Controller EventBox {get {return (Js.Controls.EventBox.Controller) Script.Eval(clientId + "_EventBoxController");}}
		public DivElement ChatHolderOuter {get {if (_ChatHolderOuter == null) {_ChatHolderOuter = (DivElement)Document.GetElementById(clientId + "_ChatHolderOuter");}; return _ChatHolderOuter;}} private DivElement _ChatHolderOuter;
		public jQueryObject ChatHolderOuterJ {get {if (_ChatHolderOuterJ == null) {_ChatHolderOuterJ = jQuery.Select("#" + clientId + "_ChatHolderOuter");}; return _ChatHolderOuterJ;}} private jQueryObject _ChatHolderOuterJ;
		public Element LatestContentUc {get {if (_LatestContentUc == null) {_LatestContentUc = (Element)Document.GetElementById(clientId + "_LatestContentUc");}; return _LatestContentUc;}} private Element _LatestContentUc;
		public jQueryObject LatestContentUcJ {get {if (_LatestContentUcJ == null) {_LatestContentUcJ = jQuery.Select("#" + clientId + "_LatestContentUc");}; return _LatestContentUcJ;}} private jQueryObject _LatestContentUcJ;//mappings.Add("Spotted.Controls.LatestContent", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
