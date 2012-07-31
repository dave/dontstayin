<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemcachedStats.ascx.cs" Inherits="Spotted.Admin.MemcachedStats" %>
<h2>
	Memcached
</h2>
<p>
	<asp:GridView ID="uiMemcachedStatsGridView" runat="server" >
	</asp:GridView>
</p>
<p>
	<asp:Button runat="server" OnClick="FlushAll" Text="Flush all - don't fucking click this button, morons!" />
</p>
