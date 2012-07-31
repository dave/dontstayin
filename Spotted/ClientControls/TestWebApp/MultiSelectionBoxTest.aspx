<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiSelectionBoxTest.aspx.cs" Inherits="TestWebApp.MultiSelectionBoxTest" %>
<%@ Register Assembly="JsWebControls" Namespace="JsWebControls" TagPrefix="js" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="Stylesheet.css" />
   <script src="jquery-1.2.3.js"></script>
    
</head> 
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
    <p>
    <js:MultiSelector runat="server" id="uiMultiSelectionBox1" 
			style="border:solid 1px black; width:200px;" WebServiceMethod="GetItems" 
			WebServiceUrl="WebService.asmx" NumberOfResultsToDisplay="7" 
			/>	
	Some content here
     <js:MultiSelector runat="server" id="uiMultiSelectionBox2" 
			style="border:solid 1px black; width:300px;" WebServiceMethod="GetItems" 
			WebServiceUrl="WebService.asmx" NumberOfResultsToDisplay="7" 
			/>	
	Some content here
	<asp:UpdatePanel ID="uiUpdatePanel" runat="server">
		<ContentTemplate>
			<% =DateTime.Now.ToLongTimeString() %>
			<js:MultiSelector runat="server" id="uiMultiSelectionBox3" 
				style="border:solid 1px black; width:200px;" WebServiceMethod="GetItems" 
				WebServiceUrl="WebService.asmx" NumberOfResultsToDisplay="7" 
			/>	
			<asp:Button ID="uiUpdatePanelTrigger" runat="server" Text="Trigger updatepanel" />
		</ContentTemplate>
	</asp:UpdatePanel>
     
 
		
    </p>

    <p>Whats this?
		<ul>
			<li>Book</li>
			<li>Quick</li>
		</ul>
		
	   <asp:Button ID="Button1" runat="server" Text="Button" />
    </p>

    
    </form>
</body>
</html>
