<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="BannerOptions.ascx.cs" Inherits="Spotted.Pages.Promoters.BannerOptions" %>
<%@ Register TagPrefix="Controls" TagName="MusicTypes" Src="/Controls/MusicTypes.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>
<%@ Import NameSpace="Bobs" %>
<style>
	h2.BannerEditHead
	{
		/*margin-top:20px;*/
	}
	table.BannerEditTable
	{
	}
	td.BannerEditLabelCell
	{
		width:100px;
		text-align:right;
		vertical-align:top;
		padding-top:6px;
		color:#A58319;
	}
	td.BannerEditControlCell
	{
	}
</style>
<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Banners">
	<p>
		This is the banner options page. You can use it to add or change a 
		banner. You can get back at any time:
	</p>
	<p style="font-weight:bold;font-size:14px;">
		<a href="" runat="server" id="IntroBannerListLink">Back to banner list</a> <span runat="server" id="IntroEventOptionsSpan" visible="false">| <a href="" runat="server" id="IntroEventOptionsAnchor">event options page</a></span>
	</p>
</dsi:PromoterIntro>

<asp:Panel Runat="server" ID="PanelDelete">
	<dsi:h1 runat="server" ID="H119">Delete a banner</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:Label Runat="server" ID="DeleteLabel"></asp:Label>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelEdit">
	
	<asp:Panel runat="server" ID="AdminPanel">
		<dsi:h1 runat="server" ID="H1ssadsf3">Admin-only options</dsi:h1>
		<div class="ContentBorder" style="background-color:#FED551;">
			<asp:Panel runat="server" ID="PausePanel">
				<p>
					This banner is currently live.
				</p>
				<p>
					<asp:LinkButton runat="server" ID="PauseButton" CausesValidation="false" OnClick="PauseButton_Click"><%=Utilities.IconHtml(Utilities.Icon.Pause)%>Pause this banner</asp:LinkButton>
				</p>
			</asp:Panel>
			<asp:Panel runat="server" ID="ResumePanel">
				<p>
					This banner is currently paused.
				</p>
				<p>
					<asp:LinkButton runat="server" ID="ResumeButton" CausesValidation="false" OnClick="ResumeButton_Click"><%=Utilities.IconHtml(Utilities.Icon.Resume)%>Resume this banner</asp:LinkButton>
				</p>
			</asp:Panel>
			<p>
				<a href="<%= CurrentPromoter.UrlApp("bannertargetting", "bannerk", CurrentBanner.K.ToString()) %>">Advanced targetting options</a>
			</p>
		</div>
	</asp:Panel>
	
	<dsi:h1 runat="server" ID="H116">Banner advert options</dsi:h1>
	<div class="ContentBorder">
		<p>
			Your banner will <b>not go live</b> if there are any crosses below. You must 
			make sure each section is ticked before the banner will start.
		</p>
		<div style="margin:5px;padding:5px;border: 2px solid #ff0000;padding-left:10px;padding-right:10px;" runat="server" visible="false">
			<p>
				Targetting is one of the most important things to think about when setting up 
				your banners. A lot of promoters ignore it and their banners are not as effective.
			</p>
			<p>
				<b>IF YOU ARE ADVERTISING A LONDON FUNKY HOUSE EVENT, THERE'S NO POINT SHOWING 
				YOUR BANNERS TO SCOTTISH HIP-HOP FANS!</b>
			</p>
			<p>
				Use the targetting section below to tell us who should see your banners.
			</p>
		</div>
		
		<asp:Panel Runat="server" ID="EditPreviewPanel">
			<h2>
				Preview
			</h2>
			<div style="margin-left:6px;width:571px;overflow:auto;padding:20px;border:1px solid #CBA21E;" runat="server" id="EditPreviewOuterDiv">
				<div class="BorderBlack All" runat="server" ID="EditPreviewDiv"></div>
			</div>
		</asp:Panel>
		<asp:Panel Runat="server" ID="EditNewPreviewPanel">
			<h2>
				Preview of banner waiting for admin check
			</h2>
			<div style="margin-left:6px;width:571px;overflow:auto;padding:20px;border:1px solid #CBA21E;" runat="server" id="EditNewPreviewOuterDiv">
				<div class="BorderBlack All" runat="server" id="EditNewPreviewDiv"></div>
			</div>
		</asp:Panel>
		<asp:Panel Runat="server" ID="BannerStatPanel">
			<h2>Current stats</h2>
			<p>
				<b>Total hits:</b> <asp:Label Runat="server" ID="EditTotalHitsLabel"></asp:Label>
			</p>
			<p>
				<b>Total clicks:</b> <asp:Label Runat="server" ID="EditTotalClicksLabel"></asp:Label> - that's a <asp:Label Runat="server" ID="EditTotalClicksPercentageLabel"></asp:Label> clickthrough rate.
			</p>
			<h2>Stats by day</h2>
			<p>
				<asp:DataGrid Runat="server" ID="BannerStatDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=0 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top">
					<Columns>
						<asp:TemplateColumn HeaderText="Date">
							<ItemTemplate>
								<%#Cambro.Misc.Utility.FriendlyDate(((Bobs.BannerStat)(Container.DataItem)).Date,true,false)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Hits" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.BannerStat)(Container.DataItem)).Hits.ToString("#,##0")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Clicks" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.BannerStat)(Container.DataItem)).Clicks.ToString("#,##0")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Clickthrough rate" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<span style="margin-right:28px;"></div><%#((double)((Bobs.BannerStat)(Container.DataItem)).Clicks / (double)((Bobs.BannerStat)(Container.DataItem)).Hits).ToString("0.00%")%></span>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
		</asp:Panel>
		<h2>Banner details</h2>
		<p>
			<table cellpadding="3" cellspacing="0">
				<tr>
					<th>Your reference:</th>
					<td runat="server" id="EditNameCell" colspan="2" class="dataGrid"></td>
					<td class="dataGrid">
						<asp:Button Runat="server" Text="Change..." OnClick="Edit_Click" ID="Button8" CausesValidation="False"/>
					</td>
				</tr>
				<tr class="dataGridAltItem">
					<th>Our reference:</th>
					<td runat="server" id="EditKCell" colspan="2" class="dataGrid"></td>
					<td class="dataGrid">&nbsp;</td>
				</tr>
				<tr>
					<th>Banner position:</th>
					<td runat="server" id="EditPositionCell" class="dataGrid"></td>
					<td class="dataGrid"><img src="/gfx/icon-tick-up.png"></td>
					<td class="dataGrid">
						<img src="~/gfx/icon-lock.png" alt="Locked" runat="server" visible="false" id="EditPositionLock"><asp:Button Runat="server" Text="Change..." OnClick="Edit_Click" ID="EditPositionChange" CausesValidation="False"/>
					</td>
				</tr>
				<tr class="dataGridAltItem">
					<th>Artwork:</th>
					<td runat="server" id="EditArtworkCell" class="dataGrid"></td>
					<td class="dataGrid"><img src="/gfx/icon-tick-up.png"></td>
					<td class="dataGrid">
						<img src="~/gfx/icon-lock.png" alt="Locked" runat="server" visible="false" id="EditArtworkLock" align="absmiddle">
						<asp:Button Runat="server" Text="Change..." OnClick="Edit_Click" ID="EditArtworkChange" CausesValidation="False"/>
						<asp:Button Runat="server" Text="Customise..." OnClick="Edit_Click" ID="EditArtworkCustomise" Visible="False" CausesValidation="False"/>
					</td>
				</tr>
				<tr>
					<th>Link target:</th>
					<td runat="server" id="EditLinkTargetCell" class="dataGrid"></td>
					<td class="dataGrid"><img src="/gfx/icon-tick-up.png"></td>
					<td class="dataGrid">
						<asp:Button ID="Button1" Runat="server" Text="Change..." OnClick="Edit_Click" CausesValidation="False"/>
					</td>
				</tr>
				<tr class="dataGridAltItem">
					<th>Dates:</th>
					<td runat="server" id="EditDatesCell" class="dataGrid"></td>
					<td class="dataGrid"><img src="~/gfx/icon-tick-up.png" runat="server" id="EditDatesTick"></td>
					<td class="dataGrid">
						<img src="~/gfx/icon-lock.png" alt="Locked" runat="server" visible="false" id="EditDatesLock">
						<asp:Button Runat="server" Text="Change..." OnClick="Edit_Click" ID="EditDatesChange" CausesValidation="False"/>
					</td>
				</tr>
				<tr>
					<th>Exposure:</th>
					<td runat="server" id="EditExposureCell" class="dataGrid"></td>
					<td class="dataGrid"><img src="~/gfx/icon-tick-up.png" runat="server" id="EditExposureTick"></td>
					<td class="dataGrid">
						<img src="~/gfx/icon-lock.png" alt="Locked" runat="server" visible="false" id="EditExposureLock">
						<asp:Button Runat="server" Text="Change..." OnClick="Edit_Click" ID="EditExposureChange" CausesValidation="False"/>
					</td>
				</tr>
				<tr class="dataGridAltItem">
					<th xstyle="border-top:2px solid #ff0000;border-left:2px solid #ff0000;">Choose towns to target:</th>
					<td xstyle="border-top:2px solid #ff0000;" runat="server" id="EditPlacesCell" class="dataGrid"></td>
					<td xstyle="border-top:2px solid #ff0000;" class="dataGrid"><img src="/gfx/icon-tick-up.png"></td>
					<td xstyle="border-top:2px solid #ff0000;border-right:2px solid #ff0000;" class="dataGrid">
						<asp:Button ID="Button2" Runat="server" Text="Choose towns..." OnClick="Edit_Click" CausesValidation="False"/>
						<!--<asp:Button ID="Button3" xRunat="server" Text="Don't target by town" OnClick="EditPlaces_Clear" CausesValidation="False"/>-->
					</td>
				</tr>
				<tr>
					<th xstyle="border-left:2px solid #ff0000;border-bottom:2px solid #ff0000;">Choose music to target:</th>
					<td xstyle="border-bottom:2px solid #ff0000;" runat="server" id="EditMusicCell" class="dataGrid"></td>
					<td xstyle="border-bottom:2px solid #ff0000;" class="dataGrid"><img src="/gfx/icon-tick-up.png"></td>
					<td xstyle="border-bottom:2px solid #ff0000;border-right:2px solid #ff0000;" class="dataGrid">
						<asp:Button ID="Button4" Runat="server" Text="Choose music..." OnClick="Edit_Click" CausesValidation="False"/>
						<!--<asp:Button ID="Button5" xRunat="server" Text="Don't target by music" OnClick="EditMusic_Clear" CausesValidation="False"/>-->
					</td>
				</tr>
				<tr class="dataGridAltItem">
					<th>Pay for your banner:</th>
					<td runat="server" id="EditPaymentCell" class="dataGrid"></td>
					<td class="dataGrid"><img src="~/gfx/icon-cross-up.png" runat="server" id="EditPaymentTick"></td>
					<td class="dataGrid">
						<Controls:Payment Runat="server" id="Payment" OnPaymentDone="PaymentReceived"/>
						<asp:Label Runat="server" ID="EditPaymentLabel"></asp:Label>
						<p id="AdminPriceEditP" style="margin-left:0px;" runat="server" visible="false">
							<table cellpadding="0" cellspacing="0" border="0" style="border:0px;">
								<tr>
