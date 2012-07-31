<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerFolders.ascx.cs" Inherits="Spotted.Pages.Promoters.BannerFolders" %>
<%@ Register src="../../Controls/PaginationControl.ascx" tagname="PaginationControl" tagprefix="uc1" %>

<dsi:PromoterIntro runat="server" ID="Promoterintro1" Header="Banners">
		<p>
			<a href="<%= CurrentPromoter.UrlApp("banneredit","mode","add") %>"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
				align="absmiddle" style="margin-right:3px;">add a banner</a>
		</p>
		<p>
			<a href="<%= CurrentPromoter.UrlApp("bannerspending") %>"><img src="/gfx/icon-document-tick.png" border="0" width="26" height="21"
				align="absmiddle" style="margin-right:3px;">book pending banners</a>
		</p>
		<p>
			<b>All your banners are listed below:</b>
		</p>
	</dsi:PromoterIntro>
<dsi:h1 runat="server" ID="BannerListHeader">Banner folders for promoter: ???</dsi:h1>
<asp:Panel ID="pnlContent" runat="server" CssClass="ContentBorder">
	<asp:Repeater runat="server" ID="uiBannerFolderRepeater">
	
		<ItemTemplate>
			<br />
			<div>
			<div style="cursor:pointer;font-size:12px" onclick='<%# "uiPlusMinusIcon_click(" + (Container as RepeaterItem).FindControl("uiPlusMinusIcon").ClientID + ", " + (Container as RepeaterItem).FindControl("uiChildrenPanel").ClientID + ");"%>'><asp:Image ID="uiPlusMinusIcon" runat="server"  ImageUrl="/Gfx/plus.gif"></asp:Image>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
			</div>
			<div style="margin: 3px; padding-left:10px">
				banners: <asp:Label ID="Label4" runat="server"  Text='<%# String.Format("{0:#,0}", (DataBinder.Eval(Container.DataItem, "Banners") as BannerSet).Count) %>'></asp:Label>,
				impressions: <asp:Label ID="Label1" runat="server"  Text='<%# String.Format("{0:#,0}", ((Int32) DataBinder.Eval(Container.DataItem, "TotalHits"))) %>'></asp:Label>,
				clicks: <asp:Label ID="Label2" runat="server"		Text='<%# String.Format("{0:#,0}", ((int) DataBinder.Eval(Container.DataItem, "TotalClicks"))) %>'></asp:Label>,
				click rate: <asp:Label ID="Label3" runat="server"	Text='<%# ((double) DataBinder.Eval(Container.DataItem, "ClickRate")).ToString("P") %>'></asp:Label>
			</div>
			<asp:Panel runat="server" ID="uiChildrenPanel" style="display:none;padding-left:12px">
			<asp:DataGrid BorderWidth=0 CssClass="dataGrid" CellPadding=3 runat="server" ID="uiBannersDataGrid" DataSource='<%# DataBinder.Eval(Container.DataItem, "Banners") %>' AutoGenerateColumns="false" ItemStyle-CssClass="dataGridAltItem" AlternatingItemStyle-CssClass="" HeaderStyle-CssClass="dataGridHeader">
				<Columns>
					<asp:TemplateColumn HeaderText="Name" ItemStyle-Width="180">
						<ItemTemplate>
						<asp:HyperLink ID="uiBannerLink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' NavigateUrl='<%# ((Banner)Container.DataItem).OptionsUrl() %>'></asp:HyperLink></ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Details" ItemStyle-Width="110">
						<ItemTemplate><nobr><span style="width:32px">from</span><%# ((DateTime) DataBinder.Eval(Container.DataItem, "FirstDay")).ToString("ddd dd MMM yy") %></nobr><br/><nobr><span style="width:32px">to</span><%# ((DateTime)DataBinder.Eval(Container.DataItem, "LastDay")).ToString("ddd dd MMM yy")%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Credits" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30">
						<ItemTemplate>
							<nobr><%#((Bobs.Banner)Container.DataItem).CampaignCredits.ToString()%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="<nobr>Total hits</nobr>" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50">
						<ItemTemplate>
							<nobr><%# String.Format("{0:#,0}", ((Int32) DataBinder.Eval(Container.DataItem, "TotalHits"))) %></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="<nobr>Total clicks</nobr>" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="55">
						<ItemTemplate>
							<nobr><%# String.Format("{0:#,0}", ((Int32) DataBinder.Eval(Container.DataItem, "TotalClicks"))) %></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="<nobr>Click rate</nobr>" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50">
						<ItemTemplate>
							<nobr><%# ((double)DataBinder.Eval(Container.DataItem, "ClickRate")).ToString("P")%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Booked" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="32">
					<ItemTemplate>
							<center><%# Utilities.TickCrossHtml((bool) DataBinder.Eval(Container.DataItem, "StatusBooked")) %></center>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Ready" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="28">
					<ItemTemplate>
							<%# Utilities.TickCrossHtml((bool) DataBinder.Eval(Container.DataItem, "IsReady")) %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Live" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="26">
						<ItemTemplate>
							<%# Utilities.TickCrossHtml((bool) DataBinder.Eval(Container.DataItem, "IsLive")) %>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			</asp:Panel>
			</div>
			
		</ItemTemplate>
		
			
		
	</asp:Repeater>
	<br />
	<uc1:PaginationControl ID="uiPaginationControl" runat="server"/>
</asp:Panel>
	<script>
	
		function uiPlusMinusIcon_click(sender, el){
			sender.src = sender.src.indexOf("/Gfx/plus.gif") == -1 ? "/Gfx/plus.gif" : "/Gfx/minus.gif";
			el.style.display = el.style.display == '' ? 'none' : '';
		}
	
	</script>
	
