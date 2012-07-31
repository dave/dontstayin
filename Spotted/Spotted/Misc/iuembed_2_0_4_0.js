// Aurigma Image Uploader Dual 5.x Embedding Script 
// Version 2.0.4.0 Feb 20, 2008
// Copyright(c) Aurigma Inc. 2002-2008

function __Browser(){
	var a=navigator.userAgent.toLowerCase();
	this.isOpera=(a.indexOf("opera")!=-1);
	this.isKonq=(a.indexOf('konqueror')!=-1);
	this.isSafari=(a.indexOf('safari')!=-1)&&(a.indexOf('mac')!=-1);
	this.isKhtml=this.isSafari||this.isKonq;
	this.isIE=(a.indexOf("msie")!=-1)&&!this.isOpera;
	this.isWinIE=this.isIE&&(a.indexOf("win")!=-1);
	this.isCSS1Compat=(!this.isIE)||(document.compatMode&&document.compatMode=="CSS1Compat");
}

var __browser=new __Browser();

//Create set/get expando methods for ActiveX
function _createExpandoMethods(id){
	var o=document.getElementById(id);
	var props=new Array();
	var hasPaneItemApiElements=false;
	for (propName in o){
		var c=propName.charAt(0);
		if (c==c.toUpperCase()){
			props.push(propName);
			if (propName=="PaneItemDesign"){
				hasPaneItemApiElements=true;
			}
		}
	}
	for (i=0;i<props.length;i++){
		//Check whether property is indexed
		if (typeof(o[props[i]])=="unknown"){
			eval("o.set"+props[i]+"=function(i,v){this."+props[i]+"(i)=v;};");
			eval("o.get"+props[i]+"=function(i){return this."+props[i]+"(i);};");
		}
		else{
			eval("o.set"+props[i]+"=function(v){this."+props[i]+"=v};");
			eval("o.get"+props[i]+"=function(){return this."+props[i]+"};");
		}
	}
	if (hasPaneItemApiElements){
		eval("o.setPaneItemDesign = function(Pane, Index, Value){this.PaneItemDesign(Pane, Index) = Value;};");
		eval("o.getPaneItemDesign = function(Pane, Index){return this.PaneItemDesign(Pane, Index);};");		
		//eval("o.getPaneItemCount = function(Pane){return this.PaneItemCount(Pane);};");
		//eval("o.getPaneItemChecked = function(Index){return this.PaneItemChecked(Index);};");
		//eval("o.getPaneItemCanBeUploaded = function(Index){return this.PaneItemCanBeUploaded(Index);};");
		eval("o.getPaneItemSelected = function(Pane, Index){return this.PaneItemSelected(Pane, Index);};");
		eval("o.setPaneItemEnabled = function(Pane, Index, Value){this.PaneItemEnabled(Pane, Index) = Value;};");
		eval("o.getPaneItemEnabled = function(Pane, Index){return this.PaneItemEnabled(Pane, Index);};");
	}
}

//Installation instructions
function _addInstructions(obj){
	obj.instructionsEnabled=true;
	if (obj.controlClass=="FileDownloader"){
		obj.instructionsCommon="<p>File Downloader ActiveX control is necessary to download "+
			"files quickly and easily. You will be able to select required files "+
			"in a user-friendly interface and simply click a <strong>Download</strong> button. "+
			"Installation will take up to few minutes, please be patient. To install File Downloader, ";
		obj.instructionsNotWinXPSP2="please reload the page and click the <strong>Yes</strong> button " +
			"when you see the control installation dialog.";
		obj.instructionsWinXPSP2="please click the <strong>Information Bar</strong>. After page reload click <strong>Install</strong> when "+
			"you see the control installation dialog.";
		obj.instructionsVista = "please click on the <strong>Information Bar</strong> and select <strong>Install ActiveX Control</strong> "+
			"from the dropdown menu. After page reload click <strong>Continue</strong> and then <strong>Install</strong> when "+
			"you see the control installation dialog.";
		obj.instructionsCommon2="</p><p><strong>NOTE:</strong> If control fails to be installed, it may mean that it has been inserted to the page incorrectly. "+
			"Refer File Downloader documentation for more details.</p>";
	} else {
		obj.instructionsCommon="<p>Image Uploader ActiveX control is necessary to upload "+
			"your files quickly and easily. You will be able to select multiple images "+
			"in user-friendly interface instead of clumsy input fields with <strong>Browse</strong> button. "+
			"Installation will take up to few minutes, please be patient. To install Image Uploader, ";
		obj.instructionsNotWinXPSP2="please reload the page and click the <strong>Yes</strong> button " +
			"when you see the control installation dialog.";
		obj.instructionsWinXPSP2="please click on the <strong>Information Bar</strong> and select " +
			"<strong>Install ActiveX Control</strong> from the dropdown menu. After page reload click <strong>Install</strong> when "+
			"you see the control installation dialog.";
		obj.instructionsVista = "please click on the <strong>Information Bar</strong> and select <strong>Install ActiveX Control</strong> "+
			"from the dropdown menu. After page reload click <strong>Continue</strong> and then <strong>Install</strong> when "+
			"you see the control installation dialog.";
		obj.instructionsCommon2="</p>";
	}
}

function ControlWriter(id,width,height){
	//Private
	this._params=new Array();
	this._events=new Array();
	
	_addInstructions(this);

	this._getObjectParamHtml=function(name,value){
		return "<param name=\""+name+"\" value=\""+value+"\" />";
	}

	this._getObjectParamsHtml=function(){
		var r="";
		var p=this._params;
		var i;
		for (i=0;i<p.length;i++){
			r+=this._getObjectParamHtml(p[i].name,p[i].value);
		}
		return r;
	}

	this._getObjectEventsHtml=function(){
		var r="";
		var e=this._events;
		for (i=0;i<e.length;i++){
			r+=this._getObjectParamHtml(e[i].name+"Listener",e[i].listener);
		}
		return r;
	}

	this._getEmbedParamHtml=function(name,value){
		return " "+name+"=\""+value+"\"";	
	}

	this._getEmbedParamsHtml=function(){
		var r="";
		var p=this._params;
		var i;
		for (i=0;i<p.length;i++){
			r+=this._getEmbedParamHtml(p[i].name,p[i].value);
		}
		return r;
	}

	this._getEmbedEventsHtml=function(){
		var r="";
		var e=this._events;
		for (i=0;i<e.length;i++){
			r+=this._getEmbedParamHtml(e[i].name+"Listener",e[i].listener);
		}
		return r;
	}

	//Public

	//Properties
	this.id=id;
	this.width=width;
	this.height=height;

	this.activeXControlEnabled=true;
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
		try
	        {
        	    	eval("var checkListener = " + eventListener + ";");
	            	if (checkListener == null)
			{
				return;
        		}
	        }
        	catch (e)
	        {
        	    	return;
	        }
		var p=new Object();
		p.name=eventName;
		p.listener=eventListener;
		this._events.push(p);
	}

	this.getActiveXInstalled=function(){
		if (this.activeXProgId){
			try{
				var a=new ActiveXObject(this.activeXProgId);
				return true;
			}
			catch(e){
				return false;
			}
		}
		return false;
	}

	this.getHtml=function(){
		var r="";
		if (this.fullPageLoadListenerName){
			r+="<" + "script type=\"text/javascript\">";
			r+="var __"+this.id+"_pageLoaded=false;";
			r+="var __"+this.id+"_controlLoaded=false;";
			r+="function __fire_"+this.id+"_fullPageLoad(){";
			r+="if (__"+this.id+"_pageLoaded&&__"+this.id+"_controlLoaded){";
			r+=this.fullPageLoadListenerName + "();";
			r+="}";
			r+="}";
			var pageLoadCode="new Function(\"__"+this.id+"_pageLoaded=true;__fire_"+this.id+"_fullPageLoad();\")";
			if (__browser.isWinIE){
				r+="window.attachEvent(\"onload\","+pageLoadCode+");";
			}
			else{
				r+="var r=window.addEventListener?window:document.addEventListener?document:null;";
				r+="if (r){r.addEventListener(\"load\","+pageLoadCode+",false);}";
			}
			r+="<"+"/script>";
		}		

		//ActiveX control
		if(__browser.isWinIE&&this.activeXControlEnabled){
			var v=this.activeXControlVersion.replace(/\./g,",")
			var cb=this.activeXControlCodeBase+(v==""?"":"#version="+v);

			r+="<" + "script for=\""+this.id+"\" event=\"InitComplete()\">";
			r+="_createExpandoMethods(\""+this.id+"\");";
			if (this.fullPageLoadListenerName){
				r+="__"+this.id+"_controlLoaded=true;";
				r+="__fire_"+this.id+"_fullPageLoad();";
			}
			r+="<"+"/script>";

			r+="<object id=\""+this.id+"\" name=\""+this.id+"\" classid=\"clsid:"+this.activeXClassId+"\" codebase=\""+cb+"\" width=\""+this.width+"\" height=\""+this.height+"\">";
			if (this.instructionsEnabled){
				r+=this.instructionsCommon;
				var isXPSP2 = (window.navigator.userAgent.indexOf("SV1") != -1) || (window.navigator.userAgent.indexOf("MSIE 7.0") != -1);
				var isVista = (window.navigator.userAgent.indexOf("Windows NT 6.0") != -1) && (window.navigator.userAgent.indexOf("MSIE 7.0") != -1);
				if (isVista) {
					r+=this.instructionsVista;
					//r+=this.instructionsWinXPSP2;
				} else if (isXPSP2) {
					r+=this.instructionsWinXPSP2;
				}
				else{
					r+=this.instructionsNotWinXPSP2;
				}
				r+=this.instructionsCommon2;
			}
			r+=this._getObjectParamsHtml();
			r+="</object>";

			//Event handlers
			var e=this._events;
			var eventParams;
			for (i=0;i<e.length;i++){
				if (this.controlClass=="FileDownloader"){
					switch (e[i].name){
						case "DownloadComplete":
							eventParams="Value";
							break;
						case "DownloadItemComplete":
							eventParams="Result, ErrorPage, Url, FileName, ContentType, FileSize";
							break;
						case "DownloadStep":
							eventParams="Step";
							break;
						case "Progress":
							eventParams="PercentTotal, PercentCurrent, Index";
							break;
						case "Error":
							eventParams="ErrorCode, HttpErrorCode, ErrorPage, Url, Index";
							break;
						default:
							eventParams="";
					}
					r+="<script for=\""+this.id+"\" event=\""+e[i].name+"("+eventParams+")\">";
					r+=e[i].listener+"("+eventParams+");";
					r+="<"+"/script>";
				}
				else {
					switch (e[i].name){
						case "Progress":
							eventParams="Status, Progress, ValueMax, Value, StatusText";
							break;
						case "InnerComplete":
							eventParams="Status, StatusText";
							break;
						case "AfterUpload":
							eventParams="htmlPage";
							break;
						case "ViewChange":
						case "SortModeChange":
							eventParams="Pane";
							break;
						case "Error":
							eventParams="ErrorCode, HttpResponseCode, ErrorPage, AdditionalInfo";
							break;
						case "PackageBeforeUpload":
							eventParams = "PackageIndex";
						    	break;
						case "PackageError":
						    	eventParams = "PackageIndex, ErrorCode, HttpResponseCode, ErrorPage, AdditionalInfo";
						    	break;
						case "PackageComplete":
						    	eventParams = "PackageIndex";
						    	break;
						case "PackageProgress":
						    	eventParams = "PackageIndex, Status, Progress, ValueMax, Value, StatusText";
						    	break;
						default:
							eventParams="";
					}
				}
				r+="<" + "script for=\""+this.id+"\" event=\""+e[i].name+"("+eventParams+")\">";
				if (e[i].name=="BeforeUpload"){
					r+="return ";
				}
				r+=e[i].listener+"("+eventParams+");";
				r+="<"+"/script>";
			}

		}
		else 
		//Java appplet
		if(this.javaAppletEnabled){
			if (this.fullPageLoadListenerName){
				r+="<" + "script type=\"text/javascript\">";
				r+="function __"+this.id+"_InitComplete(){";
				r+="__"+this.id+"_controlLoaded=true;";
				r+="__fire_"+this.id+"_fullPageLoad();";
				r+="}";
				r+="<"+"/script>";
			}

			//<object> for IE and <applet> for Safari
			if (__browser.isWinIE||__browser.isKhtml){
				if (__browser.isWinIE){
					r+="<object id=\""+this.id+"\" classid=\"clsid:8AD9C840-044E-11D1-B3E9-00805F499D93\" codebase=\""+window.location.protocol+"//java.sun.com/update/1.4.2/jinstall-1_4-windows-i586.cab#Version=1,4,0,0\" width=\""+this.width+"\" height=\""+this.height+"\">";
				}
				else{
					r+="<applet id=\""+this.id+"\" code=\""+this.javaAppletClassName+"\" java_codebase=\"../\" align=\"baseline\" archive=\""+this.javaAppletJarFileName+"\" mayscript=\"true\" scriptable=\"true\" width=\""+this.width+"\" height=\""+this.height+"\">";
				}

				if (this.javaAppletCached&&this.javaAppletVersion!=""){
					r+=this._getObjectParamHtml("cache_archive",this.javaAppletJarFileName);
					var v=this.javaAppletVersion.replace(/\,/g,".");
					//r+=this._getObjectParamHtml("cache_version",v+","+v);
					r+=this._getObjectParamHtml("cache_version",v);
				}

				r+=this._getObjectParamHtml("type","application/x-java-applet;version=1.4");
				r+=this._getObjectParamHtml("codebase",this.javaAppletCodeBase);
				r+=this._getObjectParamHtml("archive",this.javaAppletJarFileName);
				r+=this._getObjectParamHtml("code",this.javaAppletClassName);
				r+=this._getObjectParamHtml("scriptable","true");
				r+=this._getObjectParamHtml("mayscript","true");

				r+=this._getObjectParamsHtml();

				r+=this._getObjectEventsHtml();

				if (this.fullPageLoadListenerName){
					r+=this._getObjectParamHtml("InitCompleteListener","__"+this.id+"_InitComplete");
				}
				if (__browser.isWinIE){
					r+="</object>";
				}
				else{
					r+="</applet>";
				}
			}
			//<embed> for all other browsers
			else{
				r+="<embed id=\""+this.id+"\" type=\"application/x-java-applet;version=1.4\" codebase=\""+this.javaAppletCodeBase+"\" code=\""+this.javaAppletClassName+"\" archive=\""+this.javaAppletJarFileName+"\" width=\""+this.width+"\" height=\""+this.height+"\" scriptable=\"true\" mayscript=\"true\" pluginspage=\""+window.location.protocol+"//java.sun.com/products/plugin/index.html#download\"";

				if (this.javaAppletCached&&this.javaAppletVersion!=""){
					r+=this._getEmbedParamHtml("cache_archive",this.javaAppletJarFileName);
					var v=this.javaAppletVersion.replace(/\,/g,".");
					//r+=this._getEmbedParamHtml("cache_version",v+","+v);
					r+=this._getEmbedParamHtml("cache_version",v);
				}

				r+=this._getEmbedParamsHtml();

				r+=this._getEmbedEventsHtml();

				if (this.fullPageLoadListenerName){
					r+=this._getEmbedParamHtml("InitCompleteListener","__"+this.id+"_InitComplete");
				}
				r+=">";
				r+="</embed>";
			}
		}
		else
		{
			r+="Your browser is not supported.";
		}
		
		//For backward compatibility
		this.controlType=this.getControlType();
			
		return r;
	}

	this.getControlType=function(){
		return (__browser.isWinIE&&this.activeXControlEnabled)?"ActiveX":(this.javaAppletEnabled?"Java":"None");
	}
	
	this.writeHtml=function(){
		document.write(this.getHtml());
	}
}

function ImageUploaderWriter(id,width,height){
	this._base=ControlWriter;
	this._base(id,width,height);
	//These properties should be modified for private-label versions only
	this.activeXControlCodeBase="ImageUploader5.cab";
	//this.activeXClassId="BA162249-F2C5-4851-8ADC-FC58CB424243";
	this.activeXClassId="5D637FAD-E202-48D1-8F18-5B9C459BD1E3";
	this.activeXProgId="Aurigma.ImageUploader.5";
	this.javaAppletJarFileName="ImageUploader5.jar";
	this.javaAppletClassName="com.aurigma.imageuploader.ImageUploader.class";

	//Extend
	this.showNonemptyResponse="off";
	this._getHtml=this.getHtml;
	this.getHtml=function(){
		var r="";
		if (this.showNonemptyResponse!="off"){
			r+="<" + "script type=\"text/javascript\">";
			r+="function __"+this.id+"_InnerComplete(Status,StatusText){";
			r+="if (new String(Status)==\"COMPLETE\" && new String(StatusText).replace(/\\s*/g,\"\")!=\"\"){";
			if (this.showNonemptyResponse=="dump"){
				r+="var f=document.createElement(\"fieldset\");";
				r+="var l=f.appendChild(document.createElement(\"legend\"));";
				r+="l.appendChild(document.createTextNode(\"Server Response\"));";
				r+="var d=f.appendChild(document.createElement(\"div\"));";
				r+="d.innerHTML=StatusText;";
				r+="var b=f.appendChild(document.createElement(\"button\"));";
				r+="b.appendChild(document.createTextNode(\"Clear Server Response\"));";
				r+="b.onclick=function(){var f=this.parentNode;f.parentNode.removeChild(f)};";
				r+="document.body.appendChild(f);";
			}
			else{
				var s="";
				for (var i=0;i<80;i++){s+="-";}
				r+="alert(\""+s+"\\r\\nServer Response\\r\\n"+s+"\\r\\n\"+StatusText);";
			}
			r+="}";
			r+="}";
			r+="<"+"/script>";
			this.addEventListener("InnerComplete","__"+this.id+"_InnerComplete");
		}
		return r+this._getHtml();
	}
}

function ThumbnailWriter(id,width,height){
	this._base=ControlWriter;
	this._base(id,width,height);
	//These properties should be modified for private label versions only
	this.activeXControlCodeBase="ImageUploader5.cab";
	//this.activeXClassId="27BE1679-6AEE-4CE0-9748-7773EA94C3AF";
	this.activeXClassId="83AC1413-FCE4-4A46-9DD5-4F31F306E71F";
	this.activeXProgId="Aurigma.Thumbnail.5";
	this.javaAppletJarFileName="ImageUploader5.jar";
	this.javaAppletClassName="com.aurigma.imageuploader.Thumbnail.class";
}

function ShellComboBoxWriter(id,width,height){
	this._base=ControlWriter;
	this._base(id,width,height);
	//These properties should be modified for private label versions only
	this.activeXControlCodeBase="ImageUploader5.cab";
	//this.activeXClassId="8B1A14AF-E603-4356-B687-1F7D46522DD3";
	this.activeXClassId="3BF72F68-72D8-461D-A884-329D936C5581";
	this.activeXProgId="Aurigma.ShellCombo.5";
	this.javaAppletJarFileName="ImageUploader5.jar";
	this.javaAppletClassName="com.aurigma.imageuploader.ShellComboBox.class";
}

function UploadPaneWriter(id,width,height){
	this._base=ControlWriter;
	this._base(id,width,height);
	//These properties should be modified for private label versions only
	this.activeXControlCodeBase="ImageUploader5.cab";
	//this.activeXClassId="6D9D27EE-675E-4BDE-84B1-1C9A94F98555";
	this.activeXClassId="78E9D883-93CD-4072-BEF3-38EE581E2839";
	this.activeXProgId="Aurigma.UploadPane.5";
	this.javaAppletJarFileName="ImageUploader5.jar";
	this.javaAppletClassName="com.aurigma.imageuploader.UploadPane.class";
}

function FileDownloaderWriter(id,width,height){
	this._base=ControlWriter;
	this._base(id,width,height);
	//These properties should be modified for private label versions only
	this.activeXControlCodeBase="FileDownloader.cab";
	//this.activeXClassId="E1A26BBF-26C0-401D-B82B-5C4CC67457E0";
	this.activeXClassId="D6216AB8-9FF8-47C6-A2E7-49491B39C857";
	this.activeXProgId="Aurigma.FileDownloader.1";
	this.javaAppletEnabled=false;
	this.controlClass="FileDownloader";
}

function getControlObject(id){
	if (__browser.isSafari){
		return document[id];
	}
	else{
		return document.getElementById(id);
	}
}

function getImageUploader(id){
	return getControlObject(id);
}

function getFileDownloader(id){
	return getControlObject(id);
}
