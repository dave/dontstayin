<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Files.ascx.cs" Inherits="Spotted.Pages.Promoters.Files" %>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Files">
	<p>
		<a href="<%= AddFilesLink %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
			align="absmiddle" style="margin-right:3px;">upload a file</a>
	</p>
	
</dsi:PromoterIntro>

<dsi:h1 runat="server">Important instructions for designers creating banners</dsi:h1>
<div class="ContentBorder">
	<p>
		We have strict rules for animated banners, so please download the 
		instructions below and send them to your designer.	Click right-button 
		and choose "Save target as..." to download this file.
	</p>
	<p class="MedLeft">
		<a href="/misc/banners.pdf" target="_blank">Banner instructions for designers</a>
	</p>
</div>

<asp:Panel Runat="server" ID="PanelList">
	<dsi:h1 runat="server" ID="Header">Files you've uploaded</dsi:h1>
	<div class="ContentBorder">
		<p runat="server" id="MiscNoFilesP">
			You have no files uploaded. Click below to upload some.
		</p>
		<p runat="server" id="MiscDataGridP">
			<asp:DataGrid Runat="server" ID="MiscDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="MiscDataGridChangePage"
				PageSize="20" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="DSI ref">
						<ItemTemplate>
							file-<%#((Bobs.Misc)(Container.DataItem)).K%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Name">
						<ItemTemplate>
							<a href="<%#((Bobs.Misc)(Container.DataItem)).Promoter.UrlApp("files","mode","view","miscK",((Bobs.Misc)(Container.DataItem)).K.ToString())%>"><%#((Bobs.Misc)(Container.DataItem)).Name%></a> <small><%#((Bobs.Misc)(Container.DataItem)).DateTime%></small>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="File size">
						<ItemTemplate>
							<%#((Bobs.Misc)(Container.DataItem)).FileSizeString%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Options">
						<ItemTemplate>
							<nobr><a href="<%#((Bobs.Misc)(Container.DataItem)).Promoter.UrlApp("files","mode","view","miscK",((Bobs.Misc)(Container.DataItem)).K.ToString())%>">Details</a> | 
							<a href="<%#((Bobs.Misc)(Container.DataItem)).Promoter.UrlApp("files","mode","delete","miscK",((Bobs.Misc)(Container.DataItem)).K.ToString())%>" onclick="return confirm('Are you sure?');">delete</a></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
		<p class="MedCenter">
			<a href="<%= AddFilesLink %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">upload a file</a>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelUpload">
	<dsi:h1 runat="server" ID="H11">Upload new files</dsi:h1>
	<div class="ContentBorder">
		
		<p>
			<b>
				These files are ONLY for linking on www.dontstayin.com, or for banner adverts on www.dontstayin.com. 
				Please do NOT link to these files from other sites. 
			</b>
		</p>
		<p>
			<input type="file" runat="server" id="InputFile" size="40" style="height:18px;" /> 
			<asp:Button runat="server" OnClick="UploadNow_Click" Text="Upload" />
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelView">
	<dsi:h1 runat="server" ID="H12">File details</dsi:h1>
	<div class="ContentBorder">
		<p>
			<style>
				.dataGrid th{
					text-align:right;
					padding:5px;
					vertical-align:top;
				}
				.dataGrid td{
					vertical-align:top;
					padding:5px;
				}
			</style>
			<table cellpadding="3" cellspacing="0" class="dataGrid">
				<tr>
					<th><nobr>Original filename:</nobr></th>
					<td colspan="2"><a href="" runat="server" id="ViewNameAnchor" target="_blank"></a></td>
				</tr>
				<tr class="dataGridAltItem">
					<th>Url for linking:</th>
					<td colspan="2">
						<asp:TextBox Runat="server" ID="ViewUrlTextBox" Columns="85"/>
						<p>
							<b>
								This file is ONLY for use on www.dontstayin.com. 
								Please do NOT link to this file from other sites.
							</b>
						</p>
					</td>
				</tr>
				<tr runat="server" id="ViewImageHtmlTr">
					<th>Image tag:</th>
					<td colspan="2">
						<asp:TextBox Runat="server" ID="ViewImageHtmlTextBox" Columns="75" TextMode="MultiLine" Rows="3"/>
						<p>
							Paste the html tag above into a discussion post 
							or event description, and your image will appear on the page.
						</p>
						<p>
							<b>
								This file is ONLY for use on www.dontstayin.com. 
								Please do NOT link to this file from other sites.
							</b>
						</p>
					</td>
				</tr>
				<tr>
					<th>File size:</th>
					<td runat="server" id="ViewImageFileSizeCell"></td>
					<td>&nbsp;</td>
				</tr>
				<tbody runat="server" id="ViewBannerBody">
					<tbody runat="server" id="ViewImageBody">
						<tr class="dataGridAltItem">
							<th>Image width:</th>
							<td runat="server" id="ViewImageWidthCell"></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<th>Image height:</th>
							<td runat="server" id="ViewImageHeightCell"></td>
							<td>&nbsp;</td>
						</tr>
					</tbody>
					<tr class="dataGridAltItem">
						<th>Admin check:</th>
						<td><img src="~/gfx/icon-tick-up.png" runat="server" id="ViewBrokenImg"></td>
						<td><asp:Label Runat="server" ID="ViewBrokenLabel"></asp:Label></td>
					</tr>
					<tr>
						<th>Banner usage - leaderboard:</th>
						<td><img src="~/gfx/icon-tick-up.png" runat="server" id="ViewLeaderboardImg"></td>
						<td><asp:Label Runat="server" ID="ViewLeaderboardLabel"></asp:Label></td>
					</tr>
					<tr class="dataGridAltItem">
						<th>Banner usage - hotbox:</th>
						<td><img src="~/gfx/icon-tick-up.png" runat="server" id="ViewHotboxImg"></td>
						<td><asp:Label Runat="server" ID="ViewHotboxLabel"></asp:Label></td>
					</tr>
					<tr>
						<th>Banner usage - skyscraper:</th>
						<td><img src="~/gfx/icon-tick-up.png" runat="server" id="ViewSkyscraperImg"></td>
						<td><asp:Label Runat="server" ID="ViewSkyscraperLabel"></asp:Label></td>
					</tr>
					<tr class="dataGridAltItem">
						<th>Banner usage - photo banner:</th>
						<td><img src="~/gfx/icon-tick-up.png" runat="server" id="ViewPhotoBannerImg"></td>
						<td><asp:Label Runat="server" ID="ViewPhotoBannerLabel"></asp:Label></td>
					</tr>
					<tr>
						<th>Banner usage - email banner:</th>
						<td><img src="~/gfx/icon-tick-up.png" runat="server" id="ViewEmailBannerImg"></td>
						<td><asp:Label Runat="server" ID="ViewEmailBannerLabel"></asp:Label></td>
					</tr>
					<tr runat="server" id="RequiredFlashVersionTr" class="dataGridAltItem">
						<th>Advanced</th>
						<td>&nbsp;</td>
						<td>
							Required flash player version -
							<asp:TextBox runat="server" ID="RequiredFlashVersion" MaxLength="15" Columns="10" /> 
							<button runat="server" onserverclick="UpdateFlashVersion">Save</button> 
							<asp:Label runat="server" ID="UpdateFlashVersionDone" ForeColor="blue"></asp:Label><br />
							<small>
								If your flash movie requires a higher flash version that version 7, please enter the minimum required version here. Leave this blank if you're unsure.
							</small>
							<br /><br />
							Non-standard size - width: <asp:TextBox runat="server" ID="SizeWidth" MaxLength="5" Columns="8" /> height: <asp:TextBox runat="server" ID="SizeHeight" MaxLength="5" Columns="8" />
							<button id="Button1" runat="server" onserverclick="UpdateSize">Save</button> 
							<asp:Label runat="server" ID="UpdateSizeDone" ForeColor="blue"></asp:Label><br />
							<small>
								This is for when your flash artwork has a non-standard size (smaller than the default banner size). Entering a size in here will prevent the flash from being blown up to the size of the banner.
							</small>
						</td>
					</tr>
				</tbody>
			</table>
		</p>
		<p class="MedCenter">
			<a href="" runat="server" id="ViewBackAnchor">Back to file list</a>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelDelete">
	<dsi:h1 runat="server" ID="H13">Delete file</dsi:h1>
	<div class="ContentBorder">
		<p>
			You can't delete this file because it's needed. It's in use by a current or future banner.
		</p>
	</div>
</asp:Panel>
