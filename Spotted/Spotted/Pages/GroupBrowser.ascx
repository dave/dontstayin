<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupBrowser.ascx.cs" Inherits="Spotted.Pages.GroupBrowser" %>

<asp:Panel Runat="server" ID="PanelGroups">
	<dsi:h1 runat="server" ID="Header" NAME="H18">Group themes...</dsi:h1>
	<div class="ContentBorder">
		<p>
			<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;">Groups:
		</p>
		<div class="CleanLinks">
			<asp:DataList 
				Runat="server" 
				id="GroupsDataList"
				
				RepeatColumns="1" 
				RepeatDirection="Horizontal" 
				RepeatLayout="Flow"/>
		</div>
	</div>
</asp:Panel>
