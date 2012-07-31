<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentsDisplay.ascx.cs" Inherits="Spotted.Controls.CommentsDisplay" %>

<asp:Panel Runat="server" ID="uiInitialCommentPanel">
	<dsi:h1 runat="server" id="InitialCommentH1">First comment</dsi:h1>
	<div class="ContentBorder">
		<p align="center"><a href="#Comments2">Skip to replies</a></p>
		<div runat="server" id="uiInitialComment" style="display:none"></div>
		<asp:DataList Runat="server" ID="uiInitialCommentDataList" RepeatDirection="Horizontal" RepeatLayout="Flow" />
	</div>
</asp:Panel>

<a name="Comments" runat="server" id="uiCommentsAnchor"></a>
<asp:Panel Runat="server" id="uiCommentsPanel" EnableViewState="False">
	<dsi:h1 runat="server" ID="CommentsSubjectH1">Comments</dsi:h1>
	<div class="ContentBorder">
	
		<div runat="server" id="uiCommentsPanelClientSide" style="display:none"></div>
		
		<div runat="server" id="uiCommentsPanelServerSide">
			<p runat="server" id="CommentsPageP1" align="right">
				<asp:HyperLink runat="server" id="CommentsPrevPageLink1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... <asp:HyperLink runat="server" id="CommentsNextPageLink1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
			<p runat="server" align="right" id="CommentsPagesP1" style="padding-right:15px;"/>
			<asp:DataList Runat="server" ID="CommentsDataList"  RepeatDirection="Horizontal" RepeatLayout="Flow" />
			<p runat="server" align="right" id="CommentsPagesP2" style="padding-right:15px;"/>
			<p runat="server" id="CommentsPageP2" align="right">
				<asp:HyperLink runat="server" id="CommentsPrevPageLink"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... <asp:HyperLink runat="server" id="CommentsNextPageLink">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
		</div>
		
	</div>
</asp:Panel>

<input runat="server" id="uiPageNumber" type="hidden" />
<input runat="server" id="uiClientID" type="hidden" />
<input runat="server" id="uiCommentsPerPage" type="hidden" />
<input runat="server" id="uiUsrIsLoggedIn" type="hidden" />
