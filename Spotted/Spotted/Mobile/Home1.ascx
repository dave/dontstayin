<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home1.ascx.cs" Inherits="Spotted.Mobile.Home1" %>
<div class="container">
	<div class="sixteen columns">
		<h1 class="remove-bottom" style="margin-top: 40px">
			Don't Stay In</h1>
		<h5>
			Mobile site</h5>
		<hr />
	</div>
	<div class="one-third column">
		<h3>
			What's this?</h3>
		<p>
			I'm building a version of the site that'll work really well on mobiles. It won't
			do everything the main site does, but it'll have the most usefull pages.
		</p>
	</div>
	<div class="one-third column">
		<h3>
			Why isn't it working?</h3>
		<p>
			It doesn't do anything yet. I only just started building it.
		</p>
	</div>
	<div class="one-third column">
		<h3>
			Desktop site</h3>
		<p>
			If you'd like to swap to the desktop version of the site, click below:</p>
		<p>
			<asp:LinkButton runat="server" OnClick="ForceFull" Text="Desktop site" CssClass="button" />
		</p>
	</div>
	<div class="one-third column">
		<h3>
			Log in</h3>
		<ul class="tabs">
			<!-- Give href an ID value of corresponding "tabs-content" <li>'s -->
			<li><a class="active" href="#loginfacebook">Facebook</a></li>
			<li><a href="#logindsi">Don't Stay In</a></li>
		</ul>
		<!-- Standard <ul> with class of "tabs-content" -->
		<ul class="tabs-content">
			<!-- Give ID that matches HREF of above anchors -->
			<li class="active" id="loginfacebook">Coming soon...</li>
			<li id="logindsi">
				<label for="emailInput">
					Email</label>
				<input type="text" id="emailInput" />
				<label for="passwordInput">
					Password</label>
				<input type="text" id="passwordInput" type="password" />
				<button onclick="alert('Coming soon...');return false;">
					Log in</button>
			</li>
		</ul>
	</div>
</div>
