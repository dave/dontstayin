<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsLatest.ascx.cs" Inherits="Spotted.Templates.Threads.NewsLatest" %>
<div class="LatestDiv">
	<a name="ThreadK-<%#CurrentThread.K%>"></a>
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
				<a href="<%#CurrentThread.Url()%>"><img src="<%#CurrentThread.IconPath%>" align="right" border="0" class="LatestPic" hspace="0" width="50" height="50"></a>
			</td>
			<td valign="top">
				<a href="" runat="server" id="ThreadAnchor"><asp:Label Runat="server" ID="SubjectLabel"/></a><br>
				<small>
					By <a runat="server" id="AuthorAnchor"/> - 
					<asp:label Runat="server" ID="PostTimeLabel"/> - 
					<asp:label Runat="server" ID="TotalCommentsLabel"/> comment<asp:label Runat="server" ID="CommentPluralLabel"/>
				</small>
			</td>
		</tr>
	</table>
</div>
