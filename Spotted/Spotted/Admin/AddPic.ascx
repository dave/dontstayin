<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPic.ascx.cs" Inherits="Spotted.Admin.AddPic" %>
<%@ Register TagPrefix="Controls" TagName="PicCropper" Src="/Controls/PicCropper.ascx" %>

<asp:Panel Runat="server" ID="ObjectPanel">

	<p>Object type:</p>
	<p>
		<asp:DropDownList Runat="server" ID="ObjectTypeList">
			<asp:ListItem Value="Article">Article</asp:ListItem>
			<asp:ListItem Value="Brand">Brand</asp:ListItem>
			<asp:ListItem Value="Comp">Comp</asp:ListItem>
			<asp:ListItem Value="Event">Event</asp:ListItem>
			<asp:ListItem Value="Gallery">Gallery</asp:ListItem>
			<asp:ListItem Value="Para">Para</asp:ListItem>
			<asp:ListItem Value="Place">Place</asp:ListItem>
			<asp:ListItem Value="Promoter">Promoter</asp:ListItem>
			<asp:ListItem Value="Usr">Usr</asp:ListItem>
			<asp:ListItem Value="Venue">Venue</asp:ListItem>
		</asp:DropDownList>
	</p>
	<p>
		Object K:
	</p>
	<p>
		<asp:TextBox Runat="server" ID="ObjectKTextBox"/>
	</p>
	<p>
		<asp:Button Runat="server" OnClick="ObjectPanelViewPicClick" Text="Refresh picture"></asp:Button> 
		<asp:Button ID="Button1" Runat="server" OnClick="ObjectPanelAddClick" Text="Add / edit picture - &gt;"></asp:Button>
	</p>
	<asp:Panel Runat="server" ID="ViewPicPanel" Visible="False">
		<p>
			Current pic:
		</p>
		<p>
			<img src="" runat="server" id="ViewPicImg"/>
		</p>
		<p>
			<asp:Button ID="Button2" Runat="server" OnClick="DeleteImage" Text="Delete this pic"/>
		</p>
	</asp:Panel>
</asp:Panel>


<asp:Panel Runat="server" ID="CropperPanel">
	<P>
		<Controls:PicCropper id="PicCropper" runat="server" OnBackClick="PicCropperBackClick" OnSaved="PicCropperSaved"/>
	</P>
</asp:Panel>
