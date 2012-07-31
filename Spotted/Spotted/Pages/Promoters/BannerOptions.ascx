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
									<td style="border:0px"><asp:TextBox ID="FixPriceTextBox" runat="server" onmouseover="stt('Enter fixed price (£) or fixed discount (%).<br>Leave blank and click to undo price fix.');" onmouseout="htm();" Width="58"></asp:TextBox></td>
									<td style="border:0px"><div><button id="FixPriceExVatButton" runat="server" style="width:120px" onserverclick="FixPriceExVatButton_Click" onmouseover="htm();" ValidationGroup="FixPriceValidationGroup"><nobr>Fix price exVAT (£)</nobr></button>&nbsp;
										<button id="FixPriceIncVatButton" runat="server" style="width:120px" onserverclick="FixPriceIncVatButton_Click" onmouseover="htm();" ValidationGroup="FixPriceValidationGroup"><nobr>Fix price incVAT (£)</nobr></button></div>

										<div style="margin-top:2px"><button id="FixPriceDiscountButton" runat="server" style="width:120px" onserverclick="FixPriceDiscountButton_Click" onmouseover="htm();" ValidationGroup="FixPriceValidationGroup"><nobr>Fix discount (%)</nobr></button>&nbsp;
										<button id="ClearFixDiscountButton" runat="server" style="width:120px" onserverclick="ClearFixDiscountButton_Click" onmouseover="htm();" ValidationGroup="FixPriceValidationGroup"><nobr>Clear fixed discount</nobr></button></div>
									</td>
								</tr>
							</table>
						</p>
					</td>
				</tr>
				<tbody runat="server" id="EditFileBody">
					<tr>
						<th>Assigned banner file:</th>
						<td runat="server" id="EditAssignedCell" class="dataGrid">&nbsp;</td>
						<td class="dataGrid"><img src="~/gfx/icon-cross-up.png" runat="server" id="EditFileTick"></td>
						<td class="dataGrid">
							<asp:Button ID="Button6" Runat="server" Text="Upload now" OnClick="EditFile_Upload" CausesValidation="False"/>
							<asp:Button ID="Button7" Runat="server" Text="Assign existing file" OnClick="EditFile_Assign" CausesValidation="False"/>
						</td>
					</tr>
					<tr class="dataGridAltItem" runat="server" id="EditWaitingRow">
						<th>File waiting for admin check:</th>
						<td runat="server" id="EditWaitingCell" class="dataGrid">&nbsp;</td>
						<td class="dataGrid">&nbsp;</td>
						<td width="250" class="dataGrid">
							<small>
								We try to check all uploaded files within 24 hours of them being uploaded. We 
								would encourage you to upload your banner files in advance of booking your 
								advertising. You can then have several banners already checked and waiting. 
								Pre-checked files can be assigned to a banner instantly.
							</small>
						</td>
					</tr>
					<tr class="dataGridAltItem" runat="server" id="EditFailedRow">
						<th>Incompatible file:</th>
						<td runat="server" id="EditFailedCell" class="dataGrid">&nbsp;</td>
						<td><img src="/gfx/icon-cross-up.png" class="dataGrid"></td>
						<td width="250" class="dataGrid">
							<small>
								The file you uploaded is not able to be used with this banner. Click the "view details" 
								link to the left to find out why.
							</small>
						</td>
					</tr>
				</tbody>
			</table>
		</p>
	</div>
	
	<asp:Panel runat="server" ID="RefundedPanel">
		<dsi:h1 runat="server" ID="H116sdfaw">Banner refunded</dsi:h1>
		<div class="ContentBorder">
			<p>
				This banner has finished. We didn't serve all the impressions you asked for (we served <%= CurrentBanner.TotalHits.ToString("#,##0")%> out of <%= CurrentBanner.TotalRequiredImpressions.ToString("#,##0")%>).
				<%= CurrentBanner.RefundedCredits.ToString("#,##0") %> credit<%= CurrentBanner.RefundedCredits == 1 ? " has" : "s have" %> been refunded onto your account to cover this shortfall.
			</p>
		</div>
	</asp:Panel>

	<asp:Panel runat="server" ID="CancelledPanel">
		<dsi:h1 runat="server" ID="H116sdfa">Banner cancelled</dsi:h1>
		<div class="ContentBorder">
			<p>
				This banner has been cancelled. We served <%= CurrentBanner.TotalHits.ToString("#,##0")%> out of <%= CurrentBanner.TotalRequiredImpressions.ToString("#,##0")%> impressions.
				<%= CurrentBanner.RefundedCredits.ToString("#,##0") %> credit<%= CurrentBanner.RefundedCredits == 1 ? " has" : "s have" %> been refunded onto your account.
			</p>
		</div>
	</asp:Panel>
	


	<asp:Panel runat="server" ID="PanelExtension">
		<dsi:h1 runat="server" ID="PanelExtensionTitle">Increase or Extend this banner</dsi:h1>
		<div class="ContentBorder">

			<asp:Panel runat="server" ID="PanelExtensionOptions">
				<asp:Panel runat="server" ID="PanelExtensionButtons">
					<p>You can add a new banner to extend this current banner.</p>
					<p><button runat="server" onclick="ExtensionClick(this); return false;" ID="IncreaseBannerButton">Increase this banner's exposure</button></p>
					<p><button runat="server" onclick="ExtensionClick(this); return false;" ID="ExtendBannerButton">Extend this banner's lifespan</button></p>
					<asp:HiddenField runat="server" ID="ExtensionModeHidden" />
				</asp:Panel>
				<asp:Panel runat="server" ID="PanelExtensionSettings">
					<h2 class="BannerEditHead" runat="server" id="ExtensionTitleP">
					</h2>
					<h2 class="BannerEditHead">
						New Dates
					</h2>
					<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="DatesCustomEndDateVal" ControlToValidate="DatesEndCal" OnServerValidate="DatesCustomEndDateVal" ErrorMessage="Your end date must be later than the new start date." ID="DatesCustomEndDateValidator" />
					<table class="BannerEditTable">
						<tr>
							<td class="BannerEditLabelCell">
								Start date:
							</td>
							<td class="BannerEditControlCell">
								<b><p runat="server" id="DatesStartP"></p></b>
							</td>
						</tr>
						<tr>
							<td class="BannerEditLabelCell">
								End date:
							</td>
							<td class="BannerEditControlCell">
								<b><p runat="server" id="DatesEndP"></p></b>
								<div runat="server" id="DatesEndCalDiv">
									<dsi:Cal runat="server" ID="DatesEndCal" OnChange="Update();ValidatorValidate(DatesCustomEndDateValidator);" />
								</div>
							</td>
						</tr>
					</table>
					
					<h2 runat="server" id="ExposureTitle" class="BannerEditHead">
						Exposure
					</h2>
					<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="ExposureVal" OnServerValidate="ExposureVal" ErrorMessage="Please choose an option in the exposure section." ID="ExposureValidator" />
					<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="ExposureCustomVal" OnServerValidate="ExposureCustomVal" ErrorMessage="Our banner server needs at least 1,000 impressions per day to operate. Please increase your exposure, or change your dates." ID="ExposureCustomValidator" />
					<table class="BannerEditTable">
						<tr>
							<td class="BannerEditLabelCell">
								Automatic:
							</td>
							<td class="BannerEditControlCell">
								<asp:RadioButton runat="server" onClick="Update();FireExposureValidators();" GroupName="ExposureRadioGroup" ID="ExposureLightRadio" Text="Light exposure" /><br />
								<asp:RadioButton runat="server" onClick="Update();FireExposureValidators();" GroupName="ExposureRadioGroup" ID="ExposureMediumRadio" Text="Medium exposure" /><br />
								<asp:RadioButton runat="server" onClick="Update();FireExposureValidators();" GroupName="ExposureRadioGroup" ID="ExposureHeavyRadio" Text="Heavy exposure" /><br />
								<asp:RadioButton runat="server" onClick="Update();FireExposureValidators();" GroupName="ExposureRadioGroup" ID="ExposureCustomRadio" Text="Custom impressions..." />
							</td>
						</tr>
						<tr runat="server" id="ImpressionsRow">
							<td class="BannerEditLabelCell">
								Impressions:
							</td>
							<td class="BannerEditControlCell">
								<table cellpadding="0" cellspacing="0">
									<tr>
										<td rowspan="2">
											<asp:TextBox id="ImpressionsTextBox" onkeyup="ImpressionsKeepNumberWithinLimits(false);Update();" onblur="ImpressionsKeepNumberWithinLimits(true);Update('exposure');" runat="server" Text="0" Height="24" Columns="12" style="text-align:right; font-size:12; vertical-align:text-bottom; line-height:18px;"></asp:TextBox>
										</td>
										<td rowspan="2">
											<table cellpadding="0" cellspacing="0" border="0">
												<tr><td><a href="Javascript:void(0);" onclick="ImpressionsUpdateTextBoxNumber(100000);Update();FireExposureValidators();return false;" onmouseover="stt('Add 100,000 impressions');" onmouseout="htm();"><img src="/gfx/plus.gif" border="0" height="12" width="12"/></a></td></tr>
												<tr><td><a href="Javascript:void(0);" onclick="ImpressionsUpdateTextBoxNumber(-100000);Update();FireExposureValidators();return false;" onmouseover="stt('Minus 100,000 impressions');" onmouseout="htm();"><img src="/gfx/minus.gif" border="0" height="12" width="12"/></a></td></tr>
											</table>
										</td>
										<td rowspan="2">
											<table cellpadding="0" cellspacing="0" border="0">
												<tr><td><a href="Javascript:void(0);" onclick="ImpressionsUpdateTextBoxNumber(10000);Update();FireExposureValidators();return false;" onmouseover="stt('Add 10,000 impressions');" onmouseout="htm();"><img src="/gfx/plus.gif" border="0" height="12" width="12"/></a></td></tr>
												<tr><td><a href="Javascript:void(0);" onclick="ImpressionsUpdateTextBoxNumber(-10000);Update();FireExposureValidators();return false;" onmouseover="stt('Minus 10,000 impressions');" onmouseout="htm();"><img src="/gfx/minus.gif" border="0" height="12" width="12"/></a></td></tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr runat="server" id="ExposureDetailsRow">
							<td class="BannerEditLabelCell">
								Details:
							</td>
							<td class="BannerEditControlCell">
								<p id="ExposureLevelP"></p>
								<p id="ExposureCostCampaignCreditsP"></p>
								<p id="ExposureCostCashP"></p>
							</td>
						</tr>
					</table>
					<p>
						<button onclick="ExtensionReset(); return false;">Back</button>
						<button ID="SaveButton" runat="server" onserverclick="Save_Click" causesvalidation="true">Save</button>
					</p>
				</asp:Panel>
			</asp:Panel>
		</div>

		<script>
			var ExtensionTitleP = document.getElementById("<%=ExtensionTitleP.ClientID%>");
			
			var ExtensionMode;
			var ExtensionModeIncrease = <%=(int)ExtensionModes.Increase%>;
			var ExtensionModeExtend = <%=(int)ExtensionModes.Extend%>;
			var ExtensionModeHidden = document.getElementById("<%=ExtensionModeHidden.ClientID%>");
			
			var DiscountCredits = new Array(<%= DiscountCreditsString %>);
			var DiscountLevels = new Array(<%= DiscountLevelsString %>);

			var PanelExtensionButtons = document.getElementById("<%=PanelExtensionButtons.ClientID%>");
			var IncreaseBannerButton = document.getElementById("<%=IncreaseBannerButton.ClientID%>");
			var ExtendBannerButton = document.getElementById("<%=ExtendBannerButton.ClientID%>");
			
			var PanelExtensionSettings = document.getElementById("<%=PanelExtensionSettings.ClientID%>");
			
			var DatesStartP = document.getElementById("<%=DatesStartP.ClientID%>");
			var DatesEndP = document.getElementById("<%=DatesEndP.ClientID%>");
			var DatesEndCal = document.getElementById("<%=DatesEndCal.InnerClientID%>");
			var DatesEndCalDiv = document.getElementById("<%=DatesEndCalDiv.ClientID%>");
			var DatesCustomEndDateValidator = document.getElementById("<%=DatesCustomEndDateValidator.ClientID%>");
			
			var ExposureTitle = document.getElementById("<%=ExposureTitle.ClientID%>");
			var ExposureLightRadio = document.getElementById("<%=ExposureLightRadio.ClientID%>");
			var ExposureMediumRadio = document.getElementById("<%=ExposureMediumRadio.ClientID%>");
			var ExposureHeavyRadio = document.getElementById("<%=ExposureHeavyRadio.ClientID%>");
			var ExposureCustomRadio = document.getElementById("<%=ExposureCustomRadio.ClientID%>");
			var ImpressionsRow = document.getElementById("<%=ImpressionsRow.ClientID%>");
			var ImpressionsTextBox = document.getElementById("<%=ImpressionsTextBox.ClientID%>");
			var ExposureLevelP = document.getElementById("ExposureLevelP");
			var ExposureCostCampaignCreditsP = document.getElementById("ExposureCostCampaignCreditsP");
			var ExposureCostCashP = document.getElementById("ExposureCostCashP");
			var ImpressionsTextBox = document.getElementById("<%=ImpressionsTextBox.ClientID%>");

			var ExposureValidator = document.getElementById("<%=ExposureValidator.ClientID%>");
			var ExposureCustomValidator = document.getElementById("<%=ExposureCustomValidator.ClientID%>");
			
			
			function ExtensionClick(source)
			{
				if (source == IncreaseBannerButton)
				{
					DatesStartP.innerHTML = '<%=IncreaseFirstDay.ToString("dd MMM yyyy")%>';
					DatesEndP.innerHTML = '<%=IncreaseLastDay.ToString("dd MMM yyyy")%>';
					DatesEndP.style.display = '';
					DatesEndCalDiv.style.display = 'none';
					ExtensionMode = ExtensionModeIncrease;
					ExposureTitle.innerHTML = 'Additional Exposure';
					ExtensionTitleP.innerHTML = 'Increase this Banner';
					ValidatorValidate(DatesCustomEndDateValidator);
				}
				else if (source == ExtendBannerButton)
				{
					DatesStartP.innerHTML = '<%=ExtendFirstDay.ToString("dd MMM yyyy")%>';
					DatesEndP.innerHTML = '';
					DatesEndP.style.display = 'none';
					DatesEndCalDiv.style.display = '';
					ExposureTitle.innerHTML = 'Exposure';
					ExtensionTitleP.innerHTML = 'Extend this Banner';
					ExtensionMode = ExtensionModeExtend;
				}
				ExtensionModeHidden.value = ExtensionMode;
				PanelExtensionButtons.style.display = 'none';
				PanelExtensionSettings.style.display = '';
				UpdatePrices();
			}
			
			function ExtensionReset()
			{
				PanelExtensionButtons.style.display = '';
				PanelExtensionSettings.style.display = 'none';
			}
			
			
			function FireExposureValidators()
			{
				ValidatorValidate(ExposureValidator);
				ValidatorValidate(ExposureCustomValidator);
			}

			// changed
			function DatesCustomEndDateVal(sender, args)
			{
				if (ExtensionMode == ExtensionModeIncrease)
				{
					args.IsValid = true;
				}
				else
				{
					var endDate = new Date(parseInt(DatesEndCal.value.split("/")[2], 10), parseInt(DatesEndCal.value.split("/")[1], 10) - 1, parseInt(DatesEndCal.value.split("/")[0], 10));
					args.IsValid = endDate >= new Date(DatesStartP.innerHTML);
				}
			}
			
			// changed
			function DisplayImpressionsRow(){ return ExposureCustomRadio.checked; }
			// changed
			function Update()
			{
				ImpressionsRow.style.display = DisplayImpressionsRow() ? "" : "none";
				UpdatePrices();
			}

			var SingleCreditPrice = 1.00;


			//changed
			function UpdatePrices()
			{
				var totalDays = GetTotalDays();
				var impressionsPerCredit = <%= Banner.GetImpressionsPerCampaignCredit(CurrentBanner.Position) %>;

				if (totalDays==0)
				{
					ExposureLevelP.innerHTML = "Choose a new end date to work out your exposure.";
					ExposureCostCampaignCreditsP.innerHTML = "Choose a new end date to work out a price.";
					ExposureCostCashP.innerHTML = "";
					return;
				}
				
				if (ExposureCustomRadio.checked)
				{
					var impressions = parseInt(ImpressionsTextBox.value.replace(new RegExp(",", "g"),""));
					var credits = Math.ceil(impressions / impressionsPerCredit);
					
					ExposureLevelP.innerHTML = "Exposure: " + GetExposure(credits, totalDays);
					ExposureCostCampaignCreditsP.innerHTML = "Price in campaign credits: " + tsep(credits) + " credits";
					ExposureCostCashP.innerHTML = "Cash price: £" + tsep(GetCashPrice(credits).toFixed(2)) + (GetDiscount(credits) > 0 ? " (this includes a " + GetDiscount(credits) + "% discount)" : "");
				}
				else
				{
					var exposureLevel = GetSelectedExposureLevel();
					if (exposureLevel > 0)
					{
						var creditsPerDay = GetCreditsPerDay(exposureLevel);
						
						var credits = creditsPerDay * totalDays;
						var totalImpressions = credits * impressionsPerCredit;
						
						ImpressionsTextBox.value = tsep(totalImpressions);
						
						ExposureLevelP.innerHTML = "Actual impressions: " + tsep(totalImpressions);
						ExposureCostCampaignCreditsP.innerHTML = "Price in campaign credits: " + tsep(credits) + " credits";
						ExposureCostCashP.innerHTML = "Cash price: £" + tsep(GetCashPrice(credits).toFixed(2)) + (GetDiscount(credits) > 0 ? " (this includes a " + GetDiscount(credits) + "% discount)" : "");
					}
					else
					{
						ExposureLevelP.innerHTML = "Choose an exposure level...";
						ExposureCostCampaignCreditsP.innerHTML = "Choose an exposure level to work out a price.";
						ExposureCostCashP.innerHTML = "";
					}
				}
			}

			//Light = 1,
			//Medium = 2,
			//Heavy = 3
			function GetSelectedExposureLevel()
			{
				if (ExposureLightRadio.checked)
					return 1;
				else if (ExposureMediumRadio.checked)
					return 2;
				else if (ExposureHeavyRadio.checked)
					return 3;
				else
					return 0;
			}

			function GetCashPrice(credits)
			{
				return credits * SingleCreditPrice * (1 - (GetDiscount(credits) / 100.0));
			}

			function GetDiscount(credits)
			{
				var i = 0;
				var discountLevel = 0;
				while(credits>=DiscountCredits[i] && i<DiscountCredits.length)
				{
					discountLevel = DiscountLevels[i];
					i++;
				}
				return discountLevel;
			}

			function GetCreditsPerDay(exposureLevel)
			{
				if (exposureLevel == 1)
					return 15;
				else if (exposureLevel == 2)
					return 30;
				else if (exposureLevel == 3)
					return 50;
				else
					return 0;
			}

			//light: 15 credits/day (display 'light' when custom impressions makes credits < 20/day)
			//medium: 30 credits/day (display 'medium' when custom impressions makes credits < 40/day)
			//heavy: 50 credits/day
			function GetExposure(credits, days)
			{
				var creditsPerDay = credits / days;
				if (creditsPerDay < 20)
					return "light";
				else if (creditsPerDay < 40)
					return "medium";
				else
					return "heavy";
			}

			function GetDaysBetweenDates(startDate, endDate)
			{
				return Math.round((endDate - startDate) / 86400000); // Math.round account for daylight saving
			}
			
			//changed
			function GetTotalDays()
			{
				try
				{
					if (ExtensionMode == ExtensionModeIncrease)
					{
						var startDate = new Date(DatesStartP.innerHTML);
						var endDate = new Date(DatesEndP.innerHTML);
						var days = GetDaysBetweenDates(startDate, endDate);

						if (isNaN(days) || days<0)
							return 0;
						else
							return days + 1;
					}
					else
					{
						var startDate = new Date(DatesStartP.innerHTML);
						var endDate = new Date(parseInt(DatesEndCal.value.split("/")[2], 10), parseInt(DatesEndCal.value.split("/")[1], 10) - 1, parseInt(DatesEndCal.value.split("/")[0], 10));
						var days = GetDaysBetweenDates(startDate, endDate);
						
						if (isNaN(days) || days<0)
							return 0;
						else
							return days + 1;
					}
				}
				catch (ex)
				{
					return 0;
				}
			}
			
			// changed
			function ExposureVal(sender, args)
			{
				args.IsValid = 
					ExposureLightRadio.checked ||
					ExposureMediumRadio.checked ||
					ExposureHeavyRadio.checked ||
					ExposureCustomRadio.checked;
			}
			// changed
			function ExposureCustomVal(sender, args)
			{
				args.IsValid = 
					!ExposureCustomRadio.checked ||
					CustomExposureIsOk();
			}

			function CustomExposureIsOk()
			{
				var impressions = parseInt(ImpressionsTextBox.value.replace(new RegExp(",", "g"),""));
				var days = GetTotalDays();
				if (days == 0)
					return true;
				var impressionsPerDay = impressions / days;
				return impressionsPerDay > 900;
			}


			function ImpressionsUpdateTextBoxNumber(amount)
			{
				ImpressionsTextBox.value = tsep(parseInt(ImpressionsTextBox.value.replace(new RegExp(",", "g"),"")) + parseInt(amount));
				ImpressionsKeepNumberWithinLimits(false);
			}
			function ImpressionsKeepNumberWithinLimits(reformat)
			{	
				var val = ImpressionsTextBox.value.replace(new RegExp(",", "g"),"");
				if (isNaN(val))
				{
					ImpressionsTextBox.value = 0;
					return;
				}
				var valInt = parseInt(val);
				if(valInt < 0)
				{
					ImpressionsTextBox.value = "0";
					return;
				}	
				if (reformat)
					ImpressionsTextBox.value = tsep(valInt);
			}
			function tsep(n)
			{ 
				var ts=",", ds=".";
				var ns = String(n),ps=ns,ss="";
				var i = ns.indexOf("."); 
				if (i != -1)
				{
					ps = ns.substring(0,i); 
					ss = ds+ns.substring(i+1); 
				} 
				return ps.replace(/(\d)(?=(\d{3})+([.]|$))/g,"$1"+ts)+ss; 
			} 

			Update();
			
		</script>

	</asp:Panel>

	<asp:Panel runat="server" ID="CopyBannerPanel">
		<dsi:h1 runat="server">Copy this banner</dsi:h1>
		<div class="ContentBorder">
			<p><a href="<%= CurrentPromoter.UrlApp("banneredit","mode","copy","copybannerk",CurrentBanner.K.ToString()) %>">Create a new banner using this banner's details</a></p>
		</div>
	</asp:Panel>

	<asp:Panel runat="server" ID="CancelPanel">
		<dsi:h1 runat="server" ID="H1164321sdaf">Cancel this banner</dsi:h1>
		<div class="ContentBorder">
			<p>
				There are <%= CurrentBanner.RemainingImpressions.ToString("N0")%> impressions* (<%= Banner.GetImpressionsValueInCampaignCredits(CurrentBanner.RemainingImpressions, CurrentBanner.Position).ToString("N0")%> campaign credits) unused on this banner.
				Cancel this banner and refund the credits?
			</p>
			<asp:Button runat="server" ID="CancelButton" OnClientClick="return confirm('Are you sure you wish to cancel this banner?');" OnClick="CancelButton_Click" Text="Cancel" />
			<p>
				<small>* Note this figure can change if the banner is currently running.</small>
			</p>
		</div>
	</asp:Panel>
	

	
	<asp:Panel runat="server" ID="OptionsPanel" Visible="false">
		<dsi:h1 runat="server" ID="H13">Options</dsi:h1>
		<div class="ContentBorder">
			<p>
				Cancel this banner - this stops the banner and refunds unused campaign credits to your account. There are approximately XX impressions (~XX campaign credits) unused on this banner.
			</p>
			<p>
				Extend this banner - this banner may be extended to a new end date.
			</p>
			<p>
				Add impressions - if you don't think you're getting enough coverage, you can add more impressions for the remainder of the banner.
			</p>
			<p>
				Copy - if you would like to add a similar banner, use this function. It'll create a new pending banner using the same settings. You can edit id to select a new date range or change other options.
			</p>
		</div>
	</asp:Panel>
</asp:Panel>



<asp:Panel runat="server" ID="PanelExtensionSuccessful">
	<dsi:h1 runat="server" ID="H14">Banner extension successful</dsi:h1>
	<div class="ContentBorder">
		<p>Your new banner has been saved as <b><%=NewBanner.Name %></b>: it is now awaiting booking.</p>
		<p><a href="<%= CurrentPromoter.UrlApp("bannerspending") %>">Click here</a> to view all banners awaiting booking.</p>
		<p><a href="<%= CurrentPromoter.UrlApp("banneroptions", "mode", "edit", "bannerk", NewBanner.K.ToString()) %>">Click here</a> to edit the new banner's options.</p>
	</div>
</asp:Panel>


<asp:Panel runat="server" ID="PanelCancelSuccessful" Visible="false">
	<dsi:h1 runat="server" ID="H1">Banner successfully cancelled</dsi:h1>
	<div class="ContentBorder">
		<p>
			This banner has now been cancelled. The final 
			number of credits left on this banner 
			was <b><%= CurrentBanner.RefundedCredits %></b>, which 
			have been refunded to your account.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelFileAssign">
	<dsi:h1 runat="server" ID="H118">Banner advert options</dsi:h1>
	<div class="ContentBorder">
		<p>
			Assign a file by selecting it below:
		</p>
		<p runat="server" id="FileAssignDropDownP">
			<asp:DropDownList Runat="server" ID="FileAssignDropDown" AutoPostBack="True"
				OnSelectedIndexChanged="FileAssignDropDown_Change"></asp:DropDownList>
		</p>
		<p runat="server" id="FileAssignNoFiles">
			There are no files that can be assigned to this banner. <a href="" runat="server" id="FileAssignNoFilesUploadLink">Upload some here</a>
		</p>
		<div style="margin-left:6px;width:571px;overflow:auto;padding:20px;border:1px solid #CBA21E;" runat="server" id="FileAssignPreviewDiv">
			<div runat="server" id="FileAssignDiv" class="BorderBlack All"><asp:PlaceHolder Runat="server" ID="FileAssignPreview" EnableViewState="False"></asp:PlaceHolder></div>
		</div>
		<p runat="server" id="FileWaitingForCheckP" visible="false">
			<table cellpadding="0" cellspacing="0">
			<tr>
				<td><img src="/gfx/icon-warning.png" width="26" height="21" border="0" align="absmiddle"></td>
				<td style="padding-left:5px;">
					<b>We need to check this file before it can go live. 
					Click Assign, and we'll make it live as soon as it's checked OK. 
					We'll send you an email as soon as it's checked.</b>
				</td>
			</tr>
			</table>
		</p>
		<p runat="server" id="FileCheckedP" visible="false">
			<table cellpadding="0" cellspacing="0">
			<tr>
				<td><img src="/gfx/icon-tick-up.png" border="0" align="absmiddle"></td>
				<td style="padding-left:5px;">
					<b>This file can be assigned instantly.</b>
				</td>
			</tr>
			</table>
		</p>
		<p>
			<input id="Button13" type="button" Runat="server" onserverclick="FileAssign_Back" Value="&lt;- Back" CausesValidation="False"/>
			<asp:Button ID="Button14" Runat="server" OnClick="FileAssign_Next" Text="Assign"/>
		</p>
	</div>
</asp:Panel>
