<!--
var yPosition = 115;
var totWidth = 351;
var menuHomeOffset = 13;
var menuHomeWidth = 143;
var menuChatWidth = 65;
var menuGroupsWidth = 82;
var menuTicketsWidth = 81;
var menuPlacesWidth = 53;
var menuMyDsiWidth = 56;
var menuContributeWidth = 79;
var menuChatOffset = menuHomeOffset+menuHomeWidth;
var menuGroupsOffset = menuChatOffset+menuChatWidth;
var menuTicketsOffset = menuGroupsOffset+menuGroupsWidth;
var menuPlacesOffset = menuTicketsOffset+menuTicketsWidth;
var menuMyDsiOffset = menuPlacesOffset+menuPlacesWidth;
var menuContributeOffset = menuMyDsiOffset+menuMyDsiWidth;
var menuSpottersOffset = menuContributeOffset+menuContributeWidth;

new ypSlideOutMenu("menuHome", "down", menuHomeOffset, yPosition, 150, (15*19)+1);
new ypSlideOutMenu("menuChat", "down", menuChatOffset, yPosition, 150, (10*19)+1);
new ypSlideOutMenu("menuGroups", "down", menuGroupsOffset, yPosition, 200, (100*19)+1);
new ypSlideOutMenu("menuTickets", "down", menuTicketsOffset, yPosition, 150, (100*10)+1);
new ypSlideOutMenu("menuPlaces", "down", menuPlacesOffset, yPosition, 200, (100*19)+1);
new ypSlideOutMenu("menuMyDsi", "down", menuMyDsiOffset, yPosition, 150, (17*19)+1);
new ypSlideOutMenu("menuContribute", "down", menuContributeOffset, yPosition, 150, (12*19)+1);
new ypSlideOutMenu("menuSpotters", "down", menuSpottersOffset, yPosition, 150, (5*19)+1);
//-->


function drawMenuItemTwo(menuName1,menuName2,url,text,bold,openLogin){
	document.write("<TR CLASS=\"rollmenu\" ONMOUSEOVER=\"className='rollovermenu';ypSlideOutMenu.showMenu('"+menuName1+"');");
	if (menuName2!="")
		document.write("ypSlideOutMenu.showMenu('"+menuName2+"');");
	document.write("\" ONMOUSEOUT=\"className='rollmenu';ypSlideOutMenu.hideMenu('"+menuName1+"');");
	if (menuName2!="")
		document.write("ypSlideOutMenu.hideMenu('"+menuName2+"');");
	document.write("\"");
	if (url!="")
		 document.write("ONCLICK=\"javascript:document.location='"+url+"';\"");
	document.write("><TD ALIGN=LEFT VALIGN=CENTER HEIGHT=17 CLASS=rollmenu>"+(bold?"<b>":"")+(url!=""?"<a href=\""+url+"\">":"")+text+(url!=""?"</a>":"")+(bold?"</b>":"")+"</TD></TR>");
}
function drawMenuItem(menuName,url,text,openLogin){
	drawMenuItemTwo(menuName,"",url,text,openLogin);
}
function drawBlankMenuItem(menuName){
	document.write("<TR CLASS=\"rollmenu\" ONMOUSEOVER=\"ypSlideOutMenu.showMenu('"+menuName+"');\"");
	document.write("ONMOUSEOUT=\"ypSlideOutMenu.hideMenu('"+menuName+"');\">");
	document.write("<TD ALIGN=LEFT VALIGN=CENTER HEIGHT=10 CLASS=rollmenuBlank></TD></TR>");
}
	
// Courtesy of SimplytheBest.net (http://simplythebest.net/info/dhtml_scripts.html)
<!--
function swapImgRestore() { 
  var i,x,a=document.sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}
function preloadImages() { 
  var d=document; if(d.images){ if(!d.p) d.p=new Array();
    var i,j=d.p.length,a=preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.p[j]=new Image; d.p[j++].src=a[i];}}
}
function findObj(n, d) { 
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=findObj(n,d.layers[i].document);
  if(!x && document.getElementById) x=document.getElementById(n); return x;
}
function swapImage() { 
  var i,j=0,x,a=swapImage.arguments; document.sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=findObj(a[i]))!=null){document.sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
function showHideLayers() {
  var i,p,v,obj,args=showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style	; v=(v=='show')?'visible':(v='hide')?'hidden':v; }
    obj.visibility=v; }
}


//-->
