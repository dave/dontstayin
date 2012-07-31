<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Spotted.Pages.Brands.Edit" %>

<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register TagPrefix="Controls" TagName="Pic" Src="/Controls/Pic.ascx" %>
<asp:Panel Runat="server" ID="PanelManage">
	<dsi:h1 runat="server" ID="H14">Brand manager</dsi:h1>
	<div class="ContentBorder">
		<p>
			This is <a href="" runat="server" id="ManageBrandAnchor"/>
		</p>
	</div>
	
	<dsi:h1 runat="server" ID="H12">Rename this brand</dsi:h1>
	<div class="ContentBorder">
		<p>
			Name : <asp:TextBox Runat="server" ID="ManageNameTextBox" Columns="50" MaxLength="50"></asp:TextBox>
		</p>
		<p>
			<asp:Button Runat="server" OnClick="ManageNameSave" Text="Rename" ID="Button1"/>
		</p>
		<p runat="server" visible="false" style="color:#ff0000;" id="RenameError">
			This name is already used by a party brand in our database. Please choose another.
		</p>
	</div>
	
	<dsi:h1 runat="server" ID="H15">Add a picture</dsi:h1>
	<div class="ContentBorder">
		<asp:Panel Runat="server" ID="ManagePicUploadPanel">
			<Controls:Pic Runat="server" ID="ManagePic" OnActionSaved="ManagePicSaved" OnActionNoPic="ManagePicNoPic"/>
		</asp:Panel>
	</div>
	
	<asp:Panel Runat="server" ID="SuperAdminOptions">
		<dsi:h1 runat="server" ID="H16">Event admin options</dsi:h1>
		<div class="ContentBorder">
			<h2>Delete brand</h2>
			<p>
				Delete this brand, and don't merge it's contents with anything:
			</p>
			<p>
				<asp:Button Runat="server" ID="ManageDeleteButton" 
					Text="Delete THIS brand" OnClick="ManageDeleteClick"/>
			</p>
			<p runat="server" id="ManageDeleteDone" style="color:blue;" visible="false">
				Delete done.
			</p>
			<h2>Goto a brand</h2>
			<p>
				Find duplicate brands by searching in the box below...
			</p>
			<p>
			<js:HtmlAutoComplete runat="server" ID="uiManageGotoAutoComplete" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBrands" Width="580px"/>
			</p>
			<p>
				<asp:Button Runat="server" 
					Text="Goto" OnClick="ManageGotoClick" ID="Button2" NAME="Button2"/>
			</p>
		</div>
	</asp:Panel>
</asp:Panel>
