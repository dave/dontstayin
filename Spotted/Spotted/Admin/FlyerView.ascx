<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlyerView.ascx.cs" Inherits="Spotted.Admin.FlyerView" %>

<asp:Panel runat="server" ID="uiBasicInfo">
	<table>
		<tr>
			<td>
				K
			</td>
			<td>
				#<%= Flyer.K.ToString() %>
			</td>
		</tr>
		<tr>
			<td>
				Promoter
			</td>
			<td>
				<%= Flyer.Promoter != null ? Flyer.Promoter.Name + " (K " + Flyer.PromoterK + ")" : "" %>
			</td>
		</tr>
		<tr>
			<td>
				Name of flyer run
			</td>
			<td>
				<%= Flyer.Name %>
			</td>
		</tr>
		<tr>
			<td>
				Email Subject
			</td>
			<td>
				<%= Flyer.Subject %>
			</td>
		</tr>
		<tr>
			<td>
				Email From
			</td>
			<td>
				<%= Flyer.MailFromDisplayName %>
			</td>
		</tr>
		<tr>
			<td>
				Background colour
			</td>
			<td>
				<input style="background-color:#<%= Flyer.BackgroundColor %>" disabled="disabled" value="#<%= Flyer.BackgroundColor %>" />
			</td>
		</tr>
		<tr>
			<td>
				Link to URL
			</td>
			<td>
				<a href="<%= Flyer.LinkTargetUrl %>" target="_blank"><%= Flyer.LinkTargetUrl %></a>
			</td>
		</tr>
		<tr>
			<td>
				Is HTML?
			</td>
			<td>
				<%= Flyer.IsHtml %>
			</td>
		</tr>
		<tr>
			<td>
				HTML
			</td>
			<td>
<pre><%= HttpUtility.HtmlEncode(Flyer.Html) %></pre>
			</td>
		</tr>
		<tr>
			<td>
				Text alternative
			</td>
			<td>
<pre><%= HttpUtility.HtmlEncode(Flyer.TextAlternative) %></pre>
			</td>
		</tr>
		<tr>
			<td>
				Send date / time
			</td>
			<td>
				<%= Flyer.SendDateTime.ToString("F") %>
			</td>
		</tr>
		<tr>
			<td>
				Location targetting
			</td>
			<td>
				<%= Flyer.PlaceKs.Length == 0 ? "all" : Flyer.PlaceKs.Split(',').Length.ToString() %> towns
			</td>
		</tr>
		<tr>
			<td>
				Music targetting
			</td>
			<td>
				<%= Flyer.MusicTypeKs.Length == 0 || Flyer.MusicTypeKs == "1" ? "all" : Flyer.MusicTypeKs.Split(',').Length.ToString() %> music types
			</td>
		</tr>
		<tr>
			<td>
				Target promoters only?
			</td>
			<td>
				<%= Flyer.PromotersOnly.ToString() %>
			</td>
		</tr>
		<tr>
			<td>
				Target users by event
			</td>
			<td>
				<%= string.IsNullOrEmpty(Flyer.EventKs) ? "Not targetted by event" : string.Join("<br />", Flyer.EventKs.CommaSeparatedValuesToIntList().ConvertAll(k => new Event(k)).ConvertAll(e => e.Name + " @ " + e.Venue.Name + " (" + e.Venue.Place.Country.FriendlyName + "), " + e.DateTime.ToString("dd/MM/yy")).ToArray()) %>
			</td>
		</tr>
		<tr>
			<td>
			</td>
			<td>
				<%= ContainerPage.Url["count"].Exists ? ("<i>Targetting " + CountUsrBase.ToString("N0") + " people</i>") : "<a href='"+ContainerPage.Url.CurrentUrl("count","")+"'>count targetting</a>" %>
			</td>
		</tr>
		<tr>
			<td>
				Preview
			</td>
			<td>
				<p style="display:<%= Flyer.Misc == null ? "''" : "'none'" %>; color:Red;">No image uploaded</p>
				<a href="<%= Flyer.Misc != null ? Flyer.Misc.Url() : "" %>" style="display:<%= Flyer.Misc != null ? "''" : "'none'" %>" target="_blank">Image</a>
				<a href="/popup/flyer/k-<%= Flyer.K %>" style="display:<%= Flyer.Misc != null ? "''" : "'none'" %>" target="_blank">Popup</a>
			</td>
		</tr>
	</table>

	<a href="/admin/flyeredit/k-<%= Flyer.K %>">Edit</a>
	<p runat="server" id="uiValidationErrors" style="color:Red"><ul><li><%= string.Join("</li><li>", Flyer.ValidationErrors) %></li></ul></p>
	<p>Sent already: <%= Flyer.Sends.ToString("N0") %>&nbsp;<asp:Button runat="server" Text="Refresh" /></p>
	<p>
		Skipped because broken: <%= Flyer.Broken.ToString("N0") %>
	</p>
	<p>
		Failed because of exception: <%= Flyer.Exceptions.ToString("N0") %>
	</p>
	<p>
		Mail server retries: <%= Flyer.MailServerRetries.ToString("N0") %> (last: <%= Flyer.MailServerLastRetry.HasValue ? Cambro.Misc.Utility.FriendlyTime(Flyer.MailServerLastRetry) : "n/a" %>)
	</p>
	<p>
		Last activity: <%= Flyer.DateTimeLastMessageSent.HasValue ? Cambro.Misc.Utility.FriendlyTime(Flyer.DateTimeLastMessageSent) : "n/a" %>
	</p>
	<div runat="server" ID="uiSendButtons">
		<asp:Button runat="server" id="uiSend" OnClick="Send" Text="Send" OnClientClick="return confirm('Queue up this eFlyer to be sent as soon as possible?');" />
		<asp:Button runat="server" id="uiStop" OnClick="Stop" Text="Stop" OnClientClick="return confirm('Stop this eFlyer from sending?');" />
	</div>
	<asp:Button runat="server" id="uiRestart" OnClick="Restart" Text="Restart" OnClientClick="return confirm('Restart this eFlyer?');" />
	<p runat="server" id="uiSentSuccessfully">eFlyer has sent successfully.</p>
	

</asp:Panel>

