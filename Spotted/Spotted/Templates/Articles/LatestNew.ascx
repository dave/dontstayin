<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestNew.ascx.cs" Inherits="Spotted.Templates.Articles.LatestNew" %>

<dsi:h1 runat="server">
	<%#CurrentArticle.Title%>
</dsi:h1>
<div class="ContentBorder ClearAfter">
	<asp:PlaceHolder runat="server" ID="ParaPh" />
	<div runat="server" id="SummaryDisplayDiv" visible="false">
		<p class="ClearAfter">
			<a href="<%#CurrentArticle.Url()%>"><img src="<%#CurrentArticle.PicPath%>" style="margin-right:10px;" align="left" border="0" class="LatestPic" hspace="0" width="100" height="100"></a>
			<%#CurrentArticle.Summary%>
		</p>
	</div>
	<p runat="server" id="MixmagP" style="margin-top:15px;">
		<center>This is a <a href="/pages/mixmag"><img src="/gfx/logo-mixmag-small.png" border="0" align="absmiddle" width="100" height="22"></a> article.</center>
	</p>
	<div class="p ClearAfter">
		<div style="width:420px;float:left;" runat="server" id="NormalDetailsDiv">
			<small>
				<a href="<%#CurrentArticle.Url()%>"><img src="/gfx/icon-news.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Read more</a>
				- by <a runat="server" id="AuthorAnchor"/>
				- <a href="<%#CurrentArticle.UrlDiscussion()%>"><%#CurrentArticle.TotalComments.ToString("#,##0")%> comment<%#CurrentArticle.TotalComments == 1 ? "" : "s"%></a>.
				<a href="<%#CurrentArticle.UrlEdit()%>" runat="server" visible="<%# Usr.Current != null && Usr.Current.IsAdmin %>">edit</a>
			</small>
		</div>
		<div style="width:420px;float:left;" runat="server" id="ChatDetailsDiv">
			<small>
				<a href="<%#CurrentArticle.Url()%>"><img src="/gfx/icon-news.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Read more</a>
				- by <a runat="server" id="AuthorAnchor1" />
			</small>
		</div>
		<div style="float:left;width:175px;" align="right" runat="server" id="ArticlesArchiveDiv">
			<small>
				<a href="" runat="server" id="ArticlesArchiveAnchor">Articles archive<img src="/gfx/icon-calendar.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21"></a>
			</small>
		</div>
		<div style="float:left;width:175px;" align="right" runat="server" id="MixmagArchiveDiv">
			<small>
				<a href="" runat="server" id="MixmagArchiveAnchor">Mixmag archive</a><img src="/gfx/icon-calendar.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21"></a>
			</small>
		</div>
	</div>
	
</div>
