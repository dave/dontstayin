<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Video.ascx.cs" Inherits="Spotted.Controls.Video" %>
<span id="<%= ClientID %>_FlashDiv">
	<table style="width:<%= Width.ToString() %>; height:<%= (Height + 20).ToString() %>;" class="BorderBlack All">
		<tr>
			<td valign="middle" align="center" style="font-size:13px; font-weight:bold; padding:10px;">
				You should see a video here, but it's not working! You either have JavaScript turned off or an old version of Macromedia's Flash Player. 
				<a href="http://www.macromedia.com/go/getflashplayer/" target="_blank">Click here</a> to get the latest flash player.
			</td>
		</tr>
	</table>
</span>

<dsi:InlineScript runat="server">
	<script>
		var <%= ClientID %>_so = new SWFObject('/misc/flvplayer.swf', '<%= ClientID  %>_mymovie', <%= Width.ToString() %>, <%= (Height + 20).ToString()%>, 7, '#FFFFFF');
		<%= ClientID %>_so.addParam('wmode', 'transparent');
		<%= ClientID %>_so.addVariable('file', '<%=  VideoUrl %>');
		<%= ClientID %>_so.addVariable('jpg', '<%= JpgUrl %>');
		<%= ClientID %>_so.addVariable('autoStart', '<%=  (AutoStart ? "1" : "0") %>');
		<%= ClientID %>_so.addVariable('link', '<%=  LinkUrl %>');
		<%= ClientID %>_so.write('<%= ClientID  %>_FlashDiv');
	</script>
</dsi:InlineScript>
