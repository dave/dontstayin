<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact.ascx.cs" Inherits="Spotted.Pages.Contact" %>


<dsi:h1 runat="server" ID="H11" NAME="H12">General questions</dsi:h1>
<div class="ContentBorder">
	<p>
		If you've got a general question about DontStayIn, please ask in 
		the DontStayIn website group:
	</p>
	<p class="BigCenter">
		<a href="/groups/dontstayin-website">General questions?</a>
	</p>
</div>

<dsi:h1 runat="server" ID="H14" NAME="H14">Event moderators</dsi:h1>
<div class="ContentBorder">
	<p>
		Event moderators keep our database in shape. They can do everything the photo moderators can, plus:
	</p>
	<p class="BigCenter">
		Update event / venue details
	</p>
	<p class="BigCenter">
		Merge duplicate events / venues
	</p>
	<p class="BigCenter">
		Merge duplicate groups
	</p>
	<p class="BigCenter">
		Delete events / venues
	</p>
	<p>
		<asp:DataList Runat="server" ID="SuperAdminDataList" RepeatColumns="4" CellSpacing="10"
			RepeatLayout="Table" RepeatDirection="Horizontal" Width="100%" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center" />
	</p>
</div>

<dsi:h1 runat="server" ID="H12" NAME="H11">Photo moderators</dsi:h1>
<div class="ContentBorder">
	<p>
		Our photo moderators check photos and videos as they are uploaded. 
		They can do everything the chat moderators can, plus:
	</p>
	<p class="BigCenter">
		Enable photos / videos
	</p>
	<p class="BigCenter">
		Delete photos / videos
	</p>
	<p>
		<asp:DataList Runat="server" ID="SeniorAdminDataList" RepeatColumns="4" CellSpacing="10"
			RepeatLayout="Table" RepeatDirection="Horizontal" Width="100%" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center"/>
	</p>
</div>

<dsi:h1 runat="server" ID="H1ghd4" NAME="H11">Chat moderators</dsi:h1>
<div class="ContentBorder">
	<p>
		Our chat moderators are in charge of the chat forums. They can:
	</p>
	<p class="BigCenter">
		Delete chat posts
	</p>
	<p class="BigCenter">
		Delete spam
	</p>
	<p class="BigCenter">
		Ban people
	</p>
	<p>
		<asp:DataList Runat="server" ID="JuniorAdminDataList" RepeatColumns="4" CellSpacing="10"
			RepeatLayout="Table" RepeatDirection="Horizontal" Width="100%" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center"/>
	</p>
</div>


<dsi:h1 runat="server" ID="H13" NAME="H11">Admins</dsi:h1>
<div class="ContentBorder">
	<p>
		I've moved the admins section to the bottom of the page in an 
		attempt to reduce the volume of questions we get. If you have 
		a question about the site, please ask one of our moderators 
		first.
	</p>
	<p>
		<b>
			If you're interested in advertising on DontStayIn, click the 
			"Promoters" button at the top and complete your details.
		</b>
	</p>
	
	<p>
		<table cellpadding=3 cellspacing=0 border=0 width="100%">
			<tr>
				<td align="center" width="20%">
					&nbsp;
				</td>
				<td align="center" width="30%">
					<!--Jason-DSI--><a href="<%= User(378651).Url() %>"><img src="<%= User(378651).AnyPicPath %>" <%= User(378651).RolloverNoPic %> style="border:1px solid #000000;" border="0" width="100" height="100"></a>
				</td>
				<td align="center" width="30%">
					<!--Owain-DSI--><a href="<%= User(421097).Url() %>"><img src="<%= User(421097).AnyPicPath %>" <%= User(421097).RolloverNoPic %> style="border:1px solid #000000;" border="0" width="100" height="100"></a>
				</td>
				<td align="center" width="20%">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td align="center">
					&nbsp;
				</td>
				<td align="center">
					<!--Jason-DSI--><%= User(378651).FullName %>
				</td>
				<td align="center">
					<!--Owain-DSI--><%= User(421097).FullName %>
				</td>
				<td align="center">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td align="center">
					&nbsp;
				</td>
				<td align="center">
					<!--Jason-DSI--><a href="<%= User(378651).Url() %>" <%= User(378651).RolloverNoPic %>><%= User(378651).NickName %></a>
				</td>
				<td align="center">
					<!--Owain-DSI--><a href="<%= User(421097).Url() %>" <%= User(421097).RolloverNoPic %>><%= User(421097).NickName %></a>
				</td>
				<td align="center">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td align="center">
					&nbsp;
				</td>
				<td align="center">
					<!--Jason-DSI--><%= User(378651).Email.Replace("@dontstayin.com", "") %><img src="/gfx/email.gif" align="absmiddle" width="93" height="10" style="margin-left:1px;">
				</td>
				<td align="center">
					<!--Owain-DSI--><%= User(421097).Email.Replace("@dontstayin.com", "") %><img src="/gfx/email.gif" align="absmiddle" width="93" height="10" style="margin-left:1px;">
				</td>
				<td align="center">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td align="center">
					&nbsp;
				</td>
				<td align="center" valign="top">
					<small>
						<!--Jason-DSI-->
						Sales executive<br />
						Advertiser account management
					</small>
				</td>
				<td align="center" valign="top">
					<small>
						<!--Owain-DSI-->
						Sales executive<br />
						Advertiser account management
					</small>
				</td>
				<td align="center">
					&nbsp;
				</td>
			</tr>
		</table>
	</p>
	
	
	<p>
		<table cellpadding=3 cellspacing=0 border=0 width="100%">
			<tr>
				<td align="center" width="25%">
					&nbsp;
				</td>
				<td align="center" width="25%">
					<!--DaveB-DSI--><a href="<%= User(4).Url() %>"><img src="<%= User(4).AnyPicPath %>" <%= User(4).RolloverNoPic %> style="border:1px solid #000000;" border="0" width="100" height="100"></a>
				</td>
				<td align="center" width="25%">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td align="center">
					&nbsp;
				</td>
				<td align="center">
					<!--DaveB-DSI--><%= User(4).FullName %> 
				</td>
				<td align="center">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td align="center">
					&nbsp;
				</td>
				<td align="center">
					<!--DaveB-DSI--><a href="<%= User(4).Url() %>" <%= User(4).RolloverNoPic %>><%= User(4).NickName %></a>
				</td>
				<td align="center">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td align="center">
					&nbsp;
				</td>
				<td align="center">
					<!--DaveB-DSI-->d.brophy<img src="/gfx/email.gif" align="absbottom" width="93" height="10" style="margin-left:1px;">
				</td>
				<td align="center">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td align="center">
					&nbsp;
				</td>
				<td align="center" valign="top">
					<small>
						<!--DaveB-DSI-->
						Site development
					</small>
				</td>
				<td align="center">
					&nbsp;
				</td>
			</tr>
		</table>
	</p>
	
	
</div>
