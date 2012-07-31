<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyAccount.ascx.cs" Inherits="Spotted.Pages.MyAccount" %>
<dsi:h1 runat="server">
	My account
</dsi:h1>
<div class="ContentBorder">
	<p><a href="<% = Usr.Current.Url() %>">My profile page</a></p>
	<p><a href="/pages/mypicture">My profile picture</a></p>
	<p><a href="<% = Usr.Current.UrlApp("edit") %>">Update my details</a></p>
	<p><a href="/pages/changepassword">Change my password</a></p>
	<p><a href="/pages/myprivacy">My privacy</a></p>
</div>

<dsi:h1 runat="server">
	Music
</dsi:h1>
<div class="ContentBorder">
	<p><a href="/pages/mymusic">Music I listen to</a></p>
</div>

<dsi:h1 runat="server">
	My friends
</dsi:h1>
<div class="ContentBorder">
	<p><a href="<% = Usr.Current.UrlApp("buddies") %>">My buddies</a></p>
	<p><a href="<% = Usr.Current.UrlApp("buddyrequests") %>">Buddy requests</a></p>
	<p><a href="/pages/findyourfriends">Find my friends</a></p>
	<p><a href="/pages/friendinviter">Invite my friends</a></p>
</div>

<dsi:h1 runat="server">
	Contribute
</dsi:h1>
<div class="ContentBorder">
	<p><a href="/pages/uploadphotos">Add a gallery</a> - <a href="/pages/mygalleries">My galleries</a></p>
	<p><a href="/pages/myarticles/mode-add">Add an article</a> - <a href="/pages/myarticles/mode-list">My articles</a></p>
	<p><a href="/pages/events/edit">Add an event</a> - <a href="/pages/myevents">Events I've added</a></p>
	<p><a href="/pages/venues/edit">Add a venue</a> - <a href="/pages/myvenues">Venues I've added</a></p>
</div>
