<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="Spotted.Templates.Comments.Default" %>
<a name="CommentK-<%#CurrentComment.K%>"></a>
<div class="CommentOuter ClearAfter">
	<div class="CommentLeft">
		<a href="<%#CurrentComment.Usr.Url()%>" <%#CurrentComment.Usr.RolloverNoPic%>><img src="" runat="server" id="PicImg" border="0" width="100" height="100" style="margin-bottom:2px;margin-top:0px;" class="BorderBlack All Block"></a>
		<a href="<%#CurrentComment.Usr.Url()%>"><%#CurrentComment.Usr.NickName%></a>
	</div>
	<div class="CommentBody">
		<%#CurrentComment.NewHtml%><%#CurrentComment.GetHtml(this)%>
		<div class="CommentAdmin">
			<small>
				<span class="CleanLinks"><asp:PlaceHolder Runat="server" id="LolHtmlPh"></asp:PlaceHolder></span>
				<span runat="server" id="LolDownLevelSpan"><asp:LinkButton ID="LinkButton1" Runat="server" OnClick="LolClick" CausesValidation="False">This made me laugh!</asp:LinkButton><br /></span>
				<a href="#PostComment">Reply</a> <a href="" onmousedown="QuoteNow(<%#CurrentComment.Usr.K.ToString()%>);return false;" onclick="FocusNow();return false;">Quote</a><span runat="server" id="CommentEditSpan"> <a href="" runat="server" id="CommentEditAnchor">Edit</a></span><span runat="server" id="DeleteButtonSpan"> <asp:LinkButton Runat="server" ID="DeleteButton" CausesValidation="False" OnCommand="DeleteCommand" CommandName="Delete" CommandArgument="<%#CurrentComment.K%>">Delete</asp:LinkButton></span><br />
				<span onmouseover="stt('<%#CurrentComment.K.ToString("#,##0")%>');" onmouseout="htm();">Posted <%#CurrentComment.FriendlyTimeNoCaps%></span><%#CurrentComment.EditedHtml%>
			</small>
		</div>
	</div>
</div>
<a name="AfterCommentK-<%#CurrentComment.K%>"></a>
