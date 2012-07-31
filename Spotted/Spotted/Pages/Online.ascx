<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Online.ascx.cs" Inherits="Spotted.Pages.Online" %>
<dsi:h1 runat="server">
	Who's online?
</dsi:h1>
<div class="ContentBorder">
	<p>
		<asp:Label Runat="server" ID="OnlineLabel"></asp:Label>
	</p>
	<p class="CleanLinks" runat="server" id="OnlineP">
		<asp:DataList Runat="server" RepeatLayout="Flow" ID="OnlineDataList" RepeatDirection="Horizontal">
			<SeparatorTemplate><br /></SeparatorTemplate>
		</asp:DataList>
	</p>
</div>
