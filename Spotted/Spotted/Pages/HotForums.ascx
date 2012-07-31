<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotForums.ascx.cs" Inherits="Spotted.Pages.HotForums" %>


<dsi:h1 runat="server" ID="H11">Discussions</dsi:h1>

<asp:Panel Runat="server" ID="PanelBoardList" EnableViewState="False">
	<div class="ContentBorder">
		<asp:Panel runat="server" id="HotTopicsWorldwidePanel">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;">Showing hot forums worldwide. 
				<a href="" runat="server" id="HotTopicsHomeCountryLink">We can restrict this to ???</a>.
			</p>
		</asp:Panel>
		<asp:Panel runat="server" id="HotTopicsCountryPanel">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;">Showing hot forums in <a href="" runat="server" id="HotTopicsCountryLink"></a>. 
				We can show <a href="/pages/hotforums">hot forums worldwide</a>.
			</p>
		</asp:Panel>
		
		<asp:Panel Runat="server" ID="BoardPlacePanel">
			<a name="HotPlaces"></a>
			<h2>Hot places</h2>
			<p>
				<asp:DataGrid Runat="server" ID="BoardPlaceDataGrid" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass=dataGrid 
						HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign=Top
						width="100%" >
					<Columns>
						<asp:TemplateColumn HeaderText="Discussion" ItemStyle-CssClass=dataGridThreadTitles ItemStyle-Width="100%">
							<ItemTemplate>
								<a href="<%#((Bobs.Place)Container.DataItem).UrlDiscussion()%>"><%#((Bobs.Place)Container.DataItem).Name%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Comments" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<nobr><small><%#((Bobs.Place)Container.DataItem).TotalComments.ToString("#,##0")%></small></nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Last&nbsp;post" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<nobr><small><%#((Bobs.Place)Container.DataItem).LastPostFriendlyTime(true)%></small></nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
		</asp:Panel>
		
		<asp:Panel Runat="server" ID="BoardEventPanel">
			<a name="HotEvents"></a>
			<h2>Hot events</h2>
			<p>
				<asp:DataGrid Runat="server" ID="BoardEventDataGrid" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass=dataGrid 
						HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign=Top 
						width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="Discussion" ItemStyle-CssClass=dataGridThreadTitles ItemStyle-Width="100%">
							<ItemTemplate>
								<%#((Bobs.Event)Container.DataItem).TitleNoteHtml%><a href="<%#((Bobs.Event)Container.DataItem).UrlDiscussion()%>"><%#((Bobs.Event)Container.DataItem).Name%></a> <small><%#((Bobs.Event)Container.DataItem).FriendlyDate(false)%></small>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Comments" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<nobr><small><%#((Bobs.Event)Container.DataItem).TotalComments.ToString("#,##0")%></small></nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Last&nbsp;post" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<nobr><small><%#((Bobs.Event)Container.DataItem).LastPostFriendlyTime(true)%></small></nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
		</asp:Panel>
		
		<asp:Panel Runat="server" ID="BoardThreadPanel">
			<a name="HotTopics"></a>
			<h2>Hot topics</h2>
			<p>
				<asp:DataGrid Runat="server" ID="BoardThreadDataGrid" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass=dataGrid 
						HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign=Top 
						width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="Subject" ItemStyle-CssClass=dataGridThreadTitles ItemStyle-Width="100%">
							<ItemTemplate>
								<a href="<%#((Bobs.Thread)Container.DataItem).UrlDiscussion()%>" <%#((Bobs.Thread)Container.DataItem).Rollover%>><%#((Bobs.Thread)Container.DataItem).SubjectSafe%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Comments" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<nobr><small><%#((Bobs.Thread)Container.DataItem).TotalComments.ToString("#,##0")%></small></nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Last&nbsp;post" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<nobr><small><%#((Bobs.Thread)Container.DataItem).LastPostFriendlyTime(true)%></small></nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
		</asp:Panel>
	</div>

</asp:Panel>
