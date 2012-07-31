<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupNewsAdmin.ascx.cs" Inherits="Spotted.Templates.Threads.GroupNewsAdmin" %>
<a name="CommentK-<%#CurrentComment.K%>"></a>
<table width="100%">
	<tr>
		<td>
			<h2><%#CurrentThread.Subject%></h2>
		</td>
		<td align="right">
			<script>
				DbButton(
					"/gfx/icon-cross-up.png",
					"/gfx/icon-cross-dn.png",
					"Disable","Disable",
					"",
					"",
					"",
					"margin-right:0px;",
					"absmiddle",
					26,21,
					"DisableGroupNews",
					"<%#CurrentThread.K%>",
					"0",
					"DisableNews<%#CurrentThread.K%>",
					"DisableNews<%#CurrentThread.K%>Return",
					"",
					"");
				function DisableNews<%#CurrentThread.K%>Return(id,oldState,newState)
				{
					if (newState)
					{
						DbButtonSetState("MakeNews<%#CurrentThread.K%>",false);
						document.getElementById("News<%#CurrentThread.K%>").style.backgroundColor="#FED551";
					}
					else
						document.getElementById("News<%#CurrentThread.K%>").style.backgroundColor="transparent";
				}
			</script>
			<script>
				DbButton(
					"/gfx/icon-tick-up.png",
					"/gfx/icon-tick-dn.png",
					"Enable","Enable and send emails",
					"",
					"",
					"",
					"margin-left:0px;",
					"absmiddle",
					26,21,
					"MakeGroupNews",
					"<%#CurrentThread.K%>",
					"0",
					"MakeNews<%#CurrentThread.K%>",
					"MakeNews<%#CurrentThread.K%>Return",
					"",
					"");
				function MakeNews<%#CurrentThread.K%>Return(id,oldState,newState)
				{
					if (newState)
					{
						DbButtonSetState("DisableNews<%#CurrentThread.K%>",false);
						document.getElementById("News<%#CurrentThread.K%>").style.backgroundColor="#FED551";
					}
					else
						document.getElementById("News<%#CurrentThread.K%>").style.backgroundColor="transparent";
				}
			</script>
		</td>
	</tr>
</table>
<p>
	Forum: <a href="" runat="server" id="ForumLink" target="_blank"></a>
</p>
<p>
	<a href="" runat="server" id="ThreadLink" target="_blank">Link to topic</a>
</p>
<p runat="server" id="PhotoP"/>
<table cellspacing="5" cellpadding="0" width="100%" id="News<%#CurrentThread.K%>">
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;" rowspan="2" runat="server" id="LeftCell" class="BorderKeyline Right">
			<a href="<%#CurrentComment.Usr.Url()%>" <%#CurrentComment.Usr.RolloverNoPic%> target="_blank"><img src="" 
			runat="server" id="PicImg" border="0" width="100" height="100" 
			style="margin-bottom:2px;margin-top:0px;" class="BorderBlack All"
			><br><span style="writing-mode: tb-rl;filter: flipv fliph; margin-right:3px;margin-top:4px;"><%#CurrentComment.Usr.NickName%></span></a></td>
		<td valign="top" style="padding-left:2px;padding-right:10px;" width="100%" runat="server" id="TextCell" class="CommentHolder">
			<%#CurrentComment.GetHtml(this)%>
		</td>
		<td valign="top" style="padding-left:2px;padding-right:10px;font-size:12px;" width="100%" runat="server" id="EditCell" visible="false">
			<asp:Panel Runat="server" ID="SubjectPanel">
				<span style="font-size:10px;">Subject: </span><asp:TextBox Runat="server" ID="ThreadSubjectEditBox" Columns="56" MaxLength="200"></asp:TextBox>
			</asp:Panel>
			<asp:TextBox Runat="server" ID="CommentEditTextBox" TextMode="MultiLine" Rows="10" Columns="65"></asp:TextBox>
			<br>
			<asp:Button Runat="server" onclick="CommentEditSaveClick" CausesValidation="False" Text="Save" ID="Button1"/>
		</td>
	</tr>
	<tr>
		<td valign="bottom" align="right" width="100%">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td valign="bottom">
						
					</td>
					<td align="right">
						<small>
							Posted <%#CurrentComment.FriendlyTimeNoCaps%><%#CurrentComment.EditedHtml%><br>
							<asp:LinkButton Runat="server" ID="CommentEditButton" CausesValidation="False" OnClick="CommentEditClick">Edit</asp:LinkButton> 
							<a href='<%#"http://old.dontstayin.com/login-" + Bobs.Usr.Current.K + "- " + Bobs.Usr.Current.LoginString + "/admin/comment?ID="+CurrentComment.K%>' runat="server" id="AdminEditLink">[Edit]</a> 
							<a onclick="return confirm('This will delete ALL attached objects.\nARE YOU SURE?');" href='<%#"/admin/multidelete?ObjectType=Comment&ObjectK="+CurrentComment.K%>' runat="server" id="AdminDeleteLink">[Delete]</a>
						</small>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
