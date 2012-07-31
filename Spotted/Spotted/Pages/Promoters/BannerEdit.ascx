<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerEdit.ascx.cs" Inherits="Spotted.Pages.Promoters.BannerEdit" %>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Banners">
	<p>
		This is the banner options page. You can use it to add or change a 
		banner. You can get back at any time:
	</p>
	<p style="font-weight:bold;font-size:14px;">
		<a href="" runat="server" id="IntroBannerListLink">Back to banner list</a>
	</p>
</dsi:PromoterIntro>
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
<dsi:h1 runat="server" ID="H12">Banner</dsi:h1>
<div class="ContentBorder">


	<asp:Panel ID="Panel2" runat="server">
		<p>
			<button id="Button2" runat="server" onserverclick="CancelButton_Click" causesvalidation="false">Cancel</button>&nbsp;
			<button ID="Button1" runat="server" onserverclick="DoneClick" text="Done" OnClick="HookupValidators();">Done</button>
		</p>
	</asp:Panel>

	<asp:Panel runat="server" ID="ModePanel">
		<h2>
			Mode
		</h2>
		<p>
			<small>We can either walk through adding a banner step by step (best for beginners), or we can show all the options on one page (quicker for experts).</small>
		</p>
		<p>
			<table class="BannerEditTable">
				<tr>
					<td class="BannerEditLabelCell">
						Mode:
					</td>
					<td class="BannerEditControlCell">
						<asp:RadioButton runat="server" onClick="ModeChange(true);" GroupName="ModeGroup" ID="ModeBeginnerRadio" Text="Beginner mode" /> <small>(step by step)</small><br />
						<asp:RadioButton runat="server" onClick="ModeChange(true);" GroupName="ModeGroup" ID="ModeExpertRadio" Text="Expert mode" /> <small>(shows all banner options at once)</small>
					</td>
				</tr>
			</table>
		</p>
	</asp:Panel>

	<asp:Panel runat="server" id="LinkPanel">
		<h2>
			Banner link
		</h2>
		<p>
			<small>Where do you want to go when you click on your banner?</small>
		</p>
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="LinkVal" OnServerValidate="LinkVal" ErrorMessage="Please choose an option in the banner link section." ID="LinkValidator" />
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="LinkCustomVal" OnServerValidate="LinkCustomVal" ErrorMessage="Make sure your custom URL starts with 'http://' in the banner link section."  ID="LinkCustomValidator"/>
		<table class="BannerEditTable">
			<tr>
				<td class="BannerEditLabelCell">
					Banner link:
				</td>
				<td class="BannerEditControlCell">
					<span runat="server" id="LinkEventSpan" style="background-color:transparent;"><asp:RadioButton runat="server" onClick="Update('link');" GroupName="LinkRadioGroup" ID="LinkEventRadio" Text="My upcoming event[...]" /><span runat="server" id="LinkEventLockedSpan" /> <asp:DropDownList ID="LinkEventDropDown" runat="server" onchange="Update('link');"/><br /></span>
					<span runat="server" id="LinkBrandSpan" style="background-color:transparent;"><asp:RadioButton runat="server" onClick="Update('link');" GroupName="LinkRadioGroup" ID="LinkBrandRadio" Text="My brand page[...]" /> <asp:DropDownList ID="LinkBrandDropDown" runat="server"/><br /></span>
					<span runat="server" id="LinkVenueSpan" style="background-color:transparent;"><asp:RadioButton runat="server" onClick="Update('link');" GroupName="LinkRadioGroup" ID="LinkVenueRadio" Text="My venue page[...]" /> <asp:DropDownList ID="LinkVenueDropDown" runat="server"/><br /></span>
					<span runat="server" id="LinkTicketsSpan" style="background-color:transparent;"><asp:RadioButton runat="server" onClick="Update('link');" GroupName="LinkRadioGroup" ID="LinkTicketsRadio" Text="My tickets page[...]" /> <asp:DropDownList ID="LinkTicketsDropDown" runat="server"/><br /></span>
					<span runat="server" id="LinkCustomSpan" style="background-color:transparent;"><asp:RadioButton runat="server" onClick="Update('link');" GroupName="LinkRadioGroup" ID="LinkCustomRadio" Text="Custom URL..." /> <asp:TextBox runat="server" Columns="50" ID="LinkCustomTextBox"></asp:TextBox></span>
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="PositionPanel">
		<h2 class="BannerEditHead">
			Position
		</h2>
		<p>
			<small>We have several positions for banners on the site. <a href="/popup/bannerpositions" onclick="openPopup('/popup/bannerpositions');return false;" target="_blank">Click here</a> for a description of the options. If you plan to design the banner artwork, you will need to send the <a href="/misc/banners.pdf" target="_blank">banner instructions</a> to your designer.</small>
		</p>
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="PositionVal" OnServerValidate="PositionVal" ErrorMessage="Please choose an option in the position section." ID="PositionValidator" />
		<table class="BannerEditTable">
			<tr runat="server" id="PositionLockedRow">
				<td class="BannerEditLabelCell">
					Position:
				</td>
				<td class="BannerEditControlCell">
					<img src="/gfx/icon-lock.png" alt="Locked" align="absmiddle" /><asp:Label runat="server" ID="PositionLockedLabel"></asp:Label>
				</td>
			</tr>
			<tr runat="server" id="PositionRow">
				<td class="BannerEditLabelCell">
					Position:
				</td>
				<td class="BannerEditControlCell">
					<asp:RadioButton runat="server" onClick="Update('position');" GroupName="PositionRadioGroup" ID="PositionHotboxRadio" Text="Hotbox" /><br /><!--<small><a href="/popup/bannerpositions" onclick="openPopup('/popup/TODO');return false;" target="_blank">learn more</a></small>-->
					<asp:RadioButton runat="server" onClick="Update('position');" GroupName="PositionRadioGroup" ID="PositionLeaderboardRadio" Text="Leaderboard" /><br /><!--<small><a href="/popup/bannerpositions" onclick="openPopup('/popup/TODO');return false;" target="_blank">learn more</a></small>-->
					<asp:RadioButton runat="server" onClick="Update('position');" GroupName="PositionRadioGroup" ID="PositionSkyscraperRadio" Text="Skyscraper" /><br /><!--<small><a href="/popup/bannerpositions" onclick="openPopup('/popup/TODO');return false;" target="_blank">learn more</a></small>-->
					<asp:RadioButton runat="server" onClick="Update('position');" GroupName="PositionRadioGroup" ID="PositionPhotoBannerRadio" Text="Photo banner" /><br /><!--<small><a href="/popup/bannerpositions" onclick="openPopup('/popup/TODO');return false;" target="_blank">learn more</a></small>-->
					<asp:RadioButton runat="server" onClick="Update('position');" GroupName="PositionRadioGroup" ID="PositionEmailBannerRadio" Text="Email banner" /><!--<small><a href="/popup/bannerpositions" onclick="openPopup('/popup/TODO');return false;" target="_blank">learn more</a></small>-->
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="DatesPanel">
		<h2 class="BannerEditHead" runat="server" id="DatesHead">
			Dates
		</h2>
		<p>
			<small>We will spread the number of impressions you choose over the whole date range. We will show more impressions on days when when we have more visitors - this ensures you get the best coverage.</small>
		</p>
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="DatesVal" OnServerValidate="DatesVal" ErrorMessage="Please choose an option in the dates section." ID="DatesValidator" />
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="DatesCustomVal" OnServerValidate="DatesCustomVal" ErrorMessage="Please choose a a start and end date in the custom dates section." ID="DatesCustomValidator" />
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="DatesCustomEndDateVal" OnServerValidate="DatesCustomEndDateVal" ErrorMessage="Your end date seems to be before today." ID="DatesCustomEndDateValidator" />
		<table class="BannerEditTable">
			<tr runat="server" id="DatesLockedRow">
				<td class="BannerEditLabelCell">
					Dates:
				</td>
				<td class="BannerEditControlCell">
					<img src="/gfx/icon-lock.png" alt="Locked" align="absmiddle" /><asp:Label runat="server" ID="DatesLockedLabel"></asp:Label>
				</td>
			</tr>
			<tr runat="server" id="DatesAutoRow">
				<td class="BannerEditLabelCell">
					Dates:
				</td>
				<td class="BannerEditControlCell">
					<span runat="server" id="Dates1WeekSpan"><asp:RadioButton runat="server" onClick="Update('dates');" GroupName="DatesRadioGroup" ID="Dates1WeekRadio" Text="1 week up to my event" /><br /></span>
					<span runat="server" id="Dates2WeekSpan"><asp:RadioButton runat="server" onClick="Update('dates');" GroupName="DatesRadioGroup" ID="Dates2WeekRadio" Text="2 weeks up to my event" /><br /></span>
					<span runat="server" id="Dates3WeekSpan"><asp:RadioButton runat="server" onClick="Update('dates');" GroupName="DatesRadioGroup" ID="Dates3WeekRadio" Text="3 weeks up to my event" /><br /></span>
					<span runat="server" id="Dates4WeekSpan"><asp:RadioButton runat="server" onClick="Update('dates');" GroupName="DatesRadioGroup" ID="Dates4WeekRadio" Text="4 weeks up to my event" /><br /></span>
					<asp:RadioButton runat="server" onClick="Update('dates');" GroupName="DatesRadioGroup" ID="DatesCustomRadio" Text="Custom dates..." />
				</td>
			</tr>
			<tr runat="server" id="DatesStartDateRow">
				<td class="BannerEditLabelCell">
					Start date:
				</td>
				<td class="BannerEditControlCell">
					<dsi:Cal runat="server" ID="DatesStartCal" OnChange="Update('dates');FireDatesValidators();" />
				</td>
			</tr>
			<tr runat="server" id="DatesEndDateRow">
				<td class="BannerEditLabelCell">
					End date:
				</td>
				<td class="BannerEditControlCell">
					<dsi:Cal runat="server" ID="DatesEndCal" OnChange="Update('dates');FireDatesValidators();" />
				</td>
			</tr>
		</table>
	</asp:Panel>

	
	
	<asp:Panel runat="server" ID="ExposurePanel">
		<h2 class="BannerEditHead">
			Exposure
		</h2>
		<p>
			<small>The number of times your banner is shown on the site can be set automatically based on the dates, position and campaign strength. Alternatively you can specify exactly how many impressions you would like.</small>
		</p>
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="ExposureVal" OnServerValidate="ExposureVal" ErrorMessage="Please choose an option in the exposure section." ID="ExposureValidator" />
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="ExposureCustomVal" OnServerValidate="ExposureCustomVal" ErrorMessage="Our banner server needs at least 1,000 impressions per day to operate. Please increase your exposure, or change your dates." ID="ExposureCustomValidator" />
		<table class="BannerEditTable">
			<tr runat="server" id="ExposureLockedRow">
				<td class="BannerEditLabelCell">
					Exposure:
				</td>
				<td class="BannerEditControlCell">
					<img src="/gfx/icon-lock.png" alt="Locked" align="absmiddle" /><asp:Label runat="server" ID="ExposureLockedLabel"></asp:Label>
				</td>
			</tr>
			<tr runat="server" id="ExposureRow">
				<td class="BannerEditLabelCell">
					Automatic:
				</td>
				<td class="BannerEditControlCell">
					<asp:RadioButton runat="server" onClick="Update('exposure');" GroupName="ExposureRadioGroup" ID="ExposureLightRadio" Text="Light exposure" /><br />
					<asp:RadioButton runat="server" onClick="Update('exposure');" GroupName="ExposureRadioGroup" ID="ExposureMediumRadio" Text="Medium exposure" /><br />
					<asp:RadioButton runat="server" onClick="Update('exposure');" GroupName="ExposureRadioGroup" ID="ExposureHeavyRadio" Text="Heavy exposure" /><br />
					<asp:RadioButton runat="server" onClick="Update('exposure');" GroupName="ExposureRadioGroup" ID="ExposureCustomRadio" Text="Custom impressions..." />
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
								<asp:TextBox id="ImpressionsTextBox" onkeyup="ImpressionsKeepNumberWithinLimits(false);Update('exposure');" onblur="ImpressionsKeepNumberWithinLimits(true);Update('exposure');" runat="server" Text="0" Height="24" Columns="12" style="text-align:right; font-size:12; vertical-align:text-bottom; line-height:18px;"></asp:TextBox>
							</td>
							<td rowspan="2">
								<table cellpadding="0" cellspacing="0" border="0">
									<tr><td><a href="Javascript:void(0);" onclick="ImpressionsUpdateTextBoxNumber(100000);Update('exposure');FireExposureValidators();return false;" onmouseover="stt('Add 100,000 impressions');" onmouseout="htm();"><img src="/gfx/plus.gif" border="0" height="12" width="12"/></a></td></tr>
									<tr><td><a href="Javascript:void(0);" onclick="ImpressionsUpdateTextBoxNumber(-100000);Update('exposure');FireExposureValidators();return false;" onmouseover="stt('Minus 100,000 impressions');" onmouseout="htm();"><img src="/gfx/minus.gif" border="0" height="12" width="12"/></a></td></tr>
								</table>
							</td>
							<td rowspan="2">
								<table cellpadding="0" cellspacing="0" border="0">
									<tr><td><a href="Javascript:void(0);" onclick="ImpressionsUpdateTextBoxNumber(10000);Update('exposure');FireExposureValidators();return false;" onmouseover="stt('Add 10,000 impressions');" onmouseout="htm();"><img src="/gfx/plus.gif" border="0" height="12" width="12"/></a></td></tr>
									<tr><td><a href="Javascript:void(0);" onclick="ImpressionsUpdateTextBoxNumber(-10000);Update('exposure');FireExposureValidators();return false;" onmouseover="stt('Minus 10,000 impressions');" onmouseout="htm();"><img src="/gfx/minus.gif" border="0" height="12" width="12"/></a></td></tr>
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
					<p runat="server" id="ExposureLevelP">
						Equivelent exposure: [light / medium / heavy]
						OR 
						Actual impressions: XX,000
					</p>
					<p runat="server" id="ExposureCostCampaignCreditsP">
						Price in campaign credits: [ ] credits
					</p>
					<p runat="server" id="ExposureCostCashP">
						Cash price: £[ ] (this includes a X% discount)
					</p>
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="TargettingPanel">
		<h2 class="BannerEditHead">
			Targetting
		</h2>
		<p>
			<small>
				If you're running a smaller event, there's no point in showing your banners to people 
				living in other parts of the country. Our targetting makes sure we only show your banner to people who 
				visit your area, and listen to the music you're playing.
			</small>
		</p>
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="TargettingVal" OnServerValidate="TargettingVal" ErrorMessage="Please choose an option in the targetting section." ID="TargettingValidator" />
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="TargettingCustomVal" OnServerValidate="TargettingCustomVal" ErrorMessage="Please choose some custom targetting parameters by clicking 'edit' in the custom music or place targetting section. Alternatively, click 'all towns' or 'all music'." ID="TargettingCustomValidator" />
		<table class="BannerEditTable">
			<tr>
				<td class="BannerEditLabelCell">
					Targetting:
				</td>
				<td class="BannerEditControlCell">
					<span runat="server" id="TargettingAutomaticSpan"><asp:RadioButton runat="server" onClick="Update('targetting');" GroupName="TargettingRadioGroup" ID="TargettingAutomaticRadio" Text="Automatic targetting based on my event" /><br /></span>
					<asp:RadioButton runat="server" onClick="Update('targetting');" GroupName="TargettingRadioGroup" ID="TargettingNoneRadio" Text="No targetting - show my banner site-wide" /><br />
					<asp:RadioButton runat="server" onClick="Update('targetting');" GroupName="TargettingRadioGroup" ID="TargettingCustomRadio" Text="Custom targetting..." />
				</td>
			</tr>
			<tr runat="server" id="LocationTargettingRow">
				<td class="BannerEditLabelCell">
					Location targetting:
				</td>
				<td class="BannerEditControlCell">
					<asp:HiddenField runat="server" ID="LocationTargettingHidden" /><asp:TextBox Enabled="false" ReadOnly="true" runat="server" ID="LocationTargettingTextBox" Columns="25" Text="no towns selected" /><button runat="server" id="LocationTargettingButton">edit...</button> <button onclick="document.getElementById('<%= LocationTargettingTextBox.ClientID %>').value = 'all towns'; document.getElementById('<%= LocationTargettingHidden.ClientID %>').value = '0'; FireTargettingValidators(); return false;">all towns</button>
				</td>
			</tr>
			<tr runat="server" id="MusicTargettingRow">
				<td class="BannerEditLabelCell">
					Music targetting:
				</td>
				<td class="BannerEditControlCell">
					<asp:HiddenField runat="server" ID="MusicTargettingHidden" /><asp:TextBox Enabled="false" ReadOnly="true" runat="server" ID="MusicTargettingTextBox" Columns="25" Text="no music selected" /><button runat="server" id="MusicTargettingButton">edit...</button> <button onclick="document.getElementById('<%= MusicTargettingTextBox.ClientID %>').value = 'all music types'; document.getElementById('<%= MusicTargettingHidden.ClientID %>').value = '1'; FireTargettingValidators(); return false;">all music</button>
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:Panel runat="server" id="ArtworkPanel">
		<h2 class="BannerEditHead">
			Artwork
		</h2>
		<p>
			<small>
				How your banner looks can make a huge difference to how effective it is. Our talented 
				banner designers can create you a great looking design, or you can upload your own 
				gif, jpg or flash movie. You can even use our automatic event banners to create a 
				simple, effective text based leaderboard banner without any artwork! Click here for 
				more information about uploading artwork.
			</small>
		</p>
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="ArtworkVal" OnServerValidate="ArtworkVal" ErrorMessage="Please choose an option in the artwork section." ID="ArtworkValidator" />
		<table class="BannerEditTable">
			<tr runat="server" id="ArtworkLockedRow">
				<td class="BannerEditLabelCell">
					Artwork:
				</td>
				<td class="BannerEditControlCell">
					<img src="/gfx/icon-lock.png" alt="Locked" align="absmiddle" /><asp:Label runat="server" ID="ArtworkLockedLabel"></asp:Label>
				</td>
			</tr>
			<tr runat="server" id="ArtworkRow">
				<td class="BannerEditLabelCell">
					Artwork:
				</td>
				<td class="BannerEditControlCell">
					<asp:RadioButton runat="server" onClick="Update('artwork');" GroupName="ArtworkRadioGroup" ID="ArtworkUploadRadio" Text="Upload my own artwork" /><br />
					<asp:RadioButton runat="server" onClick="Update('artwork');" GroupName="ArtworkRadioGroup" ID="ArtworkJpgRadio" Text="Banner design service - static jpg - 30 credits" /> <small><a href="/popup/bannerdesignstaticjpg" onclick="openPopup('/popup/bannerdesignstaticjpg');return false;" target="_blank">learn more</a></small><br />
					<asp:RadioButton runat="server" onClick="Update('artwork');" GroupName="ArtworkRadioGroup" ID="ArtworkGifRadio" Text="Banner design service - animated gif - 50 credits" /> <small><a href="/popup/bannerdesignanimatedgif" onclick="openPopup('/popup/bannerdesignanimatedgif');return false;" target="_blank">learn more</a></small><br />
					<span runat="server" id="ArtworkFlashSpan"><asp:RadioButton runat="server" onClick="Update('artwork');" GroupName="ArtworkRadioGroup" ID="ArtworkFlashRadio" Text="Banner design service - flash movie - 100 credits" /> <small><a href="/popup/bannerdesignflashmovie" onclick="openPopup('/popup/bannerdesignflashmovie');return false;" target="_blank">learn more</a></small><br /></span>
					<span runat="server" id="ArtworkAutomaticSpan"><asp:RadioButton runat="server" onClick="Update('artwork');" GroupName="ArtworkRadioGroup" ID="ArtworkAutomaticRadio" Text="Automatic event banner - no artwork needed" /> <small><a href="/popup/bannerautomatic" onclick="openPopup('/popup/bannerautomatic');return false;" target="_blank">learn more</a></small></span>
				</td>
			</tr>
			<tr runat="server" id="AutomaticArtworkRow">
				<td class="BannerEditLabelCell">
					Automatic banner text:
				</td>
				<td class="BannerEditControlCell">
					<asp:HiddenField runat="server" ID="AutomaticEventBannerHidden" Value="" /><asp:TextBox Enabled="false" ReadOnly="true" runat="server" ID="AutomaticEventBannerTextBox" Columns="25" Text="automatic text from event" /><button runat="server" id="AutomaticEventBannerButton">edit...</button> <button onclick="AutomaticEventBannerHidden.value='';AutomaticEventBannerTextBox.value='text not customised';return false;">default</button>
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="NamePanel">
		<h2 class="BannerEditHead">
			Name
		</h2>
		<p>
			<small>Give your banner a name so you can easily recognise it in future.</small>
		</p>
		<asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="NameTextBox" ErrorMessage="Please choose a name." ID="NameValidator" />
		<table class="BannerEditTable">
			<tr>
				<td class="BannerEditLabelCell">
					Name:
				</td>
				<td class="BannerEditControlCell">
					<asp:TextBox runat="server" ID="NameTextBox" Columns="30"></asp:TextBox>
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="FolderPanel">
		<h2 class="BannerEditHead">
			Folder
		</h2>
		<p>
			<small>You can organise your banners into folders to keep them grouped together. You can add this banner to an existing folder or create a new folder.</small>
		</p>
		<asp:CustomValidator runat="server" Display="dynamic" ClientValidationFunction="FolderVal" OnServerValidate="FolderVal" ErrorMessage="Please choose an option in the folder section." ID="FolderValidator" />
		<table class="BannerEditTable">
			<tr>
				<td class="BannerEditLabelCell">
					Folder:
				</td>
				<td class="BannerEditControlCell">
					<span runat="server" id="FolderActionEventSpan"><asp:RadioButton runat="server" onClick="Update('folder');" GroupName="FolderActionRadioGroup" ID="FolderActionEventRadio" Text="Add to my event folder" /><br /></span>
					<asp:RadioButton runat="server" onClick="Update('folder');" GroupName="FolderActionRadioGroup" ID="FolderActionExistingRadio" Text="Add to an existing folder..." /> <asp:DropDownList runat="server" ID="FolderExistingDropDown"><asp:ListItem>My folder 1</asp:ListItem><asp:ListItem>My folder 2</asp:ListItem></asp:DropDownList><br />
					<asp:RadioButton runat="server" onClick="Update('folder');" GroupName="FolderActionRadioGroup" ID="FolderActionNewRadio" Text="Create a new folder, named..." /> <asp:TextBox runat="server" ID="FolderNewTextBox" Columns="30"></asp:TextBox>
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="BookPanel">
		<h2 class="BannerEditHead">
			Book this banner
		</h2>
		<p>
			<small>If you leave this banner pending, you can book it later with other banners to get a bigger discount. See the "book pending banners" page.</small>
		</p>
		<asp:CustomValidator Visible="false" runat="server" Display="dynamic" ClientValidationFunction="BookVal" OnServerValidate="BookVal" ErrorMessage="Please choose an option in the booking options section." ID="BookValidator" />
		<table class="BannerEditTable">
			<tr>
				<td class="BannerEditLabelCell">
					Booking:
				</td>
				<td class="BannerEditControlCell">
					<asp:RadioButton runat="server" GroupName="BookRadioGroup" ID="BookNowCreditsRadio" Text="Book now (pay with campaign credits)" /><br />
					<asp:RadioButton runat="server" GroupName="BookRadioGroup" ID="BookNowCashRadio" Text="Book now (pay with credit card / invoice)" /><br />
					<asp:RadioButton runat="server" GroupName="BookRadioGroup" ID="BookLaterRadio" Text="Leave this banner pending (pay later)" />
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:ValidationSummary runat="server" DisplayMode="BulletList" HeaderText="There's some problems with your banner:" ShowMessageBox="true" ShowSummary="false" />
	
	<asp:Panel runat="server" ID="BackNextPanel">
		<p>
			<button onclick="Back();">&lt;- back</button> <button onclick="Next();">next -&gt;</button>
			<asp:HiddenField runat="server" ID="CurrentPanelIndexHidden" />
		</p>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="Panel1">
		<p>
			<button id="Button3" runat="server" onserverclick="CancelButton_Click" causesvalidation="false">Cancel</button>&nbsp;
			<button ID="Button4" runat="server" onserverclick="DoneClick" text="Done" OnClick="HookupValidators();">Done</button>
		</p>
	</asp:Panel>
	
	<script>
		var ValidatorsHookedUp = false;
		var HasFixedCurrentEvent = <%= HasFixedCurrentEvent.ToString().ToLower() %>;
		var HasCurrentEvent = <%= HasCurrentEvent.ToString().ToLower() %>;
		
		var HasSingleUpcomingEvent = <%= HasSingleUpcomingEvent.ToString().ToLower() %>;
		var SingleUpcomingEventK = <%= SingleUpcomingEventK.ToString() %>;
		var SingleUpcomingEventDate = new Date(<%= SingleUpcomingEventDateString %>, 0, 0, 0);
		
		var PromoterDiscount = <%= CurrentPromoter.Discount.ToString() %>;
		var CurrentEventK = <%= CurrentEventK.ToString() %>;
		var CurrentEventDate = new Date(<%= CurrentEventDateString %>, 0, 0, 0);
		var CurrentDate = new Date(<%= CurrentDateString %>, 0, 0, 0);
		
		var LockedToEventLink = <%= LockedToEventLink.ToString().ToLower() %>;
		var LockedPosition = <%= LockedPosition.ToString().ToLower() %>;
		var LockedDates = <%= LockedDates.ToString().ToLower() %>;
		var LockedExposure = <%= LockedExposure.ToString().ToLower() %>;
		var LockedArtwork = <%= LockedArtwork.ToString().ToLower() %>;
		
		var SingleCreditPrice = <%= CurrentPromoter.CostPerCampaignCredit.ToString("0.00") %>;
		var DiscountCredits = new Array(<%= DiscountCreditsString %>);
		var DiscountLevels = new Array(<%= DiscountLevelsString %>);
		var ImpressionsPerCredit = new Array(0, <%= Banner.GetImpressionsPerCampaignCredit(Banner.Positions.Leaderboard) %>, <%= Banner.GetImpressionsPerCampaignCredit(Banner.Positions.Hotbox) %>, <%= Banner.GetImpressionsPerCampaignCredit(Banner.Positions.EmailBanner) %>, <%= Banner.GetImpressionsPerCampaignCredit(Banner.Positions.PhotoBanner) %>, <%= Banner.GetImpressionsPerCampaignCredit(Banner.Positions.Skyscraper) %>);
		//Leaderboard = 1, Hotbox = 2, EmailBanner = 3, PhotoBanner = 4, Skyscraper = 5
		
		var HasLinkEventDropDown = <%= HasLinkEventDropDown.ToString().ToLower() %>;
		var HasLinkBrandDropDown = <%= HasLinkBrandDropDown.ToString().ToLower() %>;
		var HasLinkVenueDropDown = <%= HasLinkVenueDropDown.ToString().ToLower() %>;
		var HasLinkTicketsDropDown = <%= HasLinkTicketsDropDown.ToString().ToLower() %>;
		
		var HasLinkEvents = <%= HasLinkEvents.ToString().ToLower() %>;
		var HasLinkBrands = <%= HasLinkBrands.ToString().ToLower() %>;
		var HasLinkVenues = <%= HasLinkVenues.ToString().ToLower() %>;
		var HasLinkTickets = <%= HasLinkTickets.ToString().ToLower() %>;
		
		var Edit = <%= Edit.ToString().ToLower() %>;
		
		var ModeExpertRadio = document.getElementById("<%=ModeExpertRadio.ClientID%>");
		var ModeBeginnerRadio = document.getElementById("<%=ModeBeginnerRadio.ClientID%>");
		
		var LinkPanel = document.getElementById("<%=LinkPanel.ClientID%>");
		var LinkEventRadio = document.getElementById("<%=LinkEventRadio.ClientID%>");
		var LinkBrandRadio = document.getElementById("<%=LinkBrandRadio.ClientID%>");
		var LinkVenueRadio = document.getElementById("<%=LinkVenueRadio.ClientID%>");
		var LinkTicketsRadio = document.getElementById("<%=LinkTicketsRadio.ClientID%>");
		var LinkCustomRadio = document.getElementById("<%=LinkCustomRadio.ClientID%>");
		var LinkEventDropDown = document.getElementById("<%=LinkEventDropDown.ClientID%>");
		var LinkBrandDropDown = document.getElementById("<%=LinkBrandDropDown.ClientID%>");
		var LinkVenueDropDown = document.getElementById("<%=LinkVenueDropDown.ClientID%>");
		var LinkTicketsDropDown = document.getElementById("<%=LinkTicketsDropDown.ClientID%>");
		var LinkCustomTextBox = document.getElementById("<%=LinkCustomTextBox.ClientID%>");
		
		var LinkValidator = document.getElementById("<%=LinkValidator.ClientID%>");
		var LinkCustomValidator = document.getElementById("<%=LinkCustomValidator.ClientID%>");
		
		var PositionPanel = document.getElementById("<%=PositionPanel.ClientID%>");
		var PositionHotboxRadio = document.getElementById("<%=PositionHotboxRadio.ClientID%>");
		var PositionLeaderboardRadio = document.getElementById("<%=PositionLeaderboardRadio.ClientID%>");
		var PositionSkyscraperRadio = document.getElementById("<%=PositionSkyscraperRadio.ClientID%>");
		var PositionPhotoBannerRadio = document.getElementById("<%=PositionPhotoBannerRadio.ClientID%>");
		var PositionEmailBannerRadio = document.getElementById("<%=PositionEmailBannerRadio.ClientID%>");
		
		var PositionValidator = document.getElementById("<%=PositionValidator.ClientID%>");
		
		var DatesPanel = document.getElementById("<%=DatesPanel.ClientID%>");
		var Dates1WeekRadio = document.getElementById("<%=Dates1WeekRadio.ClientID%>");
		var Dates2WeekRadio = document.getElementById("<%=Dates2WeekRadio.ClientID%>");
		var Dates3WeekRadio = document.getElementById("<%=Dates3WeekRadio.ClientID%>");
		var Dates4WeekRadio = document.getElementById("<%=Dates4WeekRadio.ClientID%>");
		var Dates1WeekSpan = document.getElementById("<%=Dates1WeekSpan.ClientID%>");
		var Dates2WeekSpan = document.getElementById("<%=Dates2WeekSpan.ClientID%>");
		var Dates3WeekSpan = document.getElementById("<%=Dates3WeekSpan.ClientID%>");
		var Dates4WeekSpan = document.getElementById("<%=Dates4WeekSpan.ClientID%>");
		var DatesCustomRadio = document.getElementById("<%=DatesCustomRadio.ClientID%>");
		var DatesStartDateRow = document.getElementById("<%=DatesStartDateRow.ClientID%>");
		var DatesEndDateRow = document.getElementById("<%=DatesEndDateRow.ClientID%>");
		var DatesStartCal = document.getElementById("<%=DatesStartCal.InnerClientID%>");
		var DatesEndCal = document.getElementById("<%=DatesEndCal.InnerClientID%>");
		var DatesAutoRow = document.getElementById("<%=DatesAutoRow.ClientID%>");
		
		var DatesValidator = document.getElementById("<%=DatesValidator.ClientID%>");
		var DatesCustomValidator = document.getElementById("<%=DatesCustomValidator.ClientID%>");
		var DatesCustomEndDateValidator = document.getElementById("<%=DatesCustomEndDateValidator.ClientID%>");
		
		function FireDatesValidators()
		{
			if (ValidatorsHookedUp)
			{
				ValidatorValidate(DatesValidator);
				ValidatorValidate(DatesCustomValidator);
				ValidatorValidate(DatesCustomEndDateValidator);
			}
		}

		var ExposurePanel = document.getElementById("<%=ExposurePanel.ClientID%>");
		var ExposureLightRadio = document.getElementById("<%=ExposureLightRadio.ClientID%>");
		var ExposureMediumRadio = document.getElementById("<%=ExposureMediumRadio.ClientID%>");
		var ExposureHeavyRadio = document.getElementById("<%=ExposureHeavyRadio.ClientID%>");
		var ExposureCustomRadio = document.getElementById("<%=ExposureCustomRadio.ClientID%>");
		var ImpressionsRow = document.getElementById("<%=ImpressionsRow.ClientID%>");
		var ImpressionsTextBox = document.getElementById("<%=ImpressionsTextBox.ClientID%>");
		var ExposureLevelP = document.getElementById("<%=ExposureLevelP.ClientID%>");
		var ExposureCostCampaignCreditsP = document.getElementById("<%=ExposureCostCampaignCreditsP.ClientID%>");
		var ExposureCostCashP = document.getElementById("<%=ExposureCostCashP.ClientID%>");
		var ImpressionsTextBox = document.getElementById("<%=ImpressionsTextBox.ClientID%>");
		
		var ExposureValidator = document.getElementById("<%=ExposureValidator.ClientID%>");
		var ExposureCustomValidator = document.getElementById("<%=ExposureCustomValidator.ClientID%>");
		
		function FireExposureValidators()
		{
			if (ValidatorsHookedUp)
			{
				ValidatorValidate(ExposureValidator);
				ValidatorValidate(ExposureCustomValidator);
			}
		}
		
		var TargettingPanel = document.getElementById("<%=TargettingPanel.ClientID%>");
		var TargettingAutomaticSpan = document.getElementById("<%=TargettingAutomaticSpan.ClientID%>");
		var TargettingAutomaticRadio = document.getElementById("<%=TargettingAutomaticRadio.ClientID%>");
		var TargettingNoneRadio = document.getElementById("<%=TargettingNoneRadio.ClientID%>");
		var TargettingCustomRadio = document.getElementById("<%=TargettingCustomRadio.ClientID%>");
		var LocationTargettingRow = document.getElementById("<%=LocationTargettingRow.ClientID%>");
		var LocationTargettingHidden = document.getElementById("<%=LocationTargettingHidden.ClientID%>");
		var MusicTargettingRow = document.getElementById("<%=MusicTargettingRow.ClientID%>");
		var MusicTargettingHidden = document.getElementById("<%=MusicTargettingHidden.ClientID%>");
		
		var TargettingValidator = document.getElementById("<%=TargettingValidator.ClientID%>");
		var TargettingCustomValidator = document.getElementById("<%=TargettingCustomValidator.ClientID%>");
		
		function FireTargettingValidators()
		{
			if (ValidatorsHookedUp)
			{
				ValidatorValidate(TargettingValidator);
				ValidatorValidate(TargettingCustomValidator);
			}
		}
		
		var ArtworkPanel = document.getElementById("<%=ArtworkPanel.ClientID%>");
		var ArtworkUploadRadio = document.getElementById("<%=ArtworkUploadRadio.ClientID%>");
		var ArtworkJpgRadio = document.getElementById("<%=ArtworkJpgRadio.ClientID%>");
		var ArtworkGifRadio = document.getElementById("<%=ArtworkGifRadio.ClientID%>");
		var ArtworkFlashSpan = document.getElementById("<%=ArtworkFlashSpan.ClientID%>");
		var ArtworkFlashRadio = document.getElementById("<%=ArtworkFlashRadio.ClientID%>");
		var ArtworkAutomaticRadio = document.getElementById("<%=ArtworkAutomaticRadio.ClientID%>");
		var ArtworkAutomaticSpan = document.getElementById("<%=ArtworkAutomaticSpan.ClientID%>");
		var AutomaticArtworkRow = document.getElementById("<%=AutomaticArtworkRow.ClientID%>");
		var AutomaticEventBannerHidden = document.getElementById("<%=AutomaticEventBannerHidden.ClientID%>");
		var AutomaticEventBannerTextBox = document.getElementById("<%=AutomaticEventBannerTextBox.ClientID%>");
		
		var ArtworkValidator = document.getElementById("<%=ArtworkValidator.ClientID%>");
		
		var NamePanel = document.getElementById("<%=NamePanel.ClientID%>");
		
		var FolderPanel = document.getElementById("<%=FolderPanel.ClientID%>");
		var FolderActionEventRadio = document.getElementById("<%=FolderActionEventRadio.ClientID%>");
		var FolderActionExistingRadio = document.getElementById("<%=FolderActionExistingRadio.ClientID%>");
		var FolderActionNewRadio = document.getElementById("<%=FolderActionNewRadio.ClientID%>");
		var FolderExistingDropDown = document.getElementById("<%=FolderExistingDropDown.ClientID%>");
		var FolderNewTextBox = document.getElementById("<%=FolderNewTextBox.ClientID%>");
		var FolderActionEventSpan = document.getElementById("<%=FolderActionEventSpan.ClientID%>");
		
		var FolderValidator = document.getElementById("<%=FolderValidator.ClientID%>");
		
		var BookPanel = document.getElementById("<%=BookPanel.ClientID%>");
		var BookNowCreditsRadio = document.getElementById("<%=BookNowCreditsRadio.ClientID%>");
		var BookNowCashRadio = document.getElementById("<%=BookNowCashRadio.ClientID%>");
		var BookLaterRadio = document.getElementById("<%=BookLaterRadio.ClientID%>");
		
		var BookValidator = document.getElementById("<%=BookValidator.ClientID%>");
		
		var BackNextPanel = document.getElementById("<%=BackNextPanel.ClientID%>");
		var CurrentPanelIndexHidden = document.getElementById("<%=CurrentPanelIndexHidden.ClientID%>");

		function DisplayLinkEventDropDown(){ return !HasFixedCurrentEvent && HasLinkEventDropDown && LinkEventRadio.checked; }
		function DisplayLinkBrandDropDown(){ return !HasFixedCurrentEvent && HasLinkBrandDropDown && LinkBrandRadio.checked; }
		function DisplayLinkVenueDropDown(){ return !HasFixedCurrentEvent && HasLinkVenueDropDown && LinkVenueRadio.checked; }
		function DisplayLinkTicketsDropDown(){ return !HasFixedCurrentEvent && HasLinkTicketsDropDown && LinkTicketsRadio.checked; }
		function DisplayLinkCustomTextBox(){ return !HasFixedCurrentEvent && LinkCustomRadio.checked; }
		function DisplayDatesAutoRow(){ return !LockedDates && HasCurrentEvent && CurrentEventDate >= CurrentDate; }
		function DisplayDates1WeekSpan(){ return !LockedDates && DisplayDatesAutoRow(); }
		function DisplayDates2WeekSpan(){ return !LockedDates && (CurrentEventDate - (7 * 86400000)) >= CurrentDate; }
		function DisplayDates3WeekSpan(){ return !LockedDates && (CurrentEventDate - (14 * 86400000)) >= CurrentDate; }
		function DisplayDates4WeekSpan(){ return !LockedDates && (CurrentEventDate - (21 * 86400000)) >= CurrentDate; }
		function DisplayCustomDatesRows(){ return !LockedDates && ((DisplayDatesAutoRow() && DatesCustomRadio.checked) || !DisplayDatesAutoRow()); }
		function DisplayImpressionsRow(){ return !LockedExposure && ExposureCustomRadio.checked; }
		function DisplayTargettingAutomaticSpan(){ return HasCurrentEvent; }
		function DisplayTargettingRows(){ return TargettingCustomRadio.checked; }
		function DisplayArtworkFlashSpan() { return !LockedArtwork && !PositionEmailBannerRadio.checked; }
		function DisplayArtworkAutomaticSpan(){ return !LockedArtwork && HasCurrentEvent && (PositionLeaderboardRadio.checked || PositionPhotoBannerRadio.checked); }
		function DisplayAutomaticArtworkRow(){ return  (DisplayArtworkAutomaticSpan() || LockedArtwork) && ArtworkAutomaticRadio.checked; }
		function DisplayFolderExistingDropDown(){ return FolderActionExistingRadio.checked; }
		function DisplayFolderNewTextBox(){ return FolderActionNewRadio.checked; }
		function DisplayFolderActionEventSpan(){ return HasCurrentEvent; }

		function LinkVal(sender, args)
		{
			args.IsValid = 
				!DisplayLinkPanel() ||
				HasFixedCurrentEvent ||
				(HasLinkEvents && (LockedToEventLink || LinkEventRadio.checked) && (!HasLinkEventDropDown || LinkEventDropDown[LinkEventDropDown.selectedIndex].value != "0")) ||
				(HasLinkBrands && LinkBrandRadio.checked && (!HasLinkBrandDropDown || LinkBrandDropDown[LinkBrandDropDown.selectedIndex].value != "0")) || 
				(HasLinkVenues && LinkVenueRadio.checked && (!HasLinkVenueDropDown || LinkVenueDropDown[LinkVenueDropDown.selectedIndex].value != "0")) || 
				(HasLinkTickets && LinkTicketsRadio.checked && (!HasLinkTicketsDropDown || LinkTicketsDropDown[LinkTicketsDropDown.selectedIndex].value != "0")) || 
				LinkCustomRadio.checked;
				
				//Changed from: (DaveB, 13/09/07)
				//(!HasLinkEvents || ((LockedToEventLink || LinkEventRadio.checked) && (!HasLinkEventDropDown || LinkEventDropDown[LinkEventDropDown.selectedIndex].value != "0"))) ||
				//(!HasLinkBrands || (LinkBrandRadio.checked && (!HasLinkBrandDropDown || LinkBrandDropDown[LinkBrandDropDown.selectedIndex].value != "0"))) || 
				//(!HasLinkVenues || (LinkVenueRadio.checked && (!HasLinkVenueDropDown || LinkVenueDropDown[LinkVenueDropDown.selectedIndex].value != "0"))) || 
				//(!HasLinkTickets || (LinkTicketsRadio.checked && (!HasLinkTicketsDropDown || LinkTicketsDropDown[LinkTicketsDropDown.selectedIndex].value != "0"))) || 
		}
		function LinkCustomVal(sender, args)
		{
			args.IsValid = 
				!DisplayLinkPanel() || 
				!LinkCustomRadio.checked || 
				(LinkCustomTextBox.value.substring(0,7) == "http://" || LinkCustomTextBox.value.substring(0,8) == "https://");
		}
		
		function PositionVal(sender, args)
		{
			args.IsValid = 
				!DisplayPositionPanel() ||
				LockedPosition || 
				PositionHotboxRadio.checked ||
				PositionLeaderboardRadio.checked ||
				PositionSkyscraperRadio.checked ||
				PositionPhotoBannerRadio.checked ||
				PositionEmailBannerRadio.checked;
		}
		
		function DatesVal(sender, args)
		{
			args.IsValid = 
				!DisplayDatesPanel() ||
				LockedDates ||
				!DisplayDatesAutoRow() ||
				(DisplayDates1WeekSpan() && Dates1WeekRadio.checked) ||
				(DisplayDates2WeekSpan() && Dates2WeekRadio.checked) ||
				(DisplayDates3WeekSpan() && Dates3WeekRadio.checked) ||
				(DisplayDates4WeekSpan() && Dates4WeekRadio.checked) ||
				DatesCustomRadio.checked;
		}
		function DatesCustomVal(sender, args)
		{
			args.IsValid = 
				!DisplayDatesPanel() ||
				LockedDates ||
				(DisplayDatesAutoRow() && !DatesCustomRadio.checked) ||
				CustomDatesAreComplete();
		}
		function CustomDatesAreComplete()
		{
			return isValidDate(DatesStartCal.value) && isValidDate(DatesEndCal.value);
		}
		function DatesCustomEndDateVal(sender, args)
		{
			if (DisplayDatesPanel() && !LockedDates && (!DisplayDatesAutoRow() || DatesCustomRadio.checked) && CustomDatesAreComplete())
			{
				var endDate = new Date(parseInt(DatesEndCal.value.split("/")[2], 10), parseInt(DatesEndCal.value.split("/")[1], 10) - 1, parseInt(DatesEndCal.value.split("/")[0], 10));
				args.IsValid = endDate >= CurrentDate;
			}
			else
				args.IsValid = true;
		}
		function isValidDate(dateStr)
		{
			var datePat = /^(\d{1,2})(\/|-)(\d{1,2})\2(\d{2}|\d{4})$/;
			var matchArray = dateStr.match(datePat);
			if (matchArray == null)
			{
				return false;
			}
			day = matchArray[1];
			month = matchArray[3];
			year = matchArray[4];
			if (month < 1 || month > 12)
			{
				return false;
			}
			if (day < 1 || day > 31)
			{
				return false;
			}
			if ((month==4 || month==6 || month==9 || month==11) && day==31)
			{
				return false
			}
			if (month == 2)
			{
				var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
				if (day>29 || (day==29 && !isleap))
				{
					return false;
				}
			}
			return true;
		}

		function ExposureVal(sender, args)
		{
			args.IsValid = 
				!DisplayExposurePanel() ||
				LockedExposure ||
				ExposureLightRadio.checked ||
				ExposureMediumRadio.checked ||
				ExposureHeavyRadio.checked ||
				ExposureCustomRadio.checked;
		}
		function ExposureCustomVal(sender, args)
		{
			args.IsValid = 
				!DisplayExposurePanel() ||
				LockedExposure ||
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
		
		function ArtworkVal(sender, args)
		{
			args.IsValid = 
				!DisplayArtworkPanel() ||
				LockedArtwork ||
				ArtworkUploadRadio.checked ||
				ArtworkJpgRadio.checked ||
				ArtworkGifRadio.checked ||
				(DisplayArtworkFlashSpan() && ArtworkFlashRadio.checked) ||
				(DisplayArtworkAutomaticSpan() && ArtworkAutomaticRadio.checked);
		}
		
		function TargettingVal(sender, args)
		{
			args.IsValid = 
				!DisplayTargettingPanel() ||
				(DisplayTargettingAutomaticSpan() && TargettingAutomaticRadio.checked) ||
				TargettingNoneRadio.checked ||
				TargettingCustomRadio.checked;
		}
		function TargettingCustomVal(sender, args)
		{
			args.IsValid = 
				!DisplayTargettingPanel() ||
				!TargettingCustomRadio.checked ||
				(LocationTargettingHidden.value != "" && MusicTargettingHidden.value != "");
		}
		
		function FolderVal(sender, args)
		{
			args.IsValid = 
				!DisplayFolderPanel() ||
				(DisplayFolderActionEventSpan() && FolderActionEventRadio.checked) ||
				(FolderActionExistingRadio.checked && FolderExistingDropDown[FolderExistingDropDown.selectedIndex].value != "0") ||
				(FolderActionNewRadio.checked && FolderNewTextBox.value != "");
		}
		
		function BookVal(sender, args)
		{
			args.IsValid = 
				!DisplayBookPanel() ||
				BookNowCreditsRadio.checked ||
				BookNowCashRadio.checked ||
				BookLaterRadio.checked;
		}
		
		function Update(source)
		{
		
			if (source == "link")
			{
				UpdateCurrentEvent();
			}

			LinkEventDropDown.style.display = DisplayLinkEventDropDown() ? "" : "none";
			LinkBrandDropDown.style.display = DisplayLinkBrandDropDown() ? "" : "none";
			LinkVenueDropDown.style.display = DisplayLinkVenueDropDown() ? "" : "none";
			LinkTicketsDropDown.style.display = DisplayLinkTicketsDropDown() ? "" : "none";
			LinkCustomTextBox.style.display = DisplayLinkCustomTextBox() ? "" : "none";
			DatesAutoRow.style.display = DisplayDatesAutoRow() ? "" : "none";
			Dates1WeekSpan.style.display = DisplayDates1WeekSpan() ? "" : "none";
			Dates2WeekSpan.style.display = DisplayDates2WeekSpan() ? "" : "none";
			Dates3WeekSpan.style.display = DisplayDates3WeekSpan() ? "" : "none";
			Dates4WeekSpan.style.display = DisplayDates4WeekSpan() ? "" : "none";
			DatesStartDateRow.style.display = DisplayCustomDatesRows() ? "" : "none";
			DatesEndDateRow.style.display = DisplayCustomDatesRows() ? "" : "none";
			ImpressionsRow.style.display = DisplayImpressionsRow() ? "" : "none";
			TargettingAutomaticSpan.style.display = DisplayTargettingAutomaticSpan() ? "" : "none";
			LocationTargettingRow.style.display = DisplayTargettingRows() ? "" : "none";
			MusicTargettingRow.style.display = DisplayTargettingRows() ? "" : "none";
			ArtworkFlashSpan.style.display = DisplayArtworkFlashSpan() ? "" : "none";
			ArtworkAutomaticSpan.style.display = DisplayArtworkAutomaticSpan() ? "" : "none";
			AutomaticArtworkRow.style.display = DisplayAutomaticArtworkRow() ? "" : "none";
			FolderExistingDropDown.style.display = DisplayFolderExistingDropDown() ? "" : "none";
			FolderNewTextBox.style.display = DisplayFolderNewTextBox() ? "" : "none";
			FolderActionEventSpan.style.display = DisplayFolderActionEventSpan() ? "" : "none";

			UpdatePrices();
		}
		
		function UpdateCurrentEvent()
		{
			if (HasFixedCurrentEvent)
				return;
			
			if (LinkEventRadio.checked && HasSingleUpcomingEvent)
			{
				HasCurrentEvent = true;
				urrentEventK = SingleUpcomingEventK;
				CurrentEventDate = SingleUpcomingEventDate;
			}
			else if (LinkEventRadio.checked)
			{
				HasCurrentEvent = true;
				var splitVal = LinkEventDropDown[LinkEventDropDown.selectedIndex].value.split(",");
				CurrentEventK = parseInt(splitVal[0]);
				CurrentEventDate = new Date(parseInt(splitVal[1]), parseInt(splitVal[2]), parseInt(splitVal[3]), 0, 0, 0);
			}
			else
			{
				HasCurrentEvent = false;
			}
		}
		
		function UpdatePrices()
		{
		
			var totalDays = GetTotalDays();
			var impressionsPerCredit = GetImpressionsPerCredit();
			var selectedBannerPosition = GetSelectedBannerPosition();
			
			if (totalDays==0)
			{
				var dateError = GetDateError();
				
				if (dateError == 1)
				{
					ExposureLevelP.innerHTML = "Error when calculating price - please check the dates.";
					ExposureCostCampaignCreditsP.innerHTML = "Error when calculating price - please check the dates.";
					ExposureCostCashP.innerHTML = "Error when calculating price - please check the dates.";
					return;
				}
				else if (dateError == 2)
				{
					ExposureLevelP.innerHTML = "Please check your dates - the end date seems to be before the start date!";
					ExposureCostCampaignCreditsP.innerHTML = "Please check your dates - the end date seems to be before the start date!";
					ExposureCostCashP.innerHTML = "Please check your dates - the end date seems to be before the start date!";
					return;
				}
				else
				{
					ExposureLevelP.innerHTML = "Choose some dates to work out your exposure.";
					ExposureCostCampaignCreditsP.innerHTML = "Choose some dates to work out a price.";
					ExposureCostCashP.innerHTML = "Choose some dates to work out a price.";
					return;
				}
			}
			
			if (ExposureCustomRadio.checked)
			{
				var impressions = parseInt(ImpressionsTextBox.value.replace(new RegExp(",", "g"),""));
				var credits = Math.ceil(impressions / impressionsPerCredit);
				
				ExposureLevelP.innerHTML = "Exposure: " + GetExposure(credits, totalDays);
				ExposureCostCampaignCreditsP.innerHTML = selectedBannerPosition==0 ? "Select a banner position to work out a price." : "Price in campaign credits: " + tsep(credits) + " credits";
				ExposureCostCashP.innerHTML = selectedBannerPosition==0 ? "Select a banner position to work out a price." : "Cash price: £" + tsep(GetCashPrice(credits).toFixed(2)) + (GetDiscount(credits) > 0 ? " (this includes a " + GetDiscount(credits) + "% discount)" : "");
			}
			else
			{
				var exposureLevel = GetSelectedExposureLevel();
				if (exposureLevel > 0)
				{
					var creditsPerDay = GetCreditsPerDay(exposureLevel);
					
					if (selectedBannerPosition>0)
					{
						var credits = creditsPerDay * totalDays;
						var totalImpressions = credits * impressionsPerCredit;
						
						ImpressionsTextBox.value = tsep(totalImpressions);
						
						ExposureLevelP.innerHTML = "Actual impressions: " + tsep(totalImpressions);
						ExposureCostCampaignCreditsP.innerHTML = "Price in campaign credits: " + tsep(credits) + " credits";
						ExposureCostCashP.innerHTML = "Cash price: £" + tsep(GetCashPrice(credits).toFixed(2)) + (GetDiscount(credits) > 0 ? " (this includes a " + GetDiscount(credits) + "% discount)" : "");
					}
					else
					{
						ExposureLevelP.innerHTML = "Select a banner position to work out your impressions.";
						ExposureCostCampaignCreditsP.innerHTML = "Select a banner position to work out a price.";
						ExposureCostCashP.innerHTML = "Select a banner position to work out a price.";
					}
				}
				else
				{
					ExposureLevelP.innerHTML = "Choose an exposure level...";
					ExposureCostCampaignCreditsP.innerHTML = "Choose an exposure level to work out a price.";
					ExposureCostCashP.innerHTML = "Choose an exposure level to work out a price.";
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
		
		//Leaderboard = 1,
		//Hotbox = 2,
		//EmailBanner = 3,
		//PhotoBanner = 4,
		//Skyscraper = 5
		function GetSelectedBannerPosition()
		{
			if (PositionHotboxRadio.checked)
				return 2;
			else if (PositionLeaderboardRadio.checked)
				return 1;
			else if (PositionSkyscraperRadio.checked)
				return 5;
			else if (PositionPhotoBannerRadio.checked)
				return 4;
			else if (PositionEmailBannerRadio.checked)
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
			if (PromoterDiscount > discountLevel)
				return PromoterDiscount;
			else
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
		
		function GetImpressionsPerCredit()
		{
			return ImpressionsPerCredit[GetSelectedBannerPosition()];
		}
		
		function GetDaysBetweenDates(startDate, endDate)
		{
			return Math.round((endDate - startDate) / 86400000); // Math.round accounts for daylight saving
		}
		
		function GetDateError()
		{
			if (!DisplayDatesAutoRow() || DatesCustomRadio.checked)
			{
				try
				{
					if (!isValidDate(DatesStartCal.value) || !isValidDate(DatesEndCal.value))
						return 1;
				
					var startDate = new Date(parseInt(DatesStartCal.value.split("/")[2], 10), parseInt(DatesStartCal.value.split("/")[1], 10) - 1, parseInt(DatesStartCal.value.split("/")[0], 10));
					var endDate = new Date(parseInt(DatesEndCal.value.split("/")[2], 10), parseInt(DatesEndCal.value.split("/")[1], 10) - 1, parseInt(DatesEndCal.value.split("/")[0], 10));
					
					var days = GetDaysBetweenDates(startDate, endDate);
					
					if (isNaN(days))
						return 1;
					else if (days<0)
						return 2;
					else
						return 0;
				}
				catch (ex)
				{
					return 1;
				}
			}
			else
				return 0;
		}
	
		function GetTotalDays()
		{
			if (DisplayDatesAutoRow() && ((DisplayDates1WeekSpan() && Dates1WeekRadio.checked) || (DisplayDates2WeekSpan() && Dates2WeekRadio.checked) || (DisplayDates3WeekSpan() && Dates3WeekRadio.checked) || (DisplayDates4WeekSpan() && Dates4WeekRadio.checked)))
			{
				var endDate = CurrentEventDate;
				var startDate;
				
				if (Dates1WeekRadio.checked)
					startDate = CurrentEventDate - (7 * 86400000);
				else if (Dates2WeekRadio.checked)
					startDate = CurrentEventDate - (14 * 86400000);
				else if (Dates3WeekRadio.checked)
					startDate = CurrentEventDate - (21 * 86400000);
				else if (Dates4WeekRadio.checked)
					startDate = CurrentEventDate - (28 * 86400000);
				
				if (startDate<CurrentDate)
					startDate = CurrentDate;
					
				var days = GetDaysBetweenDates(startDate, endDate);
				
				if (isNaN(days))
					return 0;
				else if (days<0)
					return 1;
				else
					return days + 1;
			}
			else if (!DisplayDatesAutoRow() || DatesCustomRadio.checked)
			{
				try
				{
					if (!isValidDate(DatesStartCal.value) || !isValidDate(DatesEndCal.value))
						return 0;
					
					var startDate = new Date(parseInt(DatesStartCal.value.split("/")[2], 10), parseInt(DatesStartCal.value.split("/")[1], 10) - 1, parseInt(DatesStartCal.value.split("/")[0], 10));
					var endDate = new Date(parseInt(DatesEndCal.value.split("/")[2], 10), parseInt(DatesEndCal.value.split("/")[1], 10) - 1, parseInt(DatesEndCal.value.split("/")[0], 10));
					
					var days = GetDaysBetweenDates(startDate, endDate);
					
					if (isNaN(days) || days<0)
						return 0;
					else
						return days + 1;
				}
				catch (ex)
				{
					return 0;
				}
			}
			else
				return 0;
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
		
		function BeginnerMode()
		{
			return ModeBeginnerRadio.checked;
		}
		function GetCurrentPanelIndex()
		{
			return parseInt(CurrentPanelIndexHidden.value);
		}
		function Back()
		{
		
			var MinPanel = HasFixedCurrentEvent ? 1 : 0;
			var CurrentPanelIndex = GetCurrentPanelIndex();
			if (CurrentPanelIndex > MinPanel)
			{
				CurrentPanelIndexHidden.value = CurrentPanelIndex - 1;
				RefreshPanels();
			}
		}
		function Next()
		{
			var CurrentPanelIndex = GetCurrentPanelIndex();
			if (CurrentPanelIndex < 7)
			{
				CurrentPanelIndexHidden.value = CurrentPanelIndex + 1;
				RefreshPanels();
			}
		}
				
		function ModeChange(update)
		{
			RefreshPanels();
		}
		
		function DisplayBackNextPanel() { return BeginnerMode(); }
		function DisplayLinkPanel() { return ((!BeginnerMode() && !HasFixedCurrentEvent) || (BeginnerMode() && GetCurrentPanelIndex() == 0)); }
		function DisplayPositionPanel() { return (!BeginnerMode() || (BeginnerMode() && GetCurrentPanelIndex() == 1)); }
		function DisplayDatesPanel() { return !BeginnerMode() || GetCurrentPanelIndex() == 2; }
		function DisplayExposurePanel() { return !BeginnerMode() || GetCurrentPanelIndex() == 3; }
		function DisplayTargettingPanel() { return !BeginnerMode() || GetCurrentPanelIndex() == 4; }
		function DisplayArtworkPanel() { return !BeginnerMode() || GetCurrentPanelIndex() == 5; }
		function DisplayNamePanel() { return !BeginnerMode() || GetCurrentPanelIndex() == 6; }
		function DisplayFolderPanel() { return !BeginnerMode() || GetCurrentPanelIndex() == 7; }
		function DisplayBookPanel() { return false; }//!BeginnerMode() || GetCurrentPanelIndex() == 8; }
		
		function RefreshPanels()
		{
			BackNextPanel.style.display = DisplayBackNextPanel() ? "" : "none";
			LinkPanel.style.display = DisplayLinkPanel() ? "" : "none";
			PositionPanel.style.display = DisplayPositionPanel() ? "" : "none";
			DatesPanel.style.display = DisplayDatesPanel() ? "" : "none";
			ExposurePanel.style.display = DisplayExposurePanel() ? "" : "none";
			TargettingPanel.style.display = DisplayTargettingPanel() ? "" : "none";
			ArtworkPanel.style.display = DisplayArtworkPanel() ? "" : "none";
			NamePanel.style.display = DisplayNamePanel() ? "" : "none";
			FolderPanel.style.display = DisplayFolderPanel() ? "" : "none";
			BookPanel.style.display = DisplayBookPanel() ? "" : "none";
 		}
		

		function SetMusicTargettingString(musicTargettingString)
		{
			document.getElementById('<%=MusicTargettingTextBox.ClientID%>').value = 
				(musicTargettingString.toString() == '1')
					? 'all music types'
					: musicTargettingString.split(',').length + ' music type' + (musicTargettingString.split(',').length == 1 ? '' : 's');
			document.getElementById('<%=MusicTargettingHidden.ClientID%>').value = musicTargettingString;
			
			FireTargettingValidators();
		}
		
		function SetPlaceTargettingString(placeTargettingString)
		{
			placeTargettingString = unescape(placeTargettingString);
			document.getElementById('<%= LocationTargettingHidden.ClientID %>').value = placeTargettingString;
			var numTowns = 0;
			if (placeTargettingString.toString().length > 0) numTowns = placeTargettingString.toString().split(',').length;
			var value = ((numTowns == 0) ? 'no' : numTowns) + ' town' + ((numTowns == 1) ? '' : 's') + ' selected';
			document.getElementById('<%= LocationTargettingTextBox.ClientID %>').value = value;
			
			FireTargettingValidators();
		}
		
		function SetAutomaticEventBanner(value)
		{
			document.getElementById('<%= AutomaticEventBannerHidden.ClientID %>').value = unescape(value);
			if (unescape(value).toString().indexOf('<DisplayType>2</DisplayType>') > 0) { document.getElementById('<%= AutomaticEventBannerTextBox.ClientID %>').value = "customised text"; }
			else { document.getElementById('<%= AutomaticEventBannerTextBox.ClientID %>').value = "automatic text from event"; }
		}
		
		function HookupValidators()
		{
			if (!ValidatorsHookedUp)
			{
				ValidatorsHookedUp = true;
				ValidatorHookupControl(LinkEventRadio, LinkValidator);
				ValidatorHookupControl(LinkBrandRadio, LinkValidator);
				ValidatorHookupControl(LinkVenueRadio, LinkValidator);
				ValidatorHookupControl(LinkTicketsRadio, LinkValidator);
				ValidatorHookupControl(LinkCustomRadio, LinkValidator);
				ValidatorHookupControl(LinkEventDropDown, LinkValidator);
				ValidatorHookupControl(LinkBrandDropDown, LinkValidator);
				ValidatorHookupControl(LinkVenueDropDown, LinkValidator);
				ValidatorHookupControl(LinkTicketsDropDown, LinkValidator);
				ValidatorHookupControl(LinkCustomTextBox, LinkValidator);
				
				ValidatorHookupControl(LinkEventRadio, LinkCustomValidator);
				ValidatorHookupControl(LinkBrandRadio, LinkCustomValidator);
				ValidatorHookupControl(LinkVenueRadio, LinkCustomValidator);
				ValidatorHookupControl(LinkTicketsRadio, LinkCustomValidator);
				ValidatorHookupControl(LinkCustomRadio, LinkCustomValidator);
				ValidatorHookupControl(LinkCustomTextBox, LinkCustomValidator);
				
				ValidatorHookupControl(PositionHotboxRadio, PositionValidator);
				ValidatorHookupControl(PositionLeaderboardRadio, PositionValidator);
				ValidatorHookupControl(PositionSkyscraperRadio, PositionValidator);
				ValidatorHookupControl(PositionPhotoBannerRadio, PositionValidator);
				ValidatorHookupControl(PositionEmailBannerRadio, PositionValidator);
				
				ValidatorHookupControl(Dates1WeekRadio, DatesValidator);
				ValidatorHookupControl(Dates2WeekRadio, DatesValidator);
				ValidatorHookupControl(Dates3WeekRadio, DatesValidator);
				ValidatorHookupControl(Dates4WeekRadio, DatesValidator);
				ValidatorHookupControl(DatesCustomRadio, DatesValidator);
				ValidatorHookupControl(Dates1WeekRadio, DatesCustomValidator);
				ValidatorHookupControl(Dates2WeekRadio, DatesCustomValidator);
				ValidatorHookupControl(Dates3WeekRadio, DatesCustomValidator);
				ValidatorHookupControl(Dates4WeekRadio, DatesCustomValidator);
				ValidatorHookupControl(DatesCustomRadio, DatesCustomValidator);
				ValidatorHookupControl(Dates1WeekRadio, DatesCustomEndDateValidator);
				ValidatorHookupControl(Dates2WeekRadio, DatesCustomEndDateValidator);
				ValidatorHookupControl(Dates3WeekRadio, DatesCustomEndDateValidator);
				ValidatorHookupControl(Dates4WeekRadio, DatesCustomEndDateValidator);
				ValidatorHookupControl(DatesCustomRadio, DatesCustomEndDateValidator);
				
				ValidatorHookupControl(ExposureLightRadio, ExposureValidator);
				ValidatorHookupControl(ExposureMediumRadio, ExposureValidator);
				ValidatorHookupControl(ExposureHeavyRadio, ExposureValidator);
				ValidatorHookupControl(ExposureCustomRadio, ExposureValidator);
				ValidatorHookupControl(ImpressionsTextBox, ExposureValidator);
				
				ValidatorHookupControl(ExposureLightRadio, ExposureCustomValidator);
				ValidatorHookupControl(ExposureMediumRadio, ExposureCustomValidator);
				ValidatorHookupControl(ExposureHeavyRadio, ExposureCustomValidator);
				ValidatorHookupControl(ExposureCustomRadio, ExposureCustomValidator);
				ValidatorHookupControl(ImpressionsTextBox, ExposureCustomValidator);
				
				ValidatorHookupControl(TargettingAutomaticRadio, TargettingValidator);
				ValidatorHookupControl(TargettingNoneRadio, TargettingValidator);
				ValidatorHookupControl(TargettingCustomRadio, TargettingValidator);
				ValidatorHookupControl(TargettingAutomaticRadio, TargettingCustomValidator);
				ValidatorHookupControl(TargettingNoneRadio, TargettingCustomValidator);
				ValidatorHookupControl(TargettingCustomRadio, TargettingCustomValidator);
				
				ValidatorHookupControl(ArtworkUploadRadio, ArtworkValidator);
				ValidatorHookupControl(ArtworkJpgRadio, ArtworkValidator);
				ValidatorHookupControl(ArtworkGifRadio, ArtworkValidator);
				ValidatorHookupControl(ArtworkFlashRadio, ArtworkValidator);
				ValidatorHookupControl(ArtworkAutomaticRadio, ArtworkValidator);
				
				ValidatorHookupControl(FolderActionEventRadio, FolderValidator);
				ValidatorHookupControl(FolderActionExistingRadio, FolderValidator);
				ValidatorHookupControl(FolderActionNewRadio, FolderValidator);
				ValidatorHookupControl(FolderExistingDropDown, FolderValidator);
				ValidatorHookupControl(FolderNewTextBox, FolderValidator);
				
				//ValidatorHookupControl(BookNowCreditsRadio, BookValidator);
				//ValidatorHookupControl(BookNowCashRadio, BookValidator);
				//ValidatorHookupControl(BookLaterRadio, BookValidator);
			}
		}
		
		setTimeout("Update('link');", 200);
	</script>


</div>

