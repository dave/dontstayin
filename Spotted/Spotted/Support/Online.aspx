<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Online.aspx.cs" Inherits="Spotted.Support.Online" %>

<html>
	<head>
	</head>
	<body>
		<asp:Label runat="server" id="time"></asp:Label>
		<div id="usrsOnlineDiv">
			<p>
				<asp:Label Runat="server" ID="OnlineLabel"></asp:Label>
			</p>
			<p class="CleanLinks" runat="server" id="OnlineP">
				<asp:DataList Runat="server" RepeatLayout="Flow" ID="OnlineDataList" RepeatDirection="Horizontal">
					<SeparatorTemplate><br /></SeparatorTemplate>
				</asp:DataList>
			</p>
		</div>
	</body>
</html>
