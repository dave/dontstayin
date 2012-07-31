<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StripUsr.ascx.cs" Inherits="Spotted.Admin.StripUsr" %>
<h1>
	Be careful! No undo!
</h1>
<p>
	Usr to strip:
</p>
<p>
	K = <asp:TextBox Runat="server" ID="ObjectKTextBox"></asp:TextBox>
</p>
<p>
	<asp:Button Runat="server" OnClick="DeleteNow" ID="DeleteButton" Text="Strip it now"></asp:Button>
</p>
<asp:Label Runat="server" ID="DoneLabel" ForeColor="#ff0000" Visible="False"><p>Done</p></asp:Label>
