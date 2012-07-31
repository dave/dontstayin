<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupMenuTest.aspx.cs" Inherits="TestWebApp.PopupMenuTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="Stylesheet.css" />
    <script src="jquery-1.2.3.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:ScriptManager ID="ScriptManager1" runat="server">
			<Scripts>
				<asp:ScriptReference Assembly="JsWebControls" Name="JsWebControls.Script.sscorlib.debug.js" />
				<asp:ScriptReference Assembly="JsWebControls" Name="JsWebControls.Script.ScriptSharpLibrary.debug.js" />
			</Scripts>
		</asp:ScriptManager>
		<script>
			var popupMenu;
			function divClick(){
				popupMenu = new ScriptSharpLibrary.PopupMenu.PopupMenu('', '', false);
				popupMenu.addItem('hello', 'world');
				popupMenu.addItem('goodbye', 'all');
				popupMenu.show(document.getElementById('uiPopup'));
				popupMenu.itemClick = itemClick;
			}
			function itemClick(e){
				alert(e.target.getAttribute("value"));
			}
			
		</script>
		<div id="uiPopup" onclick="divClick()" >
			click here
		</div>
		<div onclick="popupMenu.clear();">clear</div>
		<div onclick="popupMenu.hide();">hide</div>
    </div>
    </form>
</body>
</html>
