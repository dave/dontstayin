<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProSpotter.ascx.cs" Inherits="Spotted.Templates.Usrs.ProSpotter" %>
<table border="0" cellpadding="0" cellspacing="0">
<tr valign="middle">
<td>
	<h2>
		<a href="<%#CurrentUsr.Url()%>"><img src="<%#CurrentUsr.SpotterIconPath%>" border="0" align="absmiddle" style="margin-right:3px;"></a><a href="<%#CurrentUsr.Url()%>" <%#CurrentUsr.Rollover%>><%#CurrentUsr.NickNameSafe%></a> 
	</h2>
</td>
<td>
	<small>&nbsp;(<%#CurrentUsr.SpotterStatus(false, false, false)%>)</small>
</td>
</tr>
</table>

<p>
	Here are some of <%#CurrentUsr.HisString(false)%> favourite photos:
</p>
<p>
	<asp:DataList Runat="server" ID="PhotosDataList" RepeatColumns="5" RepeatDirection="Horizontal" ItemStyle-CssClass="ProSpotterPicCell"></asp:DataList>
</p>
<p>
	<small>
		<%#CurrentUsr.HeString(true)%> has taken 
		<%#CurrentUsr.TotalPhotoUploads.ToString("###,##0")%> 
		photo<%#CurrentUsr.TotalPhotoUploads==1?"":"s"%> 
		and spotted 
		<%#CurrentUsr.SpottingsTotal.ToString("###,##0")%> 
		<%#CurrentUsr.SpottingsTotal == 1 ? "person" : "people"%>.
	</small>
</p>
