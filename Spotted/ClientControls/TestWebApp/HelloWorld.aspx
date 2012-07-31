<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelloWorld.aspx.cs" Inherits="TestWebApp.HelloWorld" %>
<%@ Register Assembly="JsWebControls" Namespace="JsWebControls" TagPrefix="js" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<js:HelloWorldControl runat="server" id="uiHelloWorld"></js:HelloWorldControl>
    </div>
    </form>
</body>
</html>
