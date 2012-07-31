<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewLatest.ascx.cs" Inherits="Spotted.Templates.Threads.ReviewLatest" %>
<div class="LatestDiv">
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
				<a href="<%#CurrentThread.UrlDiscussion()%>"><img src="<%#CurrentThread.Event.AnyPicPath%>" align="right" border="0" class="LatestPic" hspace="0" width="50" height="50"></a>
			</td>
			<td valign="top">
				<a href="" runat="server" id="ThreadAnchor"></a>
				<small>
					<asp:Label Runat="server" ID="EventLabel"></asp:Label> 
					by <a runat="server" id="AuthorAnchor"/> - 
					<asp:label Runat="server" ID="TotalCommentsLabel"/> comment<asp:label Runat="server" ID="CommentPluralLabel"/>
				</small>
			</td>
		</tr>
	</table>
</div>
