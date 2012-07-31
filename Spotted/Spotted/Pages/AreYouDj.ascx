<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AreYouDj.ascx.cs" Inherits="Spotted.Pages.AreYouDj" %>
<dsi:h1 runat="server">Are you a DJ?</dsi:h1>
<div class="ContentBorder">
	<p>
		<asp:RadioButton runat="server" GroupName="IsDj" ID="IsDjYes" Text="Yes, I'm a DJ" /><br />
		<asp:RadioButton runat="server" GroupName="IsDj" ID="IsDjNo" Text="No, I'm not a DJ" />
		<asp:CustomValidator ID="CustomValidatorIsDj" Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="IsDjVal" ErrorMessage="<br>Please select if you're a DJ or not" />
	</p>
	<p>
		<asp:Button runat="server" Text="Save" OnClick="SaveNow" />
	</p>
	<asp:Panel runat="server" Visible="false" ID="SavedPanel">
		<p style="color:#0000ff;">
			Saved
		</p>
	</asp:Panel>
</div>
