<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Mobile.Home" %>
<div data-role="header">
	<h1>My Title</h1>
</div><!-- /header -->

<div data-role="content">	
	<p id="Location">
		Location	
	</p>
	<ul data-role="listview" data-inset="true" data-filter="false">
		<li><a href="#">Acura</a></li>
		<li><a href="#">Audi</a></li>
		<li><a href="#">BMW</a></li>
		<li><a href="#">Cadillac</a></li>
		<li><a href="#">Ferrari</a></li>
	</ul>
</div><!-- /content -->

<script>
	$(document).bind("mobileinit", function () {
		//apply overrides here
	});

</script>
