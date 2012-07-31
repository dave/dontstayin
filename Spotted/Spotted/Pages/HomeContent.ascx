<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeContent.ascx.cs" Inherits="Spotted.Pages.HomeContent" %>
<!--%@ OutputCache Duration="600" VaryByCustom="Country;MusicPref" VaryByParam="None" %-->
<%@ Register TagPrefix="Controls" TagName="TopPhotos" Src="/Controls/TopPhotos.ascx" %>

<!-- Welcome to DontStayIn -->

<asp:Panel Runat="server" ID="PhotoOfWeekAllPanel" Visible="false">
	<dsi:h1 runat="server" ID="H2" NAME="H11">Top photos / videos</dsi:h1>
	<div class="ContentBorder">
		<Controls:TopPhotos runat="server" id="TopPhotosUc" />
	</div>
</asp:Panel>

