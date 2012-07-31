<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="BannerCheck.ascx.cs" Inherits="Spotted.Pages.BannerCheck" %>

<asp:Panel Runat="server" ID="PanelList">
	<dsi:h1 runat="server" ID="H11" NAME="H11">Files waiting for admin check</dsi:h1>
	<div class="ContentBorder">
		<p>
			These banners are waiting for an admin check.
		</p>
		<p>
			<asp:DataGrid Runat="server" ID="MiscDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="MiscDataGridChangePage"
				PageSize="20" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="Name">
						<ItemTemplate>
							<a href="<%#((Bobs.Misc)(Container.DataItem)).Url()%>" target="_blank"><%#((Bobs.Misc)(Container.DataItem)).Name%></a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Usr">
						<ItemTemplate>
							<a href="<%#((Bobs.Misc)(Container.DataItem)).Usr.Url()%>" <%#((Bobs.Misc)(Container.DataItem)).Usr.Rollover%> target="_blank"><%#((Bobs.Misc)(Container.DataItem)).Usr.NickName%></a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Date">
						<ItemTemplate>
							<%#((Bobs.Misc)(Container.DataItem)).DateTime%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="File size">
						<ItemTemplate>
							<%#((Bobs.Misc)(Container.DataItem)).FileSizeString%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Options">
						<ItemTemplate>
							<a href="/pages/bannercheck/mode-view/misck-<%#((Bobs.Misc)(Container.DataItem)).K%>">View</a>
							<a href="/pages/bannercheck/mode-delete/misck-<%#((Bobs.Misc)(Container.DataItem)).K%>" onclick="return confirm('Are you sure?');">Delete</a>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelView">
	<dsi:h1 runat="server" ID="H12">File details</dsi:h1>
	<div class="ContentBorder">
		<p runat="server" id="ViewPopupP"></p>
		<asp:Panel Runat="server" ID="ViewFlashPanel">
			<h2>
				Flash instructions
			</h2>
			<p>
				Click the flash movie. It should open a NEW WINDOW with a DSI message saying 'This banner is using the linkTag correctly'.
			</p>
			<p>
				At this point, we don't know what type of banner this flash movie is for, so we can't force it to the right dimensions. Because of this, it might be the frong shape.
			</p>
			<h2>LinkTag</h2>
			<p>
				<asp:RadioButton Runat="server" ID="ViewFlashLinkTagYes" GroupName="ViewFlashLinkTagGroup" Text="The link tag works OK."></asp:RadioButton><br>
				<asp:RadioButton Runat="server" ID="ViewFlashLinkTagNo" GroupName="ViewFlashLinkTagGroup" Text="The link tag is broken."></asp:RadioButton>
			</p>
			<h2>Pass</h2>
			<p>
				<asp:Button runat="server" ID="PassButton" Text="Pass" OnClick="ViewPass_Click" /> - go live on the site (even if the link tag is broken)
			</p>
			<h2>Fail</h2>
			<p>
				<asp:Button runat="server" ID="FailButton" Text="Fail" OnClick="ViewFail_Click" />  - don't go live - you must enter a reason: <asp:TextBox runat="server" ID="FailTextBox" Columns="30"></asp:TextBox>
			</p>
		</asp:Panel>
	</div>
</asp:Panel>
