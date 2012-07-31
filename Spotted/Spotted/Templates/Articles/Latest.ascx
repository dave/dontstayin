<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Latest.ascx.cs" Inherits="Spotted.Templates.Articles.Latest" %>
<div class="LatestDiv">
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
				<a href="<%#CurrentArticle.Url()%>"><img src="<%#CurrentArticle.PicPath%>" align="right" border="0" class="LatestPic" hspace="0" width="50" height="50"></a>
			</td>
			<td valign="top">
				<a href="" runat="server" id="ArticleAnchor"><asp:Label Runat="server" ID="TitleLabel"/></a><br>
				
				<small>
					<%#CurrentArticle.Summary%><br>
					By <a runat="server" id="AuthorAnchor"/> - 
					<a href="<%#CurrentArticle.UrlDiscussion()%>"><asp:label Runat="server" ID="TotalCommentsLabel"/> comment<asp:label Runat="server" ID="CommentPluralLabel"/></a> - 
					<%#CurrentArticle.Views%> view<%#CurrentArticle.Views==1?"":"s"%>
				</small>
			</td>
		</tr>
	</table>
</div>
