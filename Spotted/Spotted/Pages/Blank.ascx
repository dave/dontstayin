<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Blank.ascx.cs" Inherits="Spotted.Pages.Blank" %>
<%@ Register TagPrefix="Controls" TagName="Picker" Src="~/Controls/Picker.ascx" %>

<dsi:h1 runat="server">Blank</dsi:h1>
<div class="ContentBorder">


	<!-- Load Queue widget CSS and jQuery -->
	<style type="text/css">@import url(/misc/jquery/plupload/js/queue/css/jquery.plupload.queue.css);</style>

	<!-- Load plupload and all it's runtimes and finally the jQuery queue widget -->
	<script type="text/javascript" src="/misc/jquery/plupload/js/plupload.full.js"></script>
	<script type="text/javascript" src="/misc/jquery/plupload/js/jquery.plupload.queue/jquery.plupload.queue.js"></script>
	
	<!-- Thirdparty intialization scripts, needed for the Google Gears and BrowserPlus runtimes -->
	<script type="text/javascript" src="/misc/jquery/plupload/js/plupload.gears.js"></script>
	




<script type="text/javascript">
	// Custom example logic
	$(function () {
		var uploader = new plupload.Uploader({
			runtimes: 'html5,gears,flash,silverlight',
			browse_button: 'uploadPanel',
			container: 'container',
			//max_file_size: '10mb',
			url: '/support/photoUpload.aspx?GalleryK=386986',
			flash_swf_url: '/misc/jquery/plupload/js/plupload.flash.swf',
			silverlight_xap_url: '/misc/jquery/plupload/js/plupload.silverlight.xap',
			filters: [
				{ title: "Image files", extensions: "jpg,gif,png" },
				{ title: "Zip files", extensions: "zip" }
			],
			//resize: { width: 320, height: 240, quality: 90 },
			drop_element: 'uploadPanel'
		});

		uploader.bind('Init', function (up, params) {
			$('#filelist').html("<pre>" + ImportedUtilities.U.toString(up.features) + "</pre>");
			if (ImportedUtilities.U.isTrue(up.features, "dragdrop"))
				$('#uploadPanel').html("Drag photos here to upload...");
			else
				$('#uploadPanel').html("Click here to upload photos...");
			//$('#filelist').html("<div>Current runtime: " + params.runtime + "</div>");
		});

		$('#uploadfiles').click(function (e) {
			uploader.start();
			e.preventDefault();
		});

		$('#uploadfiles').click(function (e) {
			uploader.start();
			e.preventDefault();
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
				$('#filelist').append('<div id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b>' + '</div>');
			});

			up.refresh(); // Reposition Flash/Silverlight
		});



		uploader.bind('UploadProgress', function (up, file) {
			$('#' + file.id + " b").html(file.percent + "%");
		});

		uploader.bind('Error', function (up, err) {
			$('#filelist').append("<div>Error: " + err.code + ", Message: " + err.message + (err.file ? ", File: " + err.file.name : "") + "</div>");
			up.refresh(); // Reposition Flash/Silverlight
		});

		uploader.bind('FileUploaded', function (up, file) {
			$('#' + file.id + " b").html("100%");
		});
	});
</script>

<div id="container">
	<div id="uploadPanel" style="width:100px; height:100px; border:1px solid #ff0000;"></div>
	<div id="filelist">Loading...</div>
	<br />
	<a id="uploadfiles" href="#">[Upload files]</a>
</div>
			


			


</div>
