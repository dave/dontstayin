<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChatClientTest.ascx.cs" Inherits="Spotted.Blank.ChatClientTest" %>
<%@ Register TagPrefix="Navigation" TagName="ChatClient" Src="/Controls/ChatClient.ascx" %>
<html>
	<head>
		<link rel="stylesheet" type="text/css" href="/support/style.css?a=16"/>
		<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
		<%= Spotted.Main15Script.Register %>
		<SCRIPT LANGUAGE="javascript" SRC="/misc/utilities.js"></SCRIPT>
	</head>
	<body>
		<div id="TipLayer" style="visibility:hidden;position:absolute;z-index:1000;"></div>
		
		<div runat="server" id="ChatClient" style="position:absolute;left:13px;top:13px;width:302px;height:500px;">
			<Navigation:ChatClient runat="Server" ID="NavChatClient"/>
		</div>
		
	</body>
</html>
