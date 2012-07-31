<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="MobilePage.aspx.cs" Inherits="Spotted.Master.MobilePage" %>
<!DOCTYPE html> 
<html>
<head> 
	<title>Don't Stay In</title>
	<meta name="description" content="The world's biggest dance music and clubbing website">
	<meta name="author" content="Development Hell Limited"> 
	<meta name="viewport" content="width=device-width, initial-scale=1"> 
	<link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
	<script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
	<script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head> 
<body runat="server" id="BodyTag"> 

<div data-role="page">

	
	<form id="TemplateForm" method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" EnablePageMethods="true" />
		<script>
			PageMethods.set_path("/pagemethods/genericpage.aspx");
		</script>
		<asp:PlaceHolder runat="server" id="HeaderScriptPlaceHolder"></asp:PlaceHolder>
		<asp:PlaceHolder Runat="server" ID="MainContentPlaceHolder"></asp:PlaceHolder>
	</form>

</div><!-- /page -->

</body>
</html>
