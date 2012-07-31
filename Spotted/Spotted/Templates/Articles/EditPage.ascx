<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPage.ascx.cs" Inherits="Spotted.Templates.Articles.EditPage" %>

<a name="ArticlePage<%#CurrentPage%>"></a>
<dsi:h1 runat="server" ID="H14"><asp:Label Runat="server" ID="TitleLabel"/></dsi:h1>
<div class="ContentBorder">
	<asp:Repeater Runat="server" ID="ParaRepeater"/>
</div>
