<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExDirectoryPrivacyOption.ascx.cs" Inherits="Spotted.Pages.ExDirectoryPrivacyOption" %>
<%@ Register TagPrefix="DsiControl" TagName="ExDirectory" Src="/Controls/ExDirectoryPrivacyOption.ascx" %>

<DsiControl:ExDirectory runat="server" ID="uiExDirectory" CloseDiv="false"></DsiControl:ExDirectory>
	<button onclick="javascript:history.go(-1);"><- Back</button>
	<asp:Button runat="server" OnClick="Save_Click" Text="Confirm" />
	<p><asp:Label runat="server" id="uiSuccess" ForeColor="#0000ff"></asp:Label></p>
</div>
