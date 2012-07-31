<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="Spotted.Templates.Venues.Admin" %>
<a name="ucAdminAnchorVenueK<%#Current.K.ToString()%>"></a>
<h2><%#enc(Current.Name)%> in <%#enc(Current.Place.Name)%> (<%#enc(Current.Place.Country.FriendlyName)%>)</h2>
<table border="0" cellspacing="5" class="AdminEventTable" id="ucAdminVenueTableK<%#Current.K.ToString()%>" width="100%">
	<tr>
		<th>K</th>
		<td width="100%"><%#Current.K%></td>
	</tr>
	<tr>
		<th>Name</th>
		<td><%#enc(Current.Name)%></td>
	</tr>
	<tr>
		<th>DetailsHtml</th>
		<td><%#Current.DetailsHtmlRender%></td>
	</tr>
	<tr>
		<th>Events</th>
		<td><%#EventsText%></td>
	</tr>
	<tr>
		<th>Postcode</th>
		<td>
			<a href="http://maps.google.co.uk/maps?q=<%#Current.Postcode.Replace(" ","").ToUpper()%>(<%#HttpUtility.UrlEncode(Current.Name)%>)" target="_blank"><%#enc(Current.Postcode)%></a>
		</td>
	</tr>
	<tr>
		<th>OverrideMapUrl</th>
		<td>
			<a href="<%#Current.OverrideMapUrl%>" target="_blank"><%#enc(Current.OverrideMapUrl)%></a>&nbsp;
		</td>
	</tr>
	<tr>
		<th>Place</th>
		<td><a href="<%#Current.Place.Url()%>" target="_blank"><%#enc(Current.Place.NamePlainRegion)%></a> (<a href="<%#Current.Place.Country.Url()%>" target="_blank"><%#Current.Place.Country.FriendlyName%></a>)</td>
	</tr>
	<tr>
		<th>Regular events (once a month or more)?</th>
		<td><%#Current.RegularEvents.ToString()%></td>
	</tr>
	<tr>
		<th>Capacity</th>
		<td><%#Current.Capacity.ToString()%></td>
	</tr>
	<tr>
		<th>AdminNote</th>
		<td><%#enc(Current.AdminNote).Replace("\n","<br>")%></td>
	</tr>
	<tr>
		<th>Owner</th>
		<td><a href="<%#Current.Owner.Url()%>" <%#Current.Owner.Rollover%> target="_blank"><%#Current.Owner.NickNameSafe%></a> <a href="mailto:<%#Current.Owner.Email%>">(<%#Current.Owner.Email%>)</a></td>
	</tr>
	
	<tr>
		<th>Picture</th>
		<td>
			<img runat="server" id="Pic" style="margin-right:7px;" class="BorderBlack All"><a href="<%#Current.UrlApp("edit","page","pic")%>" target="_blank">Add / replace picture</a>
		</td>
	</tr>
	<tr runat="server" id="SimilarVenuesRow">
		<th>Similar to:</th>
		<td style="border:2px solid #ff0000;">
			<h2>THIS VENUE COULD BE A DUPLICATE - Similar venues:</h2>
			<p>
				<%#SimilarText%>
			</p>
			<p>
				This venue may be a duplicate. Venues listed above have similar names, and are in or around the town selected for this venue. Note that this doesn't definately mean this venue is a duplicate, but please investigate and delete this venue if it is a duplicate.
			</p>
		</td>
	</tr>
	<tr>
		<th>Options</th>
		<td>
			<p>
				<a href="<%#Current.Url()%>" target="_blank">Preview</a>
			</p>
			<p>
				<a href="<%#Current.UrlApp("edit")%>" target="_blank">Edit</a>
			</p>
			<p>
				<a href="<%#Current.UrlApp("edit","page","pic")%>" target="_blank">Edit picture</a>
			</p>
			<p>
				<asp:LinkButton Runat="server" OnClick="Enable" ID="EnableButton">Enable</asp:LinkButton>
			</p>
			<p>
				<asp:LinkButton Runat="server" OnClick="DeleteAll" ID="DeleteButton">Delete</asp:LinkButton> <asp:Label Runat="server" ID="StatusLabel" ForeColor="#ff0000"></asp:Label>
			</p>
			<p>
				<input type="checkbox" name="ucAdminVenueSelectedK<%#Current.K.ToString()%>" id="ucAdminVenueSelectedK<%#Current.K.ToString()%>" value="1" onclick="document.getElementById('ucAdminVenueTableK<%#Current.K.ToString()%>').style.backgroundColor=this.checked?'#FFE79D':'transparent';"><label for="ucAdminVenueSelectedK<%#Current.K.ToString()%>">Select</label>
			</p>
		</td>
	</tr>
</table>
<p>&nbsp;</p>
<p>&nbsp;</p>
