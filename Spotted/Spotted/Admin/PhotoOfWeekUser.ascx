<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoOfWeekUser.ascx.cs" Inherits="Spotted.Admin.PhotoOfWeekUser" %>

<p>
	Enter photoK: <asp:TextBox runat="server" ID="uiPhotoK" EnableViewState="false"></asp:TextBox>
</p>
<p>
	<asp:Button runat="server"  OnClick="GetPhoto" Text="Get photo" />
</p>

<asp:Panel runat="server" ID="uiPhotoDetails" Visible ="false">
	<p>
		PhotoK:	<asp:Label runat="server" ID="uiPhotoKLabel"></asp:Label>
	</p>
	
	<img runat="server" id="uiPhotoImg" />
	
	<p>
		<asp:CheckBox runat="server" ID="uiPhotoOfWeek" Text="On front page" />
	</p>
	
	<p>
		Caption: <asp:TextBox runat="server" ID="uiPhotoOfWeekUserCaption" Columns="50"></asp:TextBox>
	</p>
	
	<p>
		<asp:CheckBox runat="server" ID="uiResetDateTime" Text="Move to front of list on update" />
	</p>
	
	<p>
		<asp:CheckBox runat="server" ID="uiPhotoOfWeekUserBlocked" Text="Blocked" />
	</p>
	
	<asp:Button runat="server" OnClick="UpdatePhoto" Text="Update" />
	
</asp:Panel>
