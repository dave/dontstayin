function MultiUsrDropRefresh(ClientID, UniqueID)
{
	var SelectBox = document.getElementById(ClientID+"_SelectBox");
	var Values = document.forms[0].elements[UniqueID+"$Values"].value;
	var Texts = document.forms[0].elements[UniqueID+"$Texts"].value;
	var ValueAry = Values.split("&");
	var TextAry = Texts.split("&");
	
	SelectBox.innerHTML="";
	
	var z=SelectBox.cloneNode(true);
	for (var i=0; i<ValueAry.length; i++)
	{
		if (ValueAry[i]!="")
		{
			var newOption = z.appendChild(document.createElement("OPTION"));
			newOption.value = unescape(ValueAry[i]);
			newOption.text = unescape(TextAry[i]);
		}
	}
	SelectBox.parentNode.replaceChild(z, SelectBox);
}
function MultiUsrDropNewPicName(pic){
	return pic.substr(0,2)+"/"+pic.substr(2,2)+"/"+pic;
}
function MultiUsrDropNewPicDomain(pic){
	return "s" + pic.substr(0,1);
}
function MultiUsrDropSelectChange(ClientID, UniqueID)
{
	var ob = document.forms[0].elements[UniqueID+"$SelectBox"];
	var selected = new Array();
	var done = false;
	var singleSelected = 0;
	for (var i = 0; i < ob.options.length; i++)
	{
		if (ob.options[ i ].selected)
		{
			if (done)
			{
				MultiUsrDropClearPicture(ClientID, UniqueID, "");
				return;
			}
			done = true;
			singleSelected = i;
		}
	}
	
	var PicHolder = document.getElementById(ClientID+"_PicHolder");
	var value = ob.options[singleSelected].value;
	var nickname = ob.options[singleSelected].text.toLowerCase();
	var valueAry = value.split("$");
	var pic = valueAry[1];
	if (pic == undefined || pic == "" || pic == "00000000-0000-0000-0000-000000000000")
		MultiUsrDropClearPicture(ClientID, UniqueID, nickname);
	else if (nickname.indexOf("@")==-1)
		PicHolder.innerHTML="<a tabindex=\"-1\" href=\"/members/"+nickname+"\" target=\"_blank\"><img src=\"" + StoragePath(valueAry[1]) + "\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\"></a>";
	else
		PicHolder.innerHTML="<img src=\"" + StoragePath(valueAry[1]) + "\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\">";
}

function MultiUsrDropMouseUp(evt, ClientID, UniqueID)
{
	MultiUsrDropLastMouseUp = new Date();
}

function MultiUsrDropIndexChanged(evt, ClientID, UniqueID)
{
	var Item = document.forms[0].elements[UniqueID+"$Drop"][document.forms[0].elements[UniqueID+"$Drop"].selectedIndex];
	MultiUsrDropDisplayPicture(ClientID, UniqueID, Item.value, Item.text);
	
	var elapsed = new Date() - MultiUsrDropLastMouseUp;
	
	if (elapsed<100)
		MultiUsrDropAddItem(ClientID, UniqueID);
}

function MultiUsrDropDisplayPicture(ClientID, UniqueID, Value, Text)
{
	var PicHolder = document.getElementById(ClientID+"_PicHolder");
	var value = Value;
	var nickname = Text.toLowerCase();
	var valueAry = value.split("$");
	var pic = valueAry[1];
	if (pic == undefined || pic == "" || pic == "00000000-0000-0000-0000-000000000000")
		MultiUsrDropClearPicture(ClientID, UniqueID, nickname);
	else if (nickname.indexOf("@")==-1)
		PicHolder.innerHTML="<a tabindex=\"-1\" href=\"/members/"+nickname+"\" target=\"_blank\"><img src=\"" + StoragePath(valueAry[1]) + "\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\"></a>";
	else
		PicHolder.innerHTML="<img src=\"" + StoragePath(valueAry[1]) + "\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\">";
}
var MultiUsrDropIsNetscape = false;
var MultiUsrDropIsIE = false;
var MultiUsrDropIsWhoKnows = false;
var MultiUsrDropLastMouseUp;
function MultiUsrDropKeyPress(evt, ClientID, UniqueID)
{
	var pressed;
	if (MultiUsrDropIsNetscape) 
		pressed = evt.which;
	else if (MultiUsrDropIsIE) 
		pressed = window.event.keyCode;
	else if (MultiUsrDropIsWhoKnows) 
		return true;
		
	if (pressed == 13)
	{
		MultiUsrDropAddItem(ClientID, UniqueID);
		return false;
	}
	else
		return true;
}
function MultiUsrDropKeyUp(evt, ClientID, UniqueID)
{
	var pressed;
	if (MultiUsrDropIsNetscape) 
		pressed = evt.which;
	else if(MultiUsrDropIsIE) 
		pressed = window.event.keyCode;
	else if(MultiUsrDropIsWhoKnows) 
		return true;

	if (pressed == 46)
	{
		MultiUsrDropRemoveItem(ClientID, UniqueID);
		document.getElementById(ClientID+"_SelectBox").focus();
		return false;
	}
	else
		return true;
}
function MultiUsrDropClearPicture(ClientID, UniqueID, NickName)
{
	var PicHolder = document.getElementById(ClientID+"_PicHolder");
	if (NickName.length>0)
	{
		if (NickName.indexOf("@")==-1)
			PicHolder.innerHTML="<a tabindex=\"-1\" href=\"/members/"+NickName+"\" target=\"_blank\"><img src=\"/gfx/dsi-sign-100.png\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\"></a>";
		else
			PicHolder.innerHTML="<img src=\"/gfx/dsi-sign-100.png\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\">";
	}
	else
		PicHolder.innerHTML="<img src=\"/gfx/dsi-sign-100.png\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\">";
}
function MultiUsrDropRefreshHidden(ClientID, UniqueID, TextAry, ValueAry)
{
	var Texts = "";
	var Values = "";
	for (var i=0; i<ValueAry.length; i++)
	{
		Texts += (i>0?"&":"")+TextAry[i];
		Values += (i>0?"&":"")+ValueAry[i];
	}
	document.forms[0].elements[UniqueID+"$Values"].value = Values;
	document.forms[0].elements[UniqueID+"$Texts"].value = Texts;
}
function MultiUsrDropAddItem(ClientID, UniqueID)
{
	var Item = document.forms[0].elements[UniqueID+"$Drop"][document.forms[0].elements[UniqueID+"$Drop"].selectedIndex];
	if (Item.value!="0")
		MultiUsrDropAddItemGeneric(ClientID, UniqueID, Item.value, Item.text);
}
function MultiUsrDropAddItemGeneric(ClientID, UniqueID, Value, Text)
{
	var Values = document.forms[0].elements[UniqueID+"$Values"].value;
	var Texts = document.forms[0].elements[UniqueID+"$Texts"].value;
	var ValueAry = Values.split("&");
	var TextAry = Texts.split("&");
	
	var found = false;
	for (var i=0; i<ValueAry.length; i++)
	{
		if (unescape(ValueAry[i])==Value)
		{
			found = true;
			TextAry[i]=escape(Text);
			break;
		}
	}
	if (!found)
	{
		ValueAry.length++;
		ValueAry[ValueAry.length-1]=escape(Value);
		TextAry.length++;
		TextAry[TextAry.length-1]=escape(Text);
		
		MultiUsrDropRefreshHidden(ClientID, UniqueID, TextAry, ValueAry);
		MultiUsrDropRefresh(ClientID, UniqueID);
	}
	
}
function MultiUsrDropRemoveItem(ClientID, UniqueID)
{
	var Values = document.forms[0].elements[UniqueID+"$Values"].value;
	var Texts = document.forms[0].elements[UniqueID+"$Texts"].value;
	var ValueAry = Values.split("&");
	var TextAry = Texts.split("&");
	var removed = false;
	try
	{
		var ob = document.forms[0].elements[UniqueID+"$SelectBox"];
		var selected = new Array();
		for (var i = 0; i < ob.options.length; i++)
			if (ob.options[i].selected)
				selected.push(ob.options[i].value);
		
		for (var i=0; i<ValueAry.length; i++)
		{
			for (var j=0; j<selected.length; j++)
			{
				if (unescape(ValueAry[i])==selected[j])
				{
					ValueAry.splice(i,1);
					TextAry.splice(i,1);
					removed = true;
				}
			}
		}
		if (removed)
		{
			MultiUsrDropClearPicture(ClientID, UniqueID, "");
			MultiUsrDropRefreshHidden(ClientID, UniqueID, TextAry, ValueAry);
			MultiUsrDropRefresh(ClientID, UniqueID);
		}
	}
	catch(ex){}
}
function Debug(txt)
{
	var debugHolder = document.getElementById("debugHolder");
	debugHolder.innerHTML += txt+"<br>";
}
function MultiUsrDropInit(ClientID, UniqueID)
{
	if (parseInt(navigator.appVersion) >= 4) {
		if(navigator.appName == "Netscape") {
			MultiUsrDropIsNetscape = true;
		}else if (navigator.appName == "Microsoft Internet Explorer"){
			MultiUsrDropIsIE = true;
		}else {
			MultiUsrDropIsWhoKnows = true;
		}
	}
	if (document.getElementById(ClientID+"_SelectBox")!=null)
		setTimeout("MultiUsrDropRefresh('"+ClientID+"', '"+UniqueID+"');",50);
	MultiUsrDropLastMouseUp = new Date();
}

try{Sys.Application.notifyScriptLoaded();}catch(ex){}
