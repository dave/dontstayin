<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DonationIcons.ascx.cs" Inherits="Spotted.Admin.DonationIcons" %>
<%@ Register TagPrefix="DsiControls" TagName="TimeControl" Src="/Controls/TimeControl.ascx" %>

<p>K: <asp:TextBox runat="server" ID="uiK"></asp:TextBox></p>
<p><asp:Button runat="server" OnClick="Load" Text="Load" /></p>

<table>
	<tr>
		<td>Name as on Donation page</td>
		<td><asp:TextBox runat="server" ID="uiName"></asp:TextBox></td>
	</tr>
	<tr>
		<td>Icon guid</td>
		<td><asp:TextBox runat="server" ID="uiGuid"></asp:TextBox></td>
	</tr>
	<tr>
		<td>Icon extension</td>
		<td><asp:TextBox runat="server" ID="uiExtension" Text="gif"></asp:TextBox></td>
	</tr>
	<tr>
		<td>Text next to icon</td>
		<td><asp:TextBox runat="server" ID="uiText"></asp:TextBox></td>
	</tr>
	<tr>
		<td>Activation date/time</td>
		<td><table><tr><td><dsi:Cal id="uiActivationDate" runat="server" /></td><td><DsiControls:TimeControl ID="uiActivationTime" runat="server" /></td></tr></table></td>
	</tr>
	<tr>
		<td>Enabled</td>
		<td><asp:CheckBox runat="server" ID="uiEnabled"></asp:CheckBox></td>
	</tr>
	<tr>
		<td>ThreadK</td>
		<td><asp:TextBox runat="server" ID="uiThreadK"></asp:TextBox></td>
	</tr>
	<tr>
		<td>Vatable?</td>
		<td><asp:CheckBox runat="server" ID="uiVatable"></asp:CheckBox></td>
	</tr>
</table>

<p><asp:Button runat="server" Text="Save" OnClick="Save" /></p>
<p><asp:HyperLink runat="server" ID="uiLink" Target="_blank"></asp:HyperLink></p>
