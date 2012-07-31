<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotTopicLatest.ascx.cs" Inherits="Spotted.Templates.Threads.HotTopicLatest" %>
<div class="LatestDiv">
	<a name="LatestThreadK-<%#CurrentThread.K%>"></a>
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
				<a href="<%#CurrentThread.UrlDiscussion()%>"><img src="<%#CurrentThread.IconPath%>" align="right" border="0" class="LatestPic" hspace="0" width="50" height="50"></a>
			</td>
			<td valign="top">
				<a href="" runat="server" id="ThreadAnchor"><asp:Label Runat="server" ID="SubjectLabel"/></a><br>
				<small>
					By <a runat="server" id="AuthorAnchor"/> - 
					<asp:label Runat="server" ID="TotalCommentsLabel"/> comment<asp:label Runat="server" ID="CommentPluralLabel"/> - 
					last post: <asp:label Runat="server" ID="LastPostLabel"/><br>			
				</small>
			</td>
		</tr>
	</table>
</div>
