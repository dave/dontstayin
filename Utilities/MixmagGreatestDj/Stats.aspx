<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="MixmagGreatest.Stats" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
	<h2>Total</h2>
	<div>
		<asp:DataGrid runat="server" ID="Dg1"></asp:DataGrid>
    </div>
	<h2>By DJ:</h2>
    <div>
		<asp:DataGrid runat="server" ID="Dg"></asp:DataGrid>
    </div>
	<h2>Leaders:</h2>
    <div>
		<asp:DataGrid runat="server" ID="Dg2"></asp:DataGrid>
    </div>
    </form>
</body>
</html>
