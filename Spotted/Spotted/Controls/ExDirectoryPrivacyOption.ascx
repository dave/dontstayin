<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExDirectoryPrivacyOption.ascx.cs" Inherits="Spotted.Controls.ExDirectoryPrivacyOption" %>

<dsi:h1 runat="server">Ex-directory listing</dsi:h1>
<div class="ContentBorder">
	<p>
		When people you've met want to locate you on DontStayIn, they can search for your nickname using either your real name or email address.
		If you prefer, we can hide your nickname from these search results, so a new friend won't see your nickname until you accept their buddy request.
	</p>
	<p>
		<asp:RadioButtonList runat="server" ID="uiOptions">
			<asp:ListItem Text="Allow people to search for me" Value="0"></asp:ListItem>
			<asp:ListItem Text="Hide me from the search results" Value="1"></asp:ListItem>
		</asp:RadioButtonList>
	</p>
<%= CloseDiv ? "</div>" : "" %>
