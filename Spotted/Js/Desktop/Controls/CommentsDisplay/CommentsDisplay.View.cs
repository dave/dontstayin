//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.CommentsDisplay
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public DivElement uiInitialCommentPanel {get {if (_uiInitialCommentPanel == null) {_uiInitialCommentPanel = (DivElement)Document.GetElementById(clientId + "_uiInitialCommentPanel");}; return _uiInitialCommentPanel;}} private DivElement _uiInitialCommentPanel;
		public jQueryObject uiInitialCommentPanelJ {get {if (_uiInitialCommentPanelJ == null) {_uiInitialCommentPanelJ = jQuery.Select("#" + clientId + "_uiInitialCommentPanel");}; return _uiInitialCommentPanelJ;}} private jQueryObject _uiInitialCommentPanelJ;
		public Element InitialCommentH1 {get {if (_InitialCommentH1 == null) {_InitialCommentH1 = (Element)Document.GetElementById(clientId + "_InitialCommentH1");}; return _InitialCommentH1;}} private Element _InitialCommentH1;
		public jQueryObject InitialCommentH1J {get {if (_InitialCommentH1J == null) {_InitialCommentH1J = jQuery.Select("#" + clientId + "_InitialCommentH1");}; return _InitialCommentH1J;}} private jQueryObject _InitialCommentH1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element uiInitialComment {get {if (_uiInitialComment == null) {_uiInitialComment = (Element)Document.GetElementById(clientId + "_uiInitialComment");}; return _uiInitialComment;}} private Element _uiInitialComment;
		public jQueryObject uiInitialCommentJ {get {if (_uiInitialCommentJ == null) {_uiInitialCommentJ = jQuery.Select("#" + clientId + "_uiInitialComment");}; return _uiInitialCommentJ;}} private jQueryObject _uiInitialCommentJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiInitialCommentDataList {get {if (_uiInitialCommentDataList == null) {_uiInitialCommentDataList = (Element)Document.GetElementById(clientId + "_uiInitialCommentDataList");}; return _uiInitialCommentDataList;}} private Element _uiInitialCommentDataList;
		public jQueryObject uiInitialCommentDataListJ {get {if (_uiInitialCommentDataListJ == null) {_uiInitialCommentDataListJ = jQuery.Select("#" + clientId + "_uiInitialCommentDataList");}; return _uiInitialCommentDataListJ;}} private jQueryObject _uiInitialCommentDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public AnchorElement uiCommentsAnchor {get {if (_uiCommentsAnchor == null) {_uiCommentsAnchor = (AnchorElement)Document.GetElementById(clientId + "_uiCommentsAnchor");}; return _uiCommentsAnchor;}} private AnchorElement _uiCommentsAnchor;
		public jQueryObject uiCommentsAnchorJ {get {if (_uiCommentsAnchorJ == null) {_uiCommentsAnchorJ = jQuery.Select("#" + clientId + "_uiCommentsAnchor");}; return _uiCommentsAnchorJ;}} private jQueryObject _uiCommentsAnchorJ;
		public DivElement uiCommentsPanel {get {if (_uiCommentsPanel == null) {_uiCommentsPanel = (DivElement)Document.GetElementById(clientId + "_uiCommentsPanel");}; return _uiCommentsPanel;}} private DivElement _uiCommentsPanel;
		public jQueryObject uiCommentsPanelJ {get {if (_uiCommentsPanelJ == null) {_uiCommentsPanelJ = jQuery.Select("#" + clientId + "_uiCommentsPanel");}; return _uiCommentsPanelJ;}} private jQueryObject _uiCommentsPanelJ;
		public Element CommentsSubjectH1 {get {if (_CommentsSubjectH1 == null) {_CommentsSubjectH1 = (Element)Document.GetElementById(clientId + "_CommentsSubjectH1");}; return _CommentsSubjectH1;}} private Element _CommentsSubjectH1;
		public jQueryObject CommentsSubjectH1J {get {if (_CommentsSubjectH1J == null) {_CommentsSubjectH1J = jQuery.Select("#" + clientId + "_CommentsSubjectH1");}; return _CommentsSubjectH1J;}} private jQueryObject _CommentsSubjectH1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element uiCommentsPanelClientSide {get {if (_uiCommentsPanelClientSide == null) {_uiCommentsPanelClientSide = (Element)Document.GetElementById(clientId + "_uiCommentsPanelClientSide");}; return _uiCommentsPanelClientSide;}} private Element _uiCommentsPanelClientSide;
		public jQueryObject uiCommentsPanelClientSideJ {get {if (_uiCommentsPanelClientSideJ == null) {_uiCommentsPanelClientSideJ = jQuery.Select("#" + clientId + "_uiCommentsPanelClientSide");}; return _uiCommentsPanelClientSideJ;}} private jQueryObject _uiCommentsPanelClientSideJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiCommentsPanelServerSide {get {if (_uiCommentsPanelServerSide == null) {_uiCommentsPanelServerSide = (Element)Document.GetElementById(clientId + "_uiCommentsPanelServerSide");}; return _uiCommentsPanelServerSide;}} private Element _uiCommentsPanelServerSide;
		public jQueryObject uiCommentsPanelServerSideJ {get {if (_uiCommentsPanelServerSideJ == null) {_uiCommentsPanelServerSideJ = jQuery.Select("#" + clientId + "_uiCommentsPanelServerSide");}; return _uiCommentsPanelServerSideJ;}} private jQueryObject _uiCommentsPanelServerSideJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element CommentsPageP1 {get {if (_CommentsPageP1 == null) {_CommentsPageP1 = (Element)Document.GetElementById(clientId + "_CommentsPageP1");}; return _CommentsPageP1;}} private Element _CommentsPageP1;
		public jQueryObject CommentsPageP1J {get {if (_CommentsPageP1J == null) {_CommentsPageP1J = jQuery.Select("#" + clientId + "_CommentsPageP1");}; return _CommentsPageP1J;}} private jQueryObject _CommentsPageP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement CommentsPrevPageLink1 {get {if (_CommentsPrevPageLink1 == null) {_CommentsPrevPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_CommentsPrevPageLink1");}; return _CommentsPrevPageLink1;}} private AnchorElement _CommentsPrevPageLink1;
		public jQueryObject CommentsPrevPageLink1J {get {if (_CommentsPrevPageLink1J == null) {_CommentsPrevPageLink1J = jQuery.Select("#" + clientId + "_CommentsPrevPageLink1");}; return _CommentsPrevPageLink1J;}} private jQueryObject _CommentsPrevPageLink1J;
		public AnchorElement CommentsNextPageLink1 {get {if (_CommentsNextPageLink1 == null) {_CommentsNextPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_CommentsNextPageLink1");}; return _CommentsNextPageLink1;}} private AnchorElement _CommentsNextPageLink1;
		public jQueryObject CommentsNextPageLink1J {get {if (_CommentsNextPageLink1J == null) {_CommentsNextPageLink1J = jQuery.Select("#" + clientId + "_CommentsNextPageLink1");}; return _CommentsNextPageLink1J;}} private jQueryObject _CommentsNextPageLink1J;
		public Element CommentsPagesP1 {get {if (_CommentsPagesP1 == null) {_CommentsPagesP1 = (Element)Document.GetElementById(clientId + "_CommentsPagesP1");}; return _CommentsPagesP1;}} private Element _CommentsPagesP1;
		public jQueryObject CommentsPagesP1J {get {if (_CommentsPagesP1J == null) {_CommentsPagesP1J = jQuery.Select("#" + clientId + "_CommentsPagesP1");}; return _CommentsPagesP1J;}} private jQueryObject _CommentsPagesP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element CommentsDataList {get {if (_CommentsDataList == null) {_CommentsDataList = (Element)Document.GetElementById(clientId + "_CommentsDataList");}; return _CommentsDataList;}} private Element _CommentsDataList;
		public jQueryObject CommentsDataListJ {get {if (_CommentsDataListJ == null) {_CommentsDataListJ = jQuery.Select("#" + clientId + "_CommentsDataList");}; return _CommentsDataListJ;}} private jQueryObject _CommentsDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element CommentsPagesP2 {get {if (_CommentsPagesP2 == null) {_CommentsPagesP2 = (Element)Document.GetElementById(clientId + "_CommentsPagesP2");}; return _CommentsPagesP2;}} private Element _CommentsPagesP2;
		public jQueryObject CommentsPagesP2J {get {if (_CommentsPagesP2J == null) {_CommentsPagesP2J = jQuery.Select("#" + clientId + "_CommentsPagesP2");}; return _CommentsPagesP2J;}} private jQueryObject _CommentsPagesP2J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element CommentsPageP2 {get {if (_CommentsPageP2 == null) {_CommentsPageP2 = (Element)Document.GetElementById(clientId + "_CommentsPageP2");}; return _CommentsPageP2;}} private Element _CommentsPageP2;
		public jQueryObject CommentsPageP2J {get {if (_CommentsPageP2J == null) {_CommentsPageP2J = jQuery.Select("#" + clientId + "_CommentsPageP2");}; return _CommentsPageP2J;}} private jQueryObject _CommentsPageP2J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement CommentsPrevPageLink {get {if (_CommentsPrevPageLink == null) {_CommentsPrevPageLink = (AnchorElement)Document.GetElementById(clientId + "_CommentsPrevPageLink");}; return _CommentsPrevPageLink;}} private AnchorElement _CommentsPrevPageLink;
		public jQueryObject CommentsPrevPageLinkJ {get {if (_CommentsPrevPageLinkJ == null) {_CommentsPrevPageLinkJ = jQuery.Select("#" + clientId + "_CommentsPrevPageLink");}; return _CommentsPrevPageLinkJ;}} private jQueryObject _CommentsPrevPageLinkJ;
		public AnchorElement CommentsNextPageLink {get {if (_CommentsNextPageLink == null) {_CommentsNextPageLink = (AnchorElement)Document.GetElementById(clientId + "_CommentsNextPageLink");}; return _CommentsNextPageLink;}} private AnchorElement _CommentsNextPageLink;
		public jQueryObject CommentsNextPageLinkJ {get {if (_CommentsNextPageLinkJ == null) {_CommentsNextPageLinkJ = jQuery.Select("#" + clientId + "_CommentsNextPageLink");}; return _CommentsNextPageLinkJ;}} private jQueryObject _CommentsNextPageLinkJ;
		public InputElement uiPageNumber {get {if (_uiPageNumber == null) {_uiPageNumber = (InputElement)Document.GetElementById(clientId + "_uiPageNumber");}; return _uiPageNumber;}} private InputElement _uiPageNumber;
		public jQueryObject uiPageNumberJ {get {if (_uiPageNumberJ == null) {_uiPageNumberJ = jQuery.Select("#" + clientId + "_uiPageNumber");}; return _uiPageNumberJ;}} private jQueryObject _uiPageNumberJ;
		public InputElement uiClientID {get {if (_uiClientID == null) {_uiClientID = (InputElement)Document.GetElementById(clientId + "_uiClientID");}; return _uiClientID;}} private InputElement _uiClientID;
		public jQueryObject uiClientIDJ {get {if (_uiClientIDJ == null) {_uiClientIDJ = jQuery.Select("#" + clientId + "_uiClientID");}; return _uiClientIDJ;}} private jQueryObject _uiClientIDJ;
		public InputElement uiCommentsPerPage {get {if (_uiCommentsPerPage == null) {_uiCommentsPerPage = (InputElement)Document.GetElementById(clientId + "_uiCommentsPerPage");}; return _uiCommentsPerPage;}} private InputElement _uiCommentsPerPage;
		public jQueryObject uiCommentsPerPageJ {get {if (_uiCommentsPerPageJ == null) {_uiCommentsPerPageJ = jQuery.Select("#" + clientId + "_uiCommentsPerPage");}; return _uiCommentsPerPageJ;}} private jQueryObject _uiCommentsPerPageJ;
		public InputElement uiUsrIsLoggedIn {get {if (_uiUsrIsLoggedIn == null) {_uiUsrIsLoggedIn = (InputElement)Document.GetElementById(clientId + "_uiUsrIsLoggedIn");}; return _uiUsrIsLoggedIn;}} private InputElement _uiUsrIsLoggedIn;
		public jQueryObject uiUsrIsLoggedInJ {get {if (_uiUsrIsLoggedInJ == null) {_uiUsrIsLoggedInJ = jQuery.Select("#" + clientId + "_uiUsrIsLoggedIn");}; return _uiUsrIsLoggedInJ;}} private jQueryObject _uiUsrIsLoggedInJ;
	}
}
