<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtmlAutoCompleteTest.aspx.cs" Inherits="TestWebApp.HtmlAutoCompleteTest" %>
<%@ Register Assembly="JsWebControls" Namespace=" JsWebControls" TagPrefix="js" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
	<link rel="Stylesheet" href="Stylesheet.css" />
	<script src="jquery-1.2.3.js"></script>
</head>
<body>
	
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <p>
		Some content here
		<js:HtmlAutoComplete
			runat="server" 
			id="uiHtmlAutoComplete" 
			
			WebServiceUrl="WebService.asmx" 
			WebServiceMethod="GetItems" 
			MaxNumberOfItemsToGet="50"
			Watermark="Enter nickname"
			UseFilterCache="true"
			/>
		And some more content here
	</p>
	<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
		<ContentTemplate>
			<p style="background-color:Silver;">
				<%= DateTime.Now.ToString() %>
					<js:HtmlAutoComplete
					runat="server" 
					id="uiHtmlAutoComplete2" 
					WebServiceUrl="WebService.asmx" 
					WebServiceMethod="GetItems" 
					MaxNumberOfItemsToGet="50"
					Watermark = "enter email"
				/><asp:Button ID="UpdatePanel" runat="server" Text="Button" />
				<js:HtmlAutoComplete
					runat="server" 
					id="uiHtmlAutoComplete3" 
					WebServiceUrl="WebService.asmx" 
					WebServiceMethod="GetItems" 
					MaxNumberOfItemsToGet="50"
					Watermark = "enter email"
					AutoPostBack = "true"
				/>
			</p>	
		</ContentTemplate>
	</asp:UpdatePanel>
	
    
    <p>Whats this?
		<ul>
			<li>Book</li>
			<li>Quick</li>
			
		</ul>
		<asp:Button ID="Button1" runat="server" Text="Button" />
    </p>
    <span id="eventLog"></span>
    <p> 
		<script>
			function onhighlight(sender){
				debugger;
				//document.getElementById("eventLog").innerHTML += "<br>Highlight " + sender.id + " " + sende.Value;
			}
			function onselectionmade(sender){
			debugger;
				//document.getElementById("eventLog").innerHTML += "<br>Select    " + sender.id + " " + suggestion.Value;
			}
			function onpopupcancel(sender){
			debugger;
				//document.getElementById("eventLog").innerHTML += "<br>Cancelled " + sender.id;
			}
		</script>
		
		
    </p>
 
    </form>
</body>
</html>
