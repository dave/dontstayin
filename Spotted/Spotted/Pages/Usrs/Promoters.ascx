<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Promoters.ascx.cs" Inherits="Spotted.Pages.Usrs.Promoters" %>

<asp:Panel Runat="server" ID="PanelPromoterList">
	<dsi:h1 runat="server" ID="H12">Promoters</dsi:h1>
	<div class="ContentBorder">
		<p style="font-size:20px;">
			<b>If you don't know the caller, make sure you confirm at least TWO of the following:</b>
		</p>	
		<ul>
			<li style="font-size:12px;">DSI nickname: <b><%= ThisUsr.NickName %></b></li>
			<li style="font-size:12px;">Real name: <b><%= ThisUsr.FullName %></b></li>
			<li style="font-size:12px;">Email: <b><%= ThisUsr.Email %></b></li>
			<li style="font-size:12px;">Date of birth: <b><%= ThisUsr.DateOfBirth.ToShortDateString() %></b></li>
		</ul>
				
				
		<h2>
			Promoter accounts for <%= ThisUsr.Link() %>:
		</h2>
		<ul>
			<asp:Repeater Runat="server" ID="PromoterRepeater">
				<ItemTemplate>
					<li style="font-size:12px;"><a href="<%#((Bobs.Promoter)(Container.DataItem)).Url()%>"><%#((Bobs.Promoter)(Container.DataItem)).Name%></a></li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelNoAccount">
	<dsi:h1 runat="server" ID="H1bv2">Promoters</dsi:h1>
	<div class="ContentBorder">
		<p>
			This user is not in any promoter accounts
		</p>
	</div>
</asp:Panel>
