//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.MultiUsrDrop
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public SelectElement Drop {get {if (_Drop == null) {_Drop = (SelectElement)Document.GetElementById(clientId + "_Drop");}; return _Drop;}} private SelectElement _Drop;
		public jQueryObject DropJ {get {if (_DropJ == null) {_DropJ = jQuery.Select("#" + clientId + "_Drop");}; return _DropJ;}} private jQueryObject _DropJ;
		public SelectElement SelectBox {get {if (_SelectBox == null) {_SelectBox = (SelectElement)Document.GetElementById(clientId + "_SelectBox");}; return _SelectBox;}} private SelectElement _SelectBox;
		public jQueryObject SelectBoxJ {get {if (_SelectBoxJ == null) {_SelectBoxJ = jQuery.Select("#" + clientId + "_SelectBox");}; return _SelectBoxJ;}} private jQueryObject _SelectBoxJ;
		public InputElement AddButton {get {if (_AddButton == null) {_AddButton = (InputElement)Document.GetElementById(clientId + "_AddButton");}; return _AddButton;}} private InputElement _AddButton;
		public jQueryObject AddButtonJ {get {if (_AddButtonJ == null) {_AddButtonJ = jQuery.Select("#" + clientId + "_AddButton");}; return _AddButtonJ;}} private jQueryObject _AddButtonJ;
		public InputElement RemoveButton {get {if (_RemoveButton == null) {_RemoveButton = (InputElement)Document.GetElementById(clientId + "_RemoveButton");}; return _RemoveButton;}} private InputElement _RemoveButton;
		public jQueryObject RemoveButtonJ {get {if (_RemoveButtonJ == null) {_RemoveButtonJ = jQuery.Select("#" + clientId + "_RemoveButton");}; return _RemoveButtonJ;}} private jQueryObject _RemoveButtonJ;
		public Element BuddiesTable {get {if (_BuddiesTable == null) {_BuddiesTable = (Element)Document.GetElementById(clientId + "_BuddiesTable");}; return _BuddiesTable;}} private Element _BuddiesTable;
		public jQueryObject BuddiesTableJ {get {if (_BuddiesTableJ == null) {_BuddiesTableJ = jQuery.Select("#" + clientId + "_BuddiesTable");}; return _BuddiesTableJ;}} private jQueryObject _BuddiesTableJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public Element Tr1 {get {if (_Tr1 == null) {_Tr1 = (Element)Document.GetElementById(clientId + "_Tr1");}; return _Tr1;}} private Element _Tr1;
		public jQueryObject Tr1J {get {if (_Tr1J == null) {_Tr1J = jQuery.Select("#" + clientId + "_Tr1");}; return _Tr1J;}} private jQueryObject _Tr1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element Td1 {get {if (_Td1 == null) {_Td1 = (Element)Document.GetElementById(clientId + "_Td1");}; return _Td1;}} private Element _Td1;
		public jQueryObject Td1J {get {if (_Td1J == null) {_Td1J = jQuery.Select("#" + clientId + "_Td1");}; return _Td1J;}} private jQueryObject _Td1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element ClipSpan {get {if (_ClipSpan == null) {_ClipSpan = (Element)Document.GetElementById(clientId + "_ClipSpan");}; return _ClipSpan;}} private Element _ClipSpan;
		public jQueryObject ClipSpanJ {get {if (_ClipSpanJ == null) {_ClipSpanJ = jQuery.Select("#" + clientId + "_ClipSpan");}; return _ClipSpanJ;}} private jQueryObject _ClipSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element Td2 {get {if (_Td2 == null) {_Td2 = (Element)Document.GetElementById(clientId + "_Td2");}; return _Td2;}} private Element _Td2;
		public jQueryObject Td2J {get {if (_Td2J == null) {_Td2J = jQuery.Select("#" + clientId + "_Td2");}; return _Td2J;}} private jQueryObject _Td2J;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element PicHolder {get {if (_PicHolder == null) {_PicHolder = (Element)Document.GetElementById(clientId + "_PicHolder");}; return _PicHolder;}} private Element _PicHolder;
		public jQueryObject PicHolderJ {get {if (_PicHolderJ == null) {_PicHolderJ = jQuery.Select("#" + clientId + "_PicHolder");}; return _PicHolderJ;}} private jQueryObject _PicHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement MoreButton {get {if (_MoreButton == null) {_MoreButton = (InputElement)Document.GetElementById(clientId + "_MoreButton");}; return _MoreButton;}} private InputElement _MoreButton;
		public jQueryObject MoreButtonJ {get {if (_MoreButtonJ == null) {_MoreButtonJ = jQuery.Select("#" + clientId + "_MoreButton");}; return _MoreButtonJ;}} private jQueryObject _MoreButtonJ;
		public DivElement BuddiesRemovedPanel {get {if (_BuddiesRemovedPanel == null) {_BuddiesRemovedPanel = (DivElement)Document.GetElementById(clientId + "_BuddiesRemovedPanel");}; return _BuddiesRemovedPanel;}} private DivElement _BuddiesRemovedPanel;
		public jQueryObject BuddiesRemovedPanelJ {get {if (_BuddiesRemovedPanelJ == null) {_BuddiesRemovedPanelJ = jQuery.Select("#" + clientId + "_BuddiesRemovedPanel");}; return _BuddiesRemovedPanelJ;}} private jQueryObject _BuddiesRemovedPanelJ;
		public Element BuddiesRemovedLabel {get {if (_BuddiesRemovedLabel == null) {_BuddiesRemovedLabel = (Element)Document.GetElementById(clientId + "_BuddiesRemovedLabel");}; return _BuddiesRemovedLabel;}} private Element _BuddiesRemovedLabel;
		public jQueryObject BuddiesRemovedLabelJ {get {if (_BuddiesRemovedLabelJ == null) {_BuddiesRemovedLabelJ = jQuery.Select("#" + clientId + "_BuddiesRemovedLabel");}; return _BuddiesRemovedLabelJ;}} private jQueryObject _BuddiesRemovedLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement BuddiesRemovedGroupPanel {get {if (_BuddiesRemovedGroupPanel == null) {_BuddiesRemovedGroupPanel = (DivElement)Document.GetElementById(clientId + "_BuddiesRemovedGroupPanel");}; return _BuddiesRemovedGroupPanel;}} private DivElement _BuddiesRemovedGroupPanel;
		public jQueryObject BuddiesRemovedGroupPanelJ {get {if (_BuddiesRemovedGroupPanelJ == null) {_BuddiesRemovedGroupPanelJ = jQuery.Select("#" + clientId + "_BuddiesRemovedGroupPanel");}; return _BuddiesRemovedGroupPanelJ;}} private jQueryObject _BuddiesRemovedGroupPanelJ;
		public Element BuddiesRemovedGroupLabel {get {if (_BuddiesRemovedGroupLabel == null) {_BuddiesRemovedGroupLabel = (Element)Document.GetElementById(clientId + "_BuddiesRemovedGroupLabel");}; return _BuddiesRemovedGroupLabel;}} private Element _BuddiesRemovedGroupLabel;
		public jQueryObject BuddiesRemovedGroupLabelJ {get {if (_BuddiesRemovedGroupLabelJ == null) {_BuddiesRemovedGroupLabelJ = jQuery.Select("#" + clientId + "_BuddiesRemovedGroupLabel");}; return _BuddiesRemovedGroupLabelJ;}} private jQueryObject _BuddiesRemovedGroupLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement NoBuddiesPanel {get {if (_NoBuddiesPanel == null) {_NoBuddiesPanel = (DivElement)Document.GetElementById(clientId + "_NoBuddiesPanel");}; return _NoBuddiesPanel;}} private DivElement _NoBuddiesPanel;
		public jQueryObject NoBuddiesPanelJ {get {if (_NoBuddiesPanelJ == null) {_NoBuddiesPanelJ = jQuery.Select("#" + clientId + "_NoBuddiesPanel");}; return _NoBuddiesPanelJ;}} private jQueryObject _NoBuddiesPanelJ;
		public DivElement NoBuddiesThreadPanel {get {if (_NoBuddiesThreadPanel == null) {_NoBuddiesThreadPanel = (DivElement)Document.GetElementById(clientId + "_NoBuddiesThreadPanel");}; return _NoBuddiesThreadPanel;}} private DivElement _NoBuddiesThreadPanel;
		public jQueryObject NoBuddiesThreadPanelJ {get {if (_NoBuddiesThreadPanelJ == null) {_NoBuddiesThreadPanelJ = jQuery.Select("#" + clientId + "_NoBuddiesThreadPanel");}; return _NoBuddiesThreadPanelJ;}} private jQueryObject _NoBuddiesThreadPanelJ;
		public DivElement AddAllPanel {get {if (_AddAllPanel == null) {_AddAllPanel = (DivElement)Document.GetElementById(clientId + "_AddAllPanel");}; return _AddAllPanel;}} private DivElement _AddAllPanel;
		public jQueryObject AddAllPanelJ {get {if (_AddAllPanelJ == null) {_AddAllPanelJ = jQuery.Select("#" + clientId + "_AddAllPanel");}; return _AddAllPanelJ;}} private jQueryObject _AddAllPanelJ;
		public SelectElement AddAllPlaceDrop {get {if (_AddAllPlaceDrop == null) {_AddAllPlaceDrop = (SelectElement)Document.GetElementById(clientId + "_AddAllPlaceDrop");}; return _AddAllPlaceDrop;}} private SelectElement _AddAllPlaceDrop;
		public jQueryObject AddAllPlaceDropJ {get {if (_AddAllPlaceDropJ == null) {_AddAllPlaceDropJ = jQuery.Select("#" + clientId + "_AddAllPlaceDrop");}; return _AddAllPlaceDropJ;}} private jQueryObject _AddAllPlaceDropJ;
		public SelectElement AddAllMusicDrop {get {if (_AddAllMusicDrop == null) {_AddAllMusicDrop = (SelectElement)Document.GetElementById(clientId + "_AddAllMusicDrop");}; return _AddAllMusicDrop;}} private SelectElement _AddAllMusicDrop;
		public jQueryObject AddAllMusicDropJ {get {if (_AddAllMusicDropJ == null) {_AddAllMusicDropJ = jQuery.Select("#" + clientId + "_AddAllMusicDrop");}; return _AddAllMusicDropJ;}} private jQueryObject _AddAllMusicDropJ;
		public InputElement Button1 {get {if (_Button1 == null) {_Button1 = (InputElement)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private InputElement _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;
		public CheckBoxElement AddAllShowAllItemsCheck {get {if (_AddAllShowAllItemsCheck == null) {_AddAllShowAllItemsCheck = (CheckBoxElement)Document.GetElementById(clientId + "_AddAllShowAllItemsCheck");}; return _AddAllShowAllItemsCheck;}} private CheckBoxElement _AddAllShowAllItemsCheck;
		public jQueryObject AddAllShowAllItemsCheckJ {get {if (_AddAllShowAllItemsCheckJ == null) {_AddAllShowAllItemsCheckJ = jQuery.Select("#" + clientId + "_AddAllShowAllItemsCheck");}; return _AddAllShowAllItemsCheckJ;}} private jQueryObject _AddAllShowAllItemsCheckJ;
		public DivElement AddMorePanel {get {if (_AddMorePanel == null) {_AddMorePanel = (DivElement)Document.GetElementById(clientId + "_AddMorePanel");}; return _AddMorePanel;}} private DivElement _AddMorePanel;
		public jQueryObject AddMorePanelJ {get {if (_AddMorePanelJ == null) {_AddMorePanelJ = jQuery.Select("#" + clientId + "_AddMorePanel");}; return _AddMorePanelJ;}} private jQueryObject _AddMorePanelJ;
		public Element AddMoreP {get {if (_AddMoreP == null) {_AddMoreP = (Element)Document.GetElementById(clientId + "_AddMoreP");}; return _AddMoreP;}} private Element _AddMoreP;
		public jQueryObject AddMorePJ {get {if (_AddMorePJ == null) {_AddMorePJ = jQuery.Select("#" + clientId + "_AddMoreP");}; return _AddMorePJ;}} private jQueryObject _AddMorePJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement AddMoreTextBox {get {if (_AddMoreTextBox == null) {_AddMoreTextBox = (InputElement)Document.GetElementById(clientId + "_AddMoreTextBox");}; return _AddMoreTextBox;}} private InputElement _AddMoreTextBox;
		public jQueryObject AddMoreTextBoxJ {get {if (_AddMoreTextBoxJ == null) {_AddMoreTextBoxJ = jQuery.Select("#" + clientId + "_AddMoreTextBox");}; return _AddMoreTextBoxJ;}} private jQueryObject _AddMoreTextBoxJ;
		public Element AddMoreP1 {get {if (_AddMoreP1 == null) {_AddMoreP1 = (Element)Document.GetElementById(clientId + "_AddMoreP1");}; return _AddMoreP1;}} private Element _AddMoreP1;
		public jQueryObject AddMoreP1J {get {if (_AddMoreP1J == null) {_AddMoreP1J = jQuery.Select("#" + clientId + "_AddMoreP1");}; return _AddMoreP1J;}} private jQueryObject _AddMoreP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement AddMoreButton {get {if (_AddMoreButton == null) {_AddMoreButton = (InputElement)Document.GetElementById(clientId + "_AddMoreButton");}; return _AddMoreButton;}} private InputElement _AddMoreButton;
		public jQueryObject AddMoreButtonJ {get {if (_AddMoreButtonJ == null) {_AddMoreButtonJ = jQuery.Select("#" + clientId + "_AddMoreButton");}; return _AddMoreButtonJ;}} private jQueryObject _AddMoreButtonJ;
		public CheckBoxElement AddMoreAddBuddyCheckBox {get {if (_AddMoreAddBuddyCheckBox == null) {_AddMoreAddBuddyCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_AddMoreAddBuddyCheckBox");}; return _AddMoreAddBuddyCheckBox;}} private CheckBoxElement _AddMoreAddBuddyCheckBox;
		public jQueryObject AddMoreAddBuddyCheckBoxJ {get {if (_AddMoreAddBuddyCheckBoxJ == null) {_AddMoreAddBuddyCheckBoxJ = jQuery.Select("#" + clientId + "_AddMoreAddBuddyCheckBox");}; return _AddMoreAddBuddyCheckBoxJ;}} private jQueryObject _AddMoreAddBuddyCheckBoxJ;
		public InputElement Values {get {if (_Values == null) {_Values = (InputElement)Document.GetElementById(clientId + "_Values");}; return _Values;}} private InputElement _Values;
		public jQueryObject ValuesJ {get {if (_ValuesJ == null) {_ValuesJ = jQuery.Select("#" + clientId + "_Values");}; return _ValuesJ;}} private jQueryObject _ValuesJ;
		public InputElement Texts {get {if (_Texts == null) {_Texts = (InputElement)Document.GetElementById(clientId + "_Texts");}; return _Texts;}} private InputElement _Texts;
		public jQueryObject TextsJ {get {if (_TextsJ == null) {_TextsJ = jQuery.Select("#" + clientId + "_Texts");}; return _TextsJ;}} private jQueryObject _TextsJ;
	}
}
