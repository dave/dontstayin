<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Events.Home" %>
<%@ Register TagPrefix="Controls" TagName="Latest" Src="/Controls/Latest.ascx" %>
<%@ Register TagPrefix="Headers" TagName="Event" Src="/Controls/Headers/EventHeader.ascx" %>
<%@ Register TagPrefix="Controls" TagName="AttendedEventControl" Src="/Controls/AttendedEventControl.ascx" %>




<script language="JavaScript">
	var min = 0;
	var max = <%= Vars.TICKETS_MAX_PER_USR %>;
	
  function EventHomeUpdateTextBoxNumber (textBoxId, amount)
  {
	var textBox = document.getElementById(textBoxId);
	
	textBox.value = parseInt(textBox.value) + parseInt(amount);
	
	EventHomeKeepNumberWithinLimits(textBox);
  }
  
  function EventHomeKeepNumberWithinLimits(textBox)
  {
	if(textBox.value == null || isNaN(textBox.value))
		textBox.value = 0;
	else if(textBox.value > max)
		textBox.value = max;
	else if(textBox.value < min)
		textBox.value = min;
  }
  
</script>

<asp:Panel Runat="server" ID="EventSelectedPanel">
	<Headers:Event runat="server" SuppressLink="true">
		<asp:Panel Runat="server" ID="PromoterPanel">
			<p>
				<small>
					Do you promote or organise this event? <a href="/pages/promoters/intro">Sign up as a promoter</a>
					and our team will help you get the most out of DontStayIn.
				</small>
			</p>
		</asp:Panel>
	</Headers:Event>

	
	<asp:Panel Runat="server" ID="PhotoUploadPanel">
		<a name="UploadControl"></a>
		<dsi:h1 runat="server" ID="H13">Upload photos</dsi:h1>
		<div class="ContentBorder" style="padding:0px;border:0px;margin-top:-7px;">
			<asp:Panel Runat="server" ID="PanelUpload" style="padding:0px;margin:0px;">
			

				<script type="text/javascript" src="/misc/jquery/plupload/js/plupload.full.js"></script>
				<script type="text/javascript" src="/misc/jquery/plupload/js/jquery-plupload-queue/jquery.plupload.queue.js"></script>
	
				<script type="text/javascript">
					$(function () {
						var uploader = new plupload.Uploader({
							runtimes: 'html5,gears,silverlight,flash',
							//runtimes: 'flash',
							browse_button: 'uploadPanel',
							container: 'container',
							//max_file_size: '10mb',
							url: 'http://<%= Vars.DomainName %>/support/photoUpload.aspx?EventK=<%= CurrentEvent.K.ToString() %>&source=plupload',
							flash_swf_url: '/misc/jquery/plupload/js/plupload.flash.swf',
							silverlight_xap_url: '/misc/jquery/plupload/js/plupload.silverlight.xap',
							filters: [
								{ title: "Image files", extensions: "jpg,jpeg,jpe,gif,png" },
								{ title: "Video files", extensions: "avi,dv,mov,qt,mpg,mpeg,mp4,3gp,3g2,asf,wmv" },
								{ title: "Zip files", extensions: "zip" }
							],
							//resize: { width: 320, height: 240, quality: 90 },
							drop_element: 'uploadPanel'
						});

						uploader.bind('Init', function (up) {
							if (ImportedUtilities.U.isTrue(up.features, "dragdrop"))
								$('#uploadPanel').html("<img src='/gfx/new-user-upload-photos.png' width='69' height='43' /><br /><b>Drag photos here to upload</b>");
							else
								$('#uploadPanel').html("<img src='/gfx/new-user-upload-photos.png' width='69' height='43' /><br /><b>Click here to upload photos</b>");
						});

						uploader.bind('Error', function (up, error) {
							$('#uploadPanel').html("Oops, there's been an error loading the uploader.");
						});

						$('#uploadPanel').bind("dragover", function (e) {
							$('#uploadPanel').css("background-color", "#ffffcc");
							ev.preventDefault();
							return false;
						});

						$('#uploadPanel').bind("dragleave", function (e) {
							$('#uploadPanel').css("background-color", "#ffffff");
							ev.preventDefault();
							return false;
						});

						$('#uploadPanel').bind("drop", function (e) {
							$('#uploadPanel').css("background-color", "#ffffff");
							ev.preventDefault();
							return false;
						});

						uploader.init();

						uploader.bind('FilesAdded', function (up, files) {

							$.each(files, function (i, file) {
								$('#uploadPanel').append('<div id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b>' + '</div>');
							});

							up.refresh(); // Reposition Flash/Silverlight
							WhenLoggedIn(
								function () {
									uploader.start();
								}
							);
						});

						uploader.bind('UploadProgress', function (up, file) {
							$('#' + file.id + " b").html(file.percent + "%");
						});

						uploader.bind('Error', function (up, err) {
							$('#uploadPanel').append("<div>Error: " + err.code + ", Message: " + err.message + (err.file ? ", File: " + err.file.name : "") + "</div>");
							up.refresh(); // Reposition Flash/Silverlight
						});

						uploader.bind('FileUploaded', function (up, file) {
							$('#' + file.id + " b").html("100%");
						});

						uploader.bind('UploadComplete', function (up, files) {
							$('#PhotoUploadFinished').show();
						});
					});
				</script>
				<div id="container">
					<p>
						<div id="uploadPanel" style="margin:0px; border-width:0px; padding:25px; text-align:center;">Loading...</div>
					</p>
				</div>
			</asp:Panel>
		</div>
		<div id="PhotoUploadFinished" style="display:none;">
			<div class="ContentBorder">
				<p style="text-align:center;">
					<b>Finished uploading. You can upload more or <a href='<%= CurrentEvent.UrlApp("myphotos") %>'>check them out</a>.</b>
				</p>
			</div>
		</div>
	</asp:Panel>
	
	<asp:Panel runat="server" id="SpotterRequestPanel">
		<dsi:h1 runat="server">Free Guestlist</dsi:h1>
		<div class="ContentBorder ClearAfter">
			<img src="/gfx/new-user-freeguestlist.png" width="69" height="43" border="0" align="left" style="margin-top:7px; margin-left:-10px; margin-right:-5px;" />
			<p>
				Take photos and hand out Don't Stay In cards at this event for free entry!
				Just call <asp:Label runat="server" ID="SpotterRequestName" Font-Bold="true" />
				on <asp:Label runat="server" ID="SpotterRequestPhone" Font-Bold="true" />
				to organise your exclusive Free Guestlist.
			</p>
			<asp:Panel runat="server" ID="SpotterRequestNonSpotter">
				<p>
					<b>You will be refused free entry if you don't have Don't Stay In cards to give out.</b>
					<a href="/pages/spotters">Fill in this form</a> and we'll post you some for free.
				</p>
			</asp:Panel>
			<asp:Panel runat="server" ID="SpotterRequestSpotter">
				<p>
					<b>You will be refused free entry if you don't have Don't Stay In cards to give out.</b>
					It's free to get more - just click the button on the <a href="/pages/spotters">spotters page</a>.
				</p>
			</asp:Panel>
		</div>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="TicketsPanel">
		<dsi:h1 runat="Server" id="TicketsHeading">Buy tickets</dsi:h1>
		<div class="ContentBorder">
			<p class="CleanLinks" id="BuyTicketsP" runat="server">
				<asp:Repeater ID="RunningTicketRunsRepeater" runat="server" OnItemDataBound="RunningTicketRunsRepeater_ItemDataBound">
					<HeaderTemplate>
						<p><b>I would like to buy:</b></p>
						<table cellpadding="0" cellspacing="0" border="0">
							<tr id="TicketRunHeaderRow" runat="server" valign="top" class="dataGridHeader" style="display:none;">
								<td style="padding-left:3px;">#</td>
								<td>&nbsp;</td>
								<td colspan="2">Ticket</td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr id="TicketRunDetailsRow" runat="server" style="font-weight:bold; font-size:medium; padding-top:8px;">
							<td rowspan="2"><asp:TextBox ID="TicketRunKTextBox" runat="server" Text="<%#((Bobs.TicketRun)(Container.DataItem)).K%>" Visible="false"></asp:TextBox><asp:TextBox id="NumberOfTicketsTextBox" runat="server" Text="0" Width="24" Height="24" MaxLength="2" style="font-weight:bold; font-size:12; text-align:right; vertical-align:text-bottom; line-height:18px;" onkeyup="EventHomeKeepNumberWithinLimits(this);"></asp:TextBox></td>
							<td rowspan="2"><table cellpadding="0" cellspacing="0" border="0">
									<tr>
										<td><a href="Javascript:void(0);" id="AddTicketImageLink" runat="server"><img src="/gfx/plus.gif" style="display:block;" alt="Plus" border="0" height="12" width="12"/></a></td>
									</tr>
									<tr>
										<td><a href="Javascript:void(0);" id="SubtractTicketImageLink" runat="server"><img src="/gfx/minus.gif" style="display:block;" alt="Minus" border="0" height="12" width="12"/></a><asp:RangeValidator ID="NumberOfTicketsRangeValidator" runat="server" Display="Dynamic" ControlToValidate="NumberOfTicketsTextBox" MinimumValue="0" MaximumValue="<%# Vars.TICKETS_MAX_PER_USR %>" Type="Integer" ErrorMessage="* 0-10" ValidationGroup="BuyTicketsValidation"></asp:RangeValidator></td>
									</tr>
								</table></td>
							<td id="TicketRunNameTD" runat="server" style="padding-left:5px; font-weight:bold; font-size:12;" width="600" rowspan="<%#((Bobs.TicketRun)(Container.DataItem)).Description.Length > 0 ? 1 : 2%>"><%#((Bobs.TicketRun)(Container.DataItem)).Name.Length > 0 ? ((Bobs.TicketRun)(Container.DataItem)).Name : "ticket(s)"%> @ <asp:Label ID="TicketRunPriceLabel" runat="server" style="font-weight:bold; font-size:12;" Text='<%#((Bobs.TicketRun)(Container.DataItem)).Price.ToString("c")%>'></asp:Label> each<asp:Label ID="uiTicketPostage" runat="server" Visible='<%# ((Bobs.TicketRun)(Container.DataItem)).DeliveryMethod == Bobs.TicketRun.DeliveryMethodType.SpecialDelivery %>' Text='<%# " + " + ((Bobs.TicketRun)(Container.DataItem)).DeliveryCharge.ToString("c") + " postage" %>'></asp:Label>
							</td>
						</tr>
						<tr id="TicketRunDescriptionRow" runat="server" style="padding-top:0px;">
							<td id="TicketRunDescriptionTD" runat="server" valign="top" style="padding-left:5px;" visible="<%#((Bobs.TicketRun)(Container.DataItem)).Description.Length > 0 %>"><asp:Label ID="TicketRunDescriptionLabel" runat="server" Text="<%#((Bobs.TicketRun)(Container.DataItem)).Description.Length > 0 ? ((Bobs.TicketRun)(Container.DataItem)).Description : Convert.ToString('&nbsp;')%>"></asp:Label></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
						<tr style="display:none;">
							<td colspan="2" style="padding-top:3px;">&nbsp;</td>
							<td colspan="2" align="left" style="padding-top:3px; border-top:solid 1px #CBA21E;"></td>
						</tr>
						</table>
						<br />
						<p><asp:Button ID="BuyTicketsButton" runat="server" Text="Buy" Width="60" style="font-weight:bold; font-size:12;" OnClick="BuyTicketsButton_Click" ValidationGroup="BuyTicketsValidation" OnClientClick="try { return WhenLoggedInButtonValidator(this, 'BuyTicketsValidation'); } catch(ex) { return false; }" /></p>
					</FooterTemplate>					
				</asp:Repeater>
				
			</p>
			<p id="ETicketReminderP" runat="server" visible="false">
				<b><%= Ticket.ETICKET_CARD_REMINDER_PLURAL %>.</b>
				<small>For more information or if you have any questions, please check out our <a href="/article-7168" target="_blank">FAQ</a>.</small>
			</p>
			<p id="TicketNoteP" runat="server"><asp:Label ID="TicketNoteLabel" runat="server" Font-Bold="true"></asp:Label></p>
			<asp:CustomValidator ID="ProcessingVal" Runat="server" Display="None" ValidationGroup="BuyTicketsValidation" EnableClientScript="False" ErrorMessage="Error processing tickets. Please try again."/>
			<asp:ValidationSummary ID="TicketValidationSummary" BorderWidth="2" Runat="server" EnableClientScript="False" ShowSummary="True" HeaderText="&nbsp;There were some errors:" CssClass="PaymentValidationSummary" Width="100%" Font-Bold="True" DisplayMode="BulletList" ValidationGroup="BuyTicketsValidation"/>	
		</div>
	</asp:Panel>
	<asp:Panel runat="server" ID="MyTicketsPanel">
		<dsi:h1 runat="Server" id="MyTicketsHeading">My tickets</dsi:h1>
		<div class="ContentBorder">		
			<p class="CleanLinks">
				<div>
					<asp:Label ID="TicketPurchaseResults" runat="server" Visible="false" Font-Bold="true" ForeColor="blue"></asp:Label>
				</div>
				<asp:Repeater ID="MyTicketsRepeater" runat="server" OnItemDataBound="MyTicketsRepeater_ItemDataBound" EnableViewState="true">
					<HeaderTemplate>
						<table cellpadding="3" cellspacing="0" border="0" width="100%">
							<tr id="TicketRunHeaderRow" runat="server" valign="top" class="dataGridHeader">
								<td>#</td>
								<td colspan="2">Ticket</td>
								<td align="center" id="CodeHeader" runat="server">&nbsp;</td>
								<td align="right"><nobr>Card digits</nobr></td>
								<td align="center"><small>Print</small></td>
								<td>&nbsp;</td>
							</tr>
					</HeaderTemplate>
					
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>
			</p>
			<div>
				<p id="TicketFeedbackP" runat="server">
					<h2>Ticket feedback</h2>
					<asp:Label ID="UsrTicketFeedbackHeaderLabel" runat="server" Text="In order to provide a better service in future, we would like to know if everything went OK with getting into the event..."></asp:Label>
					<div id="UsrTicketResponseGoodLinkButtonDiv" runat="server" style='font-size:12px; font-weight:bold;'><asp:LinkButton ID="UsrTicketResponseGoodLinkButton" runat="server" OnClick="UsrTicketResponseGoodLinkButton_Click" CausesValidation="false"><img src='/gfx/icon-tick-up.png' border="0" height="21" width="26" style="vertical-align:middle;"/>Yes, all OK</asp:LinkButton><br /></div>
					<div id="UsrTicketResponseGoodDiv" runat="server" style='font-size:12px; font-weight:bold;'><img src='/gfx/icon-tick-up.png' border="0" height="21" width="26" style="vertical-align:middle;"/>Yes, all OK</div>
					<div id="UsrTicketResponseBadLinkButtonDiv" runat="server" style='font-size:12px; font-weight:bold;'><asp:LinkButton ID="UsrTicketResponseBadLinkButton" runat="server" OnClick="UsrTicketResponseBadLinkButton_Click" CausesValidation="false"><img src='/gfx/icon-cross-up.png' border="0" height="21" width="26" style="vertical-align:middle;"/>No, there was a problem</asp:LinkButton><br /></div>
					<div id="UsrTicketResponseBadDiv" runat="server"><img src='/gfx/icon-cross-up.png' border="0" height="21" width="26" style="vertical-align:middle;"/><font style="font-size:12px; font-weight:bold;">No, there was a problem</font><p><asp:Label ID="UsrTicketFeedbackLabel" runat="server"></asp:Label></p></div>		
				</p>
			</div>
		</div>
		<asp:Label ID="TicketPurchaseJavascriptLabel" runat="server" VisFible="false"></asp:Label>
	</asp:Panel>
	
	<asp:PlaceHolder Runat="server" ID="EventDetailsPlainPh" EnableViewState="False"/>
	
	<asp:Panel Runat="server" ID="UsrEventAttendListPanel">
		<dsi:h1 runat="server">
			<asp:Label Runat="server" ID="UsrEventAttendListLabelFuture">Who's coming? </asp:Label>
			<asp:Label Runat="server" ID="UsrEventAttendListLabelPast">Who was there? </asp:Label>
		</dsi:h1>
		<div class="ContentBorder">
			<p>	
				<Controls:AttendedEventControl runat="server" ID="uiAttendedEvent"></Controls:AttendedEventControl>
			</p>
			
			<asp:Panel Runat="server" ID="UsrEventSpotterButtonsPanel">
				<p>	
					<asp:Label Runat="server" ID="UsrEventSpotterFutureLabel">Would you like to be our spotter? </asp:Label>
					<asp:Label Runat="server" ID="UsrEventSpotterPastLabel">Were you a spotter at this event? </asp:Label>
					<asp:RadioButton Runat="server" ID="UsrEventSpotterYes" GroupName="UsrEventSpotterRBGroup"  OnCheckedChanged="UsrEventSpotterClick" AutoPostBack="True" Text="Yes"></asp:RadioButton>
					<asp:RadioButton Runat="server" ID="UsrEventSpotterNo" GroupName="UsrEventSpotterRBGroup"  OnCheckedChanged="UsrEventSpotterClick" AutoPostBack="True" Text="Not this one"></asp:RadioButton>
				</p>
			</asp:Panel>
			
			<p Runat="server" ID="EventCapacityP">
				<asp:Label Runat="server" ID="EventCapacityLabel"></asp:Label>
			</p>
			<p Runat="server" ID="UsrEventAttendP" class="CleanLinks">
				<asp:PlaceHolder Runat="server" ID="UsrEventAttendList"/>
			</p>
			
			<asp:Panel Runat="server" ID="SpottedByPanel" class="CleanLinks" Visible="False">
				<p>
					<asp:Label Runat="server" ID="SpottedByTextLabel"/> <small><asp:PlaceHolder runat="server" id="SpottedByLinks"/></small>
				</p>
			</asp:Panel>
			
			<asp:Panel Runat="server" ID="SpotterSignUpPanel" EnableViewState="False">
				<h2>
					Do you want to be our Spotter?
				</h2>
				<p>
					You can <a href="/pages/spotters/" runat="server" id="SpotterSignUpLink1">sign-up 
					to be a spotter for this event</a>. Don't worry if there is already 
					someone spotting - the more the merrier!
				</p>
			</asp:Panel>
			<asp:Panel Runat="server" ID="NoSpotterSignUpPanel" EnableViewState="False">
				<h2>
					Spotter needed!
				</h2>
				<p>
					We don't currently have a spotter for this event... Do you want to cover it? Learn 
					how you can <a href="/pages/spotters" runat="server" id="SpotterSignUpLink2">become a spotter</a>.
				</p>
			</asp:Panel>
			
		</div>
	</asp:Panel>
	
	<Controls:Latest runat="server" ID="Latest" ParentObjectType="Event" Items="5" />
	
	
	
	<asp:Panel Runat="server" ID="GalleriesPanel" EnableViewState="False">
		<a name="Galleries"></a>
		<dsi:h1 runat="server" ID="H11" NAME="H11">Galleries</dsi:h1>
		<div class="ContentBorder" align="center">
			<p>
				<asp:DataList runat="server" ID="GalleryDataList" Width="100%" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" CssClass="CleanLinks" ItemStyle-Width="33%" />
			</p>
			<p>
				<asp:Button Runat="server" Text="Add your own gallery" OnClick="AddGalleryClick" ID="Button1" NAME="Button1" CausesValidation="False"></asp:Button>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="FlyerPanel" Visible="False">
		<dsi:h1 runat="server" ID="H14" NAME="H11">Flyer</dsi:h1>
		<div class="ContentBorder">
			<p align="center" style="font-size:15px;font-weight:bold;margin:8px;">
				<a href="/chat">Chat about this flyer (2 comments)</a>
			</p>
			<p align="center" style="margin-bottom:0px;">
				<a href="/chat"><img src="/misc/flyertest.jpg" border="0"></a>
			</p>
		</div>
	</asp:Panel>
	
	<a name="InfoPanel"></a>
	<asp:Panel Runat="server" ID="InfoPanel" EnableViewState="False">
		<dsi:h1 runat="server" id="EventBodyTitle">Info</dsi:h1>
		<div class="ContentBorder" style="width:600px;overflow:hidden;">
			<p runat="server" id="MusicTypeP">
				<b>Music</b> : <asp:Label Runat="server" ID="MusicTypeLabel"/>
			</p>
			<asp:PlaceHolder runat="server" id="EventStartTimeDescription"/>
			<asp:PlaceHolder Runat="server" ID="EventBody"/>
		</div>
	</asp:Panel>
		
	<asp:Panel Runat="server" ID="TodayEventsPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="TodayEventsHeader">Similar events in ? $</dsi:h1>
		<div class="ContentBorder">
			<asp:DataList runat="server" ID="TodayEventsDataList" RepeatLayout="Flow" RepeatDirection="Horizontal" />
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="AfterPartyPanel" EnableViewState="False">
		<dsi:h1 runat="server">Looking for an after-party?</dsi:h1>
		<div class="ContentBorder">
			<asp:DataList runat="server" ID="AfterPartyDataList" RepeatLayout="Flow" RepeatDirection="Horizontal" />
		</div>
	</asp:Panel>
</asp:Panel>
