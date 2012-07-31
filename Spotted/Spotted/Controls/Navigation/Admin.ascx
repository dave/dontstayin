<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="Spotted.Controls.Navigation.Admin" %>
<asp:Panel Runat="server" ID="AdminPanel">
	<div class="NavAdminPanel">
		<a name="NavFullAdmin"></a>
		<h2>Admin</h2>
		<p>
			<a href="/admin/blank">Admin</a>
		</p>
		<p>
			<a href="/admin/newobjects">New object admin</a>
		</p>
		<p>
			<a href="/admin/settings">Settings</a>
		</p>
		<p>
			<a href="/admin/salesfind">Find promoter</a><br />
			<a href="/admin/salesletterfollowup">Letter follow-ups</a><br />
			<a href="/admin/salesidle">Idle (shared)</a><br />
			<b><a href="/admin/salesnew">My new</a></b><br />
			<b><a href="/admin/salesproactive">My proactive</a></b><br />
			<b><a href="/admin/salesactive">My active</a></b>
		</p>
		<asp:PlaceHolder Runat="server" ID="AdminPanelContents"/>
		<asp:PlaceHolder Runat="server" ID="AdminPanelOther"/>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="SalesPanel" Visible="false">

	<div class="NavAdminPanel">
		<h2>Today</h2>
		<table id="SalesTodayTable" runat="server" width="100%" cellpadding="3" cellspacing="0" class="dataGrid" />
		
		<h2>This month</h2>
		<table id="SalesMonthTable" runat="server" width="100%" cellpadding="3" cellspacing="0" class="dataGrid" />
	</div>
	
	
	<div class="NavAdminPanel">
		<h2>Last week</h2>
		<div runat="server" id="TeamBonusLastWeekDiv"></div>
		
		<h2>This week</h2>
		<div runat="server" id="TeamBonusThisWeekDiv"></div>
	</div>
	
	
</asp:Panel>

<asp:Panel Runat="server" ID="SuperPanel">
	<div class="NavAdminPanel">
		<h2>Event mods</h2>
		<p>
			<a href="/chat/k-41843">Instructions</a>
		</p>
		<p>
			<a href="/pages/groups/merge">Merge groups</a>
		</p>
		<p>
			<a href="/pages/brands/merge">Merge brands</a>
		</p>
		<p>
			<a href="/pages/events/merge">Merge events</a>
		</p>
		<p>
			<a href="/pages/venues/merge">Merge venues</a>
		</p>
		<asp:PlaceHolder Runat="server" ID="SuperAdmin"/>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="SeniorPanel">
	<div class="NavAdminPanel">
		<h2>Photo mods</h2>
		<p>
			<a href="/chat/k-83360">Instructions</a>
		</p>
		<p>
			<a href="/pages/galleries/moderate">View new photos</a>
		</p>
		<asp:PlaceHolder Runat="server" ID="SeniorAdmin"/>
		<p>
			Calling all mods: We want more overseas traffic... be nice to people posting <a href="/uk/foreigngalleries">overseas galleries</a>
		</p>
	</div>
</asp:Panel>
