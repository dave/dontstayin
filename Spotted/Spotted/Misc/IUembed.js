// Aurigma Image Uploader Embedding Script 
// Version 1.0.0.3 July 1, 2005
// Copyright(c) Aurigma Inc. 2002-2005
var __agt=navigator.userAgent.toLowerCase();
var _isWinIE=(__agt.indexOf("msie")!=-1)&&(__agt.indexOf("win")!=-1);
var _isSafari=(__agt.indexOf("safari")!=-1);

function ImageUploaderWriter(id,width,height){
	//Private
	this._params=new Array();
	this._events=new Array();    

	this._writeObjectParam=function(name,value){
		document.writeln("<param name=\""+name+"\" value=\""+value+"\" />");
	}

	this._writeObjectParams=function(){
		var p=this._params;
		var i;
		for (i=0;i<p.length;i++){
			this._writeObjectParam(p[i].name,p[i].value);
		}
	}
	
	this._writeObjectEvents=function(){	
		var e=this._events;
		for (i=0;i<e.length;i++){
			this._writeObjectParam(e[i].name+"Listener",e[i].listener);
		}
	}

	this._writeEmbedParam=function(name,value){
		document.write(" "+name+"=\""+value+"\"");	
	}

	this._writeEmbedParams=function(){
		var p=this._params;
		var i;
		for (i=0;i<p.length;i++){
			this._writeEmbedParam(p[i].name,p[i].value);
		}
	}

	this._writeEmbedEvents=function(){	
		var e=this._events;
		for (i=0;i<e.length;i++){
			this._writeEmbedParam(e[i].name+"Listener",e[i].listener);
		}
	}

	//Public

	//Properties
	this.id=id;
	this.width=width;
	this.height=height;

	this.activeXControlEnabled=true;
	this.activeXControlCodeBase="ImageUploader3.cab";
	this.activeXControlVersion="";

	this.javaAppletEnabled=true;
	this.javaAppletCodeBase="./";
	this.javaAppletCached=true;
	this.javaAppletVersion="";

	this.fullPageLoadListenerName=null;

	//Methods
	this.addParam=function(paramName,paramValue){		
		var p=new Object();
		p.name=paramName;
		p.value=paramValue;
		this._params.push(p);
	}

	this.addEventListener=function(eventName,eventListener){
		var p=new Object();
		p.name=eventName;
		p.listener=eventListener;
		this._events.push(p);		
	}

	this.writeHtml=function(){
		var a=navigator.userAgent.toLowerCase();
		var isWinIE=(a.indexOf("msie")!=-1)&&(a.indexOf("win")!=-1);
		var isSafari=(a.indexOf("safari")!=-1);
		//ActiveX control
		if(isWinIE&&this.activeXControlEnabled){
			this.controlType="ActiveX";
			var v=this.activeXControlVersion.replace(/\./g,",")
			var cb=this.activeXControlCodeBase+(v==""?"":"#version="+v);
			
			document.writeln("<object id=\""+this.id+"\" name=\""+this.id+"\" classid=\"clsid:A18962F6-E6ED-40B1-97C9-1FB36F38BFA8\" codebase=\""+cb+"\" width=\""+this.width+"\" height=\""+this.height+"\">");

			this._writeObjectParams();

			document.writeln("</object>");
			
			//Event handlers
			var e=this._events;
			var eventParams;
			for (i=0;i<e.length;i++){
				if (e[i].name=="Progress"){
					eventParams="Status, Progress, ValueMax, Value, StatusText";
				}
				else if (e[i].name=="ViewChange"){
					eventParams="Pane";
				}
				else{
					eventParams="";
				}
				document.writeln("<" + "script for=\""+this.id+"\" event=\""+e[i].name+"("+eventParams+")\">");
				if (e[i].name=="BeforeUpload"){
					document.write("return ");
				}
				document.writeln(e[i].listener+"("+eventParams+");");
				document.writeln("<"+"/script>");
			}

			//Set/get expando methods
			var props=new Array("Action","AdditionalFolderNavigator","AdditionalFormName","AllowMultipleSelection","AllowTreePaneWidthChanging","AutoRecoverMaxTriesCount","AutoRecoverTimeOut","BackgroundColor","ButtonAddAllToUploadListText","ButtonAddFilesText","ButtonAddFoldersText","ButtonAddToUploadListText","ButtonDeleteFilesText","ButtonDeselectAllText","ButtonFontName","ButtonFontSize","ButtonPasteText","ButtonRemoveAllFromUploadListText","ButtonRemoveFromUploadListText","ButtonSelectAllText","ButtonSendText","ButtonStopText","CheckFilesBySelectAllButton","CodePage","DeniedFileMask","DescriptionEditorButtonOkText","DescriptionEditorButtonCancelText","DescriptionsReadOnly","DimensionsAreTooLargeText","DimensionsAreTooSmallText","DisplayNameActiveSelectedTextColor","DisplayNameActiveUnselectedTextColor","DisplayNameInactiveSelectedTextColor","DisplayNameInactiveUnselectedTextColor","DropFilesHereText","EditDescriptionText","EditDescriptionTextColor","EnableFileViewer","EnableRotate","ExtractExif","FileMask","FileIsTooLargeText","FilesPerOnePackageCount","FolderPaneHeight","FolderView","HashAlgorithm","HoursText","IncludeSubfoldersText","IncorrectFileActiveSelectedTextColor","IncorrectFileActiveUnselectedTextColor","IncorrectFileInactiveSelectedTextColor","IncorrectFileInactiveUnselectedTextColor","KilobytesText","Layout","LicenseKey","ListColumnFileNameText","ListColumnFileSizeText","ListColumnFileTypeText","ListColumnLastModifiedText","LoadingFilesText","MaxDescriptionTextLength","MaxFileCount","MaxFileSize","MaxImageHeight","MaxImageWidth","MaxTotalFileSize","MegabytesText","MenuAddAllToUploadListText","MenuAddToUploadListText","MenuDeselectAllText","MenuDetailsText","MenuIconsText","MenuInvertSelectionText","MenuListText","MenuRefreshText","MenuRemoveAllFromUploadListText","MenuRemoveFromUploadListText","MenuSelectAllText","MenuThumbnailsText","MessageBoxTitleText","MessageCannotConnectToInternetText","MessageDimensionsAreTooLargeText","MessageDimensionsAreTooSmallText","MessageMaxFileCountExceededText","MessageUserSpecifiedTimeoutHasExpiredText","MessageMaxFileSizeExceededText","MessageMaxTotalFileSizeExceededText","MessageNoFilesToSendText","MessageNoInternetSessionText","MessageNoResponseFromServerText","MessageRedirectText","MessageServerNotFoundText","MessageSwitchAnotherFolderWarningText","MessageUnexpectedErrorText","MessageUploadCancelledText","MessageUploadCompleteText","MessageUploadFailedText","MinImageHeight","MinImageWidth","MinutesText","Padding","PaneBackgroundColor","PreviewThumbnailActiveColor","PreviewThumbnailInactiveColor","PreviewThumbnailSize","ProgressDialogSentText","ProgressDialogCancelButtonText","ProgressDialogCloseButtonText","ProgressDialogCloseWhenUploadCompletesText","ProgressDialogEstimatedTimeText","ProgressDialogPreparingDataText","ProgressDialogTitleText","ProgressDialogWaitingForResponseFromServerText","ProgressDialogWidth","QualityMeterAcceptableQualityColor","QualityMeterBackgroundColor","QualityMeterBorderColor","QualityMeterFormats","QualityMeterHighQualityColor","QualityMeterLowQualityColor","RedirectUrl","RememberLastVisitedFolder","SecondsText","ShowButtons","ShowContextMenu","ShowDebugWindow","ShowDeleteButton","ShowDescriptions","ShowFileNames","ShowPasteButton","ShowStatusPane","ShowSubfolders","ShowUploadCompleteMessage","ShowUploadListButtons","SignatureFilter","SilentMode","StatusFilesInFolderText","StatusSelectedFilesText","TimeOut","TotalFileSize","TreePaneWidth","ThumbnailHorizontalSpacing","ThumbnailVerticalSpacing","UncheckUploadedFiles","UnixFileSystemRootText","UnixHomeDirectoryText","UploadItems","UploadFileCount","UploadMode","UploadSourceFile","UploadThumbnail1CopyExif","UploadThumbnail1FitMode","UploadThumbnail1Height","UploadThumbnail1JpegQuality","UploadThumbnail1Width","UploadThumbnail2CopyExif","UploadThumbnail2FitMode","UploadThumbnail2Height","UploadThumbnail2JpegQuality","UploadThumbnail2Width","UploadThumbnail3CopyExif","UploadThumbnail3FitMode","UploadThumbnail3Height","UploadThumbnail3JpegQuality","UploadThumbnail3Width","UploadView","UseSystemColors","Version");

			var o=document.getElementById(this.id);
			for (i=0;i<props.length;i++){
				eval("o.set"+props[i]+"=function(v){this."+props[i]+"=v};");
				eval("o.get"+props[i]+"=function(){return this."+props[i]+"};");
			}
			
			if (this.fullPageLoadListenerName!=null){
				document.writeln("<" + "script>");
				document.writeln("window.attachEvent(\"onload\",new Function(\""+this.fullPageLoadListenerName+"();\"));");
				document.writeln("<"+"/script>");
			}
		}
		else 
		//Java appplet
		if(this.javaAppletEnabled){
			this.controlType="Java";
			if (this.fullPageLoadListenerName!=null){
				document.writeln("<" + "script language=\"javascript\">");
				document.writeln("var __"+this.id+"_pageLoaded=false;");
				document.writeln("var __"+this.id+"_appletLoaded=false;");
				document.writeln("function __"+this.id+"_InitComplete(){");
				document.writeln("	__"+this.id+"_appletLoaded=true;");
				document.writeln("	__fire_"+this.id+"_fullPageLoad();");
				document.writeln("}");
				document.writeln("function __fire_"+this.id+"_fullPageLoad(){");
				document.writeln("		if (__"+this.id+"_pageLoaded&&__"+this.id+"_appletLoaded){");
				document.writeln("		"+this.fullPageLoadListenerName+"();");
				document.writeln("	}");
				document.writeln("}");
				var pageLoadCode="new Function(\"__"+this.id+"_pageLoaded=true;__fire_"+this.id+"_fullPageLoad();\")"
				if (isWinIE){
					document.writeln("window.attachEvent(\"onload\","+pageLoadCode+");");
				}
				else{
					document.writeln("var r=window.addEventListener?window:document.addEventListener?document:null;");
					document.writeln("if (r){r.addEventListener(\"load\","+pageLoadCode+",false);}");
				}
				document.writeln("<"+"/script>");
			}

			//<object> for IE and <applet> for Safari
			if (isWinIE||isSafari){
				if (isWinIE){
					document.writeln("<object id=\""+this.id+"\" classid=\"clsid:8AD9C840-044E-11D1-B3E9-00805F499D93\" codebase=\"http://java.sun.com/update/1.4.2/jinstall-1_4-windows-i586.cab#Version=1,4,0,0\" width=\""+this.width+"\" height=\""+this.height+"\">");
				}
				else{
					document.writeln("<applet id=\""+this.id+"\" code=\"com.aurigma.imageuploader.ImageUploader.class\" java_codebase=\"../\" align=\"baseline\" archive=\"ImageUploader.jar\" mayscript=\"true\" scriptable=\"true\" width=\""+this.width+"\" height=\""+this.height+"\">");
				}

				if (this.javaAppletCached&&this.javaAppletVersion!=""){
					this._writeObjectParam("cache_archive","ImageUploader.jar");
					var v=this.javaAppletVersion.replace(/\,/g,".");
					this._writeObjectParam("cache_version",v+","+v);
				}

				this._writeObjectParam("type","application/x-java-applet;version=1.4");
				this._writeObjectParam("codebase",this.javaAppletCodeBase);
				this._writeObjectParam("archive","ImageUploader.jar");
				this._writeObjectParam("code","com.aurigma.imageuploader.ImageUploader.class");
				this._writeObjectParam("scriptable","true");
				this._writeObjectParam("mayscript","true");

				this._writeObjectParams();

				this._writeObjectEvents();

				if (this.fullPageLoadListenerName!=null){
					this._writeObjectParam("InitCompleteListener","__"+this.id+"_InitComplete");
				}
				if (isWinIE){
					document.writeln("</object>");
				}
				else{
					document.writeln("</applet>");
				}
			}
			//<embed> for all other browsers
			else{
				document.write("<embed id=\""+this.id+"\" type=\"application/x-java-applet;version=1.4\" codebase=\""+this.javaAppletCodeBase+"\" code=\"com.aurigma.imageuploader.ImageUploader.class\" archive=\"ImageUploader.jar\" width=\""+this.width+"\" height=\""+this.height+"\" scriptable=\"true\" mayscript=\"true\" pluginspage=\"http://java.sun.com/products/plugin/index.html#download\"");

				if (this.javaAppletCached&&this.javaAppletVersion!=""){
					this._writeEmbedParam("cache_archive","ImageUploader.jar");
					var v=this.javaAppletVersion.replace(/\,/g,".");
					this._writeEmbedParam("cache_version",v+","+v);
				}

				this._writeEmbedParams();

				this._writeEmbedEvents();

				if (this.fullPageLoadListenerName!=null){
					this._writeEmbedParam("InitCompleteListener","__"+this.id+"_InitComplete");
				}
				document.writeln(">");
				document.writeln("</embed>");
			}
		}
		else
		{
			this.controlType="None";		
			document.writeln("Your browser is not supported.");
		}
	}
}

function getImageUploader(id){
	if (_isSafari){
		return document[id];
	}
	else{
		return document.getElementById(id);
	}
}
