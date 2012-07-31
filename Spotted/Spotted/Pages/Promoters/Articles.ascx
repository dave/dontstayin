<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Articles.ascx.cs" Inherits="Spotted.Pages.Promoters.Articles" %>


<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Promoter articles">
	<p>
		This is the promoter articles page. It lists all the articles added about your events:
	</p>
	<p>
		<a href="<%= Bobs.UrlInfo.PageUrl("myarticles", "mode", "add") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
			align="absmiddle" style="margin-right:3px;">add an article</a>
	</p>
</dsi:PromoterIntro>

<asp:Panel Runat="server" ID="ArticlePanel">
	<a name="ArticlePanel"/>
	<dsi:h1 runat="server" ID="H110" NAME="H18">Articles</dsi:h1>
	<div class="ContentBorder" style="padding-left:0px;padding-right:0px;">
		<p>
			<asp:DataGrid Runat="server" ID="ArticleDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="ArticleDataGridChangePage"
				PageSize="20" PagerStyle-Mode="NumericPages" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Pic" ItemStyle-BorderWidth="0">
						<ItemTemplate>
							<%#Bobs.Promoter.PicHtml((Bobs.Article)(Container.DataItem))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Subject">
						<ItemTemplate>
							<a href="<%#((Bobs.Article)(Container.DataItem)).Url()%>"><%#((Bobs.Article)(Container.DataItem)).Title%></a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Author">
						<ItemTemplate>
							<%#((Bobs.Article)(Container.DataItem)).Owner.Link()%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Pub-<br>lished">
						<ItemTemplate>
							<img src="<%#(((Bobs.Article)(Container.DataItem)).Status.Equals(Bobs.Article.StatusEnum.Edit) || ((Bobs.Article)(Container.DataItem)).Status.Equals(Bobs.Article.StatusEnum.Enabled))?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Ena-<br>bled">
						<ItemTemplate>
							<img src="<%#((Bobs.Article)(Container.DataItem)).Status.Equals(Bobs.Article.StatusEnum.Enabled)?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Com-<br>ments">
						<ItemTemplate>
							<%#((Bobs.Article)(Container.DataItem)).TotalComments%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Options">
						<ItemTemplate>
							<nobr>
								<a href="/pages/myarticles/mode-edit/k-<%#((Bobs.Article)(Container.DataItem)).K%>">Edit</a>
							</nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
		<p class="MedCenter">
			<a href="<%= Bobs.UrlInfo.PageUrl("myarticles", "mode", "add") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">add an article</a>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="NoArticlePanel">
	<dsi:h1 runat="server" ID="H111" NAME="H18">No articles!</dsi:h1>
	<div class="ContentBorder">
		<p>
			<img src="/gfx/icon-warning.png" border="0" width="26" height="21" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You haven't posted any articles! It's FREE to post articles!
		</p>
		<p class="MedCenter">
			<a href="<%= Bobs.UrlInfo.PageUrl("myarticles", "mode", "add") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">add an article</a>
		</p>
	</div>
</asp:Panel>
	
