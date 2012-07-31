<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateMatch.ascx.cs" Inherits="Spotted.Templates.Usrs.DateMatch" %>
<table cellspacing="5" cellpadding="0" width="100%">
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;" rowspan="2" class="BorderKeyline Right">
			<a href="<%#CurrentUsr.Url()%>"><img src="<%#CurrentUsr.AnyPicPath%>" width="100" height="100" class="BorderBlack All" align="top" border="0"></a></td>
		<td valign="top" style="padding-left:2px;padding-right:10px;" width="100%">
			<small>
				<asp:PlaceHolder Runat="server" ID="EmailPh">
					<%#HttpUtility.HtmlEncode(CurrentUsr.Email)%>	
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="NamePh">
					<a href="<%#CurrentUsr.Url()%>"><%#CurrentUsr.NickNameSafe%></a> <%#NameHtml%>
				</asp:PlaceHolder>
				was matched to you <%#FriendlyMatchDateTime%>. <br>
				DSI Date sent <a href="<%#CurrentUsrDate.MatchThread.Url()%>">this private message</a>.
			</small>
		</td>
	</tr>
</table>
