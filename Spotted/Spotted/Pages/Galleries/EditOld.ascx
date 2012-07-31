<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditOld.ascx.cs" Inherits="Spotted.Pages.Galleries.EditOld" %>

<script>
	var PhotoKList = new Array(0,0);
	function ToggleSelection(state)
	{
		for (i=1; i < PhotoKList.length; i++)
		{
			document.getElementById("ucEditGalleryPhotoSelectK" + PhotoKList[i]).checked = state;
			document.getElementById("ucEditGalleryPhotoTableK" + PhotoKList[i]).style.backgroundColor = state ? '#FFE79D' : 'transparent';
		}
	}
	function AlterUploaderVisibility(filesleft)
	{

		if (filesleft > 0)
		{
			document.getElementById("<%= PanelNoUpload.ClientID %>").style.display = "none";
			getImageUploader("ImageUploader").style.display = "";
		}
		else
		{
			document.getElementById("<%= PanelNoUpload.ClientID %>").style.display = "";
			getImageUploader("ImageUploader").style.display = "none";
		}
		
	}
	function ChangeMaxFileCount(filesLeft)
	{
		try
		{
			getImageUploader("ImageUploader").setMaxFileCount(filesLeft);
		}
		catch(ex)
		{
		}
	}
</script>
					
<asp:Panel Runat="server" id="InfoPanel">
	<dsi:h1 runat="server" ID="H11">Gallery</dsi:h1>
	<div class="ContentBorder">
		<table cellpadding="0" cellspacing="0" border="0" width="100%">
			<tr>
				<td valign="top" runat="server" id="TitleImgCell" style="padding-right:7px;">
					<p>
						<img src="" runat="server" id="TitlePicImg" class="BorderBlack All" width="100" height="100"/>
					</p>
				</td>
				<td width="100%" valign="top">
					<p>
						Name: <asp:TextBox Runat="server" ID="GalleryName" MaxLength="30" Columns="30"></asp:TextBox> <asp:Button Runat="server" onclick="NameUpdateClick" Text="Save" ID="Button1"></asp:Button> <asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="GalleryName" ErrorMessage="Please enter a name" ID="Requiredfieldvalidator1"></asp:RequiredFieldValidator>
					</p>
					<p>
						This gallery was added to <%= CurrentGallery.ParentObjectHtml(false, false) %>
					</p>
					<p>
						<small>You can view this, and all your other galleries on the <a href="/pages/mygalleries">My galleries</a> page.</small>
					</p>
					<p>
						<small>Preview this gallery: <a href="<%= CurrentGallery.Url() %>">quick browser</a> or <a href="<%= CurrentGallery.PagedUrl() %>">gallery</a>.</small>
					</p>
					<p>
						<asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
							<ContentTemplate>
								<asp:CheckBox runat="server" ID="WatchCheckBox" Checked="true" Text="Watch these photos for comments" AutoPostBack="true" OnCheckedChanged="WatchChange" />
							</ContentTemplate>
						</asp:UpdatePanel>
						
					</p>
				</td>
			</tr>
		</table>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="BackToEditArticlePanel">
	<dsi:h1 runat="server" ID="H19">You're editing an article gallery</dsi:h1>
	<div class="ContentBorder">
		<p style="font-size:12px;font-weight:bold;" align="center">
			<a href="/pages/myarticles/mode-edit/k-<%= (CurrentGallery.Article!=null?CurrentGallery.Article.K.ToString():"0") %>/photos-1">Back to editing your article</a>
		</p>
	</div>
</asp:Panel>


<a name="Upload"></a>
<asp:Panel Runat="server" ID="NotSpotterPanel">
	<dsi:h1 runat="server" ID="H124w2">You're not a spotter</dsi:h1>
	<div class="ContentBorder">
		<p>
			You're not a spotter, so you can only upload 10 photos to this gallery. 
			<%= uiYourPhotos.Visible ? "You can delete some of the photos above to make space for more." : "" %>
		</p>
		<p>
			You can upgrade for FREE, and you can upload more photos as soon 
			as you're done.
		</p>
		<p>
			We'll send you a bunch of DontStayIn cards to give out. Make sure you 
			give one to everybody you take a photo of - they won't be able to find 
			their photos otherwise!
		</p>
		<p align="center" style="font-size:15px; font-weight:bold; padding:5px;">
			<a href="/pages/spotters">Click here to upgrade for FREE!</a>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PhotoUploadPanel">
	<a name="UploadControl"></a>
	<dsi:h1 runat="server" ID="H13">Upload photos</dsi:h1>
	<div class="ContentBorder" style="padding:0px;">
		<asp:Panel Runat="server" ID="PanelNoUpload" style="display:none;">
			<div style="padding:2px 8px 2px 8px;">
				<p>
					You can't upload any more files to this gallery. 
					<%= Bobs.Usr.Current.IsSpotter ? "You can only upload 100 files per gallery." : "You can only upload 10 files per gallery. Spotters can upload 100! Click the link below to upgrade for free!" %>
				</p>
				<p align="center" style="font-size:15px; font-weight:bold; padding:5px;">
					<%= Bobs.Usr.Current.IsSpotter && CurrentGallery.EventK > 0 ? "<a href=\"/pages/galleries/add/eventk-" + CurrentGallery.EventK + "/new-1\">Click here to add a new gallery</a>" : "<a href=\"/pages/spotters\">Click here to upgrade for FREE!</a>"%>
				</p>
			</div>
		</asp:Panel>
	
		<asp:Panel Runat="server" ID="PanelUpload" style="padding:0px;">
			<script src="/misc/iuembed_5_8_10.js"></script>
			<script language="javascript">
				function ImageUploader_Progress(Status, Progress, ValueMax, Value, StatusText){
					if (Status == "COMPLETE"){
						//window.location = 'http://<%= Bobs.Vars.DomainName %><%= ContainerPage.Url.CurrentUrl() %>';
						__doPostBack('<% = UpdatePanel1.ClientID %>', '');
					}
				} 
			</script>
			<script language="javascript">
				//Init
				var iu = new ImageUploaderWriter("ImageUploader", 979, 500);
				iu.activeXControlEnabled = <%= ActiveX %>;
				iu.activeXControlCodeBase = "/misc/ActiveX_5_8_10/ImageUploader5.cab";
				iu.activeXControlVersion = "5,1,1,0"; //to find out - extract CAB file and look in INF file.
				//iu.javaAppletEnabled = true; //removed in version 5.1.00.0?
				iu.javaAppletCodeBase = "/misc/Java_5_8_10/";
				iu.javaAppletCached = true;
				iu.javaAppletVersion = "5.1.00.0"; //find this out!!!
				iu.addParam("ShowDebugWindow","<%= Vars.DevEnv ? "true" : "false"  %>"); // For debugging?
				//iu.showNonemptyResponse = "dump"; // or "alert"                          // For debugging?
				
				//Misc
				iu.addParam("BackgroundColor", "#FFFFFF");
				<%= Serial %>
				
				//Pane layout
				iu.addParam("PaneLayout","TwoPanes");
				iu.addParam("TreePaneWidth", "230");
				iu.addParam("FolderPaneHeight", "310");
				
				//Folder pane
				iu.addParam("FolderView", "Thumbnails");
				iu.addParam("CheckFilesBySelectAllButton", "true");
				//iu.addParam("ShowDescriptions", "true");
				iu.addParam("ShowDescriptions", "true");
				iu.addParam("MaxDescriptionTextLength", "512");
				iu.addParam("EditDescriptionText", "Add tags...");
				iu.addParam("EditDescriptionTextColor", "#000000");
				iu.addParam("DescriptionTooltipText", "Enter your tags here, seperated with commas.");
				iu.addParam("AllowLargePreview", "true");
				iu.addParam("LargePreviewHeight", "500");
				iu.addParam("LargePreviewWidth", "700");
				iu.addParam("LargePreviewIconTooltipText", "");
				iu.addParam("ButtonStopText", "");
				iu.addParam("ButtonSendText", "Upload selected files");
				iu.addParam("ButtonSelectAllText", "Select all files");
				iu.addParam("ButtonDeselectAllText", "Clear selection");
				iu.addParam("EnableFileViewer", "false");
				//iu.addParam("ShowButtons", "false");
				iu.addParam("EnableRotate", "true");
				iu.addParam("AllowAutoRotate", "true");
				iu.addParam("FileMask", "*.jpg;*.jpeg;*.jpe;*.gif;*.png;*.avi;*.dv;*.mov;*.qt;*.mpg;*.mpeg;*.mp4;*.3gp;*.asf;*.wmv;");
				//iu.addParam("FileMask", "*.jpg;*.jpeg;*.jpe;*.gif;*.png;");
				//iu.addParam("FileMask", "*.jpg;*.jpeg;*.jpe;*.gif;*.png;*.avi;*.dv;*.mov;*.qt;*.mpg;*.mpeg;*.mp4;*.3gp;*.asf;*.wmv;*.mp3;*.wav;*.wma;");
				
				//Upload pane
				//iu.addParam("UploadView", "AdvancedDetails");
				//iu.addParam("EnableInstantUpload", "true");
				//iu.addParam("UploadPaneAllowRotate", "false");
				
				//Upload
				iu.addParam("ProgressDialogWaitingForResponseFromServerText", "Done.");
				iu.addParam("ProgressDialogCloseWhenUploadCompletesText", "");
				iu.addParam("ProgressDialogCancelButtonText", "");
				//iu.addParam("SilentMode", "true");
				iu.addParam("MessageUploadCompleteText", "");
				iu.addParam("UploadSourceFile", "True");
				iu.addParam("MaxFileSize", "<%= Bobs.Photo.MaxFileSizeInBytes %>");
				iu.addParam("FileIsTooLargeText", "larger then <%= Bobs.Photo.MaxFileSizeInMB %>MB");
				iu.addParam("MessageMaxFileSizeExceededText", "The maximum file size is <%= Bobs.Photo.MaxFileSizeInMB %>MB");
				iu.addParam("MaxFileCount", "<%= FilesLeft %>");
				iu.addParam("MessageMaxFileCountExceededText", "<%= Bobs.Usr.Current.IsSpotter ? "You can only upload 100 files per gallery." : "You can only upload 10 files per gallery. Spotters can upload 100! Click the Spotters button at the top of the page to upgrade for free!" %>");
				iu.addParam("AutoRecoverMaxTriesCount", "3");
				iu.addParam("AutoRecoverTimeOut", "2000");
				iu.addParam("MaxConnectionCount", "4");
				iu.addParam("FilesPerOnePackageCount", "1");
				iu.addParam("Action", "http://<%= Vars.DomainName %>/Support/PhotoUpload.aspx?GalleryK=<%= CurrentGallery.K %>");
				iu.addEventListener("Progress","ImageUploader_Progress");

				iu.writeHtml();
			</script></asp:Panel></div></asp:Panel>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
	<ContentTemplate>
		<asp:Panel runat="server" ID="uiYourPhotos">
			<dsi:h1 runat="server" ID="H12">Your photos</dsi:h1>
			<div class="ContentBorder">
			
				<asp:Panel Runat="server" ID="GalleryEmptyPanel">
					<h2>Gallery empty...</h2>
					<p>
						This gallery is empty. Click below to update:
					</p>
					<p>
						<asp:Button Runat="server" ID="Button4" OnClick="PhotoRefreshButton_Click" Text="Refresh"></asp:Button>
					</p>
				</asp:Panel>
				
				<asp:Panel Runat="server" ID="PhotoProcessingPanel">
					<h2>Processing...</h2>
					<p>
						There are items currently processing. This can take a few minutes after you upload. Click the button below to update:
					</p>
					<p>
						<asp:Button Runat="server" ID="Button2" OnClick="PhotoRefreshButton_Click" Text="Refresh"></asp:Button>
					</p>
					<p>
						<asp:DataList Runat="server" Width="100%" ID="PhotoProcessingDataList" ItemStyle-HorizontalAlign="left" RepeatDirection="Vertical" RepeatLayout="Flow" />
					</p>
					<p>
						<small>(clicking on the link above might cause an error if the photo has finished processing since the page was generated)</small>
					</p>
				</asp:Panel>

				<asp:Panel Runat="server" ID="PhotoModeratePanel">
					<h2>Waiting for a moderator</h2>
					<p>
						These photos are not live on the site yet - they are waiting for one of our photo moderators to 
						check them. 
					</p>
					<p>
						<asp:DataList Runat="server" Width="100%" ID="PhotoModerateDataList" ItemStyle-HorizontalAlign="left" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table" ItemStyle-VerticalAlign=top CellPadding="8"  />
					</p>
				</asp:Panel>

				<asp:Panel Runat="server" ID="PhotoEnabledPanel">
					<h2>Live photos on site</h2>
					<p>
						These photos are live on the site - they are visible in the public gallery. 
					</p>
					<p>
						<asp:DataList Runat="server" Width="100%" ID="PhotoEnabledDataList" ItemStyle-HorizontalAlign="left" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table" ItemStyle-VerticalAlign=top CellPadding="8"  />
					</p>
				</asp:Panel>
				
				<asp:Panel Runat="server" ID="PhotoActionsPanel">
					<h2>Actions</h2>
					<p>
						<asp:Button runat="server" ID="DeleteSelectedButton" OnClick="DeleteSelected" Text="Delete selected photos" CausesValidation="false"></asp:Button> 
						<button id="ToggleButton1" onclick="ToggleSelection(true);">select all photos</button> 
						<button id="ToggleButton2" onclick="ToggleSelection(false);">clear selection</button>
					</p>
					<p runat="server" id="DeleteSelectedOutput" visible="false"></p>
				</asp:Panel>
			</div>
		</asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>

<asp:Panel Runat="server" ID="JavaPanel">
	<dsi:h1 runat="server" ID="H110">Upload photos</dsi:h1>
	<div class="ContentBorder">
		<p>
			If the uploader above isn't working, you may get better results by trying the java version:
		</p>
		<p class="BigCenter">
			<a href="<%= ContainerPage.Url.CurrentUrl("java","") %>">Java uploader</a>
		</p>
	</div>
</asp:Panel>
<dsi:h1 runat="server" ID="H112">Using a Mac?</dsi:h1>
<div class="ContentBorder">
	<p>
		If the uploader above isn't working and you're using a Mac, read the post below:
	</p>
	<p class="BigCenter">
		<a href="/groups/dontstayin-website/chat/k-198549">Problems uploading from a Mac?</a>
	</p>
</div>
