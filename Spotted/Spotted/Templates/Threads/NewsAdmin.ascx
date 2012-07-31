<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsAdmin.ascx.cs" Inherits="Spotted.Templates.Threads.NewsAdmin" %>
<hr>
<a name="CommentK-<%#CurrentComment.K%>"></a>
<p>
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
			"DisableNews",
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
	<input type="radio" value="10" onclick="DbButtonSetArgs('MakeNews<%#CurrentThread.K%>','<%#CurrentThread.K%>,10');" onmouseover="stt(html10);" onmouseout="htm();" name="ThreadRadio<%#CurrentThread.K%>">10
	<input type="radio" value="20" onclick="DbButtonSetArgs('MakeNews<%#CurrentThread.K%>','<%#CurrentThread.K%>,20');" onmouseover="stt(html20);" onmouseout="htm();" name="ThreadRadio<%#CurrentThread.K%>">20
	<input type="radio" value="30" onclick="DbButtonSetArgs('MakeNews<%#CurrentThread.K%>','<%#CurrentThread.K%>,30');" onmouseover="stt(html30);" onmouseout="htm();" name="ThreadRadio<%#CurrentThread.K%>">30
	<input type="radio" value="40" onclick="DbButtonSetArgs('MakeNews<%#CurrentThread.K%>','<%#CurrentThread.K%>,40');" onmouseover="stt(html40);" onmouseout="htm();" name="ThreadRadio<%#CurrentThread.K%>">40
	<input type="radio" value="50" onclick="DbButtonSetArgs('MakeNews<%#CurrentThread.K%>','<%#CurrentThread.K%>,50');" onmouseover="stt(html50);" onmouseout="htm();" name="ThreadRadio<%#CurrentThread.K%>">50
	<input type="radio" value="60" onclick="DbButtonSetArgs('MakeNews<%#CurrentThread.K%>','<%#CurrentThread.K%>,60');" onmouseover="stt(html60);" onmouseout="htm();" name="ThreadRadio<%#CurrentThread.K%>">60
	<script>
		DbButton(
			"/gfx/icon-tick-up.png",
			"/gfx/icon-tick-dn.png",
			"Enable","Enable",
			"",
			"",
			"",
			"margin-left:0px;",
			"absmiddle",
			26,21,
			"MakeNews",
			"<%#CurrentThread.K%>,0",
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
</p>
<h2>
	<%#CurrentThread.Subject%>
</h2>
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
			<div style="width:448px;overflow:hidden;">
				<%#CurrentComment.GetHtml(this)%>
			</div>
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
