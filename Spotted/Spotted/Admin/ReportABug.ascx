<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportABug.ascx.cs" Inherits="Spotted.Admin.ReportABug" %>
<asp:Panel ID="uiBugFormPanel" runat="server">
	<h1>EXPERIMENTAL :)</h1>
	<p>
		You are a reporting a bug. Make sure any bugs you report can be reproduced.
	</p>
	<p>
		Describe the problem in 10 words
		<br /><asp:TextBox ID="uiTitle" runat="server" Width="600px" MaxLength="100"></asp:TextBox>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please fill this in at least!!" ControlToValidate="uiTitle"></asp:RequiredFieldValidator>
	</p>
	<p>
		Describle the problem in a little more detail (what happens and how to make the error occur)
		<br /><asp:TextBox ID="uiDescription" runat="server" Height="200px" TextMode="MultiLine" Width="600px" MaxLength="400" />
	</p>
	<p>
		Whats the address of the page it happened on?
		<br /><asp:TextBox ID="uiUrl" runat="server" Width="600px" MaxLength="400"></asp:TextBox>
	</p>

	<p>
		<asp:Button ID="uiSubmit" runat="server" Text="Submit" onclick="uiSubmit_Click" />
	</p>
</asp:Panel>
<asp:Panel ID="uiSuccessPanel" runat="server" Visible="false">Bug submitted successfully</asp:Panel>

