<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminStats.ascx.cs" Inherits="Spotted.Admin.AdminStats" %>
<p></p>
<dsi:h1 ID="H1" runat="server">Job processor queue</dsi:h1>
<div class="ContentBorder" style="background-color:#FED551;">
	
	Number of items currently in queue: 
	<asp:Label ID="uiCurrentJobProcessorQueueSize" runat="server" ></asp:Label>
	<asp:GridView ID="JobProcessorDataItemsGridView" runat="server" AutoGenerateColumns="true">
	</asp:GridView>
</div>
<dsi:h1 ID="H2" runat="server">Photo uploaded tries</dsi:h1>
<div class="ContentBorder" style="background-color:#FED551;">
	
	<asp:GridView ID="uiPhotoUploaderTriesDataItemsGridView" runat="server" AutoGenerateColumns="true">
	</asp:GridView>
	<asp:GridView ID="uiPhotoUploaderSuccessFailureDataItemsGridView" runat="server" AutoGenerateColumns="true">
	</asp:GridView>
</div>
