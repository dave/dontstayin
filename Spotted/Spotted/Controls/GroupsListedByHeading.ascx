<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupsListedByHeading.ascx.cs" Inherits="Spotted.Controls.GroupsListedByHeading"%>

	<% foreach(var heading in this.Headings){ %>
<div>
	<div>
		<a href="<%= heading.Url %>"><%= heading.Title %></a>
	</div>
		<% foreach(var group in heading.Groups) { %>
			<div>
				<div><a href="<%= group.Url %>"><%= group.Title %></a></div>
				<div><%= group.Description %></div>
				<% if (group.LastThread != null) { %>
					<div><%=group.LastThread.Subject%></div>
					
				<% } %>
			</div>
		<% } %>
		
</div>	
	<% } %>
