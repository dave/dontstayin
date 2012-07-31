<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiDelete.ascx.cs" Inherits="Spotted.Admin.MultiDelete" %>
<h1>
	Be careful! No undo!
</h1>
<p>
	Object type to delete:
</p>
<p>
	<asp:DropDownList Runat="server" ID="ObjectTypeDropDown">
		<asp:ListItem Value="Photo">Photo</asp:ListItem>
		<asp:ListItem Value="Gallery">Gallery</asp:ListItem>
		<asp:ListItem Value="Venue">Venue</asp:ListItem>
		<asp:ListItem Value="Event">Event</asp:ListItem>
		<asp:ListItem Value="Comment">Comment</asp:ListItem>
		<asp:ListItem Value="Thread">Thread</asp:ListItem>
		<asp:ListItem Value="Usr">Usr</asp:ListItem>
		<asp:ListItem Value="Article">Article</asp:ListItem>
	</asp:DropDownList>
</p>
<p>
	Which one:
</p>
<p>
	K = <asp:TextBox Runat="server" ID="ObjectKTextBox"></asp:TextBox>
</p>
<p>
	<asp:Button Runat="server" OnClick="DeleteNow" ID="DeleteButton" Text="Delete it now"></asp:Button>
</p>
<asp:Label Runat="server" ID="DoneLabel" ForeColor="#ff0000" Visible="False"><p>Done</p></asp:Label>
