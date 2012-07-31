<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Spotted.Pages.Login" %>


<asp:Panel Runat=server ID="LoginPanel">
	
	<noscript><style> .jsonly { display: none } </style></noscript>
	<div class="jsonly">
		<dsi:h1 runat="server">Login page</dsi:h1>
		<div class="ContentBorder">
			<p>
				To login, click the button below:
			</p>
			<p>
				<a href="/pages/login" class="NoStyle" onclick="try { ConnectButtonClick(); } catch(ex) { } return false;" style="position:relative; top:5px;"><img src="/gfx/facebook-connect.gif" width="89" height="21" /></a>
			</p>
		</div>
	</div>
	<noscript>
		<dsi:h1 runat="server">Error</dsi:h1>
		<div class="ContentBorder">
			<p>
				You have Javascript disabled. You need Javascript enabled to log in to Don't Stay In. You may need to adjust your security settings to "medium".
			</p>
		</div>
	</noscript>
	
</asp:Panel>
<asp:Panel Runat=server ID="LoggedInPanel">
	<asp:Panel Runat=server ID="ErrorPanel1">
		<dsi:h1 runat="server">You're logged in</dsi:h1>
		<div class="ContentBorder">
			<p><a href="" onclick="history.go(-1);return false;">Back to previous page</a></p>
		</div>
	</asp:Panel>
	<div class="ContentBorder">
		<p>
			You can log out by clicking the 'log out' link above.
		</p>
	</div>
</asp:Panel>
