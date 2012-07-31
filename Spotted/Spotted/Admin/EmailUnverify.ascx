<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailUnverify.ascx.cs" Inherits="Spotted.Admin.EmailUnverify" %>
<h1>For bounced emails...</h1>
<div class="ContentBorder">
	<p>
		Enter the emails that have bounced into the box below, and they will 
		be marked as un-verified - and won't get any more bulk emails until 
		they have been verified.
	</p>
	<p>
		<asp:TextBox Columns="80" Rows="50" TextMode="MultiLine" Runat="server" ID="EmailsTextBox"></asp:TextBox>
	</p>
	<p>
		<asp:Button Runat="server" Text="Un-verify emails" OnClick="Process_Click" ID="Button1"></asp:Button>
	</p>
</div>
