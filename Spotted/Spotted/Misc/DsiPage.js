

function DsiPageShowLoginNew()
{
	document.getElementById("ContentDiv").style.display = "none";
	document.getElementById("NavLogin_ShowHideSpan").style.display = "";
	window.scrollTo(0, 0);
}
function DsiPageHideLoginNew()
{
	document.getElementById("ContentDiv").style.display = "";
	document.getElementById("NavLogin_ShowHideSpan").style.display = "none";
}
jQuery("body").ready(BodyReady);
//var BottomRightJ = null;
//var BottomLeftJ = null;
//var uiFooterRightDiv = null;
//var uiFooterRightDivJ = null;
//var uiFooterLeftDiv = null;
//var uiFooterLeftDivJ = null;
var uiLinksDiv = null;
var uiLinksDivJ = null;
var uiSidebarMaxTopDiv = null;
var uiSidebarMaxTopDivJ = null;
var uiBodyMainBackgroundInnerJ = null;
function BodyReady()
{
	setTimeout(BodyReadyAfterTimer, 0);
}
function BodyReadyAfterTimer() {
//	BottomRightJ = jQuery(".BottomRight");
//	BottomLeftJ = jQuery(".BottomLeft");

//	uiFooterRightDiv = document.getElementById("uiFooterRightDiv");
//	uiFooterRightDivJ = jQuery("#uiFooterRightDiv");
//	uiFooterLeftDiv = document.getElementById("uiFooterLeftDiv");
//	uiFooterLeftDivJ = jQuery("#uiFooterLeftDiv");

	uiLinksDiv = document.getElementById("uiLinksDiv");
	uiLinksDivJ = jQuery("#uiLinksDiv");

	uiSidebarMaxTopDiv = document.getElementById("SidebarMaxTopDiv")
	uiSidebarMaxTopDivJ = jQuery("#SidebarMaxTopDiv");

	uiBodyMainBackgroundInnerJ = jQuery("#BodyMainBackgroundInner");


	WindowScroll();
}
var BottomRightScrollIE6Mod = 0;
function WindowScroll()
{
//	FooterReposition();
}
function WindowResize()
{
//	FooterReposition();
}

function Trace(str)
{
	document.getElementById("uiTestDiv").innerHTML = "<div>" + str + "</div>";  //+ document.getElementById("uiTestDiv").innerHTML;
}



function UpdateLinksSideBarDiv() {

	var scrollTop = window.scroll().top;
	var scrollLeft = window.scroll().left;
	var clientHeight = window.size().height;
	var clientWidth = window.size().width;
	var ieOffset = 0;

	if (document.getElementById && !document.all)
	{
		ieOffset = -18;
	}

	try
	{
		if (uiLinksDiv != null)
		{
			if (uiLinksDiv.style.display == "none")
				uiLinksDiv.style.display = "";

			var left = uiSidebarMaxTopDivJ.offset().left;
			var top = uiSidebarMaxTopDivJ.offset().top + 35;

			var topMin = scrollTop + 15;

			//alert("top = " + top + ", topMin = " + topMin);

			if (top < topMin) {
				top = topMin;
				uiLinksDivJ.css("position", "fixed");
				uiLinksDivJ.css("top", "15px");
				uiLinksDivJ.css("left", left);
				uiLinksDivJ.css("display", "block");
			}
			else {
				uiLinksDivJ.css("position", "absolute");
				uiLinksDivJ.css("top", top);
				uiLinksDivJ.css("left", left);
				uiLinksDivJ.css("display", "block");
				
			}
		}

	}
	catch (ex) { }

}


window.size = function()
{
	var w = 0;
	var h = 0;

	//IE
	if (!window.innerWidth)
	{
		//strict mode
		if (!(document.documentElement.clientWidth == 0))
		{
			w = document.documentElement.clientWidth;
			h = document.documentElement.clientHeight;
		}
		//quirks mode
		else
		{
			w = document.body.clientWidth;
			h = document.body.clientHeight;
		}
	}
	//w3c
	else
	{
		w = window.innerWidth;
		h = window.innerHeight;
	}
	return { width: w, height: h };
}

window.scroll = function()
{
	var x, y;
	if (self.pageYOffset) // all except Explorer
	{
		x = self.pageXOffset;
		y = self.pageYOffset;
	}
	else if (document.documentElement && document.documentElement.scrollTop)
	// Explorer 6 Strict
	{
		x = document.documentElement.scrollLeft;
		y = document.documentElement.scrollTop;
	}
	else if (document.body) // all other Explorers
	{
		x = document.body.scrollLeft;
		y = document.body.scrollTop;
	}
	return { left: x, top: y };
}







