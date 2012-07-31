<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisableContent.ascx.cs" Inherits="Spotted.Admin.DisableContent" %>
<h1>Disable / enable content</h1>
<div class="ContentBorder">
	<p>
		This will hide the image on the site.
	</p>
	<p>
		PhotoK:<asp:TextBox Runat="server" ID="PhotoK"></asp:TextBox>
	</p>
	<p>
		<asp:Button ID="Button1" Runat="server" OnClick="Disable" Text="Disable"/>
		<asp:Button ID="Button2" Runat="server" OnClick="Enable" Text="Enable"/>
	</p>
	<p>
		<asp:Label Runat="server" ID="OutLabel"/>
	</p>
</div>
