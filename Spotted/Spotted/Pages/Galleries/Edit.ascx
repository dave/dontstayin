<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Spotted.Pages.Galleries.Edit" %>

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
					<asp:Panel Runat="server" ID="BackToEditArticlePanel">
						<p style="font-size:12px;font-weight:bold;" align="center">
							You're editing an article gallery. <a href="/pages/myarticles/mode-edit/k-<%= (CurrentGallery.Article!=null?CurrentGallery.Article.K.ToString():"0") %>/photos-1">Back to editing your article</a>
						</p>
					</asp:Panel>
				</td>
			</tr>
		</table>
	</div>
</asp:Panel>

<dsi:h1 runat="server" ID="sdH11">New uploader</dsi:h1>
<div class="ContentBorder">
	<p>
		This is the new photo uploader. If you have trouble using it, you can go back to the <a href="<%= CurrentGallery.UrlApp("editold") %>">old uploader</a>.
	</p>
</div>

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
						url: 'http://<%= Vars.DomainName %>/support/photoUpload.aspx?GalleryK=<%= CurrentGallery.K.ToString() %>&source=plupload',
						//url: 'http://www.plupload.com/upload.php',
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
						uploader.start();
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
				});
			</script>
			<div id="container">
				<p>
					<div id="uploadPanel" style="margin:0px; min-height:50px; border-width:0px; padding:35px; text-align:center;">Loading...</div>
				</p>
			</div>
		</asp:Panel>
	</div>
</asp:Panel>

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
