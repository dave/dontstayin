<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="MobilePageSkeleton.aspx.cs" Inherits="Spotted.Master.MobilePageSkeleton" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html lang="en"> <!--<![endif]-->
<head>

	<!-- Basic Page Needs
	================================================== -->
	<meta charset="utf-8">
	<title>Don't Stay In</title>
	<meta name="description" content="The world's biggest dance music and clubbing website">
	<meta name="author" content="Development Hell Limited">
	<!--[if lt IE 9]>
		<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->

	<!-- Mobile Specific Metas
	================================================== -->
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">

	<!-- CSS
	================================================== -->
	<link rel="stylesheet" href="/misc/skeleton/stylesheets/base.css">
	<link rel="stylesheet" href="/misc/skeleton/stylesheets/skeleton.css">
	<link rel="stylesheet" href="/misc/skeleton/stylesheets/layout.css">

	<!-- Favicons
	================================================== -->
	<link rel="shortcut icon" href="/favicon.ico">
	<link rel="apple-touch-icon" href="/misc/skeleton/images/apple-touch-icon.png">
	<link rel="apple-touch-icon" sizes="72x72" href="/misc/skeleton/images/apple-touch-icon-72x72.png">
	<link rel="apple-touch-icon" sizes="114x114" href="/misc/skeleton/images/apple-touch-icon-114x114.png">

</head>
<body runat="server" id="BodyTag">
	<!-- Welcome to DontStayIn -->


	<!-- Primary Page Layout
	================================================== -->

	<form id="TemplateForm" method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" EnablePageMethods="true" />
		<script>
			PageMethods.set_path("/pagemethods/genericpage.aspx");
		</script>
		<asp:PlaceHolder runat="server" id="HeaderScriptPlaceHolder"></asp:PlaceHolder>
		<asp:PlaceHolder Runat="server" ID="MainContentPlaceHolder"></asp:PlaceHolder>
	</form>

	<!-- JS
	================================================== -->
	<script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
	<script src="/misc/skeleton/javascripts/tabs.js"></script>

<!-- End Document
================================================== -->
</body>
</html>



