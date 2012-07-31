/*
 Pleas leave this notice.
 DHTML tip message version 1.5.4 copyright Essam Gamal 2003 
 Home Page: (http://migoicons.tripod.com)
 Email: (migoicons@hotmail.com)
 Updated on :7/30/2003
*/ 

var MI_IE=MI_IE4=MI_NN4=MI_ONN=MI_NN=MI_pSub=MI_sNav=0;mig_dNav()
var Style=[],Text=[],Count=0,move=0,fl=0,isOK=1,e_d,tb,w=window,PX=(MI_pSub)?"px":""
var d_r=(MI_IE&&document.compatMode=="CSS1Compat")? "document.documentElement":"document.body"
var ww=w.innerWidth
var wh=w.innerHeight
var sbw=MI_ONN? 15:0


function UrlDecode(psEncodeString) 
{
  var lsRegExp = /\+/g;
  return unescape(String(psEncodeString).replace(lsRegExp," "));
}



function newPicName(pic){
	return pic.substr(0,2)+"/"+pic.substr(2,2)+"/"+pic;
}
function newPicDomain(pic){
	return "s" + pic.substr(0,1);
}
function applyCssFilter(){
	
}
function stmu(pic, flags, donationIconK, spotterIconNumber) {
	return stmuGen(true, pic, flags, donationIconK, spotterIconNumber);
}

function stmun(flags, donationIconK, spotterIconNumber) {
	return stmuGen(false, "", flags, donationIconK, spotterIconNumber);
}
function flagsToBoolArray(flags) {
	var boolArray = new Array();
	var pow = 0;
	for (var i = 0; pow < flags; i++)
	{
		pow = Math.pow(2, i);
		boolArray[i] = (flags & pow) > 0;
	}
	return boolArray;
}
function stmuGen(showPic, pic, flags, donationIconK, spotterIconNumber) {

	var boolArray = flagsToBoolArray(flags);
	var dsiregular = boolArray[0];
	var chatterbox = boolArray[1];
	var superuser = boolArray[2];
	var spotter = boolArray[3];
	var prospotter = boolArray[4];
	var loggedOn = boolArray[5];
	var chatting = boolArray[6];
	var newuser = boolArray[7];
	var moderator = boolArray[8];
	var admin = boolArray[9];
	var ticket = boolArray[10];
	var extra = boolArray[11];
	var isLegend = boolArray[12];
	var isDj = boolArray[13];
	
	var txt="";
	var img="";
	
	if (showPic)
	{
		var style = "";
		
		if (pic == "0")
			img = "<img src=\"/gfx/dsi-sign-100.png\" width=\"100\" height=\"100\" class=\"BorderBlack Bottom\" />";
		else
			img = "<img src=\"" + StoragePath(pic) + "\" width=\"100\" height=\"100\" class=\"BorderBlack Bottom\" />";
	}
	
	
	if (chatting)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-chatting.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Chatting</b>";
	else if (loggedOn)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-me-up.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Online</b>";
	else
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-me-dn.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Offline</b>";
	
	if (ticket)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-tickets.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Got tickets!</b>";
	if (donationIconK>0)
	{
		try
		{
			txt += (txt.length>0?"<br>":"")+"<img src=\"" + eval("donationIcon" + donationIconK + "Path") + "\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>" + eval("donationIcon" + donationIconK + "Name") + "</b>";
			if (isLegend) txt += "<br><img src=\"/gfx/don/legend.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>LEGEND!</b>";
		}
		catch(e){}
	}
	if (extra)
		txt += (txt.length > 0 ? "<br>" : "") + "<img src=\"/gfx/icon-thumbs-up.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Thanks!</b>";
	if (newuser)
		txt += (txt.length > 0 ? "<br>" : "") + "<img src=\"/gfx/icon-newuser.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>New user</b>";
	if (isDj)
		txt += (txt.length > 0 ? "<br>" : "") + "<img src=\"/gfx/icon-deck.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>DJ</b>";
	if (admin)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-admin.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Admin</b>";
	if (moderator && !admin)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-admin.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Moderator</b>";
	if (superuser)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-super.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Superuser</b>";		
	if (prospotter)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/spotters1/" + spotterIconNumber + ".png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Pro Spotter</b>";
	else if (spotter)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/spotters1/" + spotterIconNumber + ".png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Spotter</b>";
	if (chatterbox)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-chatter.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Chatterbox</b>";
	if (dsiregular && !superuser)
		txt += (txt.length>0?"<br>":"")+"<img src=\"/gfx/icon-regular.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>Regular</b>";
	
			
	if (txt.length>0 && img.length>0)
		img += "<br>"
		
	if (txt.length>0 || img.length>0)
		return stm(img+txt);
}
function stma(pic) {
	var img="<img src=\"" + StoragePath(pic) + "\" width=\"100\" height=\"100\" style=\"display:block;\" />";
	return stm(img);
}
function stmfb(pic) {
	var img = "<img src=\"" + pic + "\" width=\"50\" height=\"50\" style=\"display:block;\" />";
	return stm(img);
}
function stmv(vid, width, height) {
	var newHeight = height + 20;
	return stm("<div style=\"width:" + width + "px;height:" + newHeight + "px;\"><object type=\"application/x-shockwave-flash\" data=\"/misc/flvplayer.swf?file=" + vid + "\" width=\"" + width + "\" height=\"" + newHeight + "\" wmode=\"transparent\"><param name=\"movie\" value=\"/misc/flvplayer.swf?file=" + vid + "\" /><param name=\"wmode\" value=\"transparent\" /></object></div>");
}
function stmn() {
	return stm("<img src=\"/gfx/dsi-sign-100.png\" width=\"100\" height=\"100\" style=\"display:block;\" />");
}
function sttd(type)
{
	if (type == 1)
		stt("Click to remove this chat room");
	else if (type == 2)
		stt("Click to star / un-star this room<br>Starred rooms alert you to new messages with a popup");
	else if (type == 3)
		stt("Online");
	else if (type == 4)
		stt("Chatting now!");
	else if (type == 5)
		stt("Drag the arrows to re-arrange");
	else if (type == 6)
		stt("This room can't be starred");
	else if (type == 7)
		stt("This room is always starred");
	else if (type == 8)
		stt("Click to chat with this person");
	else if (type == 9)
		stt("Click to chat in this room");
}
function stt(text){
	//var s1=["white","black","#000000","#FECA26","","","","","","","","","","","auto","",1,3,10,10,"","","","",""];
	var s1=[
	"", //0 - titleColor
	"", //1 - TitleBgColor
	"", //2 - TitleBgImag
	"", //3 - TitleTextAlign
	"", //4 - TitleFontFace
	"", //5 - TitleFontSize
	"",//"black", //6 - TextColor
	"",//"#FECA26", //7 - TextBgColor
	"", //8 - TextBgImag
	"", //9 - TextTextAlign
	"", //10 - TextFontFace
	"", //11 - TextFontSize
	"auto", //12 - Width
	"", //13 - Height
	1, //14 - BorderSize
	"#000000", //15 - BorderColor
	3,  //16 - Textpadding
	10, //24 - Xpos -> 17
	10  //25 - Ypos -> 18
	];
	return stmx(UrlDecode(text),s1);
}
function stm(t1) {
	var s1=[
	"", //0 - titleColor
	"", //1 - TitleBgColor
	"", //2 - TitleBgImag
	"", //3 - TitleTextAlign
	"", //4 - TitleFontFace
	"", //5 - TitleFontSize
	"",//"black", //6 - TextColor
	"",//"#FECA26", //7 - TextBgColor
	"", //8 - TextBgImag
	"", //9 - TextTextAlign
	"", //10 - TextFontFace
	"", //11 - TextFontSize
	100, //12 - Width
	"", //13 - Height
	1, //14 - BorderSize
	"#000000", //15 - BorderColor
	0,  //16 - Textpadding
	10, //24 - Xpos -> 17
	10  //25 - Ypos -> 18
	];
	return stmx(t1,s1);
}
function stmx(t,s)
{
	if(MI_sNav&&isOK)
	{
		var txt="<div style=\"padding:" + s[16] + "px;\">"+t+"</div>";
		mig_wlay(txt);
		tb={trans:s[17],dur:s[18],opac:s[19],st:s[20],sc:s[21],pos:s[23],xpos:s[17],ypos:s[18]}
		if(MI_IE4)
			mig_layCss().width=s[12];
		e_d=mig_ed();
		Count=0;
		move=1;
	}
}

function tst_stmx(t,s){
if(MI_sNav&&isOK){	
//if(document.onmousemove!=mig_mo||w.onresize!=mig_re) mig_hand()
var ab="";var ap=""	
var titCol=s[0]?"COLOR='"+s[0]+"'":""
var titBgCol=s[1]&&!s[2]?"BGCOLOR='"+s[1]+"'":""
var titBgImg=s[2]?"BACKGROUND='"+s[2]+"'":""
var titTxtAli=s[3]?"ALIGN='"+s[3]+"'":""
var txtCol=s[6]?"COLOR='"+s[6]+"'":""
var txtBgCol=s[7]&&!s[8]?"BGCOLOR='"+s[7]+"'":""
var txtBgImg=s[8]?"BACKGROUND='"+s[8]+"'":""
var txtTxtAli=s[9]?"ALIGN='"+s[9]+"'":""
var tipHeight=s[13]? "HEIGHT='"+s[13]+"'":""
var brdCol=s[15]? "BGCOLOR='"+s[15]+"'":""
if(!s[4])s[4]="Verdana,Arial,Helvetica" 
if(!s[5])s[5]=1 
if(!s[10])s[10]="Verdana,Arial,Helvetica" 
if(!s[11])s[11]=1
if(!s[12])s[12]=200
if(!s[14])s[14]=0
if(!s[16])s[16]=0
if(!s[17])s[17]=10
if(!s[18])s[18]=10
if(MI_pSub==20001108){
if(s[14])ab="STYLE='border:"+s[14]+"px solid"+" "+s[15]+"'";
ap="STYLE='padding:"+s[16]+"px "+s[16]+"px "+s[16]+"px "+s[16]+"px'"}
var txt="<TABLE "+ab+" WIDTH='"+s[12]+"' BORDER='0' CELLSPACING='0' CELLPADDING='"+s[14]+"' "+brdCol+"><TR><TD><TABLE WIDTH='100%' "+tipHeight+" BORDER='0' CELLPADDING='"+s[16]+"' CELLSPACING='0' "+txtBgCol+" "+txtBgImg+"><TR><TD "+txtTxtAli+" "+ap+" VALIGN='top'><FONT SIZE='"+s[11]+"' FACE='"+s[10]+"' "+txtCol +">"+t+"</FONT></TD></TR></TABLE></TD></TR></TABLE>"
mig_wlay(txt)
tb={trans:s[17],dur:s[18],opac:s[19],st:s[20],sc:s[21],pos:s[23],xpos:s[17],ypos:s[18]}
if(MI_IE4)mig_layCss().width=s[12]
e_d=mig_ed()
Count=0
move=1
}}





function mig_dNav(){
var ua=navigator.userAgent.toLowerCase()
MI_pSub=navigator.productSub
MI_OPR=ua.indexOf("opera")>-1?parseInt(ua.substring(ua.indexOf("opera")+6,ua.length)):0
MI_IE=document.all&&!MI_OPR?parseFloat(ua.substring(ua.indexOf("msie")+5,ua.length)):0
MI_IE4=parseInt(MI_IE)==4
MI_NN4=navigator.appName.toLowerCase()=="netscape"&&!document.getElementById
MI_NN=MI_NN4||document.getElementById&&!document.all
MI_ONN=MI_NN4||MI_pSub<20020823
MI_sNav=MI_NN||MI_IE||MI_OPR>=7
}


function mig_mo(e)
{
	var X = 0;
	var Y = 0;
	var s_d = mig_scd(); //scroll offset
	var w_d = mig_wd(); //window dimensions

	var mx = e.clientX + s_d[0];
	var my = e.clientY + s_d[1];
	
	//document.getElementById("QuickDebug").innerHTML = "mx:" + mx + ", my:" + my + ", s_d:" + s_d + ", w_d:" + w_d + ", MI_ONN:" + MI_ONN;
	
	//$get("MyTextBox").value = "clientX: " + mx + ", clientY: " + my + " \n";
	
	if(move)
	{
		if (MI_IE4)
			e_d = mig_ed();

		X = mx + tb.xpos;
		Y = my + tb.ypos;
		
		if (w_d[0] + s_d[0] < e_d[0] + X + sbw + 20)
		{
			X = w_d[0] + s_d[0] - e_d[0] - sbw - 20;
		}
			
		if (w_d[1] + s_d[1] < e_d[1] + Y + sbw + 20)
		{
			Y = my - e_d[1] - 20;
		}
		
		if(X < s_d[0])
			X = s_d[0];
		
		with(mig_layCss())
		{
			left = X + PX;
			top = Y + PX;
		}
		
		
		mig_dis();
	}

}

function mig_dis()
{
	Count++
	if (Count==1)
	{
		mig_layCss().visibility = MI_NN4 ? "show" : "visible";
	}
}
function mig_hide(C)
{
	if (!MI_NN4 || MI_NN4 && C)
		mig_wlay("");
	with (mig_layCss())
	{
		visibility = MI_NN4 ? "hide":"hidden";
		left=0;
		top=-800;
	}
}
function mig_scd()
{
	return [
		parseInt(MI_IE ? eval(d_r).scrollLeft : w.pageXOffset), 
		parseInt(MI_IE ? eval(d_r).scrollTop : w.pageYOffset)
	];
}
function mig_wd()
{
	if (w.innerWidth > 0)
	{
		return [
			w.innerWidth,
			w.innerHeight
		];
	}
	else
	{
		if (document.documentElement.clientWidth > 0)
		{
			return [
				document.documentElement.clientWidth,
				document.documentElement.clientHeight
			];
		}
		else
		{
			return [
				document.body.clientWidth,
				document.body.clientHeight
			];
		}
	}

//	return [
//		parseInt(MI_ONN ? w.innerWidth : eval(d_r).clientWidth), 
//		parseInt(MI_ONN ? w.innerHeight : eval(d_r).clientHeight)
//	]
}

function mig_layCss(){return MI_NN4?mig_lay():mig_lay().style}
//function mig_lay1Css(){try{return MI_NN4?mig_lay1():mig_lay1().style;}catch(ex){return null;}}
function mig_lay(){with(document)return MI_NN4?layers["TipLayer"]:MI_IE4?all["TipLayer"]:getElementById("TipLayer")}
//function mig_lay1(){try{with(document)return MI_NN4?layers["AjaxUpdateDiv"]:MI_IE4?all["AjaxUpdateDiv"]:getElementById("AjaxUpdateDiv")}catch(ex){return null;}}
function mig_wlay(txt){if(MI_NN4){with(mig_lay().document){open();write(txt);close()}}else mig_lay().innerHTML=txt}
function mig_re()
{
	var w_d = mig_wd();
	if(MI_NN4 && (w_d[0] - ww || w_d[1] - wh))
		location.reload();
}

function mig_ed()
{
	return [
		parseInt(MI_NN4?mig_lay().clip.width:mig_lay().offsetWidth)+3,
		parseInt(MI_NN4?mig_lay().clip.height:mig_lay().offsetHeight)+5
	]
}
function htm()
{
	if(MI_sNav && isOK)
	{
		move=0;
		mig_hide(1);
	}
}

function mig_hand(){
if(MI_sNav){
//w.onresize=mig_re
//document.onmousemove=mig_mo
Sys.UI.DomEvent.addHandler(document, "mousemove", mig_mo);
Sys.UI.DomEvent.addHandler(window, "resize", mig_re);
if(MI_NN4) document.captureEvents(Event.MOUSEMOVE)
}}

